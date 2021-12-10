using Admin.Models.Office;
using BusinessLogic.Interfaces.Services;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Admin.Classes.ViewModelBuilders.Office
{
    public class ListViewModelBuilder : BaseViewModelBuilder<IList<DetailsViewModel>, string>
    {
        private readonly IOfficeService _officeService;

        public ListViewModelBuilder(ILog log
            , IOfficeService officeService)
            : base(log)
        {
            _officeService = officeService;
        }

        protected override IList<DetailsViewModel> OnBuild()
        {
            return _officeService
               .GetAll()
               .Select(x => new DetailsViewModel()
               {
                   Code = x.OfficeCode,
                   Name = x.Name
               })
               .ToList();
        }

        protected override IList<DetailsViewModel> OnBuild(string id)
        {
            throw new NotImplementedException();
        }
    }
}