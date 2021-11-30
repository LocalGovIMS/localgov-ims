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
                AccessLevel = data.AccessLevel,
                AccountExist = data.AccountExist,
                AquireAddress = data.AquireAddress,
                ExportToFund = data.ExportToFund,
                ExportToLedger = data.ExportToLedger,
                DisplayName = data.DisplayName,
                FundExportFormat = data.FundExportFormat,
                FundName = data.FundName,
                GLCode = data.GeneralLedgerCode,
                MaximumAmount = data.MaximumAmount,
                Narrative = data.NarrativeFlag,
                OverPayAccount = data.OverPayAccount,
                UseGLCode = data.UseGeneralLedgerCode,
                ValidationReference = data.ValidationReference,
                FundCode = data.FundCode,
                VatCode = data.VatCode,
                VatOverride = data.VatOverride,
                IsDisabled = data.Disabled
            };
        }
    }
}