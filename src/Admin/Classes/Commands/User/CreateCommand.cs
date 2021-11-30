using Admin.Models.User;
using BusinessLogic.Interfaces.Services;
using log4net;

namespace Admin.Classes.Commands.User
{
    public class CreateCommand : BaseCommand<EditViewModel>
    {
        private readonly IUserService _userService;

        public CreateCommand(ILog log
            , IUserService userService)
            : base(log)
        {
            _userService = userService;
        }

        protected override CommandResult OnExecute(EditViewModel model)
        {
            var item = new BusinessLogic.Entities.User()
            {
                UserName = model.UserName,
                ExpiryDays = model.ExpiryDays,
                Disabled = model.Disabled,
                DisplayName = model.DisplayName,
                OfficeCode = model.OfficeCode
            };

            var result = _userService.Create(item);

            return new CommandResult(result);
        }
    }
}