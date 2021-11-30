using Admin.Models.FundGroup;
using BusinessLogic.Interfaces.Services;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Admin.Classes.ViewModelBuilders.FundGroup
{
    public class ListViewModelBuilder : BaseViewModelBuilder<IList<DetailsViewModel>, int>
    {
        private readonly IFundGroupService _fundGroupService;

        public ListViewModelBuilder(ILog log
            , IFundGroupService fundGroupService)
            : base(log)
        {
            _fundGroupService = fundGroupService;
        }

        protected override IList<DetailsViewModel> OnBuild()
        {
            return _fundGroupService
               .GetAllFundGroups()
               .Select(x => new DetailsViewModel()
               {
                   Id = x.FundGroupId,
                   FundGroupName = x.Name
               })
               .ToList();
        }

        protected override IList<DetailsViewModel> OnBuild(int id)
        {
            throw new NotImplementedException();
        }
    }
}