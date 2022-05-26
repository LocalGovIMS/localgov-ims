using Admin.Models.FundMessage;
using BusinessLogic.Interfaces.Services;
using log4net;

namespace Admin.Classes.Commands.FundMessage
{
    public class CreateCommand : BaseCommand<EditViewModel>
    {
        private readonly IFundMessageService _fundMessageService;

        public CreateCommand(ILog log
            , IFundMessageService fundMessageService)
            : base(log)
        {
            _fundMessageService = fundMessageService;
        }

        protected override CommandResult OnExecute(EditViewModel model)
        {
            var item = new BusinessLogic.Entities.FundMessage()
            {
                FundCode = model.FundCode,
                Message = model.Message
            };

            var result = _fundMessageService.Create(item);

            return new CommandResult(result);
        }
    }
}