using BusinessLogic.Classes;
using BusinessLogic.Enums;
using BusinessLogic.Extensions;
using BusinessLogic.Validators.Payment;
using Microsoft.Ajax.Utilities;
using PaymentPortal.Classes;
using PaymentPortal.Models;
using PaymentPortal.Models.Payment;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.Mvc;
using Web.Mvc;
using PaymentValidationException = PaymentPortal.Classes.PaymentValidationException;

namespace PaymentPortal.Controllers
{
    public class PaymentController : BaseController<IPaymentControllerDependencies>
    {
        public PaymentController(IPaymentControllerDependencies dependecies)
            : base(dependecies)
        {
        }

        public ActionResult Index(PaymentModel model)
        {
            try
            {
                if (TempData["exMessage"] != null)
                {
                    ViewBag.ExMessage = TempData["exMessage"].ToString();
                    TempData.Remove("exMessage");
                }

                GetPaymentTypes();

                if (TempData["PaymentModel"] != null)
                {
                    model = (PaymentModel)TempData["PaymentModel"];
                }
                else
                {
                    model = new PaymentModel();
                }

                ModelState.Clear();

                return View(model);
            }
            catch (Exception e)
            {
                Dependencies.Log.Error(e);
                ViewBag.ExMessage = "Unable to open the payment basket.";
                return View("Error");
            }
        }

        [HttpPost]
        [MultipleButton(MatchFormKey = "action", MatchFormValue = "Pay")]
        public ActionResult Pay(PaymentModel model)
        {
            try
            {
                var paymentDetailsList = new List<PaymentDetails>();

                // TODO: Could add an extension method Request.GetCurrentUrl that does the following few lines
                var currentUrl = ConfigurationManager.AppSettings["Organisation.Website"];

                if (Request.Url != null)
                {
                    currentUrl = Request.Url.GetLeftPart(UriPartial.Authority) + Request.ApplicationPath.TrimEnd('/');
                }

                // TODO: This foreach could be a method/extension method of PaymentModel (e.g. GetPayemntDetails(currentUrl)
                foreach (var item in model.BasketItems)
                {
                    paymentDetailsList.Add(new PaymentDetails()
                    {
                        AccountReference = item.Reference,
                        Fund = item.Code,
                        Amount = item.Amount,
                        CancelUrl = currentUrl + "/payment/cancel",
                        CreatedAt = DateTime.Now,
                        FailUrl = currentUrl + "/payment/fail",
                        SuccessUrl = currentUrl + "/payment/success",
                        Source = "basket",
                        AddressLine1 = model.PaymentAddressDetails == null ? string.Empty : model.PaymentAddressDetails.AddressLine1,
                        AddressLine2 = model.PaymentAddressDetails == null ? string.Empty : model.PaymentAddressDetails.AddressLine2,
                        AddressLine3 = model.PaymentAddressDetails == null ? string.Empty : model.PaymentAddressDetails.AddressLine3,
                        AddressLine4 = model.PaymentAddressDetails == null ? string.Empty : model.PaymentAddressDetails.AddressLine4,
                        Postcode = model.PaymentAddressDetails == null ? string.Empty : model.PaymentAddressDetails.PostCode,
                        PayeeName = model.PaymentAddressDetails == null ? string.Empty : model.PaymentAddressDetails.PayeeName,
                    });
                }

                if (model.PaymentAddressDetails == null)
                {
                    foreach (var fundCode in model.BasketItems.Select(y => y.Code).Distinct())
                    {
                        if (Dependencies.FundService.GetByFundCode(fundCode).AquireAddress == true)
                        {
                            return RedirectToAction("Address");
                        }
                    }
                }

                var response = Dependencies.PaymentService.CreateHppPayments(paymentDetailsList);

                return Redirect(response.ResponseUrl);
            }
            catch (Exception e)
            {
                Dependencies.Log.Error(e);
                ViewBag.ExMessage = "Unable to process card payment. The card processing system is currently unavailable.";

                return View("Error");
            }
        }

        [HttpGet]
        public ActionResult Address()
        {
            return View();
        }

        [HttpPost]
        [MultipleButton(MatchFormKey = "action", MatchFormValue = "SaveAddress")]
        public ActionResult Address(PaymentAddress model)
        {
            if (!ModelState.IsValid) return View(model);

            var paymentModel = (PaymentModel)Session["PaymentModel"];

            paymentModel.PaymentAddressDetails = model;

            Session["PaymentModel"] = paymentModel;

            return Pay(paymentModel);
        }

        [HttpPost]
        [MultipleButton(MatchFormKey = "action", MatchFormValue = "Cancel")]
        public ActionResult CancelAddress()
        {
            var model = (PaymentModel)Session["PaymentModel"];

            GetPaymentTypes();

            return View("Index", model);
        }

        public ActionResult Create(LegacyPaymentRequest legacyPaymentRequest)
        {
            try
            {
                legacyPaymentRequest.Populate();

                var response = Dependencies.PaymentService.CreateHppPayments(legacyPaymentRequest.PaymentDetails);

                return Redirect(response.ResponseUrl);
            }
            catch (Exception e)
            {
                Dependencies.Log.Error(e);

                ViewBag.ExMessage = $"Unable to process payment for reference : {legacyPaymentRequest.AccountReference}. The card processing system is currently unavailable";

                return View("Error");
            }
        }

        [HttpPost]
        public JsonResult CreatePayment(PaymentRequest paymentRequest)
        {
            try
            {
                var response = Dependencies.PaymentService.CreateHppPayments(paymentRequest.GetPaymentDetailsList());

                return Json(new { Successful = true, Url = response.ResponseUrl });
            }
            catch (Exception e)
            {
                Dependencies.Log.Error(e);
                return Json(new { Successful = false, Error = e.Message });
            }
        }

        [HttpPost]
        [MultipleButton(MatchFormKey = "action", MatchFormValue = "Card")]
        public ActionResult AddItem(PaymentModel model)
        {
            try
            {
                model.PaymentTypeDescription = Dependencies.FundService.GetByFundCode(model.PaymentType)?.FundName;

                ValidatePayment(model);

                if (ModelState.IsValid)
                {
                    ModelState.Clear();

                    model.AddBasketItem();
                    model.ClearPaymentDetails();

                    ViewBag.BasketMessage = "Your payment has been added to the basket.";
                }

                GetPaymentTypes();

                Session["PaymentModel"] = model;

                return View("Index", model);
            }
            catch (Exception e)
            {
                Dependencies.Log.Error(e);

                ViewBag.ExMessage = $"Unable to process payment for reference : {model.PaymentReference}. The card processing system is currently unavailable";

                return View("Error");
            }
        }

        public ActionResult RemoveItem(string reference)
        {
            try
            {
                PaymentModel model = (PaymentModel)Session["PaymentModel"];

                model.RemoveBasketItem(reference);

                ModelState.Clear();

                GetPaymentTypes();

                return View("Index", model);
            }
            catch (Exception)
            {
                ViewBag.ExMessage = "Failed to clear items from basket";

                return View("Error");
            }
        }

        public ActionResult EmptyBasket()
        {
            Session.Clear();

            GetPaymentTypes();

            return View("Index", new PaymentModel());
        }

        public ActionResult Success(string id)
        {
            TempData["Receipt"] = id;
            TempData["Hash"] = Dependencies.CryptographyService.GetHash(id);

            return View();
        }

        public ActionResult Cancel()
        {
            TempData.Add("exMessage", "Your payment was cancelled");

            return RedirectToAction("Index");
        }

        public ActionResult Fail()
        {
            TempData.Add("exMessage", "Your payment failed");

            return RedirectToAction("Index");
        }

        // TODO: This should be in a validator class, not in this controller
        private void ValidatePayment(PaymentModel model)
        {
            if (model == null) return;

            try
            {
                if (model.BasketItems.Count >= 10)
                    ModelState.AddModelError("BasketItems", "You cannot add more than 10 items to the basket.");

                if (!ModelState.IsValid)
                    return;

                if (model.Amount <= 0)
                    ModelState.AddModelError("Amount", "The payment amount must be more than £0");

                if (model.Amount != decimal.Round(model.Amount, 2))
                    ModelState.AddModelError("Amount", "The payment amount is invalid");

                if (model.PaymentType.ToUpperInvariant() == "NONE")
                    ModelState.AddModelError("PaymentType", "A payment type must be selected");

                if (string.IsNullOrWhiteSpace(model.PaymentTypeDescription))
                    ModelState.AddModelError("PaymentType", "Cannot find payment type");

                if (!ModelState.IsValid)
                    return;

                if (!model.PaymentReference.All(x => char.IsLetterOrDigit(x) || x == '/' || x == ' '))
                    ModelState.AddModelError("PaymentReference", "Please enter a valid reference (only letters and numbers a '/' or a space)");

                if (model.BasketItems.Exists(m => m.Reference == model.PaymentReference))
                    ModelState.AddModelError("PaymentReference", $"Payment reference {model.PaymentReference} already exists in the basket");

                if (!ModelState.IsValid)
                    return;

                var paymentValidationHandler = Dependencies.PaymentValidationHandler.Validate(new PaymentValidationArgs()
                {
                    Reference = model.PaymentReference,
                    FundCode = model.PaymentType,
                    MopCode = Dependencies.MethodOfPaymentService.GetAllMops(true).FirstOrDefault(x => x.IsACardSelfServicePayment()).MopCode,
                    Amount = model.Amount,
                    Source = AccountReferenceValidationSource.Payments
                });
                
                if (!paymentValidationHandler.Success)
                {
                    var errorMessage = paymentValidationHandler.Error.IsNullOrWhiteSpace()
                        ? "Please enter a valid payment reference"
                        : paymentValidationHandler.Error;

                    ModelState.AddModelError("PaymentReference", errorMessage);
                }
            }
            catch (Exception ex)
            {
                Dependencies.Log.Error(ex);

                throw new PaymentValidationException("An error occurred whilst validating the payment details", ex);
            }
        }

        private void GetPaymentTypes()
        {
            var listItems = new List<PaymentTypeListItem>();

            foreach (var fund in Dependencies.FundService.GetBasketFunds())
            {
                listItems.Add(new PaymentTypeListItem()
                {
                    Text = string.IsNullOrEmpty(fund.DisplayName) ? fund.FundName : fund.DisplayName,
                    Value = fund.FundCode,
                    ReferenceFieldMessage = fund.Metadata.GetValue(FundMetadataKeys.Basket.ReferenceFieldMessage),
                    ReferenceFieldLabel = fund.Metadata.GetValue(FundMetadataKeys.Basket.ReferenceFieldLabel),
                });
            }

            ViewBag.PaymentTypes = listItems.OrderBy(l => l.Text).ToList();
        }

        [HttpPost]
        public ActionResult EmailReceipt(EmailReceiptViewModel model)
        {
            try
            {
                // See if the user tampered with the psp reference.
                // This stops then from getting other people receipts then
                if (model.Hash != Dependencies.CryptographyService.GetHash(model.PspReference))
                {
                    return Json(new { ok = false });
                }

                var transaction = Dependencies.TransactionService.GetTransactionByPspReference(model.PspReference);
                var result = Dependencies.EmailService.SendVatReceiptEmail(model.EmailAddress, transaction);

                // Might help debugging in future...
                if (!result.Success)
                {
                    Dependencies.Log.WarnFormat("Failed to send receipt email: {0} for model: {1}"
                        , Newtonsoft.Json.JsonConvert.SerializeObject(result)
                        , Newtonsoft.Json.JsonConvert.SerializeObject(model));
                }
                else
                {
                    Dependencies.TransactionService.ReceiptIssued(model.PspReference);
                }

                return Json(new { ok = result.Success });
            }
            catch (Exception e)
            {
                Dependencies.Log.Error(string.Format("Error processing transfers for model: {0}", Newtonsoft.Json.JsonConvert.SerializeObject(model)), e);
                return Json(new { ok = false });
            }
        }
    }
}