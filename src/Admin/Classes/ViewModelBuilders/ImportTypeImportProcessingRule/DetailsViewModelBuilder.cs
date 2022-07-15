using Admin.Models.ImportTypeImportProcessingRule;
using BusinessLogic.Interfaces.Services;
using log4net;
using System;

namespace Admin.Classes.ViewModelBuilders.ImportTypeImportProcessingRule
{
    public class DetailsViewModelBuilder : BaseViewModelBuilder<DetailsViewModel, int>
    {
        private readonly IImportTypeImportProcessingRuleService _importTypeImportProcessingRuleService;

        public DetailsViewModelBuilder(ILog log
            , IImportTypeImportProcessingRuleService importTypeImportProcessingRuleService)
            : base(log)
        {
            _importTypeImportProcessingRuleService = importTypeImportProcessingRuleService;
        }

        protected override DetailsViewModel OnBuild()
        {
            throw new NotImplementedException();
        }

        protected override DetailsViewModel OnBuild(int id)
        {
            var data = _importTypeImportProcessingRuleService.Get(id);

            return new DetailsViewModel()
            {
                Id = data.Id,
                ImportTypeId = data.ImportType.Id,
                ImportTypeName = data.ImportType.Name,
                ImportProcessingRuleId = data.ImportProcessingRule.Id,
                ImportProcessingRuleName = data.ImportProcessingRule.Name
            };
        }
    }
}