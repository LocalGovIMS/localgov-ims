using BusinessLogic.Entities;
using BusinessLogic.Interfaces.Result;
using BusinessLogic.Models;
using BusinessLogic.Models.Shared;
using BusinessLogic.Models.Transactions;
using BusinessLogic.Services;
using System.Collections.Generic;

namespace BusinessLogic.Interfaces.Services
{
    public interface ITransactionService
    {
        Response AuthorisePendingTransactionByInternalReference(AuthorisePendingTransactionByInternalReferenceArgs args);
        IResult AuthoriseRefundByNotification(string internalReference, string pspReference);
        IResult FailPendingTransaction(string reference, string pspReference, string authResult);
        Response SaveChequesToProcessed(List<ProcessedTransaction> transactions);
        List<PendingTransaction> GetPendingTransactionsByInternalReference(string internalReference);
        Response SavePendingTransaction(PendingTransaction pendingTransaction, string source);
        Response SavePendingTransactions(IEnumerable<PendingTransaction> pendingTransactions, string source);
        IResult SuspendPendingTransaction(string reference, string pspReference, string authResult);
        List<ProcessedTransaction> GetTransactionsByInternalReference(string internalReference);
        ProcessedTransaction GetTransaction(string transactionReference);
        SearchResult<ProcessedTransaction> SearchTransactions(SearchCriteria criteria);
        List<PendingTransaction> GetPendingRefunds(string transactionReference);
        List<ProcessedTransaction> GetProcessedRefunds(string transactionReference);
        List<ProcessedTransaction> GetTransfers(string transferGuid);
        Transaction GetTransactionByPspReference(string pspReference);
        decimal GetAmountForPendingTransactionByReference(string reference);
        IResult MarkRefundsAsFailed(string pspReference, string reason);
        IResult ReceiptIssued(string pspReference);
        ProcessedTransaction GetTransactionByAppReference(string appReference);
        IResult CreateProcessedTransaction(ProcessedTransaction processedTransaction);
        IResult CreateProcessedTransaction(ProcessedTransaction processedTransaction, bool saveChanges);
        IResult CreateProcessedTransaction(CreateProcessedTransactionArgs args);
        IResult UpdateCardDetails(UpdateCardDetailsArgs args);
    }
}