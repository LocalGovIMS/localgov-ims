using BusinessLogic.Interfaces.Services;

namespace BusinessLogic.Services
{
    public class MaintenanceService : IMaintenanceService
    {
        private readonly INotificationService _notificationService;

        public MaintenanceService(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        public void ProcessNotifications()
        {
            _notificationService.ProcessNotifications();
        }
    }
}
