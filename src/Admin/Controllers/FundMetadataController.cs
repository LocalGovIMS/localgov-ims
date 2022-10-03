using Admin.Classes.ViewModelBuilders.FundMetadata;
using Admin.Models.FundMetadata;
using Admin.Models.Shared;
using BusinessLogic.Security;
using System.Web.Mvc;
using Web.Mvc.Navigation;

namespace Admin.Controllers
{
    [Classes.Security.Attributes.Authorize(Roles = Role.SystemAdmin)]
    public class FundMetadataController : BaseController<IFundMetadataControllerDependencies>
    {
        public FundMetadataController(IFundMetadataControllerDependencies dependecies)
            : base(dependecies)
        {
        }

        [ChildActionOnly]
        public ActionResult _ListForFund(string fundCode)
        {
            var model = Dependencies.ListViewModelBuilder.Build(new SearchCriteria() { FundCode = fundCode });

            return PartialView("_List", model);
        }

        [ChildActionOnly]
        public ActionResult _EditListForFund(string fundCode)
        {
            var model = Dependencies.ListViewModelBuilder.Build(new SearchCriteria() { FundCode = fundCode });

            return PartialView("_EditList", model);
        }

        [NavigatablePageActionFilter(DisplayText = "Create Metadata")]
        [Classes.Security.Attributes.Authorize(Roles = Role.SystemAdmin)]
        [HttpGet]
        public ActionResult Create(string fundCode)
        {
            var model = Dependencies.CreateViewModelBuilder.Build(new CreateViewModelBuilderArgs()
            {
                FundCode = fundCode
            });

            return View(model);
        }

        [Classes.Security.Attributes.Authorize(Roles = Role.SystemAdmin)]
        [HttpPost]
        public ActionResult Create(EditViewModel model)
        {
            return BaseCreate(model, Dependencies.CreateCommand);
        }

        [NavigatablePageActionFilter(DisplayText = "Edit Metadata")]
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
        public ActionResult Delete(int id, string mopCode)
        {
            var result = Dependencies.DeleteCommand.Execute(id);

            if (!result.Success)
            {
                var errorMsg = new ErrorMessage(result.Messages);

                TempData["Message"] = errorMsg;
            }

            return RedirectToAction("Edit", "Fund", new { id = mopCode });
        }
    }
}