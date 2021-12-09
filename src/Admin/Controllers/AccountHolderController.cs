using Admin.Classes.ViewModelBuilders.AccountHolder;
using Admin.Models.AccountHolder;
using BusinessLogic.Security;
using System;
using System.Web.Mvc;
using Web.Mvc.Navigation;

namespace Admin.Controllers
{
    public class AccountHolderController : BaseController<IAccountHolderControllerDependencies>
    {
        private const string IsAPaymentSearchSessionKey = "AccountHolderController::IsAPaymentSearch";

        public AccountHolderController(IAccountHolderControllerDependencies dependencies)
            : base(dependencies)
        {
        }

        [NavigatablePageActionFilter(DisplayText = "Account Holders")]
        [Classes.Security.Attributes.Authorize(Roles = Role.SystemAdmin)] // TODO: What should this be, a new role?
        [HttpGet]
        public ActionResult List()
        {
            return RedirectToAction("Search");
        }

        [NavigatablePageActionFilter(DisplayText = "Account Holders")]
        [Classes.Security.Attributes.Authorize(Roles = Role.SystemAdmin)] // TODO: What should this be, a new role?
        [AcceptVerbs("GET", "POST")]
        public ActionResult Search(SearchCriteria criteria)
        {
            Session[IsAPaymentSearchSessionKey] = criteria.IsAPaymentSearch;

            var model = Dependencies.ListViewModelBuilder.Build(criteria);

            return View("List", model);
        }

        [NavigatablePageActionFilter(DisplayText = "Account Holder Details")]
        [Classes.Security.Attributes.Authorize(Roles = Role.TransactionDetails)] // TODO: What should this be, a new role?
        [HttpGet]
        public ActionResult Details(string id)
        {
            var model = Dependencies.DetailsViewModelBuilder.Build(
                new DetailsViewModelBuilderArgs 
                { 
                    AccountReference = id, 
                    ShowSelect = Convert.ToBoolean(Session[IsAPaymentSearchSessionKey]) 
                });

            return View(model);
        }

        [Classes.Security.Attributes.Authorize(Roles = Role.Transfer + "," + Role.TransactionJournal)]
        [HttpPost]
        public JsonResult Lookup(LookupViewModel model)
        {
            var result = Dependencies.LookupAccountHolderCommand.Execute(model);

            // HIGH: Create an extension method for this, or is there one already?
            return Json(new
            {
                ok = result.Success,
                message = string.Join(", ", result.Messages),
                data = result.Data
            });
        }

        [NavigatablePageActionFilter(DisplayText = "Edit Account Holder")]
        [Classes.Security.Attributes.Authorize(Roles = Role.SystemAdmin)] // TODO: What should this be, a new role?
        [HttpGet]
        public ActionResult Edit(string id)
        {
            var model = Dependencies.EditViewModelBuilder.Build(id);

            return View(model);
        }

        [Classes.Security.Attributes.Authorize(Roles = Role.SystemAdmin)] // TODO: What should this be, a new role?
        [HttpPost]
        public ActionResult Edit(EditViewModel model)
        {
            return BaseEdit(model, Dependencies.EditCommand);
        }

        [NavigatablePageActionFilter(DisplayText = "Create Account Holder")]
        [Classes.Security.Attributes.Authorize(Roles = Role.SystemAdmin)] // TODO: What should this be, a new role?
        [HttpGet]
        public ActionResult Create()
        {
            var model = new EditViewModel();
            model = Dependencies.EditViewModelBuilder.Rebuild(model);

            return View(model);
        }

        [Classes.Security.Attributes.Authorize(Roles = Role.SystemAdmin)] // TODO: What should this be, a new role?
        [HttpPost]
        public ActionResult Create(EditViewModel model)
        {
            return BaseCreate(model, Dependencies.CreateCommand);
        }
    }
}