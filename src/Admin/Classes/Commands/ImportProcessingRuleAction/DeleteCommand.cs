using BusinessLogic.Interfaces.Services;
using log4net;

namespace Admin.Classes.Commands.ImportProcessingRuleAction
{
    public class DeleteCommand : BaseCommand<int>
    {
        private readonly IImportProcessingRuleActionService _service;

        public DeleteCommand(ILog log
            , IImportProcessingRuleActionService service)
            : base(log)
        {
            _service = service;
        }

        protected override CommandResult OnExecute(int id)
        {
            var result = _service.Delete(id);

            return new CommandResult(result);
        }
    }
}