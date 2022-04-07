using BusinessLogic.Entities;

namespace BusinessLogic.Interfaces.Repositories
{
    public interface IAccountReferenceValidatorRepository : IRepository<AccountReferenceValidator>
    {
        AccountReferenceValidator GetByFundCode(string fundCode);
    }
}