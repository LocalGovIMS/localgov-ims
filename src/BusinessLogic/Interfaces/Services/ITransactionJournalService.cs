using BusinessLogic.Interfaces.Result;
using BusinessLogic.Models;
using System;
using System.Collections.Generic;

namespace BusinessLogic.Interfaces.Services
{
    public interface ITransactionJournalService
    {
        IResult Transfer(List<TransferItem> transferItems, string pspReference, string transactionReference);
        IResult CreateJournal(TransferItem transferIn
            , TransferItem transferOut
            , List<TransferItem> creditNotes
            , string transferReference, DateTime suspenseTransactionDate);
        IResult UndoTransfer(string transferGuid);
    }
}
