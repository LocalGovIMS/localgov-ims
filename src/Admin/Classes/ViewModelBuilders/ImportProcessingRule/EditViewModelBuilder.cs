using Admin.Models.ImportProcessingRule;
using BusinessLogic.Interfaces.Services;
using log4net;

namespace Admin.Classes.ViewModelBuilders.ImportProcessingRule
{
    public class EditViewModelBuilder : BaseViewModelBuilder<EditViewModel, int>
    {
        private readonly IImportProcessingRuleService _importProcessingRuleService;
        private readonly IImportTypeService _importTypeService;

        public EditViewModelBuilder(ILog log
            , IImportProcessingRuleService importProcessingRuleService
            , IImportTypeService importTypeService)
            : base(log)
        {
            _importProcessingRuleService = importProcessingRuleService;
            _importTypeService = importTypeService;
        }

        protected override EditViewModel OnBuild()
        {
            return new EditViewModel();
        }

        protected override EditViewModel OnBuild(int id)
        {
            var data = _importProcessingRuleService.Get(id);
            var model = new EditViewModel();

            if (data == null) return model;

            model.Id = data.Id;
            model.Name = data.Name;
            model.Description = data.Description;
            model.IsGlobal = data.IsGlobal;
            model.IsDisabled = data.Disabled;
            model.ImportTypesAreAvailableToAdd = ImportTypesAreAvailableToAdd(data);

            return model;
        }

        private bool ImportTypesAreAvailableToAdd(BusinessLogic.Entities.ImportProcessingRule data)
        {
            var allImportTypes = _importTypeService.GetAll();

            return allImportTypes.Count > data.ImportTypes.Count;
        }
    }
}