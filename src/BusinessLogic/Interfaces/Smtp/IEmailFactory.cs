using BusinessLogic.Entities;
using BusinessLogic.Models;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;

namespace BusinessLogic.Interfaces.Smtp
{
    public interface IEmailFactory
    {
        IEmail CreateVatReceiptEmail(string recipientAddress, Transaction transactions);
        IEmail CreateEReturnDeletedEmail(string eReturnNumber, string deletedBy);
        IEmail CreateNotificationEmail(TransactionNotification notification);
        IEmail SendDuplicateTransactionEmail(List<ProcessedTransaction> transactions);
        IEmail SendPasswordResetEmail(IdentityMessage message);
    }
}
