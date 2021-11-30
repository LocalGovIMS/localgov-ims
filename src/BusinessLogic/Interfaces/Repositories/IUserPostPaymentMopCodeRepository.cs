using BusinessLogic.Entities;
using System.Collections.Generic;

namespace BusinessLogic.Interfaces.Repositories
{
    public interface IUserPostPaymentMopCodeRepository : IRepository<UserPostPaymentMopCode>
    {
        List<UserPostPaymentMopCode> GetUserPostPaymentMopCodes(int id);
    }
}