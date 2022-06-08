using Admin.Models.TransactionImportType;
using BusinessLogic.Interfaces.Services;
using BusinessLogic.Models.Shared;
using log4net;
using PagedList;
using System;

namespace Admin.Classes.ViewModelBuilders.TransactionImportType
{
    public class ListViewModelBuilder : BaseViewModelBuilder<ListViewModel, SearchCriteria>
    {
        private readonly ITransactionImportTypeService _transactionImportTypeService;

        public ListViewModelBuilder(ILog log
            , ITransactionImportTypeService transactionImportTypeService)
            : base(log)
        {
            _transactionImportTypeService = transactionImportTypeService;
        }

        protected override ListViewModel OnBuild()
        {
            var searchCriteria = new SearchCriteria();
            var criteria = new BusinessLogic.Models.TransactionImportType.SearchCriteria();
            var searchResult = _transactionImportTypeService.Search(criteria);

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
            var criteria = new BusinessLogic.Models.TransactionImportType.SearchCriteria()
            {
                Name = searchCriteria.Name,
                ExternalReference = searchCriteria.ExternalReference,
                Page = searchCriteria.Page,
                PageSize = 20
            };

            var searchResult = _transactionImportTypeService.Search(criteria);

            return new ListViewModel()
            {
                Items = GetSearchResultAsPagedList(searchResult),
                SearchCriteria = searchCriteria,
                Count = searchResult.Count,
                Pages = (int)Math.Ceiling((double)searchResult.Count / searchResult.PageSize),
                Page = searchResult.Page
            };
        }

        private StaticPagedList<BusinessLogic.Entities.TransactionImportType> GetSearchResultAsPagedList(
            SearchResult<BusinessLogic.Entities.TransactionImportType> searchResult)
        {
            return new StaticPagedList<BusinessLogic.Entities.TransactionImportType>(
                searchResult.Items,
                searchResult.Page,
                searchResult.PageSize,
                searchResult.Count);
        }
    }
}