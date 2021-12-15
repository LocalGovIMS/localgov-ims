using BusinessLogic.Entities;
using BusinessLogic.Models.FundMetadata;
using System.Collections.Generic;

namespace BusinessLogic.Interfaces.Repositories
{
    public interface IFundMetadataRepository : IRepository<FundMetaData>
    {
        IEnumerable<FundMetaData> Search(SearchCriteria criteria, out int resultCount);
        FundMetaData Get(int id);
        void Update(FundMetaData entity);
    }
}