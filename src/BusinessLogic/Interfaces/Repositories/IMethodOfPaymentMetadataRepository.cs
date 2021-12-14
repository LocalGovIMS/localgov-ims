using BusinessLogic.Entities;
using BusinessLogic.Models.MethodOfPaymentMetadata;
using System.Collections.Generic;

namespace BusinessLogic.Interfaces.Repositories
{
    public interface IMethodOfPaymentMetadataRepository : IRepository<MopMetaData>
    {
        IEnumerable<MopMetaData> Search(SearchCriteria criteria, out int resultCount);
        MopMetaData Get(int id);
        void Update(MopMetaData entity);
    }
}