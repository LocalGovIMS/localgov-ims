using Admin.Models.Vat;
using BusinessLogic.Interfaces.Services;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Admin.Classes.ViewModelBuilders.Vat
{
    public class ListViewModelBuilder : BaseViewModelBuilder<IList<DetailsViewModel>, string>
    {
        private readonly IVatService _vatService;

        public ListViewModelBuilder(ILog log
            , IVatService vatService)
            : base(log)
        {
            _vatService = vatService;
        }

        protected override IList<DetailsViewModel> OnBuild()
        {
            return _vatService
               .GetAllCodes(true)
               .Select(x => new DetailsViewModel()
               {
                   Code = x.VatCode,
                   Percentage = x.Percentage,
                   IsDisabled = x.Disabled
               })
               .ToList();
        }

        protected override IList<DetailsViewModel> OnBuild(string id)
        {
            throw new NotImplementedException();
        }
    }
}