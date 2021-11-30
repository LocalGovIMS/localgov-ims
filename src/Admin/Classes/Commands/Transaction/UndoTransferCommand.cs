using BusinessLogic.Interfaces.Services;
using log4net;

namespace Admin.Classes.Commands.Transaction
{
    public class UndoTransferCommand : BaseCommand<string>
    {
        private readonly ITransactionJournalService _transactionJournalService;

        public UndoTransferCommand(ILog log
            , ITransactionJournalService transactionJournalService)
            : base(log)
        {
            _transactionJournalService = transactionJournalService;
        }

        protected override CommandResult OnExecute(string model)
        {
            var result = _transactionJournalService.UndoTransfer(model);

            return new CommandResult(result.Success, result.Errors);
        }
    }
}