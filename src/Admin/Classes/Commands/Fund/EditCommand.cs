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
                AccountExist = model.AccountExist,
                AquireAddress = model.AquireAddress,
                DisplayName = model.DisplayName,
                FundName = model.FundName,
                MaximumAmount = model.MaximumAmount,
                OverPayAccount = model.OverPayAccount,
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