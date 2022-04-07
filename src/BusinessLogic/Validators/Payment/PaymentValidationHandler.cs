using BusinessLogic.Classes.Result;
using BusinessLogic.Interfaces.Result;
using BusinessLogic.Interfaces.Security;
using BusinessLogic.Interfaces.Services;
using BusinessLogic.Interfaces.Validators;
using log4net;
using System;

namespace BusinessLogic.Validators.Payment
{
    public class PaymentValidationHandler : IPaymentValidationHandler
    {
        private readonly ILog _logger;
        private readonly IFundService _fundService;
        private readonly IAccountReferenceValidatorService _accountReferenceValidatorService;
        private readonly ISecurityContext _securityContext;
        private readonly Func<string, IValidator<PaymentValidationArgs>> _validatorFactory;

        private Entities.Fund _fund;
        private Entities.AccountReferenceValidator _accountReferenceValidator;

        private IValidator<PaymentValidationArgs> _validationChain;

        public PaymentValidationHandler(
            ILog logger,
            IFundService fundService,
            IAccountReferenceValidatorService accountReferenceValidatorService,
            ISecurityContext securityContext,
            Func<string, IValidator<PaymentValidationArgs>> validatorFactory
            )
        {
            _logger = logger;
            _fundService = fundService;
            _accountReferenceValidatorService = accountReferenceValidatorService;
            _securityContext = securityContext;
            _validatorFactory = validatorFactory;
        }

        public IResult Validate(PaymentValidationArgs args)
        {
            try
            {
                LoadData(args);

                UpdateArgs(args);

                CreateValidationChain();

                _validationChain.Validate(args);

                return new Result();
            }
            catch (PaymentValidationException ex)
            {
                _logger.Error(null, ex);

                return new Result(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.Error(null, ex);

                return new Result("The account reference is not valid");
            }
        }

        private void LoadData(PaymentValidationArgs args)
        {
            GetFund(args.FundCode);
            GetAccountReferenceValidator(args.FundCode);
        }

        private void GetFund(string fundCode)
        {
            _fund = _fundService.GetByFundCode(fundCode);

            if (_fund == null)
                throw new PaymentValidationException(string.Format("Fund is null: {0}", fundCode));
        }

        private void GetAccountReferenceValidator(string fundCode)
        {
            _accountReferenceValidator = _accountReferenceValidatorService.GetByFundCode(fundCode);

            if (_accountReferenceValidator == null)
                throw new PaymentValidationException(string.Format("AccountReferenceValidator is null for fund: {0}", fundCode));
        }

        private void UpdateArgs(PaymentValidationArgs args)
        {
            args.Fund = _fund;
            args.AccountReferenceValidator = _accountReferenceValidator;
        }

        private void CreateValidationChain()
        {
            var amountValidator = _validatorFactory(nameof(AmountValidator));

            amountValidator
                .SetNext(_validatorFactory(nameof(ReferenceLengthValidator)))
                .SetNext(_validatorFactory(nameof(CharacterTypeValidator)))
                .SetNext(_validatorFactory(nameof(RegexValidator)))
                .SetNext(_fund.AccountExist 
                            ? _validatorFactory(nameof(AccountExistsValidator)) 
                            : _validatorFactory(nameof(InputMaskValidator)));

            _validationChain = amountValidator; // AmountValidator is the first item in the chain we just created.
        }
    }
}
