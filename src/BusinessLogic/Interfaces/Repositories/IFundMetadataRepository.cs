using BusinessLogic.Entities;
using BusinessLogic.Models.FundMetadata;
using System.Collections.Generic;

namespace BusinessLogic.Interfaces.Repositories
{
    public interface IFundMetadataRepository : IRepository<FundMetadata>
    {
        IEnumerable<FundMetadata> Search(SearchCriteria criteria, out int resultCount);
        FundMetadata Get(int id);
        FundMetadata Get(string fundCode, string key);
        void Update(FundMetadata entity);
    }
}