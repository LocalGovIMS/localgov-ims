using Admin.Models.ImportProcessingRuleAction;
using BusinessLogic.Interfaces.Services;
using log4net;
using System;

namespace Admin.Classes.ViewModelBuilders.ImportProcessingRuleAction
{
    public class DetailsViewModelBuilder : BaseViewModelBuilder<DetailsViewModel, int>
    {
        private readonly IImportProcessingRuleActionService _service;

        public DetailsViewModelBuilder(ILog log
            , IImportProcessingRuleActionService importProcessingRuleActionService)
            : base(log)
        {
            _service = importProcessingRuleActionService;
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
                FieldName = data.Field.DisplayName,
                Value = data.Value
            };
        }
    }
}