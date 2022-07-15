using Admin.Models.ImportProcessingRuleImportType;
using BusinessLogic.Interfaces.Services;
using log4net;

namespace Admin.Classes.Commands.ImportProcessingRuleImportType
{
    public class EditCommand : BaseCommand<EditViewModel>
    {
        private readonly IImportTypeImportProcessingRuleService _service;

        public EditCommand(ILog log
            , IImportTypeImportProcessingRuleService service)
            : base(log)
        {
            _service = service;
        }

        protected override CommandResult OnExecute(EditViewModel model)
        {
            var item = new BusinessLogic.Entities.ImportTypeImportProcessingRule()
            {
                Id = model.Id,
                ImportTypeId = model.ImportTypeId,
                ImportProcessingRuleId = model.ImportProcessingRuleId
            };

            var result = _service.Update(item);

            return new CommandResult(result);
        }
    }
}