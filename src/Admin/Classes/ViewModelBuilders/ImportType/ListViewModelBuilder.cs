using Admin.Models.ImportType;
using BusinessLogic.Interfaces.Services;
using BusinessLogic.Models.Shared;
using log4net;
using PagedList;
using System;
using System.Web.Mvc.Html;
using Web.Mvc;

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
            var criteria = new BusinessLogic.Models.ImportType.SearchCriteria();
            var searchResult = _importTypeService.Search(criteria);
            var searchCriteria = new SearchCriteria()
            {
                ImportTypes = GetImportTypes()
            };

            return new ListViewModel()
            {
                Items = GetSearchResultAsPagedList(searchResult),
                SearchCriteria = searchCriteria,
                Count = searchResult.Count,
                Pages = (int)Math.Ceiling((double)searchResult.Count / searchResult.PageSize),
                Page = searchResult.Page
            };
        }

        protected override ListViewModel OnBuild(SearchCriteria criteria)
        {
            var searchCriteria = new BusinessLogic.Models.ImportType.SearchCriteria()
            {
                DataType = criteria.DataType,
                Name = criteria.Name,
                ExternalReference = criteria.ExternalReference,
                Page = criteria.Page,
                PageSize = 20
            };

            var searchResult = _importTypeService.Search(searchCriteria);

            criteria.ImportTypes = GetImportTypes();

            return new ListViewModel()
            {
                Items = GetSearchResultAsPagedList(searchResult),
                SearchCriteria = criteria,
                Count = searchResult.Count,
                Pages = (int)Math.Ceiling((double)searchResult.Count / searchResult.PageSize),
                Page = searchResult.Page
            };
        }

        private SelectList GetImportTypes()
        {
            return new SelectList(EnumHelper.GetSelectList(typeof(BusinessLogic.Enums.ImportDataTypeEnum)), false);
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