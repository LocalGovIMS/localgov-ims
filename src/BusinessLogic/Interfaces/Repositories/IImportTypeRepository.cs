using BusinessLogic.Entities;
using BusinessLogic.Models.ImportType;
using System.Collections.Generic;

namespace BusinessLogic.Interfaces.Repositories
{
    public interface IImportTypeRepository : IRepository<ImportType>
    {
        IEnumerable<ImportType> Search(SearchCriteria criteria, out int resultCount);
        ImportType Get(int id);
        void Update(ImportType entity);
    }
}