using Admin.Models.FundMessageMetadata;
using BusinessLogic.Interfaces.Services;
using log4net;

namespace Admin.Classes.Commands.FundMessageMetadata
{
    public class EditCommand : BaseCommand<EditViewModel>
    {
        private readonly IFundMessageMetadataService _service;

        public EditCommand(ILog log
            , IFundMessageMetadataService service)
            : base(log)
        {
            _service = service;
        }

        protected override CommandResult OnExecute(EditViewModel model)
        {
            var item = new BusinessLogic.Entities.FundMessageMetadata()
            {
                Id = model.Id,
                FundMessageId = model.FundMessageId,
                Key = model.Key,
                Value = model.Value
            };

            var result = _service.Update(item);

            return new CommandResult(result);
        }
    }
}