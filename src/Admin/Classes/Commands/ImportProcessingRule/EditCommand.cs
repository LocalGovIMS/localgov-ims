using Admin.Models.ImportProcessingRule;
using BusinessLogic.Interfaces.Services;
using log4net;

namespace Admin.Classes.Commands.ImportProcessingRule
{
    public class EditCommand : BaseCommand<EditViewModel>
    {
        private readonly IImportProcessingRuleService _service;

        public EditCommand(ILog log
            , IImportProcessingRuleService importProcessingRuleService)
            : base(log)
        {
            _service = importProcessingRuleService;
        }

        protected override CommandResult OnExecute(EditViewModel model)
        {
            var item = new BusinessLogic.Entities.ImportProcessingRule()
            {
                Id = model.Id,
                Name = model.Name,
                Description = model.Description,
                IsGlobal = model.IsGlobal,  
                Disabled = model.IsDisabled
            };

            var result = _service.Update(item);

            return new CommandResult(result);
        }
    }
}