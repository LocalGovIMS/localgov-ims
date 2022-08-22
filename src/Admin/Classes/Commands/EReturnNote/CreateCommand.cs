using Admin.Models.EReturnNote;
using BusinessLogic.Interfaces.Services;
using log4net;

namespace Admin.Classes.Commands.EReturnNote
{
    public class CreateCommand : BaseCommand<EditViewModel>
    {
        private readonly IEReturnNoteService _eReturnNoteService;

        public CreateCommand(ILog log
            , IEReturnNoteService eReturnNoteService)
            : base(log)
        {
            _eReturnNoteService = eReturnNoteService;
        }

        protected override CommandResult OnExecute(EditViewModel model)
        {
            var item = new BusinessLogic.Entities.EReturnNote()
            {
                Note = model.Note,
                EReturnId = model.EReturnId
            };

            var result = _eReturnNoteService.Create(item);

            return new CommandResult(result);
        }
    }
}