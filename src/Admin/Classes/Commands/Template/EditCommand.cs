using Admin.Models.Template;
using BusinessLogic.Interfaces.Services;
using log4net;

namespace Admin.Classes.Commands.Template
{
    public class EditCommand : BaseCommand<EditViewModel>
    {
        private readonly ITemplateService _templateService;

        public EditCommand(ILog log
            , ITemplateService templateService)
            : base(log)
        {
            _templateService = templateService;
        }

        protected override CommandResult OnExecute(EditViewModel model)
        {
            var item = new BusinessLogic.Entities.Template()
            {
                Id = model.Id,
                Name = model.Name,
                Cash = model.AllowCash,
                Cheque = model.AllowCheque,
                Pdq = model.AllowPdq,
                TemplateRows = model.TemplateRows
            };

            return new CommandResult(_templateService.Update(item));
        }
    }
}