using BusinessLogic.Interfaces.Result;
using BusinessLogic.Models;
using System.Collections.Generic;

namespace BusinessLogic.Interfaces.Validators
{
    public interface ITransactionJournalValidator
    {
        IResult Validate(Transaction transaction
            , IList<TransferItem> transferItems
            , string transactionReference);
    }
}
