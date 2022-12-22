using Admin.Models.ImportType;
using BusinessLogic.Enums;
using BusinessLogic.Interfaces.Services;
using log4net;
using System;
using System.Web.Mvc.Html;
using Web.Mvc;

namespace Admin.Classes.ViewModelBuilders.ImportType
{
    public class EditViewModelBuilder : BaseViewModelBuilder<EditViewModel, int>
    {
        private readonly IImportTypeService _ImportTypeService;
        private readonly IImportProcessingRuleService _importProcessingRuleService;

        public EditViewModelBuilder(ILog log
            , IImportTypeService ImportTypeService
            , IImportProcessingRuleService importProcessingRuleService)
            : base(log)
        {
            _ImportTypeService = ImportTypeService;
            _importProcessingRuleService = importProcessingRuleService;
        }

        protected override EditViewModel OnBuild()
        {
            throw new NotImplementedException();
        }

        protected override EditViewModel OnBuild(int id)
        {
            var data = _ImportTypeService.Get(id);
            var model = new EditViewModel();

            if (data == null) return model;

            model.Id = data.Id;
            model.DataType = (ImportDataTypeEnum)data.DataType;
            model.Name = data.Name;
            model.Description = data.Description;
            model.ExternalReference = data.ExternalReference;
            model.IsReversible = data.IsReversible;
            model.ImportProcessingRulesAreAvailableToAdd = ImportProcessingRulesAreAvailableToAdd(data);

            model.ImportTypes = GetImportTypes();

            return model;
        }

        private SelectList GetImportTypes()
        {
            return new SelectList(EnumHelper.GetSelectList(typeof(BusinessLogic.Enums.ImportDataTypeEnum)), false);
        }

        protected override EditViewModel OnRebuild(EditViewModel model)
        {
            model.ImportTypes = GetImportTypes();
            return model;
        }

        private bool ImportProcessingRulesAreAvailableToAdd(BusinessLogic.Entities.ImportType data)
        {
            var allImportProcessingRules = _importProcessingRuleService.GetAll(false);

            return allImportProcessingRules.Count > data.ImportProcessingRules.Count;
        }
    }
}