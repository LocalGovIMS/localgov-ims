using BusinessLogic.Interfaces.Result;
using BusinessLogic.Models.Shared;
using BusinessLogic.Models.Suspense;
using BusinessLogic.Services;
using System.Collections.Generic;

namespace BusinessLogic.Interfaces.Services
{
    public interface ISuspenseService
    {
        IResult Create(Entities.Suspense suspense);
        IResult Create(CreateSuspenseArgs args);
        SearchResult<SuspenseWrapper> Search(SearchCriteria criteria);
        SuspenseWrapper GetSuspense(int id);
        IResult Journal(List<int> suspenseItems, List<Journal> journalItems, List<CreditNote> creditNotes);
        IResult SaveNotes(int id, string notes);
    }
}
