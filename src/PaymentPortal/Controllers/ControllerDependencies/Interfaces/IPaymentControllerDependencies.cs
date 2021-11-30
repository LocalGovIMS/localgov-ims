using BusinessLogic.Interfaces.Services;
using BusinessLogic.Interfaces.Services.Cryptography;

namespace PaymentPortal.Controllers
{
    public interface IPaymentControllerDependencies : IBaseControllerDependencies
    {
        IPaymentService PaymentService { get; }
        IFundService FundService { get; }
        IValidationService ValidationService { get; }
        IEmailService EmailService { get; }
        ITransactionService TransactionService { get; }
        ICryptographyService CryptographyService { get; }
    }
}
