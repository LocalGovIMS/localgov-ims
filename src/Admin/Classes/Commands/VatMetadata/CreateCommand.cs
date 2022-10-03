using Admin.Models.VatMetadata;
using BusinessLogic.Interfaces.Services;
using log4net;

namespace Admin.Classes.Commands.VatMetadata
{
    public class CreateCommand : BaseCommand<EditViewModel>
    {
        private readonly IVatMetadataService _service;

        public CreateCommand(ILog log
            , IVatMetadataService service)
            : base(log)
        {
            _service = service;
        }

        protected override CommandResult OnExecute(EditViewModel model)
        {
            var item = new BusinessLogic.Entities.VatMetadata()
            {
                VatCode = model.VatCode,
                MetadataKeyId = model.MetadataKeyId,
                Value = model.Value
            };

            var result = _service.Create(item);

            return new CommandResult(result);
        }
    }
}