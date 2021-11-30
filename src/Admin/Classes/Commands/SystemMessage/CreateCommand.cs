using Admin.Models.SystemMessage;
using BusinessLogic.Interfaces.Services;
using log4net;

namespace Admin.Classes.Commands.SystemMessage
{
    public class CreateCommand : BaseCommand<EditViewModel>
    {
        private readonly ISystemMessageService _systemMessageService;

        public CreateCommand(ILog log
            , ISystemMessageService systemMessageService)
            : base(log)
        {
            _systemMessageService = systemMessageService;
        }

        protected override CommandResult OnExecute(EditViewModel model)
        {
            var item = new BusinessLogic.Entities.SystemMessage()
            {
                Message = model.Message,
                StartDate = model.StartDate,
                EndDate = model.EndDate,
                Severity = model.Severity,
            };

            var result = _systemMessageService.Create(item);

            return new CommandResult(result);
        }
    }
}