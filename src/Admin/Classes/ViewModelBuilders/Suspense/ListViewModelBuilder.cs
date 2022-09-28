using Admin.Models.Suspense;
using BusinessLogic.Interfaces.Services;
using BusinessLogic.Models.Shared;
using log4net;
using PagedList;
using System;

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
            var searchCriteria = new SearchCriteria()
                { Status = BusinessLogic.Enums.SuspenseAllocationStatusEnum.Unallocated };
            var criteria = new BusinessLogic.Models.Suspense.SearchCriteria()
                { Status = BusinessLogic.Enums.SuspenseAllocationStatusEnum.Unallocated };
            var searchResult = _suspenseService.Search(criteria);

            return new ListViewModel()
            {
                Items = GetSearchResultAsPagedList(searchResult),
                SearchCriteria = searchCriteria,
                Count = searchResult.Count,
                Pages = (int)Math.Ceiling((double)searchResult.Count / searchResult.PageSize),
                Page = searchResult.Page
            };
        }

        protected override ListViewModel OnBuild(SearchCriteria searchCriteria)
        {
            var criteria = new BusinessLogic.Models.Suspense.SearchCriteria()
            {
                CreatedAtDateFrom = searchCriteria.TransactionDateFrom,
                CreatedAtDateTo = searchCriteria.TransactionDateTo,
                AccountNumber = searchCriteria.AccountNumber,
                Amount = searchCriteria.Amount,
                Narrative = searchCriteria.Narrative,
                Status = searchCriteria.Status,
                Page = searchCriteria.Page,
                PageSize = 6
            };

            var searchResult = _suspenseService.Search(criteria);

            return new ListViewModel()
            {
                Items = GetSearchResultAsPagedList(searchResult),
                SearchCriteria = searchCriteria,
                Count = searchResult.Count,
                Pages = (int)Math.Ceiling((double)searchResult.Count / searchResult.PageSize),
                Page = searchResult.Page
            };
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