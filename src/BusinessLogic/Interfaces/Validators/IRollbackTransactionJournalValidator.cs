using BusinessLogic.Entities;
using BusinessLogic.Interfaces.Result;
using System.Collections.Generic;

namespace BusinessLogic.Interfaces.Validators
{
    public interface IRollbackTransactionJournalValidator
    {
        IResult Validate(IList<ProcessedTransaction> transfers);
    }
}
