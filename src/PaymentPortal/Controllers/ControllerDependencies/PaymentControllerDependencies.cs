using BusinessLogic.Interfaces.Services;
using BusinessLogic.Interfaces.Services.Cryptography;
using log4net;
using System;

namespace PaymentPortal.Controllers
{
    public class PaymentControllerDependencies : BaseControllerDependencies, IPaymentControllerDependencies
    {
        public PaymentControllerDependencies(ILog log
            , IPaymentService paymentService
            , IFundService fundService
            , IValidationService validationService
            , IEmailService emailService
            , ITransactionService transactionService
            , ICryptographyService cryptographyService)
            : base(log)
        {
            PaymentService = paymentService ?? throw new ArgumentNullException("paymentService");
            FundService = fundService ?? throw new ArgumentNullException("fundService");
            ValidationService = validationService ?? throw new ArgumentNullException("validationService");
            EmailService = emailService ?? throw new ArgumentNullException("emailService");
            CryptographyService = cryptographyService ?? throw new ArgumentNullException("cryptographyService");
            TransactionService = transactionService ?? throw new ArgumentNullException("transactionService");
        }

        public IPaymentService PaymentService { get; private set; }
        public IFundService FundService { get; private set; }
        public IValidationService ValidationService { get; private set; }
        public IEmailService EmailService { get; private set; }
        public ICryptographyService CryptographyService { get; private set; }
        public ITransactionService TransactionService { get; private set; }
    }
}