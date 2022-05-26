using BusinessLogic.Entities;
using BusinessLogic.Interfaces.Result;
using BusinessLogic.Models;
using BusinessLogic.Models.FundMessageMetadata;
using BusinessLogic.Models.Shared;
using System.Collections.Generic;

namespace BusinessLogic.Interfaces.Services
{
    public interface IFundMessageMetadataService
    {
        IResult Create(FundMessageMetadata item);
        FundMessageMetadata Get(int id);
        SearchResult<FundMessageMetadata> Search(SearchCriteria criteria);
        IResult Update(FundMessageMetadata item);
        IResult Delete(int id);
        List<Metadata> GetMetadata();
    }
}