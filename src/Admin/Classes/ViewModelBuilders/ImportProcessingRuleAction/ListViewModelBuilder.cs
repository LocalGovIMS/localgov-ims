using Admin.Models.ImportProcessingRuleAction;
using BusinessLogic.Interfaces.Services;
using BusinessLogic.Models.Shared;
using log4net;
using PagedList;
using System;

namespace Admin.Classes.ViewModelBuilders.ImportProcessingRuleAction
{
    public class ListViewModelBuilder : BaseViewModelBuilder<ListViewModel, SearchCriteria>
    {
        private readonly IImportProcessingRuleActionService _service;

        public ListViewModelBuilder(ILog log
            , IImportProcessingRuleActionService importProcessingRuleActionService)
            : base(log)
        {
            _service = importProcessingRuleActionService;
        }

        protected override ListViewModel OnBuild()
        {
            return OnBuild(new SearchCriteria());
        }

        protected override ListViewModel OnBuild(SearchCriteria searchCriteria)
        {
            var criteria = new BusinessLogic.Models.ImportProcessingRuleAction.SearchCriteria()
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

        private StaticPagedList<BusinessLogic.Entities.ImportProcessingRuleAction> GetSearchResultAsPagedList(
            SearchResult<BusinessLogic.Entities.ImportProcessingRuleAction> searchResult)
        {
            return new StaticPagedList<BusinessLogic.Entities.ImportProcessingRuleAction>(
                searchResult.Items,
                searchResult.Page,
                searchResult.PageSize,
                searchResult.Count);
        }
    }
}