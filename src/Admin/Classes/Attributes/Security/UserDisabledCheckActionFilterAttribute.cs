using BusinessLogic.Interfaces.Services;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Admin.Classes.Attributes.Security
{
    public class UserDisabledCheckActionFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.RouteData.Values["controller"].ToString() == "Home"
                && filterContext.RouteData.Values["action"].ToString() == "AccountDisabled") return;

            if (filterContext.RouteData.Values["controller"].ToString() == "Home"
                && filterContext.RouteData.Values["action"].ToString() == "GetSystemMessages") return;

            if (filterContext.RouteData.Values["controller"].ToString() == "Account") return;

            if (filterContext.RouteData.Values["controller"].ToString() == "Home"
                && filterContext.RouteData.Values["action"].ToString() == "Error") return;

            var _userService = DependencyResolver.Current.GetService<IUserService>();
            var isUserDisabled = (bool)_userService.IsUserDisabled(HttpContext.Current.User.Identity.Name).Data;

            if (!isUserDisabled) return;

            HttpSessionStateBase session = filterContext.HttpContext.Session;
            var user = session["User"];

            if (((user == null) && (!session.IsNewSession)) || (session.IsNewSession))
            {
                session.RemoveAll();
                session.Clear();
                session.Abandon();

                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary { { "controller", "Home" }, { "action", "AccountDisabled" } });
            }

            base.OnActionExecuting(filterContext);
        }
    }
}