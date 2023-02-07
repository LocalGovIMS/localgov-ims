using Admin.Models.AccountHolder;
using BusinessLogic.Interfaces.Services;
using log4net;
using System;

namespace Admin.Classes.Commands.AccountHolder
{
    public class CreateCommand : BaseCommand<EditViewModel>
    {
        private readonly IAccountHolderService _accountHolderService;

        public CreateCommand(ILog log
            , IAccountHolderService accountHolderService)
            : base(log)
        {
            _accountHolderService = accountHolderService ?? throw new ArgumentNullException("accountHolderService");
        }

        protected override CommandResult OnExecute(EditViewModel model)
        {
            var item = new BusinessLogic.Entities.AccountHolder()
            {
                AccountReference = model.AccountReference,
                AddressLine1 = model.AddressLine1,
                AddressLine2 = model.AddressLine2,
                AddressLine3 = model.AddressLine3,
                AddressLine4 = model.AddressLine4,
                CurrentBalance = model.CurrentBalance,
                Forename = model.Forename,
                FundCode = model.FundCode,
                PeriodCredit = model.PeriodCredit,
                PeriodDebit = model.PeriodDebit,
                Postcode = model.Postcode,
                RecordType = model.RecordType,
                FundMessageId = model.FundMessageId,
                Surname = model.Surname,
                Title = model.Title,
                UserField1 = model.UserField1,
                UserField2 = model.UserField2,
                UserField3 = model.UserField3
            };

            var result = _accountHolderService.Create(item);

            return new CommandResult(result);
        }
    }
}