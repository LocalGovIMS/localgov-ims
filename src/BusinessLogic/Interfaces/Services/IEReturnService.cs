using BusinessLogic.Entities;
using BusinessLogic.Interfaces.Result;
using BusinessLogic.Models;
using BusinessLogic.Models.EReturns;
using BusinessLogic.Models.Shared;
using System.Collections.Generic;

namespace BusinessLogic.Interfaces.Services
{
    public interface IEReturnService
    {
        SearchResult<EReturnWrapper> SearchTransactions(SearchCriteria criteria);
        EReturnWrapper GetEReturn(int id);
        IResult Create(EReturn item);
        IResult Update(EReturn item);
        IResult Delete(int id);
        IResult Submit(int id);
        IResult Approve(List<int> items);
    }
}
