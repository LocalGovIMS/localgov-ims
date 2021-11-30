using BusinessLogic.Classes;
using BusinessLogic.Classes.Result;
using BusinessLogic.Entities;
using BusinessLogic.Interfaces.Persistence;
using BusinessLogic.Interfaces.Result;
using BusinessLogic.Interfaces.Security;
using BusinessLogic.Interfaces.Services;
using log4net;
using System;

namespace BusinessLogic.Services
{
    public class NotificationService : BaseService, INotificationService
    {
        ITransactionService _transactionService;
        private readonly IEmailService _emailService;

        public NotificationService(ILog logger
            , IUnitOfWork unitOfWork
            , ISecurityContext securityContext
            , ITransactionService transactionService
            , IEmailService emailService)
            : base(logger, unitOfWork, securityContext)
        {
            _transactionService = transactionService;
            _emailService = emailService;
        }

        public bool SaveNotification(TransactionNotification notification)
        {
            try
            {
                ExtractAuthorisationFieldsFromNotification(notification);

                UnitOfWork.TransactionNotifications.Add(notification);
                UnitOfWork.Complete(SecurityContext.UserId);
            }
            catch (Exception e)
            {
                Logger.Error(null, e);
                return false;
            }
            return true;
        }

        private static PaymentAuthorisation ExtractAuthorisationFieldsFromNotification(TransactionNotification notification)
        {
            if (notification.EventCode != NotificationEvent.Authorisation) return null;
            var authFragments = notification.Reason.Split(':');
            if (authFragments.Length < 3) return new PaymentAuthorisation("", notification.Reason, "");

            notification.Reason = authFragments[0];
            return new PaymentAuthorisation(authFragments[2], authFragments[0], authFragments[1]);
        }

        public IResult ProcessNotification(TransactionNotification notification)
        {
            Logger.DebugFormat("Event_Code: {0}", notification.EventCode);
            Logger.DebugFormat("Success_Flag: {0}", notification.Success);

            if (notification.EventCode == NotificationEvent.Authorisation && notification.Success)
            {
                var paymentAuthorisation = ExtractAuthorisationFieldsFromNotification(notification);
                return _transactionService.AuthoriseTransactionByNotification(notification);
            }
            if (notification.EventCode == NotificationEvent.Refund && notification.Success)
            {
                return _transactionService.AuthoriseRefundByNotification(notification.MerchantReference, notification.PspReference);
            }
            if (notification.EventCode == NotificationEvent.Refund && !notification.Success)
            {
                return _transactionService.MarkRefundsAsFailed(notification.MerchantReference, notification.Reason);
            }

            if (notification.EventCode == NotificationEvent.Chargeback
                || notification.EventCode == NotificationEvent.ChargebackReversed
                || notification.EventCode == NotificationEvent.NotificationOfChargeback
                || notification.EventCode == NotificationEvent.RequestForInformation)
            {
                return _emailService.SendNotificationEmail(notification);
            }

            Logger.Debug("Nothing to process");

            return new Result();
        }

        public void ProcessNotifications()
        {
            var notifications = _transactionService.GetUnprocessedNotifications();
            foreach (var notification in notifications)
            {
                try
                {
                    var processResult = ProcessNotification(notification);
                    if (processResult.Success)
                    {
                        _transactionService.MarkNotificationAsProcessed(notification.Id);
                    }
                }
                catch (Exception e)
                {
                    Logger.Error("Failed to re-process notification ID " + notification.Id, e);
                }
            }
        }
    }
}