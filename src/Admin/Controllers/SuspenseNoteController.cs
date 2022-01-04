using Admin.Models.SuspenseNote;
using BusinessLogic.Security;
using System.Web.Mvc;
using Web.Mvc.Navigation;

namespace Admin.Controllers
{
    [Classes.Security.Attributes.Authorize(Roles = Role.Finance + "," + Role.Reporting)]
    public class SuspenseNoteController : BaseController<ISuspenseNoteControllerDependencies>
    {
        public SuspenseNoteController(ISuspenseNoteControllerDependencies dependencies)
            : base(dependencies)
        {
        }

        [ChildActionOnly]
        [HttpGet]
        public ActionResult _ListForSuspense(int id)
        {
            var model = Dependencies.ListViewModelBuilder.Build(id);

            return PartialView("_List", model);
        }

        [NavigatablePageActionFilter(OnlyComparePath = true, DisplayText = "Suspense Notes")]
        [HttpGet]
        public ActionResult List(int id)
        {
            var model = Dependencies.ListViewModelBuilder.Build(id);

            return View("List", model);
        }

        [NavigatablePageActionFilter(DisplayText = "Create Note")]
        [HttpGet]
        public ActionResult Create(int id)
        {
            var model = new EditViewModel() { SuspenseId = id };

            return View(model);
        }

        [HttpPost]
        public ActionResult Create(EditViewModel model)
        {
            return BaseCreate(model, Dependencies.CreateCommand);
        }
    }
}