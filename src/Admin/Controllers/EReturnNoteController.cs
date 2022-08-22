using Admin.Models.EReturnNote;
using BusinessLogic.Security;
using System.Web.Mvc;
using Web.Mvc.Navigation;

namespace Admin.Controllers
{
    [Classes.Security.Attributes.Authorize(Roles = Role.EReturns + "," + Role.EReturnAuthoriser + "," + Role.EReturnDelete)]
    public class EReturnNoteController : BaseController<IEReturnNoteControllerDependencies>
    {
        public EReturnNoteController(IEReturnNoteControllerDependencies dependencies)
            : base(dependencies)
        {
        }

        [ChildActionOnly]
        [HttpGet]
        public ActionResult _ListForEReturn(int id)
        {
            var model = Dependencies.ListViewModelBuilder.Build(id);

            return PartialView("_List", model);
        }

        [NavigatablePageActionFilter(OnlyComparePath = true, DisplayText = "EReturn Notes")]
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
            var model = new EditViewModel() { EReturnId = id };

            return View(model);
        }

        [HttpPost]
        public ActionResult Create(EditViewModel model)
        {
            return BaseCreate(model, Dependencies.CreateCommand);
        }
    }
}