using BusinessLogic.Interfaces.Services;
using log4net;

namespace Admin.Classes.Commands.ImportProcessingRuleCondition
{
    public class DeleteCommand : BaseCommand<int>
    {
        private readonly IImportProcessingRuleConditionService _service;

        public DeleteCommand(ILog log
            , IImportProcessingRuleConditionService service)
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