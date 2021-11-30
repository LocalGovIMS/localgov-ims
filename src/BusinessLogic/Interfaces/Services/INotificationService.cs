using BusinessLogic.Entities;
using BusinessLogic.Interfaces.Result;

namespace BusinessLogic.Interfaces.Services
{
    public interface INotificationService
    {
        bool SaveNotification(TransactionNotification notification);
        IResult ProcessNotification(TransactionNotification notification);
        void ProcessNotifications();
    }
}
