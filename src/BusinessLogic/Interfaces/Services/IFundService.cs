using BusinessLogic.Entities;
using BusinessLogic.Interfaces.Result;
using BusinessLogic.Models.Fund;
using BusinessLogic.Models.Shared;
using System.Collections.Generic;

namespace BusinessLogic.Interfaces.Services
{
    public interface IFundService
    {
        List<Fund> GetAllFunds();
        List<Fund> GetAllFunds(bool includeDisabled);
        Fund GetByFundCode(string fundCode);
        List<Fund> GetCreditNoteFunds();
        List<Fund> GetBasketFunds();
        IResult Create(Fund item);
        IResult Update(Fund item);
        SearchResult<Fund> Search(SearchCriteria criteria);
    }
}