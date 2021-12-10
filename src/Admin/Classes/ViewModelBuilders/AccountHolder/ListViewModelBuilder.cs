using Admin.Models.AccountHolder;
using BusinessLogic.Interfaces.Services;
using BusinessLogic.Models.Shared;
using log4net;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using Web.Mvc;

namespace Admin.Classes.ViewModelBuilders.AccountHolder
{
    public class ListViewModelBuilder : BaseViewModelBuilder<ListViewModel, Admin.Models.AccountHolder.SearchCriteria>
    {
        private readonly IAccountHolderService _accountHolderService;
        private readonly IFundService _fundService;

        public ListViewModelBuilder(ILog log
            , IAccountHolderService accountHolderService
            , IFundService fundService
            )
            : base(log)
        {
            _accountHolderService = accountHolderService;
            _fundService = fundService;
        }

        protected override ListViewModel OnBuild()
        {
            var searchResult = _accountHolderService.Search(new BusinessLogic.Models.AccountHolder.SearchCriteria());

            var searchCriteria = new Admin.Models.AccountHolder.SearchCriteria
            {
                Funds = GetFundsList()
            };

            return new ListViewModel()
            {
                Items = GetSearchResultAsPagedList(searchResult),
                SearchCriteria = searchCriteria,
                Count = searchResult.Count,
                Pages = (int)Math.Ceiling((double)searchResult.Count / searchResult.PageSize),
                Page = searchResult.Page
            };
        }

        protected override ListViewModel OnBuild(Admin.Models.AccountHolder.SearchCriteria criteria)
        {
            var searchCriteria = new BusinessLogic.Models.AccountHolder.SearchCriteria()
            {
                AccountReference = criteria.AccountReference,
                HouseNumberName = criteria.HouseNumberName,
                FundCode = criteria.FundCode,
                PostCode = criteria.PostCode,
                Street = criteria.Street,
                Surname = criteria.Surname,
                Page = criteria.Page == 0 ? 1 : criteria.Page,
                PageSize = 20,
            };

            var searchResult = _accountHolderService.Search(searchCriteria);

            criteria.Funds = GetFundsList();

            return new ListViewModel()
            {
                Items = GetSearchResultAsPagedList(searchResult),
                SearchCriteria = criteria,
                Count = searchResult.Count,
                Pages = (int)Math.Ceiling((double)searchResult.Count / searchResult.PageSize),
                Page = searchResult.Page,
                FundName = GetFundName(searchCriteria.FundCode)
            };
        }

        private StaticPagedList<BusinessLogic.Entities.AccountHolder> GetSearchResultAsPagedList(
            SearchResult<BusinessLogic.Entities.AccountHolder> searchResult)
        {
            return new StaticPagedList<BusinessLogic.Entities.AccountHolder>(
                searchResult.Items,
                searchResult.Page,
                searchResult.PageSize,
                searchResult.Count);
        }

        private string GetFundName(string fundCode)
        {
            var fundName = string.Empty;
            if (!string.IsNullOrEmpty(fundCode))
            {
                fundName = _fundService.GetByFundCode(fundCode).FundName;
            }

            return fundName;
        }

        private SelectList GetFundsList()
        {
            var selectListItems = new List<SelectListItem>();
            var items = _fundService.GetAllFunds(true).OrderBy(x => x.FundName);

            foreach (var item in items)
            {
                selectListItems.Add(new SelectListItem()
                {
                    Value = item.FundCode,
                    Text = string.Format("{0} {1}", item.FundName, item.Disabled ? "(Disabled)" : string.Empty)
                });
            }

            return new SelectList(selectListItems, true);
        }
    }
}