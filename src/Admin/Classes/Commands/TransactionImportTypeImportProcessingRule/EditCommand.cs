using Admin.Models.TransactionImportTypeImportProcessingRule;
using BusinessLogic.Interfaces.Services;
using log4net;

namespace Admin.Classes.Commands.TransactionImportTypeImportProcessingRule
{
    public class EditCommand : BaseCommand<EditViewModel>
    {
        private readonly ITransactionImportTypeImportProcessingRuleService _service;

        public EditCommand(ILog log
            , ITransactionImportTypeImportProcessingRuleService service)
            : base(log)
        {
            _service = service;
        }

        protected override CommandResult OnExecute(EditViewModel model)
        {
            var item = new BusinessLogic.Entities.TransactionImportTypeImportProcessingRule()
            {
                Id = model.Id,
                TransactionImportTypeId = model.TransactionImportTypeId,
                ImportProcessingRuleId = model.ImportProcessingRuleId
            };

            var result = _service.Update(item);

            return new CommandResult(result);
        }
    }
}