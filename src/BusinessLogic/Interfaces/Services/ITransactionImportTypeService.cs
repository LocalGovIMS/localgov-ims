using BusinessLogic.Entities;
using BusinessLogic.Interfaces.Result;
using BusinessLogic.Models.Shared;
using BusinessLogic.Models.TransactionImportType;
using System.Collections.Generic;

namespace BusinessLogic.Interfaces.Services
{
    public interface ITransactionImportTypeService
    {
        SearchResult<TransactionImportType> Search(SearchCriteria criteria);
        TransactionImportType Get(int id);
        List<TransactionImportType> GetAll();
        IResult Create(TransactionImportType item);
        IResult Update(TransactionImportType item);
    }
}