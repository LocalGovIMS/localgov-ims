using Admin.Classes.Commands.Transaction;
using Admin.Models.Shared;
using Admin.Models.Transaction;
using BusinessLogic.Security;
using System;
using System.Linq;
using System.Web.Mvc;
using Web.Mvc.Navigation;

namespace Admin.Controllers
{
    public class TransactionController : BaseController<ITransactionControllerDependencies>
    {
        public TransactionController(ITransactionControllerDependencies dependencies)
            : base(dependencies)
        {
        }

        [NavigatablePageActionFilter(ClearNavigation = true, DisplayText = "Transactions")]
        [Classes.Security.Attributes.Authorize(Roles = Role.TransactionList)]
        [HttpGet]
        public ActionResult List()
        {
            var model = Dependencies.ListViewModelBuilder.Build();

            return View("~/Views/Transaction/List.cshtml", model);
        }

        [NavigatablePageActionFilter(ClearNavigation = true, DisplayText = "Transactions")]
        [Classes.Security.Attributes.Authorize(Roles = Role.TransactionList)]
        [AcceptVerbs("GET", "POST")]
        public ActionResult Search(SearchCriteria criteria)
        {
            var model = Dependencies.ListViewModelBuilder.Build(criteria);

            return View("~/Views/Transaction/List.cshtml", model);
        }

        [NavigatablePageActionFilter(DisplayText = "Transaction Details")]
        [Classes.Security.Attributes.Authorize(Roles = Role.TransactionDetails)]
        [HttpGet]
        public ActionResult Details(string id)
        {
            var model = Dependencies.DetailsViewModelBuilder.Build(id);

            if (model.Transaction == null) return RedirectToAction("List");

            return View(model);
        }

        [NavigatablePageActionFilter(DisplayText = "Origin Details")]
        [Classes.Security.Attributes.Authorize(Roles = Role.TransactionDetails)]
        [HttpGet]
        public ActionResult Origin(string id)
        {
            var model = Dependencies.DetailsViewModelBuilder.Build(id);

            if (model.Transaction == null) return Back();

            return View("Details", model);
        }

        [Classes.Security.Attributes.Authorize(Roles = Role.TransactionDetails)]
        [HttpGet]
        public ActionResult Receipt(string id)
        {
            var model = Dependencies.DetailsViewModelBuilder.Build(id);

            if (model.Transaction == null) return RedirectToAction("List");

            return View(model);
        }

        [Classes.Security.Attributes.Authorize(Roles = "Transaction.Refund")]
        [HttpGet]
        [ChildActionOnly]
        public ActionResult _Refund(string id)
        {
            var model = Dependencies.RefundViewModelBuilder.Build(id);

            return PartialView(model);
        }

        [Classes.Security.Attributes.Authorize(Roles = Role.TransactionRefund)]
        [HttpPost]
        public ActionResult Refund(RefundViewModel model)
        {
            var message = (Message)Dependencies.RefundCommand.Execute(model).Data;

            TempData["Message"] = message;

            return RedirectToAction("Details", new { id = model.Reference });
        }

        [Classes.Security.Attributes.Authorize(Roles = Role.TransactionJournal)]
        [HttpGet]
        [ChildActionOnly]
        public ActionResult _Transfer(string id)
        {
            var model = Dependencies.TransferViewModelBuilder.Build(id);
            return PartialView(model);
        }

        [Classes.Security.Attributes.Authorize(Roles = Role.TransactionJournal)]
        [HttpPost]
        public JsonResult SubmitTransfers(TransferViewModel model)
        {
            var result = Dependencies.TransferCommand.Execute(model);
            return Json(new { ok = result.Success, message = string.Join(", ", result.Messages) });
        }

        [Classes.Security.Attributes.Authorize(Roles = Role.TransactionJournal)]
        [HttpGet]
        public ActionResult UndoTransfer(string transferGuid, string pspReference)
        {
            var message = (Message)Dependencies.UndoTransferCommand.Execute(transferGuid).Data;

            return RedirectToAction("Details", new { id = pspReference });
        }

        [Classes.Security.Attributes.Authorize(Roles = Role.TransactionDetails)]
        [HttpGet]
        [ChildActionOnly]
        public ActionResult _Activity(string id)
        {
            return PartialView();
        }

        [HttpPost]
        [Classes.Security.Attributes.Authorize(Roles = Role.TransactionDetails)]
        public ActionResult EmailReceipt(EmailReceiptViewModel model)
        {
            var result = Dependencies.EmailReceiptCommand.Execute(model);

            // Might help debugging in future...
            if (!result.Success)
            {
                Dependencies.Log.WarnFormat("Failed to send receipt email: {0} for model: {1}"
                    , Newtonsoft.Json.JsonConvert.SerializeObject(result)
                    , Newtonsoft.Json.JsonConvert.SerializeObject(model));
            }

            return Json(new { ok = result.Success });
        }

        [NavigatablePageActionFilter(ClearNavigation = true, DisplayText = "Export")]
        [Classes.Security.Attributes.Authorize(Roles = Role.TransactionList)]
        [HttpGet]
        public ActionResult Export(SearchCriteria criteria)
        {
            try
            {
                criteria.Page = 1;
                criteria.PageSize = 1000;

                var model = Dependencies.ListViewModelBuilder.Build(criteria);

                var file = Dependencies.CreateCsvFileForExportCommand.Execute(new CreateCsvFileForExportCommandArgs()
                {
                    Transactions = model.Transactions.ToList()
                });

                return (FileContentResult)file.Data;
            }
            catch(Exception ex)
            {
                Dependencies.Log.Error(ex);

                TempData["Message"] = new ErrorMessage("Unable to export the selected data");

                return RedirectToAction("Search", criteria);
            }
            
        }
    }
}