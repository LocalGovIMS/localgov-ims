using Admin.Models.Fund;
using BusinessLogic.Interfaces.Services;
using log4net;

namespace Admin.Classes.Commands.Fund
{
    public class EditCommand : BaseCommand<EditViewModel>
    {
        private readonly IFundService _fundService;

        public EditCommand(ILog log
            , IFundService fundService)
            : base(log)
        {
            _fundService = fundService;
        }

        protected override CommandResult OnExecute(EditViewModel model)
        {
            var item = new BusinessLogic.Entities.Fund()
            {
                FundCode = model.FundCode,
                AccessLevel = model.AccessLevel,
                AccountExist = model.AccountExist,
                AquireAddress = model.AquireAddress,
                ExportToFund = model.ExportToFund,
                ExportToLedger = model.ExportToLedger,
                DisplayName = model.DisplayName,
                FundExportFormat = model.FundExportFormat,
                FundName = model.FundName,
                GeneralLedgerCode = model.GLCode,
                MaximumAmount = model.MaximumAmount,
                NarrativeFlag = model.Narrative,
                OverPayAccount = model.OverPayAccount,
                UseGeneralLedgerCode = model.UseGLCode,
                AccountReferenceValidatorId = model.AccountReferenceValidatorId,
                VatCode = model.VatCode,
                VatOverride = model.VatOverride,
                Disabled = model.IsDisabled
            };

            var result = _fundService.Update(item);

            return new CommandResult(result);
        }
    }
}