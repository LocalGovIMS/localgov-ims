using BusinessLogic.Interfaces.Services;
using log4net;
using System;
using System.Net;
using System.Web.Http;

namespace Api.Controllers.Notification
{
    public class NotificationController : ApiController
    {
        private readonly ILog _log;
        private readonly INotificationService _notificationService;

        public NotificationController(
            ILog log,
            INotificationService notificationService)
        {
            _log = log ?? throw new ArgumentNullException("log");
            _notificationService = notificationService ?? throw new ArgumentNullException("notificationService");
        }

        [HttpPost]
        public IHttpActionResult Post([FromBody] NotificationModel model)
        {
            try
            {
                var notificationSaved = SaveAndTryProcessNotification(model);

                if (notificationSaved)
                    return StatusCode(HttpStatusCode.Accepted);

                return StatusCode(HttpStatusCode.InternalServerError);
            }
            catch (Exception ex)
            {
                _log.Error(ex);

                return BadRequest();
            }
        }

        // TODO: Rename
        // TODO: Split into two methods
        private bool SaveAndTryProcessNotification(NotificationModel model)
        {
            try
            {
                _notificationService.ProcessNotification(model.GetTransactionNotification());
                model.Processed = true;
            }
            catch (Exception ex)
            {
                _log.Error("Exception trying to save and process notification", ex);
            }

            _log.Debug("Saving notification");

            return _notificationService.SaveNotification(model.GetTransactionNotification());
        }
    }
}
