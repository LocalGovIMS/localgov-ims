using BusinessLogic.Entities;
using BusinessLogic.Interfaces.Result;
using System.Collections.Generic;

namespace BusinessLogic.Interfaces.Services
{
    public interface IUserMethodOfPaymentService
    {
        List<UserMethodOfPayment> GetByUserId(int id);
        Mop GetDefaultUserMethodOfPayment();
        IResult Update(List<UserMethodOfPayment> userMopCodes, int userId);
    }
}