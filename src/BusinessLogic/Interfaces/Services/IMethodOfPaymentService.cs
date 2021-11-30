using BusinessLogic.Entities;
using BusinessLogic.Interfaces.Result;
using BusinessLogic.Models.MethodOfPayment;
using BusinessLogic.Models.Shared;
using System.Collections.Generic;

namespace BusinessLogic.Interfaces.Services
{
    public interface IMethodOfPaymentService
    {
        List<Mop> GetAllMops();
        List<Mop> GetAllMops(bool includeDisabled);
        Mop GetMop(string id);
        SearchResult<Mop> Search(SearchCriteria criteria);
        IResult Create(Mop item);
        IResult Update(Mop item);
    }
}