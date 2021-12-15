using BusinessLogic.Interfaces.Services;
using log4net;

namespace Admin.Classes.Commands.VatMetadata
{
    public class DeleteCommand : BaseCommand<int>
    {
        private readonly IVatMetadataService _service;

        public DeleteCommand(ILog log
            , IVatMetadataService service)
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