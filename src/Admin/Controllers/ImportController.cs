using Admin.Models.Import;
using BusinessLogic.Security;
using System.Web.Mvc;
using Web.Mvc.Navigation;

namespace Admin.Controllers
{
    [Classes.Security.Attributes.Authorize(Roles = Role.SystemAdmin + "," + Role.Finance)]
    public class ImportController : BaseController<IImportControllerDependencies>
    {
        public ImportController(IImportControllerDependencies dependecies)
            : base(dependecies)
        {
        }

        [NavigatablePageActionFilter(ClearNavigation = true, DisplayText = "Imports")]
        [HttpGet]
        public ActionResult List()
        {
            var model = Dependencies.ListViewModelBuilder.Build();

            return View("List", model);
        }

        [NavigatablePageActionFilter(ClearNavigation = true, DisplayText = "Imports")]
        [AcceptVerbs("GET", "POST")]
        public ActionResult Search(SearchCriteria criteria)
        {
            var model = Dependencies.ListViewModelBuilder.Build(criteria);

            return View("List", model);
        }

        [HttpGet]
        [ChildActionOnly]
        public ActionResult _List()
        {
            var model = Dependencies.ListViewModelBuilder.Build();

            return PartialView(model);
        }

        [HttpGet]
        [ChildActionOnly]
        public ActionResult _Search(SearchCriteria criteria)
        {
            var model = Dependencies.ListViewModelBuilder.Build(criteria);

            return PartialView("_List", model);
        }

        [NavigatablePageActionFilter(DisplayText = "Import Details")]
        [HttpGet]
        public ActionResult Details(int id)
        {
            var model = Dependencies.DetailsViewModelBuilder.Build(id);

            return View(model);
        }
    }
}