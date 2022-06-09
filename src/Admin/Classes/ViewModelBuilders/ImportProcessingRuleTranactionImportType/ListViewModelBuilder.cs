using Admin.Models.ImportProcessingRuleTransactionImportType;
using BusinessLogic.Interfaces.Services;
using BusinessLogic.Models.Shared;
using log4net;
using PagedList;
using System;
using System.Linq;

namespace Admin.Classes.ViewModelBuilders.ImportProcessingRuleTransactionImportType
{
    public class ListViewModelBuilder : BaseViewModelBuilder<ListViewModel, SearchCriteria>
    {
        private readonly ITransactionImportTypeImportProcessingRuleService _service;

        public ListViewModelBuilder(ILog log
            , ITransactionImportTypeImportProcessingRuleService fundMetadataService)
            : base(log)
        {
            _service = fundMetadataService;
        }

        protected override ListViewModel OnBuild()
        {
            return OnBuild(new SearchCriteria());
        }

        protected override ListViewModel OnBuild(SearchCriteria searchCriteria)
        {
            var criteria = new BusinessLogic.Models.TransactionImportTypeImportProcessingRule.SearchCriteria()
            {
                ImportProcessingRuleId = searchCriteria.ImportProcessingRuleId,
                Page = searchCriteria.Page,
                PageSize = 20
            };

            var searchResult = _service.Search(criteria);

            return new ListViewModel()
            {
                Items = GetSearchResultAsPagedList(searchResult),
                SearchCriteria = searchCriteria,
                Count = searchResult.Count,
                Pages = (int)Math.Ceiling((double)searchResult.Count / searchResult.PageSize),
                Page = searchResult.Page
            };
        }

        private StaticPagedList<BusinessLogic.Entities.TransactionImportTypeImportProcessingRule> GetSearchResultAsPagedList(
            SearchResult<BusinessLogic.Entities.TransactionImportTypeImportProcessingRule> searchResult)
        {
            return new StaticPagedList<BusinessLogic.Entities.TransactionImportTypeImportProcessingRule>(
                searchResult.Items.OrderBy(x => x.TransactionImportType.Name),
                searchResult.Page,
                searchResult.PageSize,
                searchResult.Count);
        }
    }
}