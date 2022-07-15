using Admin.Models.ImportTypeImportProcessingRule;
using BusinessLogic.Interfaces.Services;
using log4net;

namespace Admin.Classes.Commands.ImportTypeImportProcessingRule
{
    public class CreateCommand : BaseCommand<EditViewModel>
    {
        private readonly IImportTypeImportProcessingRuleService _importTypeImportProcessingRuleService;

        public CreateCommand(ILog log
            , IImportTypeImportProcessingRuleService service)
            : base(log)
        {
            _importTypeImportProcessingRuleService = service;
        }

        protected override CommandResult OnExecute(EditViewModel model)
        {
            var item = new BusinessLogic.Entities.ImportTypeImportProcessingRule()
            {
                ImportTypeId = model.ImportTypeId,
                ImportProcessingRuleId = model.ImportProcessingRuleId,                
            };

            var result = _importTypeImportProcessingRuleService.Create(item);

            return new CommandResult(result);
        }
    }
}