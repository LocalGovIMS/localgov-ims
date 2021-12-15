using BusinessLogic.Entities;
using BusinessLogic.Models.VatMetadata;
using System.Collections.Generic;

namespace BusinessLogic.Interfaces.Repositories
{
    public interface IVatMetadataRepository : IRepository<VatMetaData>
    {
        IEnumerable<VatMetaData> Search(SearchCriteria criteria, out int resultCount);
        VatMetaData Get(int id);
        void Update(VatMetaData entity);
    }
}