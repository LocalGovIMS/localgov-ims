using Admin.Models.ImportProcessingRule;
using BusinessLogic.Interfaces.Services;
using log4net;

namespace Admin.Classes.ViewModelBuilders.ImportProcessingRule
{
    public class EditViewModelBuilder : BaseViewModelBuilder<EditViewModel, int>
    {
        private readonly IImportProcessingRuleService _service;

        public EditViewModelBuilder(ILog log
            , IImportProcessingRuleService importProcessingRuleService)
            : base(log)
        {
            _service = importProcessingRuleService;
        }

        protected override EditViewModel OnBuild()
        {
            return new EditViewModel();
        }

        protected override EditViewModel OnBuild(int id)
        {
            var data = _service.Get(id);
            var model = new EditViewModel();

            if (data == null) return model;

            model.Id = data.Id;
            model.Name = data.Name;
            model.Description = data.Description;
            model.IsDisabled = data.Disabled;

            return model;
        }
    }
}