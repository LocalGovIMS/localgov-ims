using Admin.Models.VatMetadata;
using BusinessLogic.Interfaces.Services;
using log4net;

namespace Admin.Classes.Commands.VatMetadata
{
    public class EditCommand : BaseCommand<EditViewModel>
    {
        private readonly IVatMetadataService _service;

        public EditCommand(ILog log
            , IVatMetadataService service)
            : base(log)
        {
            _service = service;
        }

        protected override CommandResult OnExecute(EditViewModel model)
        {
            var item = new BusinessLogic.Entities.VatMetadata()
            {
                Id = model.Id,
                VatCode = model.VatCode,
                MetadataKeyId = model.MetadataKeyId,
                Value = model.Value
            };

            var result = _service.Update(item);

            return new CommandResult(result);
        }
    }
}