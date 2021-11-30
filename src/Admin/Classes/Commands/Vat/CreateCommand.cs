using Admin.Models.Vat;
using BusinessLogic.Interfaces.Services;
using log4net;

namespace Admin.Classes.Commands.Vat
{
    public class CreateCommand : BaseCommand<EditViewModel>
    {
        private readonly IVatService _vatService;

        public CreateCommand(ILog log
            , IVatService vatService)
            : base(log)
        {
            _vatService = vatService;
        }

        protected override CommandResult OnExecute(EditViewModel model)
        {
            var item = new BusinessLogic.Entities.Vat()
            {
                VatCode = model.Code,
                Percentage = model.Percentage
            };

            var result = _vatService.Create(item);

            return new CommandResult(result);
        }
    }
}