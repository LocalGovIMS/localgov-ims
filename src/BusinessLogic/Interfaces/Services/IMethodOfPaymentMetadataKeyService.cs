using BusinessLogic.Entities;
using BusinessLogic.Interfaces.Result;
using BusinessLogic.Models.MethodOfPaymentMetadataKey;
using BusinessLogic.Models.Shared;
using System.Collections.Generic;

namespace BusinessLogic.Interfaces.Services
{
    public interface IMethodOfPaymentMetadataKeyService
    {
        IResult Create(MopMetadataKey item);
        MopMetadataKey Get(int id);
        List<MopMetadataKey> GetAll();
        SearchResult<MopMetadataKey> Search(SearchCriteria criteria);
        IResult Update(MopMetadataKey item);
        IResult Delete(int id);
    }
}