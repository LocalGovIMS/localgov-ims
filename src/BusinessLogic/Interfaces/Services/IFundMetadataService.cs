using BusinessLogic.Entities;
using BusinessLogic.Interfaces.Result;
using BusinessLogic.Models;
using BusinessLogic.Models.FundMetadata;
using BusinessLogic.Models.Shared;
using System.Collections.Generic;

namespace BusinessLogic.Interfaces.Services
{
    public interface IFundMetadataService
    {
        IResult Create(FundMetaData item);
        FundMetaData Get(int id);
        SearchResult<FundMetaData> Search(SearchCriteria criteria);
        IResult Update(FundMetaData item);
        IResult Delete(int id);
        List<Metadata> GetMetadata();
    }
}