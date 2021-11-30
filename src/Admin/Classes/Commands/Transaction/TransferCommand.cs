using Admin.Models.Transaction;
using BusinessLogic.Interfaces.Services;
using log4net;

namespace Admin.Classes.Commands.Transaction
{
    public class TransferCommand : BaseCommand<TransferViewModel>
    {
        private readonly ITransactionJournalService _transactionJournalService;

        public TransferCommand(ILog log
            , ITransactionJournalService transactionJournalService)
            : base(log)
        {
            _transactionJournalService = transactionJournalService;
        }

        protected override CommandResult OnExecute(TransferViewModel model)
        {
            var result = _transactionJournalService.Transfer(model.TransferItems, model.PspReference, model.TransactionReference);
            return new CommandResult(result.Success, result.Errors);
        }
    }
}