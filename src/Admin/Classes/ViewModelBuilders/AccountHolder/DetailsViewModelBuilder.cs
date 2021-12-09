using Admin.Models.AccountHolder;
using BusinessLogic.Extensions;
using BusinessLogic.Interfaces.Services;
using log4net;
using System;

namespace Admin.Classes.ViewModelBuilders.AccountHolder
{
    public class DetailsViewModelBuilder : BaseViewModelBuilder<DetailsViewModel, DetailsViewModelBuilderArgs>
    {
        private readonly IAccountHolderService _accountHolderService;

        public DetailsViewModelBuilder(ILog log
            , IAccountHolderService accountHolderService)
            : base(log)
        {
            _accountHolderService = accountHolderService;
        }

        protected override DetailsViewModel OnBuild()
        {
            throw new NotImplementedException();
        }

        protected override DetailsViewModel OnBuild(DetailsViewModelBuilderArgs args)
        {
            var item = _accountHolderService.GetByAccountReference(args.AccountReference);

            return new DetailsViewModel()
            {
                AccountReference = item.AccountReference,
                FundCode = item.FundCode,
                FundName = item.Fund?.FundName,
                CurrentBalance = item.CurrentBalance,
                PeriodDebit = item.PeriodDebit,
                Title = item.Title,
                Forename = item.Forename,
                Surname = item.Surname,
                FullNameAndTitle = item.FullNameAndTitle(),
                AddressLine1 = item.AddressLine1,
                AddressLine2 = item.AddressLine2,
                AddressLine3 = item.AddressLine3,
                AddressLine4 = item.AddressLine4,
                Address = item.Address(),
                Postcode = item.Postcode,
                PeriodCredit = item.PeriodCredit,
                RecordType = item.RecordType,
                UserField1 = item.UserField1,
                UserField2 = item.UserField2,
                UserField3 = item.UserField3,
                StopMessageReference = item.StopMessageReference,
                StopMessage = item.StopMessage?.Message,
                LastUpdated = item.LastUpdated,
                ShowSelect = args.ShowSelect
            };
        }
    }

    public class DetailsViewModelBuilderArgs
    {
        public string AccountReference { get; set; }
        public bool ShowSelect { get; set; } = false;
    }
}