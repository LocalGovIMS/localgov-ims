using BusinessLogic.Models.Suspense;
using System.Collections.Generic;

namespace BusinessLogic.Interfaces.Strategies
{
    public interface IJournalAllocationStrategy
    {
        Result.IResult Execute(List<int> suspenseItems
            , List<Journal> journalItems
            , List<CreditNote> creditNotes);
    }
}
