using BusinessLogic.Entities;
using BusinessLogic.Models.VatMetadata;
using System.Collections.Generic;

namespace BusinessLogic.Interfaces.Repositories
{
    public interface IVatMetadataRepository : IRepository<VatMetadata>
    {
        IEnumerable<VatMetadata> Search(SearchCriteria criteria, out int resultCount);
        VatMetadata Get(int id);
        void Update(VatMetadata entity);
    }
}