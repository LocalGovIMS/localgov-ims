using Admin.Models.Suspense;
using BusinessLogic.Interfaces.Services;
using log4net;

namespace Admin.Classes.Commands.Suspense
{
    public class SaveNoteCommand : BaseCommand<SaveNoteViewModel>
    {
        private readonly ISuspenseService _suspenseService;

        public SaveNoteCommand(ILog log
            , ISuspenseService suspenseService)
            : base(log)
        {
            _suspenseService = suspenseService;
        }

        protected override CommandResult OnExecute(SaveNoteViewModel model)
        {
            var result = _suspenseService.SaveNotes(model.Id, model.Note);

            return new CommandResult(result);
        }
    }
}