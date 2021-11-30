using Admin.Models.ImportProcessingRule;
using BusinessLogic.Interfaces.Services;
using log4net;

namespace Admin.Classes.Commands.ImportProcessingRule
{
    public class CreateCommand : BaseCommand<EditViewModel>
    {
        private readonly IImportProcessingRuleService _service;

        public CreateCommand(ILog log
            , IImportProcessingRuleService importProcessingRuleService)
            : base(log)
        {
            _service = importProcessingRuleService;
        }

        protected override CommandResult OnExecute(EditViewModel model)
        {
            var item = new BusinessLogic.Entities.ImportProcessingRule()
            {
                Name = model.Name,
                Description = model.Description,
                Disabled = model.IsDisabled
            };

            var result = _service.Create(item);

            return new CommandResult(result);
        }
    }
}