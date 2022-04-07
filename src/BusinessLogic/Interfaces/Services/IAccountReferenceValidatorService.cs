using BusinessLogic.Entities;
using System.Collections.Generic;

namespace BusinessLogic.Interfaces.Services
{
    public interface IAccountReferenceValidatorService
    {
        AccountReferenceValidator GetByFundCode(string fundCode);
        List<AccountReferenceValidator> GetAll();
    }
}