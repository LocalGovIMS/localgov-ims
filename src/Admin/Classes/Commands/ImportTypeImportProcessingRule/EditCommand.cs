using Admin.Models.ImportTypeImportProcessingRule;
using BusinessLogic.Interfaces.Services;
using log4net;

namespace Admin.Classes.Commands.ImportTypeImportProcessingRule
{
    public class EditCommand : BaseCommand<EditViewModel>
    {
        private readonly IImportTypeImportProcessingRuleService _importTypeImportProcessingRuleService;

        public EditCommand(ILog log
            , IImportTypeImportProcessingRuleService service)
            : base(log)
        {
            _importTypeImportProcessingRuleService = service;
        }

        protected override CommandResult OnExecute(EditViewModel model)
        {
            var item = new BusinessLogic.Entities.ImportTypeImportProcessingRule()
            {
                Id = model.Id,
                ImportTypeId = model.ImportTypeId,
                ImportProcessingRuleId = model.ImportProcessingRuleId
            };

            var result = _importTypeImportProcessingRuleService.Update(item);

            return new CommandResult(result);
        }
    }
}