using Admin.Models.Transfer;
using BusinessLogic.Interfaces.Services;
using log4net;

namespace Admin.Classes.Commands.Transfer
{
    public class TransferCommand : BaseCommand<TransferViewModel>
    {
        private readonly ITransferService _transferService;

        public TransferCommand(ILog log
            , ITransferService transferService)
            : base(log)
        {
            _transferService = transferService;
        }

        protected override CommandResult OnExecute(TransferViewModel model)
        {
            var result = _transferService.Transfer(model.TransferItems, model.SourceItems);

            return new CommandResult(result);
        }
    }
}