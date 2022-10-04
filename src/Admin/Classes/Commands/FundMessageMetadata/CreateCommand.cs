using Admin.Models.FundMessageMetadata;
using BusinessLogic.Interfaces.Services;
using log4net;

namespace Admin.Classes.Commands.FundMessageMetadata
{
    public class CreateCommand : BaseCommand<EditViewModel>
    {
        private readonly IFundMessageMetadataService _service;

        public CreateCommand(ILog log
            , IFundMessageMetadataService service)
            : base(log)
        {
            _service = service;
        }

        protected override CommandResult OnExecute(EditViewModel model)
        {
            var item = new BusinessLogic.Entities.FundMessageMetadata()
            {
                FundMessageId = model.FundMessageId,
                MetadataKeyId = model.MetadataKeyId,                
                Value = model.Value
            };

            var result = _service.Create(item);

            return new CommandResult(result);
        }
    }
}