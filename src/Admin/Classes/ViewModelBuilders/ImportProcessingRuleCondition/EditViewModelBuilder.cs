using Admin.Models.ImportProcessingRuleCondition;
using BusinessLogic.Interfaces.Services;
using log4net;
using System.Collections.Generic;
using System.Linq;
using Web.Mvc;

namespace Admin.Classes.ViewModelBuilders.ImportProcessingRuleCondition
{
    public class EditViewModelBuilder : BaseViewModelBuilder<EditViewModel, int>
    {
        private readonly IImportProcessingRuleConditionService _importProcessingRuleConditionService;
        private readonly IImportProcessingRuleFieldService _importProcessingRuleFieldService;
        private readonly IImportProcessingRuleOperatorService _importProcessingRuleOperatorService;
        private readonly IImportProcessingRuleService _importProcessingRuleService;

        public EditViewModelBuilder(ILog log
            , IImportProcessingRuleConditionService importProcessingRuleConditionService
            , IImportProcessingRuleFieldService importProcessingRuleFieldService
            , IImportProcessingRuleOperatorService importProcessingRuleOperatorService
            , IImportProcessingRuleService importProcessingRuleService)
            : base(log)
        {
            _importProcessingRuleConditionService = importProcessingRuleConditionService;
            _importProcessingRuleOperatorService = importProcessingRuleOperatorService;
            _importProcessingRuleFieldService = importProcessingRuleFieldService;
            _importProcessingRuleService = importProcessingRuleService;
        }

        protected override EditViewModel OnBuild()
        {
            return OnBuild(0);
        }

        protected override EditViewModel OnBuild(int id)
        {
            var data = _importProcessingRuleConditionService.Get(id);

            var model = new EditViewModel();

            model.LogicalOperators = GetLogicalOperatorList();
            model.Fields = GetFieldList();
            model.Operators = GetOperatorList();

            if (data == null) return model;

            model.Id = data.Id;
            model.ImportProcessingRuleId = data.ImportProcessingRuleId;
            model.ImportProcessingRuleFieldId = data.ImportProcessingRuleFieldId;
            model.ImportProcessingRuleOperatorId = data.ImportProcessingRuleOperatorId;
            model.Value = data.Value;
            model.LogicalOperator = data.LogicalOperator;
            model.IsFirstItemInTheGroup = IsFirstItemInTheGroup(data);

            return model;
        }

        private SelectList GetLogicalOperatorList()
        {
            var logicalOperators = new List<string> { "AND", "OR" };
            var selectListItems = new List<SelectListItem>();

            foreach (var item in logicalOperators)
            {
                selectListItems.Add(new SelectListItem()
                {
                    Value = item,
                    Text = item
                });
            }

            return new SelectList(selectListItems, false);
        }

        private SelectList GetFieldList()
        {
            var selectListItems = new List<SelectListItem>();

            foreach (var item in _importProcessingRuleFieldService.GetAll().OrderBy(x => x.DisplayOrder))
            {
                var dataAttributes = new List<ValuePair>();

                dataAttributes.Add(new ValuePair() { Key = "type", Value = item.Type });

                selectListItems.Add(new SelectListItem()
                {
                    Value = item.Id.ToString(),
                    Text = item.DisplayName,
                    DataAttributes = dataAttributes
                });
            }

            return new SelectList(selectListItems, false);
        }

        private SelectList GetOperatorList()
        {
            var selectListItems = new List<SelectListItem>();

            foreach (var item in _importProcessingRuleOperatorService.GetAll().OrderBy(x => x.DisplayOrder))
            {
                var dataAttributes = new List<ValuePair>();

                dataAttributes.Add(new ValuePair() { Key = "type", Value = item.Type });

                selectListItems.Add(new SelectListItem()
                {
                    Value = item.Id.ToString(),
                    Text = item.DisplayName,
                    DataAttributes = dataAttributes
                });
            }

            return new SelectList(selectListItems, false);
        }

        private bool IsFirstItemInTheGroup(BusinessLogic.Entities.ImportProcessingRuleCondition condition)
        {
            // Get all in the rule
            var conditions = _importProcessingRuleService.Get(condition.ImportProcessingRuleId).Conditions;

            var firstConditionsInTheGroup = conditions
                .Where(x => x.Group == condition.Group)
                .OrderBy(x => x.Id)
                .First();

            return firstConditionsInTheGroup.Id == condition.Id;
        }
    }
}