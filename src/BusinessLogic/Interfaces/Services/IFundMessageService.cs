using BusinessLogic.Entities;
using System.Collections.Generic;

namespace BusinessLogic.Interfaces.Services
{
    public interface IFundMessageService
    {
        List<FundMessage> GetAll();
        FundMessage GetById(int id);
    }
}