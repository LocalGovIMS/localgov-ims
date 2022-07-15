using Admin.Models.ImportProcessingRuleImportType;
using BusinessLogic.Interfaces.Services;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using Web.Mvc;

namespace Admin.Classes.ViewModelBuilders.ImportProcessingRuleImportType
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

            model.ImportTypes = GetImportTypes(data);

            if (data == null) return model;

            model.Id = data.Id;
            model.ImportTypeId = data.ImportType.Id;
            model.ImportProcessingRuleId = data.ImportProcessingRule.Id;
            model.ImportProcessingRuleName = data.ImportProcessingRule.Name;

            return model;
        }

        private SelectList GetImportTypes(BusinessLogic.Entities.ImportTypeImportProcessingRule importTypeImportProcessingRule)
        {
            var selectListItems = new List<SelectListItem>();
            var items = GetAvailableImportTypes(importTypeImportProcessingRule);

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

        private IList<BusinessLogic.Entities.ImportType> GetAvailableImportTypes(BusinessLogic.Entities.ImportTypeImportProcessingRule importTypeImportProcessingRule)
        {
            var allImportTypes = _importTypeService.GetAll();
            var allSelectedImportTypes = _importProcessingRuleService.Get(importTypeImportProcessingRule.ImportProcessingRuleId).ImportTypes.Select(x => x.ImportType);
            var importTypeBeingEdited = allSelectedImportTypes.Where(x => x.Id == importTypeImportProcessingRule.ImportTypeId);

            return allImportTypes
                .Except(allSelectedImportTypes)
                .Concat(importTypeBeingEdited)
                .OrderBy(x => x.Name)
                .ToList();
        }
    }
}