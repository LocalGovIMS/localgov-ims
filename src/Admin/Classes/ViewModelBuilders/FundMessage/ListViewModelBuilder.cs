using Admin.Models.FundMessage;
using BusinessLogic.Interfaces.Services;
using BusinessLogic.Models.Shared;
using log4net;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using Web.Mvc;

namespace Admin.Classes.ViewModelBuilders.FundMessage
{
    public class ListViewModelBuilder : BaseViewModelBuilder<ListViewModel, SearchCriteria>
    {
        private readonly IFundMessageService _fundMessageService;
        private readonly IFundService _fundService;
        
        public ListViewModelBuilder(ILog log
            , IFundMessageService fundMessageService
            , IFundService fundService)
            : base(log)
        {
            _fundMessageService = fundMessageService;
            _fundService = fundService;
        }

        protected override ListViewModel OnBuild()
        {            
            var criteria = new BusinessLogic.Models.FundMessage.SearchCriteria();

            var searchResult = _fundMessageService.Search(criteria);

            var searchCriteria = new SearchCriteria
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

        protected override ListViewModel OnBuild(SearchCriteria criteria)
        {
            var searchCriteria = new BusinessLogic.Models.FundMessage.SearchCriteria()
            {
                FundCode = criteria.FundCode,
                Message = criteria.Message,
                Page = criteria.Page,
                PageSize = 20
            };

            var searchResult = _fundMessageService.Search(searchCriteria);

            criteria.Funds = GetFundsList();

            return new ListViewModel()
            {
                Items = GetSearchResultAsPagedList(searchResult),
                SearchCriteria = criteria,
                Count = searchResult.Count,
                Pages = (int)Math.Ceiling((double)searchResult.Count / searchResult.PageSize),
                Page = searchResult.Page
            };
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

        private StaticPagedList<BusinessLogic.Entities.FundMessage> GetSearchResultAsPagedList(
            SearchResult<BusinessLogic.Entities.FundMessage> searchResult)
        {
            return new StaticPagedList<BusinessLogic.Entities.FundMessage>(
                searchResult.Items,
                searchResult.Page,
                searchResult.PageSize,
                searchResult.Count);
        }
    }
}