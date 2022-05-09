using BusinessLogic.Extensions;
using BusinessLogic.Interfaces.Security;
using BusinessLogic.Interfaces.Services;
using log4net;
using System;

namespace BusinessLogic.Validators.Payment
{
    public class AccountExistsValidator : AbstractValidator
    {
        private readonly ILog _logger;
        private readonly ISecurityContext _securityContext;
        private readonly IAccountHolderService _accountHolderService;

        private Entities.AccountHolder _accountHolder;

        public AccountExistsValidator(
            ILog logger,
            ISecurityContext securityContext,
            IAccountHolderService accountHolderService)
        {
            _logger = logger;
            _securityContext = securityContext;
            _accountHolderService = accountHolderService;
        }

        protected override void OnValidate(PaymentValidationArgs args)
        {
            try
            {
                GetAccountHolder(args);

                CheckAccountHolderExists();

                CheckIfPaymentsAreAllowed();
            }
            catch(PaymentValidationException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                _logger.Error(null, ex);

                throw new PaymentValidationException();
            }
        }

        private void GetAccountHolder(PaymentValidationArgs args)
        {
            _accountHolder = _accountHolderService.GetByAccountReference(args.Reference, args.Fund.FundCode);
        }

        private void CheckAccountHolderExists()
        {
            if (_accountHolder == null)
                throw new PaymentValidationException("No account for the specified reference exists");
        }

        private void CheckIfPaymentsAreAllowed()
        {
            // Finance can post/pay to stopped accounts, so payments are allowed whatever the 'stop' status
            if (_securityContext.IsInRole(Security.Role.Finance)) 
                return;

            if (_accountHolder.IsOnStop()) 
                throw new PaymentValidationException("This account has been stopped");
        }
    }
}
