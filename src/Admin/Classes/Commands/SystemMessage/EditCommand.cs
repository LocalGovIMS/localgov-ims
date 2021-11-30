using Admin.Models.SystemMessage;
using BusinessLogic.Interfaces.Services;
using log4net;

namespace Admin.Classes.Commands.SystemMessage
{
    public class EditCommand : BaseCommand<EditViewModel>
    {
        private readonly ISystemMessageService _systemMessageService;

        public EditCommand(ILog log
            , ISystemMessageService systemMessageService)
            : base(log)
        {
            _systemMessageService = systemMessageService;
        }

        protected override CommandResult OnExecute(EditViewModel model)
        {
            var item = new BusinessLogic.Entities.SystemMessage()
            {
                Id = model.Id,
                Message = model.Message,
                StartDate = model.StartDate,
                EndDate = model.EndDate,
                Severity = model.Severity
            };

            var result = _systemMessageService.Update(item);

            return new CommandResult(result);
        }
    }
}