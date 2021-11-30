using Admin.Models.Transaction;
using BusinessLogic.Interfaces.Services;
using log4net;

namespace Admin.Classes.Commands.Transaction
{
    public class EmailReceiptCommand : BaseCommand<EmailReceiptViewModel>
    {
        private readonly IEmailService _emailService;
        private readonly ITransactionService _transactionService;

        public EmailReceiptCommand(ILog log
            , IEmailService emailService
            , ITransactionService transactionService)
            : base(log)
        {
            _emailService = emailService;
            _transactionService = transactionService;
        }

        protected override CommandResult OnExecute(EmailReceiptViewModel model)
        {
            var transaction = _transactionService.GetTransactionByPspReference(model.PspReference);
            var result = _emailService.SendVatReceiptEmail(model.EmailAddress, transaction);

            if (result.Success)
            {
                _transactionService.ReceiptIssued(model.PspReference);
            }

            return new CommandResult(result.Success, result.Errors);
        }
    }
}