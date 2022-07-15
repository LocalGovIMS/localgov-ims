using Admin.Models.ImportProcessingRuleImportType;
using BusinessLogic.Interfaces.Services;
using log4net;

namespace Admin.Classes.Commands.ImportProcessingRuleImportType
{
    public class CreateCommand : BaseCommand<EditViewModel>
    {
        private readonly IImportTypeImportProcessingRuleService _service;

        public CreateCommand(ILog log
            , IImportTypeImportProcessingRuleService service)
            : base(log)
        {
            _service = service;
        }

        protected override CommandResult OnExecute(EditViewModel model)
        {
            var item = new BusinessLogic.Entities.ImportTypeImportProcessingRule()
            {
                ImportTypeId = model.ImportTypeId,
                ImportProcessingRuleId = model.ImportProcessingRuleId,                
            };

            var result = _service.Create(item);

            return new CommandResult(result);
        }
    }
}