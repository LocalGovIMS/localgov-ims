using BusinessLogic.Entities;

namespace BusinessLogic.Interfaces.Repositories
{
    public interface IEReturnCashRepository : IRepository<EReturnCash>
    {
        void Update(EReturnCash item, int eReturnId);
    }
}