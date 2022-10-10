using Admin.Classes.ViewModelBuilders.ImportProcessingRuleCondition;
using Admin.Models.ImportProcessingRuleCondition;
using Admin.Models.Shared;
using BusinessLogic.Security;
using System.Web.Mvc;
using Web.Mvc.Navigation;

namespace Admin.Controllers
{
    [Classes.Security.Attributes.Authorize(Roles = Role.SystemAdmin + "," + Role.ServiceDesk + "," + Role.Finance)]
    public class ImportProcessingRuleConditionController : BaseController<IImportProcessingRuleConditionControllerDependencies>
    {
        public ImportProcessingRuleConditionController(IImportProcessingRuleConditionControllerDependencies dependecies)
            : base(dependecies)
        {
        }

        [ChildActionOnly]
        public ActionResult _ListForImportProcessingRule(int id)
        {
            var model = Dependencies.ListViewModelBuilder.Build(new SearchCriteria() { ImportProcessingRuleId = id });

            return PartialView("_List", model);
        }

        [ChildActionOnly]
        public ActionResult _EditListForImportProcessingRule(int id)
        {
            var model = Dependencies.ListViewModelBuilder.Build(new SearchCriteria() { ImportProcessingRuleId = id });

            return PartialView("_EditList", model);
        }

        [NavigatablePageActionFilter(DisplayText = "Create Condition")]
        [HttpGet]
        public ActionResult Create(int importProcessingRuleId, int? group)
        {
            var model = Dependencies.CreateViewModelBuilder.Build(new CreateViewModelBuilderArgs()
            {
                ImportProcessingRuleId = importProcessingRuleId,
                Group = group
            });

            return View(model);
        }

        [HttpPost]
        public ActionResult Create(EditViewModel model)
        {
            return BaseCreate(model, Dependencies.CreateCommand);
        }

        [NavigatablePageActionFilter(DisplayText = "Edit Condition")]
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var model = Dependencies.EditViewModelBuilder.Build(id);

            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(EditViewModel model)
        {
            return BaseEdit(model, Dependencies.EditCommand);
        }

        [HttpGet]
        public ActionResult Delete(int id, int importProcessingRuleId)
        {
            var result = Dependencies.DeleteCommand.Execute(id);

            if (!result.Success)
            {
                var errorMsg = new ErrorMessage(result.Messages);

                TempData["Message"] = errorMsg;
            }

            return RedirectToAction("Edit", "ImportProcessingRule", new { id = importProcessingRuleId });
        }
    }
}