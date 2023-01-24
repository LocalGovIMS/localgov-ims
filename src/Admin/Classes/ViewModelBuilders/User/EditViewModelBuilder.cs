using Admin.Models.User;
using BusinessLogic.Interfaces.Services;
using log4net;
using System.Collections.Generic;
using System.Configuration;
using Web.Mvc;

namespace Admin.Classes.ViewModelBuilders.User
{
    public class EditViewModelBuilder : BaseViewModelBuilder<EditViewModel, int>
    {
        private readonly IUserService _userService;
        private readonly IOfficeService _officeService;

        public EditViewModelBuilder(ILog log
            , IUserService userService
            , IOfficeService officeService)
            : base(log)
        {
            _userService = userService;
            _officeService = officeService;
        }

        protected override EditViewModel OnBuild()
        {
            var model = new EditViewModel();

            model.ExpiryDays = DefaultExpiryDays();
            model.Offices = GetOfficeList(_officeService.GetAll());

            return model;
        }

        protected override EditViewModel OnBuild(int id)
        {
            var data = _userService.GetUser(id);
            var model = new EditViewModel();

            if (data == null) return model;

            model.UserId = data.UserId;
            model.UserName = data.UserName;
            model.LastLogin = data.LastLogin;
            model.ExpiryDays = data.ExpiryDays;
            model.Disabled = data.Disabled;
            model.DisplayName = data.DisplayName;
            model.OfficeCode = data.OfficeCode;
            model.Offices = GetOfficeList(_officeService.GetAll());

            return model;
        }

        protected override EditViewModel OnRebuild(EditViewModel model)
        {
            model.Offices = GetOfficeList(_officeService.GetAll());

            return base.OnRebuild(model);
        }

        private SelectList GetOfficeList(List<BusinessLogic.Entities.Office> offices)
        {
            var officeSelectListItems = new List<SelectListItem>();

            foreach (var office in offices)
            {
                officeSelectListItems.Add(new SelectListItem()
                {
                    Value = office.OfficeCode,
                    Text = office.Name
                });
            }

            return new SelectList(officeSelectListItems, true);
        }

        private static int DefaultExpiryDays()
        {
            int defaultExpiryDays;

            return int.TryParse(ConfigurationManager.AppSettings["User.Default.ExpiryDays"], out defaultExpiryDays)
                ? defaultExpiryDays
                : 90;
        }
    }
}