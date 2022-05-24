using Admin.Models.FundMessage;
using BusinessLogic.Interfaces.Services;
using log4net;

namespace Admin.Classes.Commands.FundMessage
{
    public class EditCommand : BaseCommand<EditViewModel>
    {
        private readonly IFundMessageService _fundMessageService;

        public EditCommand(ILog log
            , IFundMessageService fundMessageService)
            : base(log)
        {
            _fundMessageService = fundMessageService;
        }

        protected override CommandResult OnExecute(EditViewModel model)
        {
            var item = new BusinessLogic.Entities.FundMessage()
            {
                Id = model.Id,
                FundCode = model.FundCode,
                Message = model.Message
            };

            var result = _fundMessageService.Update(item);

            return new CommandResult(result);
        }
    }
}