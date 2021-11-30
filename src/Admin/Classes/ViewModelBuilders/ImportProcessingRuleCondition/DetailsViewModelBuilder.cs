using Admin.Models.ImportProcessingRuleCondition;
using BusinessLogic.Interfaces.Services;
using log4net;
using System;

namespace Admin.Classes.ViewModelBuilders.ImportProcessingRuleCondition
{
    public class DetailsViewModelBuilder : BaseViewModelBuilder<DetailsViewModel, int>
    {
        private readonly IImportProcessingRuleConditionService _service;

        public DetailsViewModelBuilder(ILog log
            , IImportProcessingRuleConditionService importProcessingRuleConditionService)
            : base(log)
        {
            _service = importProcessingRuleConditionService;
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
                OperatorName = data.Operator.DisplayName,
                Value = data.Value,
                LogicalOperator = data.LogicalOperator
            };
        }
    }
}