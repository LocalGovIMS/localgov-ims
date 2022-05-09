using Admin.Models.Fund;
using BusinessLogic.Interfaces.Services;
using log4net;
using System;

namespace Admin.Classes.ViewModelBuilders.Fund
{
    public class DetailsViewModelBuilder : BaseViewModelBuilder<DetailsViewModel, string>
    {
        private readonly IFundService _fundService;

        public DetailsViewModelBuilder(ILog log
            , IFundService fundService)
            : base(log)
        {
            _fundService = fundService;
        }

        protected override DetailsViewModel OnBuild()
        {
            throw new NotImplementedException();
        }

        protected override DetailsViewModel OnBuild(string id)
        {
            var data = _fundService.GetByFundCode(id);

            return new DetailsViewModel()
            {
                AccountExist = data.AccountExist,
                AquireAddress = data.AquireAddress,
                DisplayName = data.DisplayName,
                FundName = data.FundName,
                MaximumAmount = data.MaximumAmount,
                OverPayAccount = data.OverPayAccount,
                AccountReferenceValidatorId = data.AccountReferenceValidatorId,
                AccountReferenceValidatorName = data.AccountReferenceValidator?.Name,
                FundCode = data.FundCode,
                VatCode = data.VatCode,
                VatOverride = data.VatOverride,
                IsDisabled = data.Disabled
            };
        }
    }
}