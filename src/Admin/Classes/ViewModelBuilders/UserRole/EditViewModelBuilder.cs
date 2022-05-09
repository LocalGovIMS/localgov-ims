using Admin.Models.UserRole;
using BusinessLogic.Interfaces.Services;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using Web.Mvc;

namespace Admin.Classes.ViewModelBuilders.UserRole
{
    public class EditViewModelBuilder : BaseViewModelBuilder<EditViewModel, int>
    {
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;
        private readonly IUserRoleService _userRoleService;

        public EditViewModelBuilder(ILog log
            , IUserService userService
            , IRoleService roleService
            , IUserRoleService userRoleService)
            : base(log)
        {
            _userService = userService;
            _roleService = roleService;
            _userRoleService = userRoleService;
        }

        protected override EditViewModel OnBuild()
        {
            throw new NotImplementedException();
        }

        protected override EditViewModel OnBuild(int id)
        {
            var user = _userService.GetUser(id);
            var userRoles = _userRoleService.GetByUserId(id);

            var model = new EditViewModel();

            if (user == null) return model;
            if (userRoles == null) return model;

            model.UserId = user.UserId;
            model.UserName = user.UserName;
            model.Roles = GetRoles(userRoles);

            return model;
        }

        private ICollection<CheckBoxListItem> GetRoles(List<BusinessLogic.Entities.UserRole> existingItems)
        {
            var allItems = _roleService.GetAllRoles().OrderBy(x => x.Name);

            return allItems.Select(x => new CheckBoxListItem()
            {
                Id = x.RoleId.ToString(),
                Text = x.Name,
                IsChecked = existingItems.Any(y => y.RoleId == x.RoleId)
            })
                .ToList();
        }
    }
}