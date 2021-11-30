using Admin.Models.Vat;
using BusinessLogic.Interfaces.Services;
using log4net;
using System;

namespace Admin.Classes.ViewModelBuilders.Vat
{
    public class EditViewModelBuilder : BaseViewModelBuilder<EditViewModel, string>
    {
        private readonly IVatService _vatService;

        public EditViewModelBuilder(ILog log
            , IVatService vatService)
            : base(log)
        {
            _vatService = vatService;
        }

        protected override EditViewModel OnBuild()
        {
            throw new NotImplementedException();
        }

        protected override EditViewModel OnBuild(string id)
        {
            var data = _vatService.GetByVatCode(id);
            var model = new EditViewModel();

            if (data == null) return model;

            model.Code = data.VatCode;
            model.Percentage = data.Percentage;
            model.IsDisabled = data.Disabled;

            return model;
        }
    }
}