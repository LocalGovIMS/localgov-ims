using Admin.Models.Vat;
using BusinessLogic.Interfaces.Services;
using log4net;

namespace Admin.Classes.Commands.Vat
{
    public class EditCommand : BaseCommand<EditViewModel>
    {
        private readonly IVatService _vatService;

        public EditCommand(ILog log
            , IVatService vatService)
            : base(log)
        {
            _vatService = vatService;
        }

        protected override CommandResult OnExecute(EditViewModel model)
        {
            var item = new BusinessLogic.Entities.Vat
            {
                VatCode = model.Code,
                Percentage = model.Percentage,
                Disabled = model.IsDisabled
            };

            var result = _vatService.Update(item);

            return new CommandResult(result);
        }
    }
}