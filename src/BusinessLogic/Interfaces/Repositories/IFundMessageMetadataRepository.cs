using BusinessLogic.Entities;
using BusinessLogic.Models.FundMessageMetadata;
using System.Collections.Generic;

namespace BusinessLogic.Interfaces.Repositories
{
    public interface IFundMessageMetadataRepository : IRepository<FundMessageMetadata>
    {
        IEnumerable<FundMessageMetadata> Search(SearchCriteria criteria, out int resultCount);
        FundMessageMetadata Get(int id);
        void Update(FundMessageMetadata entity);
    }
}