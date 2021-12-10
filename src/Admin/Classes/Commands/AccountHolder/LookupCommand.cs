using Admin.Models.AccountHolder;
using BusinessLogic.Interfaces.Services;
using log4net;
using System;

namespace Admin.Classes.Commands.AccountHolder
{
    public class LookupCommand : BaseCommand<LookupViewModel>
    {
        private readonly IAccountHolderService _accountHolderService;

        public LookupCommand(ILog log,
            IAccountHolderService accountHolderService)
            : base(log)
        {
            _accountHolderService = accountHolderService ?? throw new ArgumentNullException("accountHolderService");
        }

        protected override CommandResult OnExecute(LookupViewModel model)
        {
            var result = new CommandResult(true);
            var accountHolder = _accountHolderService.GetByAccountReference(model.AccountReference, model.FundCode);

            if (accountHolder != null)
            {
                result.Data = new
                {
                    name = accountHolder.Forename + " " + accountHolder.Surname,
                    outstandingBalance = accountHolder.CurrentBalance
                };
            }
            else
            {
                result.Data = new
                {
                    name = string.Empty,
                    outstandingBalance = 0
                };
            }

            return result;   
        }
    }
}