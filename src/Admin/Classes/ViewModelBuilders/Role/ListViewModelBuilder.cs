using Admin.Models.Role;
using BusinessLogic.Interfaces.Services;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Admin.Classes.ViewModelBuilders.Role
{
    public class ListViewModelBuilder : BaseViewModelBuilder<IList<DetailsViewModel>, int>
    {
        private readonly IRoleService _roleService;

        public ListViewModelBuilder(ILog log
            , IRoleService roleService)
            : base(log)
        {
            _roleService = roleService;
        }

        protected override IList<DetailsViewModel> OnBuild()
        {
            return _roleService
               .GetAllRoles()
               .Select(x => new DetailsViewModel()
               {
                   Id = x.RoleId,
                   Name = x.Name,
                   DisplayName = x.DisplayName
               })
               .ToList();
        }

        protected override IList<DetailsViewModel> OnBuild(int id)
        {
            throw new NotImplementedException();
        }
    }
}