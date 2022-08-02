using BusinessLogic.Interfaces.Services;
using BusinessLogic.Security;
using System.Web.Mvc;
using Web.Mvc.Navigation;

namespace Admin.Controllers
{
    [Classes.Security.Attributes.Authorize(Roles = Role.SystemAdmin)]
    public class SettingsController : Controller
    {        
        public SettingsController()
        {           
        }

        [NavigatablePageActionFilter(ClearNavigation = true)]
        public ActionResult Index()
        {
            return View();
        }
    }
}