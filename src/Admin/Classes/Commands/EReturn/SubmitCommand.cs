using BusinessLogic.Interfaces.Services;
using log4net;

namespace Admin.Classes.Commands.EReturn
{
    public class SubmitCommand : BaseCommand<int>
    {
        private readonly IEReturnService _eReturnService;

        public SubmitCommand(ILog log
            , IEReturnService eReturnService)
            : base(log)
        {
            _eReturnService = eReturnService;
        }

        protected override CommandResult OnExecute(int id)
        {
            var result = _eReturnService.Submit(id);

            return new CommandResult(result);
        }
    }
}