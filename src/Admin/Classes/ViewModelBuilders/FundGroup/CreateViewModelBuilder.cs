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
    public class CreateViewModelBuilder : BaseViewModelBuilder<EditViewModel, int>
    {
        private readonly IFundService _fundService;

        public CreateViewModelBuilder(ILog log
            , IFundService fundService)
            : base(log)
        {
            _fundService = fundService;
        }

        protected override EditViewModel OnBuild()
        {
            var model = new EditViewModel();

            model.Funds = GetFunds(null);

            return model;
        }

        protected override EditViewModel OnBuild(int id)
        {
            throw new NotImplementedException();
        }

        private ICollection<CheckBoxListItem> GetFunds(List<FundGroupFund> existingItems)
        {
            var allItems = _fundService.GetAllFunds();

            return allItems.Select(x => new CheckBoxListItem()
            {
                Id = x.FundCode,
                Text = x.FundName,
                IsChecked = existingItems == null ? false : existingItems.Any(y => y.FundCode == x.FundCode)
            })
                .ToList();
        }
    }
}