using BusinessLogic.Interfaces.Services;
using BusinessLogic.Interfaces.Services.Cryptography;
using BusinessLogic.Interfaces.Validators;
using log4net;
using System;

namespace PaymentPortal.Controllers
{
    public class PaymentControllerDependencies : BaseControllerDependencies, IPaymentControllerDependencies
    {
        public PaymentControllerDependencies(ILog log
            , IPaymentService paymentService
            , IFundService fundService
            , IEmailService emailService
            , ITransactionService transactionService
            , ICryptographyService cryptographyService
            , IPaymentValidationHandler paymentValidationHandler)
            : base(log)
        {
            PaymentService = paymentService ?? throw new ArgumentNullException("paymentService");
            FundService = fundService ?? throw new ArgumentNullException("fundService");
            EmailService = emailService ?? throw new ArgumentNullException("emailService");
            CryptographyService = cryptographyService ?? throw new ArgumentNullException("cryptographyService");
            TransactionService = transactionService ?? throw new ArgumentNullException("transactionService");
            PaymentValidationHandler = paymentValidationHandler ?? throw new ArgumentNullException("paymentValidationHandler");
        }

        public IPaymentService PaymentService { get; private set; }
        public IFundService FundService { get; private set; }
        public IEmailService EmailService { get; private set; }
        public ICryptographyService CryptographyService { get; private set; }
        public ITransactionService TransactionService { get; private set; }
        public IPaymentValidationHandler PaymentValidationHandler { get; private set; }
    }
}