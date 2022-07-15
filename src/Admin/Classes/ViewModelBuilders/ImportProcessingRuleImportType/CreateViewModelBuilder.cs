using Admin.Models.ImportProcessingRuleImportType;
using BusinessLogic.Interfaces.Services;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using Web.Mvc;

namespace Admin.Classes.ViewModelBuilders.ImportProcessingRuleImportType
{
    public class CreateViewModelBuilder : BaseViewModelBuilder<EditViewModel, int>
    {
        private readonly IImportProcessingRuleService _importProcessingRuleService;
        private readonly IImportTypeService _importTypeService;

        public CreateViewModelBuilder(ILog log
            , IImportProcessingRuleService importProcessingRuleService
            , IImportTypeService importTypeService)
            : base(log)
        {
            _importProcessingRuleService = importProcessingRuleService;
            _importTypeService = importTypeService;
        }

        protected override EditViewModel OnBuild()
        {
            throw new NotImplementedException();
        }

        protected override EditViewModel OnBuild(int id)
        {
            var model = new EditViewModel();

            model.ImportProcessingRuleId = id;

            model.ImportTypes = GetImportTypes(model.ImportProcessingRuleId);

            return model;
        }

        protected override EditViewModel OnRebuild(EditViewModel model)
        {
            base.OnRebuild(model);

            model.ImportTypes = GetImportTypes(model.ImportProcessingRuleId);

            return model;
        }

        private SelectList GetImportTypes(int importProcessingRuleId)
        {
            var selectListItems = new List<SelectListItem>();
            var items = GetAvailableImportTypes(importProcessingRuleId);

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

        private IList<BusinessLogic.Entities.ImportType> GetAvailableImportTypes(int importProcessingRuleId)
        {
            var allImportTypes = _importTypeService.GetAll();
            var allSelectedImportTypes = _importProcessingRuleService.Get(importProcessingRuleId).ImportTypes.Select(x => x.ImportType);

            return allImportTypes.Except(allSelectedImportTypes).ToList();
        }
    }
}