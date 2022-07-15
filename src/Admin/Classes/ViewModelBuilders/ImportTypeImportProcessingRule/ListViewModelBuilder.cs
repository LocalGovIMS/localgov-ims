using Admin.Models.ImportTypeImportProcessingRule;
using BusinessLogic.Interfaces.Services;
using BusinessLogic.Models.Shared;
using log4net;
using PagedList;
using System;
using System.Linq;

namespace Admin.Classes.ViewModelBuilders.ImportTypeImportProcessingRule
{
    public class ListViewModelBuilder : BaseViewModelBuilder<ListViewModel, SearchCriteria>
    {
        private readonly IImportTypeImportProcessingRuleService _importTypeImportProcessingRuleService;

        public ListViewModelBuilder(ILog log
            , IImportTypeImportProcessingRuleService importTypeImportProcessingRuleService)
            : base(log)
        {
            _importTypeImportProcessingRuleService = importTypeImportProcessingRuleService;
        }

        protected override ListViewModel OnBuild()
        {
            return OnBuild(new SearchCriteria());
        }

        protected override ListViewModel OnBuild(SearchCriteria searchCriteria)
        {
            var criteria = new BusinessLogic.Models.ImportTypeImportProcessingRule.SearchCriteria()
            {
                ImportTypeId = searchCriteria.ImportTypeId,
                Page = searchCriteria.Page,
                PageSize = 20
            };

            var searchResult = _importTypeImportProcessingRuleService.Search(criteria);

            return new ListViewModel()
            {
                Items = GetSearchResultAsPagedList(searchResult),
                SearchCriteria = searchCriteria,
                Count = searchResult.Count,
                Pages = (int)Math.Ceiling((double)searchResult.Count / searchResult.PageSize),
                Page = searchResult.Page
            };
        }

        private StaticPagedList<BusinessLogic.Entities.ImportTypeImportProcessingRule> GetSearchResultAsPagedList(
            SearchResult<BusinessLogic.Entities.ImportTypeImportProcessingRule> searchResult)
        {
            return new StaticPagedList<BusinessLogic.Entities.ImportTypeImportProcessingRule>(
                searchResult.Items.OrderBy(x => x.ImportProcessingRule.Name),
                searchResult.Page,
                searchResult.PageSize,
                searchResult.Count);
        }
    }
}