using BusinessLogic.Entities;
using BusinessLogic.Models.Suspense;
using System;
using System.Collections.Generic;

namespace BusinessLogic.Interfaces.Repositories
{
    public interface ISuspenseRepository : IRepository<Suspense>
    {
        List<Suspense> Search(SearchCriteria criteria, out int resultCount);
        Suspense GetSuspense(int id);
        List<Suspense> GetSuspensesBeingProcessed(Guid processId);
        void Lock(List<int> items, Guid processId);
        void Unlock(Guid processId);
    }
}