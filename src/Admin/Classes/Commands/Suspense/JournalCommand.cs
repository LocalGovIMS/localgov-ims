using Admin.Models.Suspense;
using BusinessLogic.Interfaces.Services;
using log4net;

namespace Admin.Classes.Commands.Suspense
{
    public class JournalCommand : BaseCommand<JournalViewModel>
    {
        private readonly ISuspenseService _suspenseService;

        public JournalCommand(ILog log
            , ISuspenseService suspenseService)
            : base(log)
        {
            _suspenseService = suspenseService;
        }

        protected override CommandResult OnExecute(JournalViewModel model)
        {
            var result = _suspenseService.Journal(model.SuspenseItems, model.JournalItems, model.CreditNotes);

            return new CommandResult(result);
        }
    }
}