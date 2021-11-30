using Admin.Models.ImportProcessingRuleCondition;
using BusinessLogic.Interfaces.Services;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using Web.Mvc;

namespace Admin.Classes.ViewModelBuilders.ImportProcessingRuleCondition
{
    public class CreateViewModelBuilder : BaseViewModelBuilder<EditViewModel, CreateViewModelBuilderArgs>
    {
        private readonly IImportProcessingRuleConditionService _importProcessingRuleConditionService;
        private readonly IImportProcessingRuleOperatorService _importProcessingRuleOperatorService;
        private readonly IImportProcessingRuleFieldService _importProcessingRuleFieldService;

        public CreateViewModelBuilder(ILog log
            , IImportProcessingRuleConditionService importProcessingRuleConditionService
            , IImportProcessingRuleOperatorService importProcessingRuleOperatorService
            , IImportProcessingRuleFieldService importProcessingRuleFieldService)
            : base(log)
        {
            _importProcessingRuleConditionService = importProcessingRuleConditionService;
            _importProcessingRuleOperatorService = importProcessingRuleOperatorService;
            _importProcessingRuleFieldService = importProcessingRuleFieldService;
        }

        protected override EditViewModel OnBuild()
        {
            throw new NotImplementedException();
        }

        protected override EditViewModel OnBuild(CreateViewModelBuilderArgs args)
        {
            var model = new EditViewModel();

            model.ImportProcessingRuleId = args.ImportProcessingRuleId;
            model.Group = args.Group ?? GetNextGroup(args.ImportProcessingRuleId);
            model.IsFirstItemInTheGroup = args.Group == null;
            model.LogicalOperators = GetLogicalOperatorList();
            model.Fields = GetFieldList();
            model.Operators = GetOperatorList();

            return model;
        }

        private int GetNextGroup(int importProcessingRuleId)
        {
            var result = _importProcessingRuleConditionService.Search(new BusinessLogic.Models.ImportProcessingRuleCondition.SearchCriteria() { ImportProcessingRuleId = importProcessingRuleId });

            return result.Items.Select(x => x.Group)
                .Distinct()
                .OrderByDescending(x => x)
                .FirstOrDefault() + 1;
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


    }

    public class CreateViewModelBuilderArgs
    {
        public int ImportProcessingRuleId { get; set; }
        public int? Group { get; set; }
    }
}