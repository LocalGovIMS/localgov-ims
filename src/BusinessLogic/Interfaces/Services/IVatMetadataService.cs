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
        IResult Create(VatMetaData item);
        VatMetaData Get(int id);
        SearchResult<VatMetaData> Search(SearchCriteria criteria);
        IResult Update(VatMetaData item);
        IResult Delete(int id);
        List<Metadata> GetMetadata();
    }
}