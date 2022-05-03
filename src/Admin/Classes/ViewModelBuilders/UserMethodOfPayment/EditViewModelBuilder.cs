using Admin.Models.UserMethodOfPayment;
using BusinessLogic.Interfaces.Services;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using Web.Mvc;

namespace Admin.Classes.ViewModelBuilders.UserMethodOfPayment
{
    public class EditViewModelBuilder : BaseViewModelBuilder<EditViewModel, int>
    {
        private readonly IUserService _userService;
        private readonly IMethodOfPaymentService _methodOfPaymentService;
        private readonly IUserMethodOfPaymentService _userMethodOfPaymentService;

        public EditViewModelBuilder(ILog log
            , IUserService userService
            , IMethodOfPaymentService methodOfPaymentService
            , IUserMethodOfPaymentService userMethodOfPaymentService)
            : base(log)
        {
            _userService = userService;
            _methodOfPaymentService = methodOfPaymentService;
            _userMethodOfPaymentService = userMethodOfPaymentService;
        }

        protected override EditViewModel OnBuild()
        {
            throw new NotImplementedException();
        }

        protected override EditViewModel OnBuild(int id)
        {
            var user = _userService.GetUser(id);
            var userMopCodes = _userMethodOfPaymentService.GetByUserId(id);

            var model = new EditViewModel();

            if (user == null) return model;
            if (userMopCodes == null) return model;

            model.UserId = user.UserId;
            model.UserName = user.UserName;
            model.MopCodes = GetMethodOfPayments(userMopCodes);

            return model;
        }

        private ICollection<CheckBoxListItem> GetMethodOfPayments(List<BusinessLogic.Entities.UserMethodOfPayment> existingItems)
        {
            var allItems = _methodOfPaymentService.GetAllMops().OrderBy(x => x.MopName);

            return allItems.Select(x => new CheckBoxListItem()
            {
                Id = x.MopCode.ToString(),
                Text = x.MopName,
                IsChecked = existingItems.Any(y => y.MopCode == x.MopCode)
            })
                .ToList();
        }
    }
}