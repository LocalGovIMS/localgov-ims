using Admin.Models.MetadataKey;
using BusinessLogic.Interfaces.Services;
using BusinessLogic.Models.Shared;
using log4net;
using PagedList;
using System;
using System.Web.Mvc.Html;
using Web.Mvc;

namespace Admin.Classes.ViewModelBuilders.MetadataKey
{
    public class ListViewModelBuilder : BaseViewModelBuilder<ListViewModel, SearchCriteria>
    {
        private readonly IMetadataKeyService _metadataKeyService;
        
        public ListViewModelBuilder(ILog log
            , IMetadataKeyService metadataKeyService)
            : base(log)
        {
            _metadataKeyService = metadataKeyService;
        }

        protected override ListViewModel OnBuild()
        {            
            var criteria = new BusinessLogic.Models.MetadataKey.SearchCriteria();

            var searchResult = _metadataKeyService.Search(criteria);
            var searchCriteria = new SearchCriteria()
            {
                EntityTypes = GetEntityTypes()
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
            var searchCriteria = new BusinessLogic.Models.MetadataKey.SearchCriteria()
            {
                Name = criteria.Name,
                EntityType = criteria.EntityType,
                Page = criteria.Page,
                PageSize = 20
            };

            var searchResult = _metadataKeyService.Search(searchCriteria);

            criteria.EntityTypes = GetEntityTypes();

            return new ListViewModel()
            {
                Items = GetSearchResultAsPagedList(searchResult),
                SearchCriteria = criteria,
                Count = searchResult.Count,
                Pages = (int)Math.Ceiling((double)searchResult.Count / searchResult.PageSize),
                Page = searchResult.Page
            };
        }

        private SelectList GetEntityTypes()
        {
            return new SelectList(EnumHelper.GetSelectList(typeof(BusinessLogic.Enums.MetadataKeyEntityType)), false);
        }

        private StaticPagedList<BusinessLogic.Entities.MetadataKey> GetSearchResultAsPagedList(
            SearchResult<BusinessLogic.Entities.MetadataKey> searchResult)
        {
            return new StaticPagedList<BusinessLogic.Entities.MetadataKey>(
                searchResult.Items,
                searchResult.Page,
                searchResult.PageSize,
                searchResult.Count);
        }
    }
}