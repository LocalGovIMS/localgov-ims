using BusinessLogic.Interfaces.Services;
using log4net;

namespace Admin.Classes.Commands.ImportTypeImportProcessingRule
{
    public class DeleteCommand : BaseCommand<int>
    {
        private readonly IImportTypeImportProcessingRuleService _importTypeImportProcessingRuleService;

        public DeleteCommand(ILog log
            , IImportTypeImportProcessingRuleService service)
            : base(log)
        {
            _importTypeImportProcessingRuleService = service;
        }

        protected override CommandResult OnExecute(int id)
        {
            var result = _importTypeImportProcessingRuleService.Delete(id);

            return new CommandResult(result);
        }
    }
}