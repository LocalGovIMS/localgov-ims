using Admin.Models.SuspenseNote;
using BusinessLogic.Interfaces.Services;
using log4net;

namespace Admin.Classes.Commands.SuspenseNote
{
    public class CreateCommand : BaseCommand<EditViewModel>
    {
        private readonly ISuspenseNoteService _suspenseNoteService;

        public CreateCommand(ILog log
            , ISuspenseNoteService suspenseNoteService)
            : base(log)
        {
            _suspenseNoteService = suspenseNoteService;
        }

        protected override CommandResult OnExecute(EditViewModel model)
        {
            var item = new BusinessLogic.Entities.SuspenseNote()
            {
                Note = model.Note,
                SuspenseId = model.SuspenseId
            };

            var result = _suspenseNoteService.Create(item);

            return new CommandResult(result);
        }
    }
}