using Admin.Models.ImportTypeImportProcessingRule;
using BusinessLogic.Interfaces.Services;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using Web.Mvc;

namespace Admin.Classes.ViewModelBuilders.ImportTypeImportProcessingRule
{
    public class CreateViewModelBuilder : BaseViewModelBuilder<EditViewModel, int>
    {
        private readonly IImportTypeService _importTypeService;
        private readonly IImportProcessingRuleService _importProcessingRuleService;

        public CreateViewModelBuilder(ILog log
            , IImportTypeService importTypeService
            , IImportProcessingRuleService importProcessingRuleService)
            : base(log)
        {
            _importTypeService = importTypeService;
            _importProcessingRuleService = importProcessingRuleService;
        }

        protected override EditViewModel OnBuild()
        {
            throw new NotImplementedException();
        }

        protected override EditViewModel OnBuild(int id)
        {
            var model = new EditViewModel();

            model.ImportTypeId = id;
            
            model.ImportProcessingRules = GetImportProcessingRules(model.ImportTypeId);

            return model;
        }        

        protected override EditViewModel OnRebuild(EditViewModel model)
        {
            base.OnRebuild(model);

            model.ImportProcessingRules = GetImportProcessingRules(model.ImportTypeId);

            return model;
        }

        private SelectList GetImportProcessingRules(int ImportTypeId)
        {
            var selectListItems = new List<SelectListItem>();
            var items = GetAvailableImportProcessingRules(ImportTypeId);

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

        private IList<BusinessLogic.Entities.ImportProcessingRule> GetAvailableImportProcessingRules(int ImportTypeId)
        {
            var allImportProcessingRules = _importProcessingRuleService.GetAll(false);
            var allSelectedImportProcessingRules = _importTypeService.Get(ImportTypeId).ImportProcessingRules.Select(x => x.ImportProcessingRule);

            return allImportProcessingRules.Except(allSelectedImportProcessingRules).ToList();
        }
    }
}