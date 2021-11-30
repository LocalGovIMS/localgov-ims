using Admin.Models.ImportProcessingRuleCondition;
using BusinessLogic.Interfaces.Services;
using BusinessLogic.Models.Shared;
using log4net;
using PagedList;
using System;

namespace Admin.Classes.ViewModelBuilders.ImportProcessingRuleCondition
{
    public class ListViewModelBuilder : BaseViewModelBuilder<ListViewModel, SearchCriteria>
    {
        private readonly IImportProcessingRuleConditionService _service;

        public ListViewModelBuilder(ILog log
            , IImportProcessingRuleConditionService importProcessingRuleConditionService)
            : base(log)
        {
            _service = importProcessingRuleConditionService;
        }

        protected override ListViewModel OnBuild()
        {
            return OnBuild(new SearchCriteria());
        }

        protected override ListViewModel OnBuild(SearchCriteria searchCriteria)
        {
            var criteria = new BusinessLogic.Models.ImportProcessingRuleCondition.SearchCriteria()
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

        private StaticPagedList<BusinessLogic.Entities.ImportProcessingRuleCondition> GetSearchResultAsPagedList(
            SearchResult<BusinessLogic.Entities.ImportProcessingRuleCondition> searchResult)
        {
            return new StaticPagedList<BusinessLogic.Entities.ImportProcessingRuleCondition>(
                searchResult.Items,
                searchResult.Page,
                searchResult.PageSize,
                searchResult.Count);
        }
    }
}