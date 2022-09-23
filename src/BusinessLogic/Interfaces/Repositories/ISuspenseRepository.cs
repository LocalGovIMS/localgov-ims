using BusinessLogic.Models.Suspense;
using System;
using System.Collections.Generic;

namespace BusinessLogic.Interfaces.Repositories
{
    public interface ISuspenseRepository : IRepository<Entities.Suspense>
    {
        List<Entities.Suspense> Search(SearchCriteria criteria, out int resultCount);
        Entities.Suspense GetSuspense(int id);
        List<Entities.Suspense> GetSuspensesBeingProcessed(Guid processId);
        void Lock(List<int> items, Guid processId);
        void Unlock(Guid processId);
    }
}