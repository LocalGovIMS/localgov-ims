using BusinessLogic.Interfaces.Result;
using BusinessLogic.Models;
using System;

namespace BusinessLogic.Interfaces.Services
{
    public interface ISuspenseJournalService
    {
        string GetPspReference();

        IResult CreateJournal(
            TransferItem journal,
            Guid transferReference,
            Guid transferGuid,
            string pspReference,
            DateTime suspenseTransactionDate);
    }
}
