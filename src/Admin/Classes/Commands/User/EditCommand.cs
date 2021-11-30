using Admin.Models.User;
using BusinessLogic.Interfaces.Services;
using log4net;

namespace Admin.Classes.Commands.User
{
    public class EditCommand : BaseCommand<EditViewModel>
    {
        private readonly IUserService _userService;

        public EditCommand(ILog log
            , IUserService userService)
            : base(log)
        {
            _userService = userService;
        }

        protected override CommandResult OnExecute(EditViewModel model)
        {
            var item = new BusinessLogic.Entities.User()
            {
                UserId = model.UserId,
                UserName = model.UserName,
                ExpiryDays = model.ExpiryDays,
                Disabled = model.Disabled,
                DisplayName = model.DisplayName,
                OfficeCode = model.OfficeCode
            };

            var result = _userService.Update(item);

            return new CommandResult(result);
        }
    }
}