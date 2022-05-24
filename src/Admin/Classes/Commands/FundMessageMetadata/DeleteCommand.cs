using BusinessLogic.Interfaces.Services;
using log4net;

namespace Admin.Classes.Commands.FundMessageMetadata
{
    public class DeleteCommand : BaseCommand<int>
    {
        private readonly IFundMessageMetadataService _service;

        public DeleteCommand(ILog log
            , IFundMessageMetadataService service)
            : base(log)
        {
            _service = service;
        }

        protected override CommandResult OnExecute(int id)
        {
            var result = _service.Delete(id);

            return new CommandResult(result);
        }
    }
}