using Admin.Models.Role;
using BusinessLogic.Interfaces.Services;
using log4net;
using System;

namespace Admin.Classes.ViewModelBuilders.Role
{
    public class EditViewModelBuilder : BaseViewModelBuilder<EditViewModel, int>
    {
        private readonly IRoleService _roleService;

        public EditViewModelBuilder(ILog log
            , IRoleService roleService)
            : base(log)
        {
            _roleService = roleService;
        }

        protected override EditViewModel OnBuild()
        {
            throw new NotImplementedException();
        }

        protected override EditViewModel OnBuild(int id)
        {
            var data = _roleService.GetRole(id);
            var model = new EditViewModel();

            if (data == null) return model;

            model.Id = data.RoleId;
            model.Name = data.Name;
            model.DisplayName = data.DisplayName;

            return model;
        }
    }
}