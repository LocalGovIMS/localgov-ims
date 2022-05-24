using BusinessLogic.Entities;
using BusinessLogic.Interfaces.Result;
using BusinessLogic.Models.FundMessage;
using BusinessLogic.Models.Shared;
using System.Collections.Generic;

namespace BusinessLogic.Interfaces.Services
{
    public interface IFundMessageService
    {
        SearchResult<FundMessage> Search(SearchCriteria criteria);
        List<FundMessage> GetAll();
        FundMessage GetById(int id);
        IResult Create(FundMessage item);
        IResult Update(FundMessage item);
    }
}