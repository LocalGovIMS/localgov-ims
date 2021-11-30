using Admin.Models.Template;
using BusinessLogic.Interfaces.Services;
using log4net;

namespace Admin.Classes.Commands.Template
{
    public class CreateCommand : BaseCommand<EditViewModel>
    {
        private readonly ITemplateService _templateService;

        public CreateCommand(ILog log
            , ITemplateService templateService)
            : base(log)
        {
            _templateService = templateService;
        }

        protected override CommandResult OnExecute(EditViewModel model)
        {
            var item = new BusinessLogic.Entities.Template()
            {
                Name = model.Name,
                Cheque = model.AllowCheque,
                Cash = model.AllowCash,
                Pdq = model.AllowPdq
            };

            _templateService.Create(item);

            return new CommandResult(true);
        }
    }
}