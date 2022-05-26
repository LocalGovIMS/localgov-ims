using BusinessLogic.Entities;
using BusinessLogic.Models.MethodOfPaymentMetadata;
using System.Collections.Generic;

namespace BusinessLogic.Interfaces.Repositories
{
    public interface IMethodOfPaymentMetadataRepository : IRepository<MopMetadata>
    {
        IEnumerable<MopMetadata> Search(SearchCriteria criteria, out int resultCount);
        MopMetadata Get(int id);
        void Update(MopMetadata entity);
    }
}