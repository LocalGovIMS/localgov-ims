using BusinessLogic.Entities;
using BusinessLogic.Models.MethodOfPaymentMetadataKey;
using System.Collections.Generic;

namespace BusinessLogic.Interfaces.Repositories
{
    public interface IMethodOfPaymentMetadataKeyRepository : IRepository<MopMetadataKey>
    {
        IEnumerable<MopMetadataKey> Search(SearchCriteria criteria, out int resultCount);
        MopMetadataKey Get(int id);
        void Update(MopMetadataKey entity);
    }
}