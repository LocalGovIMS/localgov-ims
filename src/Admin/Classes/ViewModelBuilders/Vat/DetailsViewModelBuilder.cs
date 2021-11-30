using Admin.Models.Vat;
using BusinessLogic.Interfaces.Services;
using log4net;
using System;

namespace Admin.Classes.ViewModelBuilders.Vat
{
    public class DetailsViewModelBuilder : BaseViewModelBuilder<DetailsViewModel, string>
    {
        private readonly IVatService _vatService;

        public DetailsViewModelBuilder(ILog log
            , IVatService vatService)
            : base(log)
        {
            _vatService = vatService;
        }

        protected override DetailsViewModel OnBuild()
        {
            throw new NotImplementedException();
        }

        protected override DetailsViewModel OnBuild(string id)
        {
            var data = _vatService.GetByVatCode(id);

            return new DetailsViewModel()
            {
                Code = data.VatCode,
                Percentage = data.Percentage,
                IsDisabled = data.Disabled
            };
        }
    }
}