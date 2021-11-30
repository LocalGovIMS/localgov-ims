using BusinessLogic.Entities;
using BusinessLogic.Models.EReturns;
using System;
using System.Collections.Generic;

namespace BusinessLogic.Interfaces.Repositories
{
    public interface IEReturnRepository : IRepository<EReturn>
    {
        List<EReturn> Search(SearchCriteria criteria, out int resultCount);
        EReturn GetEReturn(int id);
        void SetEReturnNo(EReturn item);
        void Update(EReturn item);
        void Delete(EReturn item);
        List<EReturn> GetEReturnsBeingProcessed(Guid processId);
        void Lock(List<int> items, Guid processId);
        void Unlock(Guid processId);
    }
}