using Admin.Models.FundGroup;
using BusinessLogic.Entities;
using BusinessLogic.Interfaces.Services;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using Web.Mvc;

namespace Admin.Classes.ViewModelBuilders.FundGroup
{
    public class EditViewModelBuilder : BaseViewModelBuilder<EditViewModel, int>
    {
        private readonly IFundGroupService _fundGroupService;
        private readonly IFundService _fundService;

        public EditViewModelBuilder(ILog log
            , IFundGroupService fundGroupService
            , IFundService fundService)
            : base(log)
        {
            _fundGroupService = fundGroupService;
            _fundService = fundService;
        }

        protected override EditViewModel OnBuild()
        {
            throw new NotImplementedException();
        }

        protected override EditViewModel OnBuild(int id)
        {
            var data = _fundGroupService.GetFundGroup(id);
            var model = new EditViewModel();

            if (data == null) return model;

            model.Id = data.FundGroupId;
            model.FundGroupName = data.Name;
            model.Funds = GetFunds(data.FundGroupFunds.ToList());

            return model;
        }

        private ICollection<CheckBoxListItem> GetFunds(List<FundGroupFund> existingItems)
        {
            var allItems = _fundService.GetAllFunds(true);

            return allItems.Select(x => new CheckBoxListItem()
            {
                Id = x.FundCode,
                Text = string.Format("{0} {1}", x.FundName, x.Disabled ? "(Disabled)" : string.Empty),
                IsChecked = existingItems == null ? false : existingItems.Any(y => y.FundCode == x.FundCode)
            })
                .ToList();
        }
    }
}