using Admin.Models.MethodOfPaymentMetadata;
using Admin.Models.Shared;
using BusinessLogic.Interfaces.Services;
using BusinessLogic.Models.Shared;
using log4net;
using PagedList;
using System;
using System.Linq;

namespace Admin.Classes.ViewModelBuilders.MethodOfPaymentMetadata
{
    public class ListViewModelBuilder : BaseViewModelBuilder<ListViewModel, SearchCriteria>
    {
        private readonly IMethodOfPaymentMetadataService _service;

        public ListViewModelBuilder(ILog log
            , IMethodOfPaymentMetadataService methodOfPaymentMetadataService)
            : base(log)
        {
            _service = methodOfPaymentMetadataService;
        }

        protected override ListViewModel OnBuild()
        {
            return OnBuild(new SearchCriteria());
        }

        protected override ListViewModel OnBuild(SearchCriteria searchCriteria)
        {
            var criteria = new BusinessLogic.Models.MethodOfPaymentMetadata.SearchCriteria()
            {
                MopCode = searchCriteria.MopCode,
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

        private StaticPagedList<MetadataViewModel> GetSearchResultAsPagedList(
            SearchResult<BusinessLogic.Entities.MopMetadata> searchResult)
        {
            return new StaticPagedList<MetadataViewModel>(
                searchResult.Items.Select(x => new MetadataViewModel() 
                { 
                    Id = x.Id,
                    ParentCode = x.MopCode,
                    Key = x.MopMetadataKey.Name,
                    Description = x.MopMetadataKey.Description,
                    Value = x.Value
                }),
                searchResult.Page,
                searchResult.PageSize,
                searchResult.Count);
        }
    }
}