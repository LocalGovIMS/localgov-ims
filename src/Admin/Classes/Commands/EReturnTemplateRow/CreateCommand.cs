using Admin.Models.EReturnTemplateRow;
using BusinessLogic.Interfaces.Services;
using log4net;

namespace Admin.Classes.Commands.EReturnTemplateRow
{
    public class CreateCommand : BaseCommand<EditViewModel>
    {
        private readonly IEReturnTemplateRowService _service;

        public CreateCommand(ILog log
            , IEReturnTemplateRowService service)
            : base(log)
        {
            _service = service;
        }

        protected override CommandResult OnExecute(EditViewModel model)
        {
            var item = new BusinessLogic.Entities.TemplateRow()
            {
                TemplateId = model.EReturnTemplateId,
                Reference = model.Reference,
                ReferenceOverride = model.ReferenceOverride,
                VatCode = model.VatCode,
                VatOverride = model.VatOverride,
                Description = model.Description,
                DescriptionOverride = model.DescriptionOverride
            };

            var result = _service.Create(item);

            return new CommandResult(result);
        }
    }
}