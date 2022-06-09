using Admin.Models.ImportProcessingRule;
using BusinessLogic.Interfaces.Services;
using log4net;

namespace Admin.Classes.ViewModelBuilders.ImportProcessingRule
{
    public class EditViewModelBuilder : BaseViewModelBuilder<EditViewModel, int>
    {
        private readonly IImportProcessingRuleService _service;
        private readonly ITransactionImportTypeService _transactionImportTypeService;

        public EditViewModelBuilder(ILog log
            , IImportProcessingRuleService importProcessingRuleService
            , ITransactionImportTypeService transactionImportTypeService)
            : base(log)
        {
            _service = importProcessingRuleService;
            _transactionImportTypeService = transactionImportTypeService;
        }

        protected override EditViewModel OnBuild()
        {
            return new EditViewModel();
        }

        protected override EditViewModel OnBuild(int id)
        {
            var data = _service.Get(id);
            var model = new EditViewModel();

            if (data == null) return model;

            model.Id = data.Id;
            model.Name = data.Name;
            model.Description = data.Description;
            model.IsDisabled = data.Disabled;
            model.TransactionImportTypesAreAvailableToAdd = TransactionImportTypesAreAvailableToAdd(data);

            return model;
        }

        private bool TransactionImportTypesAreAvailableToAdd(BusinessLogic.Entities.ImportProcessingRule data)
        {
            var allTransactionImportTypes = _transactionImportTypeService.GetAll();

            return allTransactionImportTypes.Count > data.TransactionImportTypes.Count;
        }
    }
}