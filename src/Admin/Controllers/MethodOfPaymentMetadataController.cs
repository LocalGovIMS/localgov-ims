using Admin.Classes.ViewModelBuilders.MethodOfPaymentMetadata;
using Admin.Models.MethodOfPaymentMetadata;
using Admin.Models.Shared;
using BusinessLogic.Security;
using System.Web.Mvc;
using Web.Mvc.Navigation;

namespace Admin.Controllers
{
    [Classes.Security.Attributes.Authorize(Roles = Role.SystemAdmin)]
    public class MethodOfPaymentMetadataController : BaseController<IMethodOfPaymentMetadataControllerDependencies>
    {
        public MethodOfPaymentMetadataController(IMethodOfPaymentMetadataControllerDependencies dependecies)
            : base(dependecies)
        {
        }

        [ChildActionOnly]
        public ActionResult _ListForMethodOfPayment(string mopCode)
        {
            var model = Dependencies.ListViewModelBuilder.Build(new SearchCriteria() { MopCode = mopCode });

            return PartialView("_List", model);
        }

        [ChildActionOnly]
        public ActionResult _EditListForMethodOfPayment(string mopCode)
        {
            var model = Dependencies.ListViewModelBuilder.Build(new SearchCriteria() { MopCode = mopCode });

            return PartialView("_EditList", model);
        }

        [NavigatablePageActionFilter(DisplayText = "Create Metadata")]
        [Classes.Security.Attributes.Authorize(Roles = Role.SystemAdmin)]
        [HttpGet]
        public ActionResult Create(string mopCode)
        {
            var model = Dependencies.CreateViewModelBuilder.Build(new CreateViewModelBuilderArgs()
            {
                MopCode = mopCode
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

            return RedirectToAction("Edit", "MethodOfPayment", new { id = mopCode });
        }
    }
}