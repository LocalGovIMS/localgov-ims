using Admin.Models.TransactionImportTypeImportProcessingRule;
using Admin.Models.Shared;
using BusinessLogic.Security;
using System.Web.Mvc;
using Web.Mvc.Navigation;

namespace Admin.Controllers
{
    [Classes.Security.Attributes.Authorize(Roles = Role.SystemAdmin)]
    public class TransactionImportTypeImportProcessingRuleController : BaseController<ITransactionImportTypeImportProcessingRuleControllerDependencies>
    {
        public TransactionImportTypeImportProcessingRuleController(ITransactionImportTypeImportProcessingRuleControllerDependencies dependecies)
            : base(dependecies)
        {
        }

        [ChildActionOnly]
        public ActionResult _List(int id)
        {
            var model = Dependencies.ListViewModelBuilder.Build(new SearchCriteria() { TransactionImportTypeId = id });

            return PartialView("_List", model);
        }

        [ChildActionOnly]
        public ActionResult _Edit(int id)
        {
            var model = Dependencies.ListViewModelBuilder.Build(new SearchCriteria() { TransactionImportTypeId = id });

            return PartialView("_EditList", model);
        }

        [NavigatablePageActionFilter(DisplayText = "Create Import Processing Rule Link")]
        [Classes.Security.Attributes.Authorize(Roles = Role.SystemAdmin)]
        [HttpGet]
        public ActionResult Create(int id)
        {
            var model = Dependencies.CreateViewModelBuilder.Build(id);

            return View("Create", model);
        }

        [Classes.Security.Attributes.Authorize(Roles = Role.SystemAdmin)]
        [HttpPost]
        public ActionResult Create(EditViewModel model)
        {
            return BaseCreate(model, Dependencies.CreateCommand);
        }

        [NavigatablePageActionFilter(DisplayText = "Edit Transaction ")]
        [Classes.Security.Attributes.Authorize(Roles = Role.SystemAdmin)]
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var model = Dependencies.EditViewModelBuilder.Build(id);

            return View(model);
        }

        [Classes.Security.Attributes.Authorize(Roles = Role.SystemAdmin)]
        [HttpPost]
        public ActionResult Edit(EditViewModel model)
        {
            return BaseEdit(model, Dependencies.EditCommand);
        }

        [Classes.Security.Attributes.Authorize(Roles = Role.SystemAdmin)]
        [HttpGet]
        public ActionResult Delete(int id, int transactionImportTypeId)
        {
            var result = Dependencies.DeleteCommand.Execute(id);

            if (!result.Success)
            {
                var errorMsg = new ErrorMessage(result.Messages);

                TempData["Message"] = errorMsg;
            }

            return RedirectToAction("Edit", "TransactionImportType", new { id = transactionImportTypeId });
        }
    }
}