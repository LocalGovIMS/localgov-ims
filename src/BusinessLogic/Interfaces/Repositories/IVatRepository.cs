using BusinessLogic.Entities;
using System.Collections.Generic;

namespace BusinessLogic.Interfaces.Repositories
{
    public interface IVatRepository : IRepository<Vat>
    {
        Vat GetVatByVatCode(string vatCode);
        void Update(Vat entity);
        IEnumerable<Vat> GetAll(bool includeDisabled);
    }
}