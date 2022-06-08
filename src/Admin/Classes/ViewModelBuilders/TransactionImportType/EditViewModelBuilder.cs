using Admin.Models.TransactionImportType;
using BusinessLogic.Interfaces.Services;
using log4net;
using System;

namespace Admin.Classes.ViewModelBuilders.TransactionImportType
{
    public class EditViewModelBuilder : BaseViewModelBuilder<EditViewModel, int>
    {
        private readonly ITransactionImportTypeService _transactionImportTypeService;
        private readonly IImportProcessingRuleService _importProcessingRuleService;

        public EditViewModelBuilder(ILog log
            , ITransactionImportTypeService transactionImportTypeService
            , IImportProcessingRuleService importProcessingRuleService)
            : base(log)
        {
            _transactionImportTypeService = transactionImportTypeService;
            _importProcessingRuleService = importProcessingRuleService;
        }

        protected override EditViewModel OnBuild()
        {
            throw new NotImplementedException();
        }

        protected override EditViewModel OnBuild(int id)
        {
            var data = _transactionImportTypeService.Get(id);
            var model = new EditViewModel();

            if (data == null) return model;

            model.Id = data.Id;
            model.Name = data.Name;
            model.Description = data.Description;
            model.ExternalReference = data.ExternalReference;
            model.ImportProcessingRulesAreAvailableToAdd = ImportProcessingRulesAreAvailableToAdd(data);

            return model;
        }

        private bool ImportProcessingRulesAreAvailableToAdd(BusinessLogic.Entities.TransactionImportType data)
        {
            var allImportProcessingRules = _importProcessingRuleService.GetAll(false);

            return allImportProcessingRules.Count > data.ImportProcessingRules.Count;
        }
    }
}