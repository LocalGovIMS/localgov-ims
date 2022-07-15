using BusinessLogic.Entities;
using BusinessLogic.Models.Shared;
using BusinessLogic.Models.Import;

namespace BusinessLogic.Interfaces.Services
{
    public interface IImportService
    {
        SearchResult<Import> Search(SearchCriteria criteria);
        Import Get(int id);
    }
}