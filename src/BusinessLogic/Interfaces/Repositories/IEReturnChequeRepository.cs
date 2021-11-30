using BusinessLogic.Entities;
using System.Collections.Generic;

namespace BusinessLogic.Interfaces.Repositories
{
    public interface IEReturnChequeRepository : IRepository<EReturnCheque>
    {
        void Update(List<EReturnCheque> items, int eReturnId);
    }
}