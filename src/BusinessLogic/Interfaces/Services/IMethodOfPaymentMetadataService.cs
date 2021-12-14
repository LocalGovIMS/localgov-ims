using BusinessLogic.Entities;
using BusinessLogic.Interfaces.Result;
using BusinessLogic.Models;
using BusinessLogic.Models.MethodOfPaymentMetadata;
using BusinessLogic.Models.Shared;
using System.Collections.Generic;

namespace BusinessLogic.Interfaces.Services
{
    public interface IMethodOfPaymentMetadataService
    {
        IResult Create(MopMetaData item);
        MopMetaData Get(int id);
        SearchResult<MopMetaData> Search(SearchCriteria criteria);
        IResult Update(MopMetaData item);
        IResult Delete(int id);
        List<Metadata> GetMetadata();
    }
}