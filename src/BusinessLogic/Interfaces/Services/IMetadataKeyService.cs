using BusinessLogic.Entities;
using BusinessLogic.Interfaces.Result;
using BusinessLogic.Models.MetadataKey;
using BusinessLogic.Models.Shared;
using System.Collections.Generic;

namespace BusinessLogic.Interfaces.Services
{
    public interface IMetadataKeyService
    {
        IResult Create(MetadataKey item);
        MetadataKey Get(int id);
        List<MetadataKey> GetAll();
        SearchResult<MetadataKey> Search(SearchCriteria criteria);
        IResult Update(MetadataKey item);
        IResult Delete(int id);
    }
}