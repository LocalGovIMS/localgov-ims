using BusinessLogic.Entities;
using BusinessLogic.Interfaces.Result;
using BusinessLogic.Models;
using BusinessLogic.Models.VatMetadata;
using BusinessLogic.Models.Shared;
using System.Collections.Generic;

namespace BusinessLogic.Interfaces.Services
{
    public interface IVatMetadataService
    {
        IResult Create(VatMetadata item);
        VatMetadata Get(int id);
        SearchResult<VatMetadata> Search(SearchCriteria criteria);
        IResult Update(VatMetadata item);
        IResult Delete(int id);
        List<Metadata> GetMetadata();
    }
}