using Admin.Models.ImportType;
using BusinessLogic.Interfaces.Services;
using BusinessLogic.Models.Shared;
using log4net;
using PagedList;
using System;

namespace Admin.Classes.ViewModelBuilders.ImportType
{
    public class ListViewModelBuilder : BaseViewModelBuilder<ListViewModel, SearchCriteria>
    {
        private readonly IImportTypeService _importTypeService;

        public ListViewModelBuilder(ILog log
            , IImportTypeService importTypeService)
            : base(log)
        {
            _importTypeService = importTypeService;
        }

        protected override ListViewModel OnBuild()
        {
            var searchCriteria = new SearchCriteria();
            var criteria = new BusinessLogic.Models.ImportType.SearchCriteria();
            var searchResult = _importTypeService.Search(criteria);

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
            var criteria = new BusinessLogic.Models.ImportType.SearchCriteria()
            {
                DataType = searchCriteria.DataType,
                Name = searchCriteria.Name,
                ExternalReference = searchCriteria.ExternalReference,
                Page = searchCriteria.Page,
                PageSize = 20
            };

            var searchResult = _importTypeService.Search(criteria);

            return new ListViewModel()
            {
                Items = GetSearchResultAsPagedList(searchResult),
                SearchCriteria = searchCriteria,
                Count = searchResult.Count,
                Pages = (int)Math.Ceiling((double)searchResult.Count / searchResult.PageSize),
                Page = searchResult.Page
            };
        }

        private StaticPagedList<BusinessLogic.Entities.ImportType> GetSearchResultAsPagedList(
            SearchResult<BusinessLogic.Entities.ImportType> searchResult)
        {
            return new StaticPagedList<BusinessLogic.Entities.ImportType>(
                searchResult.Items,
                searchResult.Page,
                searchResult.PageSize,
                searchResult.Count);
        }
    }
}