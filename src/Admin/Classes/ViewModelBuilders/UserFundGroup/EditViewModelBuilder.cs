using Admin.Models.UserFundGroup;
using BusinessLogic.Interfaces.Services;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using Web.Mvc;

namespace Admin.Classes.ViewModelBuilders.UserFundGroup
{
    public class EditViewModelBuilder : BaseViewModelBuilder<EditViewModel, int>
    {
        private readonly IUserService _userService;
        private readonly IFundGroupService _fundGroupService;
        private readonly IUserFundGroupService _userFundGroupService;

        public EditViewModelBuilder(ILog log
            , IUserService userService
            , IFundGroupService fundGroupService
            , IUserFundGroupService userFundGroupService)
            : base(log)
        {
            _userService = userService;
            _fundGroupService = fundGroupService;
            _userFundGroupService = userFundGroupService;
        }

        protected override EditViewModel OnBuild()
        {
            throw new NotImplementedException();
        }

        protected override EditViewModel OnBuild(int id)
        {
            var user = _userService.GetUser(id);
            var items = _userFundGroupService.GetUserFundGroups(id);

            var model = new EditViewModel();

            if (user == null) return model;
            if (items == null) return model;

            model.UserId = user.UserId;
            model.UserName = user.UserName;
            model.FundGroups = GetUserFundGroups(items);

            return model;
        }

        private ICollection<CheckBoxListItem> GetUserFundGroups(List<BusinessLogic.Entities.UserFundGroup> existingItems)
        {
            var allItems = _fundGroupService.GetAllFundGroups();

            return allItems.Select(x => new CheckBoxListItem()
            {
                Id = x.FundGroupId.ToString(),
                Text = x.Name,
                IsChecked = existingItems.Any(y => y.FundGroupId == x.FundGroupId)
            })
                .ToList();
        }
    }
}