using Admin.Models.ImportProcessingRuleCondition;
using BusinessLogic.Interfaces.Services;
using log4net;

namespace Admin.Classes.Commands.ImportProcessingRuleCondition
{
    public class EditCommand : BaseCommand<EditViewModel>
    {
        private readonly IImportProcessingRuleConditionService _service;

        public EditCommand(ILog log
            , IImportProcessingRuleConditionService importProcessingRuleConditionService)
            : base(log)
        {
            _service = importProcessingRuleConditionService;
        }

        protected override CommandResult OnExecute(EditViewModel model)
        {
            var item = new BusinessLogic.Entities.ImportProcessingRuleCondition()
            {
                Id = model.Id,
                ImportProcessingRuleId = model.ImportProcessingRuleId,
                Group = model.Group,
                LogicalOperator = model.LogicalOperator,
                ImportProcessingRuleFieldId = model.ImportProcessingRuleFieldId,
                ImportProcessingRuleOperatorId = model.ImportProcessingRuleOperatorId,
                Value = model.Value
            };

            var result = _service.Update(item);

            return new CommandResult(result);
        }
    }
}