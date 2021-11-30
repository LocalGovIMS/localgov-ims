using Admin.Models.Fund;
using BusinessLogic.Interfaces.Services;
using BusinessLogic.Models.Shared;
using log4net;
using PagedList;
using System;

namespace Admin.Classes.ViewModelBuilders.Fund
{
    public class ListViewModelBuilder : BaseViewModelBuilder<ListViewModel, SearchCriteria>
    {
        private readonly IFundService _fundService;

        public ListViewModelBuilder(ILog log
            , IFundService fundService)
            : base(log)
        {
            _fundService = fundService;
        }

        protected override ListViewModel OnBuild()
        {
            var searchCriteria = new SearchCriteria();
            var criteria = new BusinessLogic.Models.Fund.SearchCriteria();
            var searchResult = _fundService.Search(criteria);

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
            var criteria = new BusinessLogic.Models.Fund.SearchCriteria()
            {
                FundCode = searchCriteria.FundCode,
                FundName = searchCriteria.FundName,
                Page = searchCriteria.Page,
                PageSize = 20
            };

            var searchResult = _fundService.Search(criteria);

            return new ListViewModel()
            {
                Items = GetSearchResultAsPagedList(searchResult),
                SearchCriteria = searchCriteria,
                Count = searchResult.Count,
                Pages = (int)Math.Ceiling((double)searchResult.Count / searchResult.PageSize),
                Page = searchResult.Page
            };
        }

        private StaticPagedList<BusinessLogic.Entities.Fund> GetSearchResultAsPagedList(
            SearchResult<BusinessLogic.Entities.Fund> searchResult)
        {
            return new StaticPagedList<BusinessLogic.Entities.Fund>(
                searchResult.Items,
                searchResult.Page,
                searchResult.PageSize,
                searchResult.Count);
        }
    }
}