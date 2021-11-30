using BusinessLogic.Interfaces.Services;
using BusinessLogic.Security;
using System.Web.Mvc;
using Web.Mvc.Navigation;

namespace Admin.Controllers
{
    [Classes.Security.Attributes.Authorize(Roles = Role.SystemAdmin)]
    public class SettingsController : Controller
    {
        private readonly IMaintenanceService _maintenanceService;

        public SettingsController(IMaintenanceService maintenanceService)
        {
            _maintenanceService = maintenanceService;
        }

        [NavigatablePageActionFilter(ClearNavigation = true)]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ProcessNotifications()
        {
            _maintenanceService.ProcessNotifications();
            return View("Index");
        }
    }
}