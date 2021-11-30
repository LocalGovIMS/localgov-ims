using BusinessLogic.Interfaces.Services;
using log4net;

namespace Admin.Classes.Commands.FundGroup
{
    public class DeleteCommand : BaseCommand<int>
    {
        private readonly IFundGroupService _fundGroupService;

        public DeleteCommand(ILog log
            , IFundGroupService fundGroupService)
            : base(log)
        {
            _fundGroupService = fundGroupService;
        }

        protected override CommandResult OnExecute(int model)
        {
            var result = _fundGroupService.Remove(model);

            return new CommandResult(result);
        }
    }
}