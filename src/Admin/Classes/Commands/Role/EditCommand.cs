using Admin.Models.Role;
using BusinessLogic.Interfaces.Services;
using log4net;

namespace Admin.Classes.Commands.Role
{
    public class EditCommand : BaseCommand<EditViewModel>
    {
        private readonly IRoleService _roleService;

        public EditCommand(ILog log
            , IRoleService roleService)
            : base(log)
        {
            _roleService = roleService;
        }

        protected override CommandResult OnExecute(EditViewModel model)
        {
            var item = new BusinessLogic.Entities.Role
            {
                RoleId = model.Id,
                Name = model.Name,
                DisplayName = model.DisplayName
            };

            var result = _roleService.Update(item);

            return new CommandResult(result);
        }
    }
}