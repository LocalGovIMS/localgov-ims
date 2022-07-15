using Admin.Models.ImportProcessingRuleImportType;
using BusinessLogic.Interfaces.Services;
using log4net;
using System;

namespace Admin.Classes.ViewModelBuilders.ImportProcessingRuleImportType
{
    public class DetailsViewModelBuilder : BaseViewModelBuilder<DetailsViewModel, int>
    {
        private readonly IImportTypeImportProcessingRuleService _service;

        public DetailsViewModelBuilder(ILog log
            , IImportTypeImportProcessingRuleService fundMetadataService)
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
                ImportTypeId = data.ImportType.Id,
                ImportTypeName = data.ImportType.Name,
                ImportProcessingRuleId = data.ImportProcessingRule.Id,
                ImportProcessingRuleName = data.ImportProcessingRule.Name
            };
        }
    }
}