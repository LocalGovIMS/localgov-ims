using Admin.Models.ImportProcessingRuleAction;
using BusinessLogic.Interfaces.Services;
using log4net;

namespace Admin.Classes.Commands.ImportProcessingRuleAction
{
    public class EditCommand : BaseCommand<EditViewModel>
    {
        private readonly IImportProcessingRuleActionService _service;

        public EditCommand(ILog log
            , IImportProcessingRuleActionService importProcessingRuleActionService)
            : base(log)
        {
            _service = importProcessingRuleActionService;
        }

        protected override CommandResult OnExecute(EditViewModel model)
        {
            var item = new BusinessLogic.Entities.ImportProcessingRuleAction()
            {
                Id = model.Id,
                ImportProcessingRuleId = model.ImportProcessingRuleId,
                ImportProcessingRuleFieldId = model.ImportProcessingRuleFieldId,
                Value = model.Value
            };

            var result = _service.Update(item);

            return new CommandResult(result);
        }
    }
}