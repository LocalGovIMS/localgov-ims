using Admin.Models.UserFundGroup;
using BusinessLogic.Security;
using System.Web.Mvc;
using Web.Mvc.Navigation;

namespace Admin.Controllers
{
    [Classes.Security.Attributes.Authorize(Roles = Role.SystemAdmin + "," + Role.ServiceDesk)]
    public class UserFundGroupController : BaseController<IUserFundGroupControllerDependencies>
    {
        public UserFundGroupController(IUserFundGroupControllerDependencies dependencies)
            : base(dependencies)
        {
        }

        [ChildActionOnly]
        public ActionResult _ListForUser(int id)
        {
            var model = Dependencies.BasicListViewModelBuilder.Build(id);

            return PartialView("_BasicList", model);
        }

        [NavigatablePageActionFilter(DisplayText = "User Fund Groups")]
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