using Admin.Models.UserRole;
using BusinessLogic.Security;
using System.Web.Mvc;
using Web.Mvc.Navigation;

namespace Admin.Controllers
{
    [Classes.Security.Attributes.Authorize(Roles = Role.SystemAdmin + "," + Role.ServiceDesk)]
    public class UserRoleController : BaseController<IUserRoleControllerDependencies>
    {
        public UserRoleController(IUserRoleControllerDependencies dependencies)
            : base(dependencies)
        {
        }

        // TODO: Is there any need for a ViewModelBuilder for this?
        // Is it possible to create a generic ViewModelBuilder for a BasicListViewModel?
        // Are there any benefits to that?
        [ChildActionOnly]
        public ActionResult _ListForUser(int id)
        {
            var model = Dependencies.BasicListViewModelBuilder.Build(id);

            return PartialView("_BasicList", model);
        }

        [NavigatablePageActionFilter(DisplayText = "Edit Roles")]
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
    }
}