using Admin.Models.Office;
using BusinessLogic.Interfaces.Services;
using log4net;
using System;

namespace Admin.Classes.ViewModelBuilders.Office
{
    public class DetailsViewModelBuilder : BaseViewModelBuilder<DetailsViewModel, string>
    {
        private readonly IOfficeService _officeService;

        public DetailsViewModelBuilder(ILog log
            , IOfficeService officeService)
            : base(log)
        {
            _officeService = officeService;
        }

        protected override DetailsViewModel OnBuild()
        {
            throw new NotImplementedException();
        }

        protected override DetailsViewModel OnBuild(string id)
        {
            var data = _officeService.Get(id);

            return new DetailsViewModel()
            {
                Code = data.OfficeCode,
                Name = data.Name
            };
        }
    }
}