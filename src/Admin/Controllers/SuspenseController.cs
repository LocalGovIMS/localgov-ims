using Admin.Models.Suspense;
using BusinessLogic.Security;
using System.Web.Mvc;
using Web.Mvc.Navigation;

namespace Admin.Controllers
{
    [Classes.Security.Attributes.Authorize(Roles = Role.Finance + "," + Role.Reporting)]
    public class SuspenseController : BaseController<ISuspenseControllerDependencies>
    {
        public SuspenseController(ISuspenseControllerDependencies dependencies)
            : base(dependencies)
        {
        }

        [NavigatablePageActionFilter(ClearNavigation = true, DisplayText = "Suspense")]
        [HttpGet]
        public ActionResult List()
        {
            var model = Dependencies.ListViewModelBuilder.Build();

            return View("List", model);
        }

        [NavigatablePageActionFilter(ClearNavigation = true, DisplayText = "Suspense")]
        [AcceptVerbs("GET", "POST")]
        public ActionResult Search(SearchCriteria criteria)
        {
            var model = Dependencies.ListViewModelBuilder.Build(criteria);

            return View("List", model);
        }

        [NavigatablePageActionFilter(DisplayText = "Suspense Details")]
        [HttpGet]
        public ActionResult Details(int id)
        {
            var model = Dependencies.DetailsViewModelBuilder.Build(id);

            return View(model);
        }

        [HttpGet]
        [ChildActionOnly]
        [Classes.Security.Attributes.Authorize(Roles = Role.Finance)]
        public ActionResult _Journal()
        {
            var model = Dependencies.JournalViewModelBuilder.Build();

            return PartialView(model);
        }

        [HttpGet]
        [ChildActionOnly]
        [Classes.Security.Attributes.Authorize(Roles = Role.Finance)]
        public ActionResult _CreditNote()
        {
            var model = Dependencies.JournalViewModelBuilder.Build();

            return PartialView(model);
        }

        [HttpPost]
        [Classes.Security.Attributes.Authorize(Roles = Role.Finance)]
        public JsonResult SubmitTransfers(JournalViewModel model)
        {
            var result = Dependencies.JournalCommand.Execute(model);
            return Json(new { ok = result.Success, message = string.Join(", ", result.Messages) });
        }

        [HttpPost]
        [Classes.Security.Attributes.Authorize(Roles = Role.Finance)]
        public JsonResult SaveNote(SaveNoteViewModel model)
        {
            var result = Dependencies.SaveNoteCommand.Execute(model);
            return Json(new { ok = result.Success, message = string.Join(", ", result.Messages) });
        }
    }
}