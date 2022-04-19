using Admin.Models.CheckDigitConfiguration;
using BusinessLogic.Enums;
using BusinessLogic.Interfaces.Services;
using log4net;

namespace Admin.Classes.ViewModelBuilders.CheckDigitConfiguration
{
    public class EditViewModelBuilder : BaseViewModelBuilder<EditViewModel, int>
    {
        private readonly ICheckDigitConfigurationService _service;

        public EditViewModelBuilder(ILog log
            , ICheckDigitConfigurationService checkDigitConfigurationService)
            : base(log)
        {
            _service = checkDigitConfigurationService;
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

            model.ApplySubtraction = data.ApplySubtraction;
            model.Id = data.Id;
            model.Modulus = data.Modulus;
            model.Name = data.Name;
            model.ResultSubstitutions = data.ResultSubstitutions;
            model.SourceSubstitutions = data.SourceSubstitutions;
            model.Type = (CheckDigitType)data.Type;
            model.Weightings = data.Weightings;

            return model;
        }
    }
}