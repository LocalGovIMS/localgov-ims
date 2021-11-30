using Admin.Models.Role;
using BusinessLogic.Interfaces.Services;
using log4net;
using System;

namespace Admin.Classes.ViewModelBuilders.Role
{
    public class DetailsViewModelBuilder : BaseViewModelBuilder<DetailsViewModel, int>
    {
        private readonly IRoleService _roleService;

        public DetailsViewModelBuilder(ILog log
            , IRoleService roleService)
            : base(log)
        {
            _roleService = roleService;
        }

        protected override DetailsViewModel OnBuild()
        {
            throw new NotImplementedException();
        }

        protected override DetailsViewModel OnBuild(int id)
        {
            var data = _roleService.GetRole(id);

            return new DetailsViewModel()
            {
                Id = data.RoleId,
                Name = data.Name,
                DisplayName = data.DisplayName
            };
        }
    }
}