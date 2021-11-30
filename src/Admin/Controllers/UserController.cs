using Admin.Models.User;
using BusinessLogic.Security;
using System.Web.Mvc;
using Web.Mvc.Navigation;

namespace Admin.Controllers
{
    [Classes.Security.Attributes.Authorize(Roles = Role.SystemAdmin + "," + Role.ServiceDesk)]
    public class UserController : BaseController<IUserControllerDependencies>
    {
        public UserController(IUserControllerDependencies dependecies)
            : base(dependecies)
        {
        }

        [NavigatablePageActionFilter(ClearNavigation = true, DisplayText = "Users")]
        [HttpGet]
        public ActionResult List()
        {
            var model = Dependencies.ListViewModelBuilder.Build();

            return View("List", model);
        }

        [NavigatablePageActionFilter(ClearNavigation = true, DisplayText = "Users")]
        [AcceptVerbs("GET", "POST")]
        public ActionResult Search(SearchCriteria criteria)
        {
            var model = Dependencies.ListViewModelBuilder.Build(criteria);

            return View("List", model);
        }

        [NavigatablePageActionFilter(DisplayText = "User Details")]
        [HttpGet]
        public ActionResult Details(int id)
        {
            var model = Dependencies.DetailsViewModelBuilder.Build(id);

            return View(model);
        }

        [NavigatablePageActionFilter(DisplayText = "Edit User")]
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var model = Dependencies.EditViewModelBuilder.Build(id);

            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(EditViewModel model)
        {
            if (!ModelState.IsValid) return View(Dependencies.EditViewModelBuilder.Rebuild(model));

            Dependencies.EditCommand.Execute(model);

            return RedirectToAction("Back");
        }

        [NavigatablePageActionFilter(DisplayText = "Create User")]
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
    }
}