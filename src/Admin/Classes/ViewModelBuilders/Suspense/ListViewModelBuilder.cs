using Admin.Models.Suspense;
using BusinessLogic.Interfaces.Services;
using BusinessLogic.Models.Shared;
using log4net;
using PagedList;
using System;
using System.Web.Mvc.Html;
using Web.Mvc;

namespace Admin.Classes.ViewModelBuilders.Suspense
{
    public class ListViewModelBuilder : BaseViewModelBuilder<ListViewModel, SearchCriteria>
    {
        private readonly ISuspenseService _suspenseService;

        public ListViewModelBuilder(ILog log
            , ISuspenseService suspenseService)
            : base(log)
        {
            _suspenseService = suspenseService;
        }

        protected override ListViewModel OnBuild()
        {            
            var criteria = new BusinessLogic.Models.Suspense.SearchCriteria()
                { Status = BusinessLogic.Enums.SuspenseAllocationStatusEnum.Unallocated };
            var searchResult = _suspenseService.Search(criteria);

            var searchCriteria = new SearchCriteria()
            { 
                Status = BusinessLogic.Enums.SuspenseAllocationStatusEnum.Unallocated,
                Statuses = GetStatuses()
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
            var searchCriteria = new BusinessLogic.Models.Suspense.SearchCriteria()
            {
                CreatedAtDateFrom = criteria.TransactionDateFrom,
                CreatedAtDateTo = criteria.TransactionDateTo,
                AccountNumber = criteria.AccountNumber,
                Amount = criteria.Amount,
                Narrative = criteria.Narrative,
                Status = criteria.Status,
                Page = criteria.Page,
                PageSize = 6
            };

            var searchResult = _suspenseService.Search(searchCriteria);

            criteria.Statuses = GetStatuses();

            return new ListViewModel()
            {
                Items = GetSearchResultAsPagedList(searchResult),
                SearchCriteria = criteria,
                Count = searchResult.Count,
                Pages = (int)Math.Ceiling((double)searchResult.Count / searchResult.PageSize),
                Page = searchResult.Page
            };
        }

        private SelectList GetStatuses()
        {
            return new SelectList(EnumHelper.GetSelectList(typeof(BusinessLogic.Enums.SuspenseAllocationStatusEnum)), false);
        }

        private StaticPagedList<BusinessLogic.Models.Suspense.SuspenseWrapper> GetSearchResultAsPagedList(
            SearchResult<BusinessLogic.Models.Suspense.SuspenseWrapper> searchResult)
        {
            return new StaticPagedList<BusinessLogic.Models.Suspense.SuspenseWrapper>(
                searchResult.Items,
                searchResult.Page,
                searchResult.PageSize,
                searchResult.Count);
        }
    }
}