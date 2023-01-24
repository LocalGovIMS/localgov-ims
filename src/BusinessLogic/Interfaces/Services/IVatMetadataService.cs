using BusinessLogic.Entities;
using BusinessLogic.Interfaces.Result;
using BusinessLogic.Models.Shared;
using BusinessLogic.Models.VatMetadata;

namespace BusinessLogic.Interfaces.Services
{
    public interface IVatMetadataService
    {
        IResult Create(VatMetadata item);
        VatMetadata Get(int id);
        SearchResult<VatMetadata> Search(SearchCriteria criteria);
        IResult Update(VatMetadata item);
        IResult Delete(int id);
    }
}