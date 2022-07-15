using Admin.Models.ImportTypeImportProcessingRule;
using BusinessLogic.Interfaces.Services;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using Web.Mvc;

namespace Admin.Classes.ViewModelBuilders.ImportTypeImportProcessingRule
{
    public class EditViewModelBuilder : BaseViewModelBuilder<EditViewModel, int>
    {
        private readonly IImportTypeImportProcessingRuleService _importTypeImportProcessingRuleService;
        private readonly IImportTypeService _importTypeService;
        private readonly IImportProcessingRuleService _importProcessingRuleService;

        public EditViewModelBuilder(ILog log
            , IImportTypeImportProcessingRuleService importTypeImportProcessingRuleService
            , IImportTypeService importTypeService
            , IImportProcessingRuleService importProcessingRuleService)
            : base(log)
        {
            _importTypeImportProcessingRuleService = importTypeImportProcessingRuleService;
            _importTypeService = importTypeService;
            _importProcessingRuleService = importProcessingRuleService;
        }

        protected override EditViewModel OnBuild()
        {
            throw new NotImplementedException();
        }

        protected override EditViewModel OnBuild(int id)
        {
            var data = _importTypeImportProcessingRuleService.Get(id);

            var model = new EditViewModel();

            model.ImportProcessingRules = GetImportProcessingRules(data);

            if (data == null) return model;

            model.Id = data.Id;
            model.ImportTypeId = data.ImportType.Id;
            model.ImportTypeName = data.ImportType.Name;
            model.ImportProcessingRuleId = data.ImportProcessingRule.Id;

            return model;
        }

        private SelectList GetImportProcessingRules(BusinessLogic.Entities.ImportTypeImportProcessingRule importTypeImportProcessingRule)
        {
            var selectListItems = new List<SelectListItem>();
            var items = GetAvailableImportProcessingRules(importTypeImportProcessingRule);

            foreach (var item in items)
            {
                selectListItems.Add(new SelectListItem()
                {
                    Value = item.Id.ToString(),
                    Text = item.Name,
                });
            }

            return new SelectList(selectListItems, true);
        }

        private IList<BusinessLogic.Entities.ImportProcessingRule> GetAvailableImportProcessingRules(BusinessLogic.Entities.ImportTypeImportProcessingRule importTypeImportProcessingRule)
        {
            var allImportProcessingRules = _importProcessingRuleService.GetAll(false);
            var allSelectedImportProcessingRules = _importTypeService.Get(importTypeImportProcessingRule.ImportTypeId).ImportProcessingRules.Select(x => x.ImportProcessingRule);
            var importProcessingRuleBeingEdited = allSelectedImportProcessingRules.Where(x => x.Id == importTypeImportProcessingRule.ImportProcessingRuleId);

            return allImportProcessingRules
                .Except(allSelectedImportProcessingRules)
                .Concat(importProcessingRuleBeingEdited)
                .OrderBy(x => x.Name)
                .ToList();
        }
    }
}