using Admin.Models.User;
using BusinessLogic.Interfaces.Services;
using log4net;
using System;

namespace Admin.Classes.ViewModelBuilders.User
{
    public class DetailsViewModelBuilder : BaseViewModelBuilder<DetailsViewModel, int>
    {
        private readonly IUserService _userService;

        public DetailsViewModelBuilder(ILog log
            , IUserService userService)
            : base(log)
        {
            _userService = userService;
        }

        protected override DetailsViewModel OnBuild()
        {
            throw new NotImplementedException();
        }

        protected override DetailsViewModel OnBuild(int id)
        {
            var data = _userService.GetUser(id);

            return new DetailsViewModel()
            {
                UserId = data.UserId,
                UserName = data.UserName,
                LastLogin = data.LastLogin,
                ExpiryDays = data.ExpiryDays,
                Disabled = data.Disabled,
                DisplayName = data.DisplayName,
                OfficeCode = data.OfficeCode
            };
        }
    }
}