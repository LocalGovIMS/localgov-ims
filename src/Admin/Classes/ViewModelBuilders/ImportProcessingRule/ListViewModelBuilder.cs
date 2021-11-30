using Admin.Models.ImportProcessingRule;
using BusinessLogic.Interfaces.Services;
using BusinessLogic.Models.Shared;
using log4net;
using PagedList;
using System;

namespace Admin.Classes.ViewModelBuilders.ImportProcessingRule
{
    public class ListViewModelBuilder : BaseViewModelBuilder<ListViewModel, SearchCriteria>
    {
        private readonly IImportProcessingRuleService _service;

        public ListViewModelBuilder(ILog log
            , IImportProcessingRuleService importProcessingRuleService)
            : base(log)
        {
            _service = importProcessingRuleService;
        }

        protected override ListViewModel OnBuild()
        {
            var searchCriteria = new SearchCriteria();

            return OnBuild(searchCriteria);
        }

        protected override ListViewModel OnBuild(SearchCriteria searchCriteria)
        {
            var criteria = new BusinessLogic.Models.ImportProcessingRule.SearchCriteria()
            {
                Name = searchCriteria.Name,
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

        private StaticPagedList<BusinessLogic.Entities.ImportProcessingRule> GetSearchResultAsPagedList(
            SearchResult<BusinessLogic.Entities.ImportProcessingRule> searchResult)
        {
            return new StaticPagedList<BusinessLogic.Entities.ImportProcessingRule>(
                searchResult.Items,
                searchResult.Page,
                searchResult.PageSize,
                searchResult.Count);
        }
    }
}