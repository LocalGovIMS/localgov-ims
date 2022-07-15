using BusinessLogic.Entities;
using BusinessLogic.Interfaces.Result;
using BusinessLogic.Models.Shared;
using BusinessLogic.Models.ImportType;
using System.Collections.Generic;

namespace BusinessLogic.Interfaces.Services
{
    public interface IImportTypeService
    {
        SearchResult<ImportType> Search(SearchCriteria criteria);
        ImportType Get(int id);
        List<ImportType> GetAll();
        IResult Create(ImportType item);
        IResult Update(ImportType item);
    }
}