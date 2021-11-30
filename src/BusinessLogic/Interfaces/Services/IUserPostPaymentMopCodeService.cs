using BusinessLogic.Entities;
using System.Collections.Generic;

namespace BusinessLogic.Interfaces.Services
{
    public interface IUserPostPaymentMopCodeService
    {
        List<UserPostPaymentMopCode> GetUserPostPaymentMopCodes(int id);
        Mop GetDefaultUserPostPaymentMopCode();
    }
}