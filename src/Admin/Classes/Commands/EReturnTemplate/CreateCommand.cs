using Admin.Models.EReturnTemplate;
using BusinessLogic.Interfaces.Services;
using log4net;

namespace Admin.Classes.Commands.EReturnTemplate
{
    public class CreateCommand : BaseCommand<EditViewModel>
    {
        private readonly IEReturnTemplateService _service;

        public CreateCommand(ILog log
            , IEReturnTemplateService service)
            : base(log)
        {
            _service = service;
        }

        protected override CommandResult OnExecute(EditViewModel model)
        {
            var item = new BusinessLogic.Entities.Template()
            {
                Name = model.Name,
                Cash = model.AllowCash,
                Cheque = model.AllowCheque,
                Pdq = model.AllowPdq,
            };

            var result = _service.Create(item);

            return new CommandResult(result);
        }
    }
}