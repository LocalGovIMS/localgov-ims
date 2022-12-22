using Admin.Models.EReturnTemplateRow;
using BusinessLogic.Interfaces.Services;
using log4net;

namespace Admin.Classes.Commands.EReturnTemplateRow
{
    public class EditCommand : BaseCommand<EditViewModel>
    {
        private readonly IEReturnTemplateRowService _service;

        public EditCommand(ILog log
            , IEReturnTemplateRowService service)
            : base(log)
        {
            _service = service;
        }

        protected override CommandResult OnExecute(EditViewModel model)
        {
            var item = new BusinessLogic.Entities.TemplateRow()
            {
                Id = model.Id,
                TemplateId = model.EReturnTemplateId,
                Reference = model.Reference,
                ReferenceOverride = model.ReferenceOverride,
                VatCode = model.VatCode,
                VatOverride = model.VatOverride,
                Description = model.Description,
                DescriptionOverride = model.DescriptionOverride
            };

            var result = _service.Update(item);

            return new CommandResult(result);
        }
    }
}