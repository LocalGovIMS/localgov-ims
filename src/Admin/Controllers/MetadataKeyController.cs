using Admin.Models.MetadataKey;
using Admin.Models.Shared;
using BusinessLogic.Security;
using System.Web.Mvc;
using Web.Mvc.Navigation;

namespace Admin.Controllers
{
    [Classes.Security.Attributes.Authorize(Roles = Role.SystemAdmin)]
    public class MetadataKeyController : BaseController<IMetadataKeyControllerDependencies>
    {
        public MetadataKeyController(IMetadataKeyControllerDependencies dependecies)
            : base(dependecies)
        {
        }

        [NavigatablePageActionFilter(DisplayText = "Metadata Keys")]
        [Classes.Security.Attributes.Authorize(Roles = Role.SystemAdmin)]
        [HttpGet]
        public ActionResult List()
        {
            var model = Dependencies.ListViewModelBuilder.Build();

            return View("List", model);
        }

        [NavigatablePageActionFilter(DisplayText = "Metadata Keys")]
        [Classes.Security.Attributes.Authorize(Roles = Role.SystemAdmin)]
        [AcceptVerbs("GET", "POST")]
        public ActionResult Search(SearchCriteria criteria)
        {
            var model = Dependencies.ListViewModelBuilder.Build(criteria);

            return View("List", model);
        }

        [NavigatablePageActionFilter(DisplayText = "Metadata Key Details")]
        [HttpGet]
        public ActionResult Details(int id)
        {
            var model = Dependencies.DetailsViewModelBuilder.Build(id);

            return View(model);
        }

        [NavigatablePageActionFilter(DisplayText = "Edit Metadata Key")]
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var model = Dependencies.EditViewModelBuilder.Build(id);

            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(EditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                Dependencies.EditViewModelBuilder.Rebuild(model);

                return View(model);
            }

            Dependencies.EditCommand.Execute(model);

            return RedirectToAction("Back");
        }

        [NavigatablePageActionFilter(DisplayText = "Create Metadata Key")]
        [HttpGet]
        public ActionResult Create()
        {
            var model = new EditViewModel();
            model = Dependencies.EditViewModelBuilder.Rebuild(model);

            return View(model);
        }

        [HttpPost]
        public ActionResult Create(EditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                Dependencies.EditViewModelBuilder.Rebuild(model);

                return View(model);
            }

            Dependencies.CreateCommand.Execute(model);

            return RedirectToAction("Back");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var result = Dependencies.DeleteCommand.Execute(id);

            if (!result.Success)
            {
                var errorMsg = new ErrorMessage(result.Messages);

                TempData["Message"] = errorMsg;
            }

            return RedirectToAction("List");
        }
    }
}