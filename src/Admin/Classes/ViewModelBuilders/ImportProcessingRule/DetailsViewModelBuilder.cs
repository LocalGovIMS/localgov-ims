using Admin.Models.ImportProcessingRule;
using BusinessLogic.Interfaces.Services;
using log4net;
using System;

namespace Admin.Classes.ViewModelBuilders.ImportProcessingRule
{
    public class DetailsViewModelBuilder : BaseViewModelBuilder<DetailsViewModel, int>
    {
        private readonly IImportProcessingRuleService _service;

        public DetailsViewModelBuilder(ILog log
            , IImportProcessingRuleService importProcessingRuleService)
            : base(log)
        {
            _service = importProcessingRuleService;
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
                Name = data.Name,
                Description = data.Description,
                IsDisabled = data.Disabled
            };
        }
    }
}