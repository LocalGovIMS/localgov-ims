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
        IResult Create(MopMetadata item);
        MopMetadata Get(int id);
        SearchResult<MopMetadata> Search(SearchCriteria criteria);
        IResult Update(MopMetadata item);
        IResult Delete(int id);
        List<Metadata> GetMetadata();
    }
}