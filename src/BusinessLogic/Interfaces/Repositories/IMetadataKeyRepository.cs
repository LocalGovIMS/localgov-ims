using BusinessLogic.Entities;
using BusinessLogic.Models.MetadataKey;
using System.Collections.Generic;

namespace BusinessLogic.Interfaces.Repositories
{
    public interface IMetadataKeyRepository : IRepository<MetadataKey>
    {
        IEnumerable<MetadataKey> Search(SearchCriteria criteria, out int resultCount);
        MetadataKey Get(int id);
        void Update(MetadataKey entity);
    }
}