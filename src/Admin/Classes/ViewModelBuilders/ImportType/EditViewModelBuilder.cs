using Admin.Models.ImportType;
using BusinessLogic.Enums;
using BusinessLogic.Interfaces.Services;
using log4net;
using System;

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

            return model;
        }

        private bool ImportProcessingRulesAreAvailableToAdd(BusinessLogic.Entities.ImportType data)
        {
            var allImportProcessingRules = _importProcessingRuleService.GetAll(false);

            return allImportProcessingRules.Count > data.ImportProcessingRules.Count;
        }
    }
}