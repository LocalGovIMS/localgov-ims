using Admin.Models.Shared;
using BusinessLogic.Interfaces.Services;
using log4net;
using System;
using System.Linq;

namespace Admin.Classes.ViewModelBuilders.UserRole
{
    public class BasicListViewModelBuilder : BaseViewModelBuilder<BasicListViewModel, int>
    {
        private readonly IUserRoleService _userRoleService;

        public BasicListViewModelBuilder(ILog log
            , IUserRoleService userRoleService)
            : base(log)
        {
            _userRoleService = userRoleService;
        }

        protected override BasicListViewModel OnBuild()
        {
            throw new NotImplementedException();
        }

        protected override BasicListViewModel OnBuild(int id)
        {
            var data = _userRoleService.GetUserRoles(id).OrderBy(x => x.Role.Name);

            var model = new BasicListViewModel()
            {
                ListTitle = "User Roles",
                ColumnTitle = "Role",
                NoItemsMessage = "There are no user roles specified",
                Items = data.Select(x => x.Role.DisplayName).ToList()
            };

            return model;
        }
    }
}