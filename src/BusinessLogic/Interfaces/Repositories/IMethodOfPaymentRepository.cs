using BusinessLogic.Entities;
using BusinessLogic.Models.MethodOfPayment;
using System.Collections.Generic;

namespace BusinessLogic.Interfaces.Repositories
{
    public interface IMethodOfPaymentRepository : IRepository<Mop>
    {
        Mop GetMop(string id);
        List<Mop> Search(SearchCriteria criteria, out int resultCount);
        void Update(Mop entity);
        IEnumerable<Mop> GetAll(bool includeDisabled);
    }
}