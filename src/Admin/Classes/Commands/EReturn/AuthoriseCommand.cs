using Admin.Models.EReturn;
using BusinessLogic.Interfaces.Services;
using log4net;
using System;

namespace Admin.Classes.Commands.EReturn
{
    public class ApproveCommand : BaseCommand<ApproveViewModel>
    {
        private readonly IEReturnService _eReturnService;

        public ApproveCommand(ILog log
            , IEReturnService eReturnService)
            : base(log)
        {
            _eReturnService = eReturnService ?? throw new ArgumentNullException("eReturnService");
        }

        protected override CommandResult OnExecute(ApproveViewModel model)
        {
            var result = _eReturnService.Approve(model.Items);

            return new CommandResult(result);
        }
    }
}