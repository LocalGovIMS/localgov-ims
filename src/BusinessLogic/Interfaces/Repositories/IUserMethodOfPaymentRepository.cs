using BusinessLogic.Entities;
using System.Collections.Generic;

namespace BusinessLogic.Interfaces.Repositories
{
    public interface IUserMethodOfPaymentRepository : IRepository<UserMethodOfPayment>
    {
        List<UserMethodOfPayment> GetByUserId(int id);
        void Update(List<UserMethodOfPayment> userMopCodes, int userId);
    }
}