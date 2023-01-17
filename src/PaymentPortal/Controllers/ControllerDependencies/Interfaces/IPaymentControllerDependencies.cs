using BusinessLogic.Interfaces.Services;
using BusinessLogic.Interfaces.Services.Cryptography;
using BusinessLogic.Interfaces.Validators;

namespace PaymentPortal.Controllers
{
    public interface IPaymentControllerDependencies : IBaseControllerDependencies
    {
        IPaymentService PaymentService { get; }
        IFundService FundService { get; }
        IMethodOfPaymentService MethodOfPaymentService { get; }
        IEmailService EmailService { get; }
        ITransactionService TransactionService { get; }
        ICryptographyService CryptographyService { get; }
        IPaymentValidationHandler PaymentValidationHandler { get; }
    }
}
