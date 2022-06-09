using Admin.Models.TransactionImportTypeImportProcessingRule;
using BusinessLogic.Interfaces.Services;
using log4net;
using System;

namespace Admin.Classes.ViewModelBuilders.TransactionImportTypeImportProcessingRule
{
    public class DetailsViewModelBuilder : BaseViewModelBuilder<DetailsViewModel, int>
    {
        private readonly ITransactionImportTypeImportProcessingRuleService _service;

        public DetailsViewModelBuilder(ILog log
            , ITransactionImportTypeImportProcessingRuleService fundMetadataService)
            : base(log)
        {
            _service = fundMetadataService;
        }

        protected override DetailsViewModel OnBuild()
        {
            throw new NotImplementedException();
        }

        protected override DetailsViewModel OnBuild(int id)
        {
            var data = _service.Get(id);

            return new DetailsViewModel()
            {
                Id = data.Id,
                TransactionImportTypeId = data.TransactionImportType.Id,
                TransactionImportTypeName = data.TransactionImportType.Name,
                ImportProcessingRuleId = data.ImportProcessingRule.Id,
                ImportProcessingRuleName = data.ImportProcessingRule.Name
            };
        }
    }
}