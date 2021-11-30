using Admin.Models.FundGroup;
using BusinessLogic.Interfaces.Services;
using log4net;
using System;
using System.Linq;

namespace Admin.Classes.ViewModelBuilders.FundGroup
{
    public class DetailsViewModelBuilder : BaseViewModelBuilder<DetailsViewModel, int>
    {
        private readonly IFundGroupService _fundGroupService;

        public DetailsViewModelBuilder(ILog log
            , IFundGroupService fundGroupService)
            : base(log)
        {
            _fundGroupService = fundGroupService;
        }

        protected override DetailsViewModel OnBuild()
        {
            throw new NotImplementedException();
        }

        protected override DetailsViewModel OnBuild(int id)
        {
            var data = _fundGroupService.GetFundGroup(id);

            return new DetailsViewModel()
            {
                Id = data.FundGroupId,
                FundGroupName = data.Name,
                Funds = data.FundGroupFunds.ToList()
            };
        }
    }
}