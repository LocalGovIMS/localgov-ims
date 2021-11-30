using Admin.Models.Shared;
using BusinessLogic.Interfaces.Services;
using log4net;
using System;
using System.Linq;

namespace Admin.Classes.ViewModelBuilders.UserFundGroup
{
    public class BasicListViewModelBuilder : BaseViewModelBuilder<BasicListViewModel, int>
    {
        private readonly IUserFundGroupService _userFundGroupService;

        public BasicListViewModelBuilder(ILog log
            , IUserFundGroupService userFundGroupService)
            : base(log)
        {
            _userFundGroupService = userFundGroupService;
        }

        protected override BasicListViewModel OnBuild()
        {
            throw new NotImplementedException();
        }

        protected override BasicListViewModel OnBuild(int id)
        {
            var data = _userFundGroupService.GetUserFundGroups(id);

            var model = new BasicListViewModel()
            {
                ListTitle = "User fund groups",
                ColumnTitle = "Fund group",
                Items = data.Select(x => x.FundGroup.Name).ToList()
            };

            return model;
        }
    }
}