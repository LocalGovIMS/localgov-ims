using Admin.Models.ImportProcessingRuleTransactionImportType;
using BusinessLogic.Interfaces.Services;
using log4net;

namespace Admin.Classes.Commands.ImportProcessingRuleTransactionImportType
{
    public class CreateCommand : BaseCommand<EditViewModel>
    {
        private readonly ITransactionImportTypeImportProcessingRuleService _service;

        public CreateCommand(ILog log
            , ITransactionImportTypeImportProcessingRuleService service)
            : base(log)
        {
            _service = service;
        }

        protected override CommandResult OnExecute(EditViewModel model)
        {
            var item = new BusinessLogic.Entities.TransactionImportTypeImportProcessingRule()
            {
                TransactionImportTypeId = model.TransactionImportTypeId,
                ImportProcessingRuleId = model.ImportProcessingRuleId,                
            };

            var result = _service.Create(item);

            return new CommandResult(result);
        }
    }
}