using Admin.Models.TransactionImportType;
using BusinessLogic.Interfaces.Services;
using log4net;

namespace Admin.Classes.Commands.TransactionImportType
{
    public class EditCommand : BaseCommand<EditViewModel>
    {
        private readonly ITransactionImportTypeService _transactionImportTypeService;

        public EditCommand(ILog log
            , ITransactionImportTypeService transactionImportTypeService)
            : base(log)
        {
            _transactionImportTypeService = transactionImportTypeService;
        }

        protected override CommandResult OnExecute(EditViewModel model)
        {
            var item = new BusinessLogic.Entities.TransactionImportType
            {
                Id = model.Id,
                Name = model.Name,
                Description = model.Description,
                ExternalReference = model.ExternalReference
            };

            var result = _transactionImportTypeService.Update(item);

            return new CommandResult(result);
        }
    }
}