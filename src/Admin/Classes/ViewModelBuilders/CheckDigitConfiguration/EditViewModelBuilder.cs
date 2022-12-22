using Admin.Models.CheckDigitConfiguration;
using BusinessLogic.Enums;
using BusinessLogic.Interfaces.Services;
using log4net;
using Web.Mvc;
using System.Web.Mvc.Html;

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
            var model = new EditViewModel();

            model.Types = GetTypes();

            return model;
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

            model.Types = GetTypes();

            return model;
        }

        protected override EditViewModel OnRebuild(EditViewModel model)
        {
            model.Types = GetTypes();
            return model;
        }

        private SelectList GetTypes()
        {
            return new SelectList(EnumHelper.GetSelectList(typeof(CheckDigitType)), false);
        }
    }
}