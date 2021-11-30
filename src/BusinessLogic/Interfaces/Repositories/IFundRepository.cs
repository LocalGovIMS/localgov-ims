using BusinessLogic.Entities;
using BusinessLogic.Models.Fund;
using System.Collections.Generic;

namespace BusinessLogic.Interfaces.Repositories
{
    public interface IFundRepository : IRepository<Fund>
    {
        Fund GetByFundCode(string fundCode);
        IEnumerable<Fund> GetAll(bool includeDisabled);
        void Update(Fund entity);
        IEnumerable<Fund> Search(SearchCriteria criteria, out int resultCount);
    }
}