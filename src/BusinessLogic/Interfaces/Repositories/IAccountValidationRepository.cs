using BusinessLogic.Entities;

namespace BusinessLogic.Interfaces.Repositories
{
    public interface IAccountValidationRepository : IRepository<AccountValidation>
    {
        AccountValidation GetAccountValidation(string validationReference);
    }
}