using Admin.Models.ImportProcessingRuleCondition;
using BusinessLogic.Interfaces.Services;
using log4net;

namespace Admin.Classes.Commands.ImportProcessingRuleCondition
{
    public class CreateCommand : BaseCommand<EditViewModel>
    {
        private readonly IImportProcessingRuleConditionService _service;

        public CreateCommand(ILog log
            , IImportProcessingRuleConditionService service)
            : base(log)
        {
            _service = service;
        }

        protected override CommandResult OnExecute(EditViewModel model)
        {
            var item = new BusinessLogic.Entities.ImportProcessingRuleCondition()
            {
                Group = model.Group,
                ImportProcessingRuleId = model.ImportProcessingRuleId,
                LogicalOperator = model.LogicalOperator,
                ImportProcessingRuleFieldId = model.ImportProcessingRuleFieldId,
                ImportProcessingRuleOperatorId = model.ImportProcessingRuleOperatorId,
                Value = model.Value
            };

            var result = _service.Create(item);

            return new CommandResult(result);
        }
    }
}