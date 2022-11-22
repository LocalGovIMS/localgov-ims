using Admin.Models.EReturnTemplate;
using BusinessLogic.Interfaces.Services;
using BusinessLogic.Models.Shared;
using log4net;
using PagedList;
using System;

namespace Admin.Classes.ViewModelBuilders.EReturnTemplate
{
    public class ListViewModelBuilder : BaseViewModelBuilder<ListViewModel, SearchCriteria>
    {
        private readonly IEReturnTemplateService _service;

        public ListViewModelBuilder(ILog log
            , IEReturnTemplateService service)
            : base(log)
        {
            _service = service;
        }

        protected override ListViewModel OnBuild()
        {
            var searchCriteria = new SearchCriteria();
            var criteria = new BusinessLogic.Models.EReturnTemplate.SearchCriteria();
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

        protected override ListViewModel OnBuild(SearchCriteria searchCriteria)
        {
            var criteria = new BusinessLogic.Models.EReturnTemplate.SearchCriteria()
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

        private StaticPagedList<BusinessLogic.Entities.Template> GetSearchResultAsPagedList(
            SearchResult<BusinessLogic.Entities.Template> searchResult)
        {
            return new StaticPagedList<BusinessLogic.Entities.Template>(
                searchResult.Items,
                searchResult.Page,
                searchResult.PageSize,
                searchResult.Count);
        }
    }
}