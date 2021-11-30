using System;
using System.Security;
using System.Web.Mvc;
using Web.Mvc.Navigation;

namespace Admin.Controllers
{
    public class HomeController : BaseController<IHomeControllerDependencies>
    {
        public HomeController(IHomeControllerDependencies dependencies)
            : base(dependencies)
        {
        }

        [Authorize]
        [NavigatablePageActionFilter(ClearNavigation = true, AddToStack = false)]
        [HttpGet]
        public ActionResult Index()
        {
            var a = new Classes.Security.PaymentsAdminRoleProvider();

            try
            {
                var roles = a.GetRolesForUser(User.Identity.Name);
                if (roles.Length == 0) throw new SecurityException(string.Format("User not authorised to access this system: {0}", User.Identity.Name));
            }
            catch (Exception e)
            {
                Dependencies.Log.Warn(e);
                return RedirectToAction("Unauthorised");
            }

            return View();
        }

        [HttpGet]
        public ActionResult Unauthorised()
        {
            return View();
        }

        [HttpGet]
        public ActionResult AccountDisabled()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Error()
        {
            return View();
        }

        public ActionResult GetSystemMessages()
        {
            var model = Dependencies.ListViewModelBuilder.Build();
            return PartialView("_SystemMessage", model);
        }
    }
}