using BusinessLogic.Entities;
using BusinessLogic.Models.Import;
using System.Collections.Generic;

namespace BusinessLogic.Interfaces.Repositories
{
    public interface IImportRepository : IRepository<Import>
    {
        Import Get(int id);
        List<Import> Search(SearchCriteria criteria, out int resultCount);
    }
}