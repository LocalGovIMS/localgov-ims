using BusinessLogic.Entities;
using BusinessLogic.Models;
using System.Collections.Generic;

namespace BusinessLogic.Interfaces.Smtp
{
    public interface IEmailFactory
    {
        IEmail CreateVatReceiptEmail(string recipientAddress, Transaction transactions);
        IEmail CreateEReturnDeletedEmail(string eReturnNumber, string deletedBy);
        IEmail CreateNotificationEmail(TransactionNotification notification);
        IEmail SendDuplicateTransactionEmail(List<ProcessedTransaction> transactions);
    }
}
