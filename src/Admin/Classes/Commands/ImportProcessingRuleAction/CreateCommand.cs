using Admin.Models.ImportProcessingRuleAction;
using BusinessLogic.Interfaces.Services;
using log4net;

namespace Admin.Classes.Commands.ImportProcessingRuleAction
{
    public class CreateCommand : BaseCommand<EditViewModel>
    {
        private readonly IImportProcessingRuleActionService _service;

        public CreateCommand(ILog log
            , IImportProcessingRuleActionService service)
            : base(log)
        {
            _service = service;
        }

        protected override CommandResult OnExecute(EditViewModel model)
        {
            var item = new BusinessLogic.Entities.ImportProcessingRuleAction()
            {
                ImportProcessingRuleId = model.ImportProcessingRuleId,
                ImportProcessingRuleFieldId = model.ImportProcessingRuleFieldId,
                Value = model.Value
            };

            var result = _service.Create(item);

            return new CommandResult(result);
        }
    }
}