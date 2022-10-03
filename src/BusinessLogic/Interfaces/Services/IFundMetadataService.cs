using BusinessLogic.Entities;
using BusinessLogic.Interfaces.Result;
using BusinessLogic.Models.FundMetadata;
using BusinessLogic.Models.Shared;

namespace BusinessLogic.Interfaces.Services
{
    public interface IFundMetadataService
    {
        IResult Create(FundMetadata item);
        FundMetadata Get(int id);
        FundMetadata Get(string fundCode, string key);
        SearchResult<FundMetadata> Search(SearchCriteria criteria);
        IResult Update(FundMetadata item);
        IResult Delete(int id);
    }
}