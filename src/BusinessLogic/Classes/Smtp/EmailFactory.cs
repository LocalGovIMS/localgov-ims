using BusinessLogic.Classes.Smtp.Emails;
using BusinessLogic.Entities;
using BusinessLogic.Interfaces.Smtp;
using BusinessLogic.Models;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;

namespace BusinessLogic.Classes.Smtp
{
    public class EmailFactory : IEmailFactory
    {
        public IEmail CreateVatReceiptEmail(string recipientAddress, Transaction transaction)
        {
            return new VatReceiptEmail(new VatReceiptArgs(recipientAddress, transaction));
        }

        public IEmail CreateEReturnDeletedEmail(string eReturnNumber, string deletedBy)
        {
            return new EReturnDeletedEmail(eReturnNumber, deletedBy);
        }

        public IEmail CreateNotificationEmail(TransactionNotification notification)
        {
            return new NotificationEmail(notification);
        }

        public IEmail SendDuplicateTransactionEmail(List<ProcessedTransaction> transactions)
        {
            return new DuplicateTransactionEmail(transactions);
        }

        public IEmail SendPasswordResetEmail(IdentityMessage message)
        {
            return new PasswordResetEmail(message);
        }
    }
}
