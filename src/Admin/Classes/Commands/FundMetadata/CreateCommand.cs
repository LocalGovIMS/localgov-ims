using Admin.Models.FundMetadata;
using BusinessLogic.Interfaces.Services;
using log4net;

namespace Admin.Classes.Commands.FundMetadata
{
    public class CreateCommand : BaseCommand<EditViewModel>
    {
        private readonly IFundMetadataService _service;

        public CreateCommand(ILog log
            , IFundMetadataService service)
            : base(log)
        {
            _service = service;
        }

        protected override CommandResult OnExecute(EditViewModel model)
        {
            var item = new BusinessLogic.Entities.FundMetadata()
            {
                FundCode = model.FundCode,
                MetadataKeyId = model.MetadataKeyId,
                Value = model.Value
            };

            var result = _service.Create(item);

            return new CommandResult(result);
        }
    }
}