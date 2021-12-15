using Admin.Models.FundMetadata;
using BusinessLogic.Interfaces.Services;
using log4net;

namespace Admin.Classes.Commands.FundMetadata
{
    public class EditCommand : BaseCommand<EditViewModel>
    {
        private readonly IFundMetadataService _service;

        public EditCommand(ILog log
            , IFundMetadataService service)
            : base(log)
        {
            _service = service;
        }

        protected override CommandResult OnExecute(EditViewModel model)
        {
            var item = new BusinessLogic.Entities.FundMetaData()
            {
                Id = model.Id,
                FundCode = model.FundCode,
                Key = model.Key,
                Value = model.Value
            };

            var result = _service.Update(item);

            return new CommandResult(result);
        }
    }
}