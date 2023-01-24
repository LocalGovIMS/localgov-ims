using BusinessLogic.Entities;
using BusinessLogic.Models.Import;
using BusinessLogic.Models.Shared;

namespace BusinessLogic.Interfaces.Services
{
    public interface IImportService
    {
        SearchResult<Import> Search(SearchCriteria criteria);
        Import Get(int id);
    }
}