using Admin.Classes.Commands.ImportProcessingRule;
using Admin.Models.ImportProcessingRule;
using Admin.Models.Shared;
using BusinessLogic.Security;
using System.Web.Mvc;
using Web.Mvc.Navigation;

namespace Admin.Controllers
{
    [Classes.Security.Attributes.Authorize(Roles = Role.SystemAdmin + "," + Role.ServiceDesk + "," + Role.Finance)]
    public class ImportProcessingRuleController : BaseController<ImportProcessingRuleControllerDependencies>
    {
        public ImportProcessingRuleController(ImportProcessingRuleControllerDependencies dependecies)
            : base(dependecies)
        {
        }

        [NavigatablePageActionFilter(ClearNavigation = true, DisplayText = "Import Rules")]
        [HttpGet]
        public ActionResult List()
        {
            var model = Dependencies.ListViewModelBuilder.Build();

            return View("List", model);
        }

        [NavigatablePageActionFilter(OnlyComparePath = true, DisplayText = "Import Rules")]
        [HttpGet]
        public ActionResult ListMaintainingNavigation()
        {
            var model = Dependencies.ListViewModelBuilder.Build();

            return View("List", model);
        }

        [NavigatablePageActionFilter(DisplayText = "Import Rules")]
        [AcceptVerbs("GET", "POST")]
        public ActionResult Search(SearchCriteria criteria)
        {
            var model = Dependencies.ListViewModelBuilder.Build(criteria);

            return View("List", model);
        }

        [NavigatablePageActionFilter(DisplayText = "Rule Details")]
        [HttpGet]
        public ActionResult Details(int id)
        {
            var model = Dependencies.DetailsViewModelBuilder.Build(id);

            return View(model);
        }

        [NavigatablePageActionFilter(DisplayText = "Edit Rule")]
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

        [NavigatablePageActionFilter(DisplayText = "Create Rule")]
        [HttpGet]
        public ActionResult Create()
        {
            var model = Dependencies.EditViewModelBuilder.Build();

            return View(model);
        }

        [HttpPost]
        public ActionResult Create(EditViewModel model)
        {
            return BaseCreate(model, Dependencies.CreateCommand);
        }

        [HttpGet]
        public ActionResult ChangeStatus(int id, bool isDisabled)
        {
            var result = Dependencies.ChangeStatusCommand.Execute(new ChangeStatusCommandArgs() { ImportProcessingRuleId = id, IsDisabled = isDisabled });

            if (!result.Success)
            {
                var errorMsg = new ErrorMessage(result.Messages);

                TempData["Message"] = errorMsg;
            }

            return RedirectToAction("Edit", "ImportProcessingRule", new { id = id });
        }
    }
}