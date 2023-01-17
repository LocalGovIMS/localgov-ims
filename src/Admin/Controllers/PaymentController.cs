using Admin.Classes.Models;
using Admin.Models.Payment;
using Admin.Models.Shared;
using BusinessLogic.Enums;
using BusinessLogic.Models.Payments;
using BusinessLogic.Security;
using System;
using System.Web.Mvc;
using Web.Mvc.Navigation;

namespace Admin.Controllers
{
    [Classes.Security.Attributes.Authorize(Roles = Role.Payments + "," + Role.ChequeProcess + "," + Role.PostPayment)]
    public class PaymentController : BaseController<IPaymentControllerDependencies>
    {
        private const string PaymentModelSessionKey = "PaymentModel";
        private readonly IPaymentControllerDependencies _dependencies;

        public PaymentController(IPaymentControllerDependencies dependencies)
            : base(dependencies)
        {
            _dependencies = dependencies;
        }

        [NavigatablePageActionFilter(ClearNavigation = true, AddToStack = false)]
        [HttpGet]
        public ActionResult Index()
        {
            Session[PaymentModelSessionKey] = null;
            return RedirectToAction("Create");
        }

        [NavigatablePageActionFilter(ClearNavigation = true, DisplayText = "Payment")]
        [HttpGet]
        public ActionResult Create()
        {
            IndexViewModel model = null;

            if (Session[PaymentModelSessionKey] == null)
            {
                model = _dependencies.IndexViewModelBuilder.Build();
                Session[PaymentModelSessionKey] = model;
            }
            else
            {
                model = (IndexViewModel)Session[PaymentModelSessionKey];
            }

            _dependencies.IndexViewModelBuilder.Build(model);

            return View("Index", model);
        }

        // This is the Add mechanism...
        [HttpPost]
        public ActionResult Create(IndexViewModel model)
        {
            var existingModel = HttpContext.Session[PaymentModelSessionKey] as IndexViewModel;
            if (existingModel == null) existingModel = new IndexViewModel();
            if (existingModel.Basket == null) existingModel.Basket = new Basket();

            model.Basket = existingModel.Basket;
            model.Address = existingModel.Address;

            if (!ModelState.IsValid)
            {
                _dependencies.IndexViewModelBuilder.Build(model);
                return View("Index", model);
            }

            var result = _dependencies.AddCommand.Execute(model);
            HttpContext.Session[PaymentModelSessionKey] = model;

            if (result.Success)
            {
                _dependencies.SetAddressCommand.Execute(model);
                Session[PaymentModelSessionKey] = (IndexViewModel)result.Data;
                return RedirectToAction("Create");
            }

            _dependencies.IndexViewModelBuilder.Build(model);

            return View("Index", model);
        }

        [HttpGet]
        public ActionResult RemoveItem(Guid id)
        {
            var updatedModel = (IndexViewModel)_dependencies.RemoveCommand.Execute(id).Data;
            updatedModel = (IndexViewModel)_dependencies.SetAddressCommand.Execute(updatedModel).Data;

            var model = _dependencies.IndexViewModelBuilder.Build(updatedModel);

            return View("Index", model);
        }

        [HttpGet]
        public ActionResult EmptyBasket()
        {
            var updatedModel = (IndexViewModel)_dependencies.EmptyBasketCommand.Execute(string.Empty).Data;

            HttpContext.Session[PaymentModelSessionKey] = updatedModel;

            var model = _dependencies.IndexViewModelBuilder.Build(updatedModel);

            return View("Index", model);
        }

        [HttpGet]
        [Classes.Security.Attributes.Authorize(Roles = Role.Payments)]
        public ActionResult Pay()
        {
            var model = (IndexViewModel)Session[PaymentModelSessionKey];
            if (model == null) return RedirectToAction("Index");

            if ((bool)Dependencies.CheckAddressCommand.Execute(model).Data)
            {
                return RedirectToAction("Address");
            }

            var result = Dependencies.CreatePaymentsCommand.Execute(model);

            return Redirect((string)result.Data);
        }

        [HttpGet]
        [Classes.Security.Attributes.Authorize(Roles = Role.ChequeProcess)]
        public ActionResult Cheque()
        {
            var paymentModel = (IndexViewModel)Session[PaymentModelSessionKey];

            var model = paymentModel.Cheques ?? new Cheques();

            return View(model);
        }

        [HttpPost]
        [Classes.Security.Attributes.Authorize(Roles = Role.ChequeProcess)]
        public ActionResult Cheque(Cheques model)
        {
            if (!ModelState.IsValid) return View(model);

            var paymentModel = (IndexViewModel)Session[PaymentModelSessionKey];
            paymentModel.Cheques = model;

            Session[PaymentModelSessionKey] = paymentModel;

            var result = Dependencies.ProcessPaymentCommand.Execute(new ProcessPaymentCommandAgrs(paymentModel, PaymentTypeEnum.Cheque));

            if (result.Success)
            {
                Session[PaymentModelSessionKey] = null;
            }

            return Redirect((string)result.Data);
        }

        [HttpGet]
        [Classes.Security.Attributes.Authorize(Roles = Role.PostPayment)]
        public ActionResult PostPayment()
        {
            var model = (IndexViewModel)Session[PaymentModelSessionKey];
            if (!ModelState.IsValid) return View(model);

            Session[PaymentModelSessionKey] = model;

            var result = Dependencies.ProcessPaymentCommand.Execute(new ProcessPaymentCommandAgrs(model, PaymentTypeEnum.Post));

            if (result.Success)
            {
                Session[PaymentModelSessionKey] = null;
                return Redirect((string)result.Data);
            }
            else
            {
                var errorMsg = new ErrorMessage(result.Messages);

                TempData["Message"] = errorMsg;
                return View(model);
            }

        }

        [HttpGet]
        public ActionResult Address()
        {
            var paymentModel = (IndexViewModel)Session[PaymentModelSessionKey];

            var model = paymentModel.Address ?? new Address();

            return View(model);
        }

        [HttpPost]
        public ActionResult Address(Address model)
        {

            //check if user is a call recorded user.
            //if so stop recording
            if (!ModelState.IsValid) return View(model);

            var paymentModel = (IndexViewModel)Session[PaymentModelSessionKey];

            paymentModel.Address = model;
            paymentModel.AddressReviewed = true;

            Session[PaymentModelSessionKey] = paymentModel;

            return RedirectToAction("Pay");
        }

        [HttpGet]
        public ActionResult Cancel()
        {
            var model = (IndexViewModel)Session[PaymentModelSessionKey];

            Session[PaymentModelSessionKey] = model;

            return RedirectToAction("Create");
        }

        public ActionResult Success(string id)
        {
            // All is ok, so wipe the model and load the transaction detail screen
            Session[PaymentModelSessionKey] = null;
            TempData["message"] = new SuccessMessage()
            {
                Title = "Payment success",
                Text = "The payment was successful, please see the details of the transaction below"
            };
            return RedirectToAction("Details", "Transaction", new { id = id });
        }

        public ActionResult CancelPayment()
        {
            ProcessMessage(new ErrorMessage("Your payment was cancelled"));
            return RedirectToAction("Create");
        }

        public ActionResult Fail()
        {
            ProcessMessage(new ErrorMessage("Your payment failed"));
            return RedirectToAction("Create");
        }

        private void ProcessMessage(Message message)
        {
            var model = (IndexViewModel)Session[PaymentModelSessionKey];
            model.Message = message;
            Session[PaymentModelSessionKey] = model;
        }

        public ActionResult SelectAccountReference(string id)
        {
            var model = (IndexViewModel)Session[PaymentModelSessionKey];

            model.AccountReference = id;

            Session[PaymentModelSessionKey] = model;

            return RedirectToAction("Create");
        }

        [Route("{fundCode}/{accountReference?}")]
        public ActionResult AccountHolderSearch(string fundCode, string accountReference)
        {
            var model = (IndexViewModel)Session[PaymentModelSessionKey];

            model.FundCode = fundCode;
            model.AccountReference = accountReference;

            Session[PaymentModelSessionKey] = model;

            return RedirectToAction("PaymentSearch", "AccountHolder", new { fundCode = fundCode, accountReference = accountReference, isAPaymentSearch = true });
        }
    }
}