using Admin.Classes.ViewModelBuilders.EReturnTemplateRow;
using Admin.Models.EReturnTemplateRow;
using Admin.Models.Shared;
using BusinessLogic.Security;
using System.Web.Mvc;
using Web.Mvc.Navigation;

namespace Admin.Controllers
{
    [Classes.Security.Attributes.Authorize(Roles = Role.SystemAdmin)]
    public class EReturnTemplateRowController : BaseController<IEReturnTemplateRowControllerDependencies>
    {
        public EReturnTemplateRowController(IEReturnTemplateRowControllerDependencies dependecies)
            : base(dependecies)
        {
        }

        [ChildActionOnly]
        public ActionResult _ListForEReturnTemplate(int id)
        {
            var model = Dependencies.ListViewModelBuilder.Build(new SearchCriteria() { EReturnTemplateId = id });

            return PartialView("_List", model);
        }

        [ChildActionOnly]
        public ActionResult _EditListForEReturnTemplate(int id)
        {
            var model = Dependencies.ListViewModelBuilder.Build(new SearchCriteria() { EReturnTemplateId = id });

            return PartialView("_EditList", model);
        }

        [NavigatablePageActionFilter(DisplayText = "Create eReturn Template Row")]
        [HttpGet]
        public ActionResult Create(int eReturnTemplateId)
        {
            var model = Dependencies.CreateViewModelBuilder.Build(new CreateViewModelBuilderArgs()
            {
                EReturnTemplateId = eReturnTemplateId
            });

            return View(model);
        }

        [HttpPost]
        public ActionResult Create(EditViewModel model)
        {
            return BaseCreate(model, Dependencies.CreateCommand);
        }

        [NavigatablePageActionFilter(DisplayText = "Edit eReturn Template Row")]
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
        public ActionResult Delete(int id, int eReturnTemplateId)
        {
            var result = Dependencies.DeleteCommand.Execute(id);

            if (!result.Success)
            {
                var errorMsg = new ErrorMessage(result.Messages);

                TempData["Message"] = errorMsg;
            }

            return RedirectToAction("Edit", "EReturnTemplate", new { id = eReturnTemplateId });
        }
    }
}