using Admin.Models.UserRole;
using BusinessLogic.Interfaces.Services;
using log4net;
using System;
using System.Linq;

namespace Admin.Classes.Commands.UserRole
{
    public class EditCommand : BaseCommand<EditViewModel>
    {
        private readonly IUserRoleService _userRoleService;

        public EditCommand(ILog log
            , IUserRoleService userRoleService)
            : base(log)
        {
            _userRoleService = userRoleService;
        }

        protected override CommandResult OnExecute(EditViewModel model)
        {
            var items = model.Roles
                .Where(x => x.IsChecked == true)
                .Select(x => new BusinessLogic.Entities.UserRole
                {
                    UserId = model.UserId,
                    RoleId = Convert.ToInt32(x.Id)
                }).ToList();

            var result = _userRoleService.Update(items, model.UserId);

            return new CommandResult(result);
        }
    }
}