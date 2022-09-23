using BusinessLogic.Interfaces.Result;
using BusinessLogic.Models.Suspense;
using System.Collections.Generic;

namespace BusinessLogic.Suspense.JournalAllocation
{
    public interface IJournalAllocationStrategy
    {
        IResult Execute(List<int> suspenseItems
            , List<Journal> journalItems
            , List<CreditNote> creditNotes);
    }
}
