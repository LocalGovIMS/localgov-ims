using Admin.Models.VatMetadata;
using Admin.Models.Shared;
using BusinessLogic.Interfaces.Services;
using BusinessLogic.Models.Shared;
using log4net;
using PagedList;
using System;
using System.Linq;

namespace Admin.Classes.ViewModelBuilders.VatMetadata
{
    public class ListViewModelBuilder : BaseViewModelBuilder<ListViewModel, SearchCriteria>
    {
        private readonly IVatMetadataService _service;

        public ListViewModelBuilder(ILog log
            , IVatMetadataService fundMetadataService)
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
            var criteria = new BusinessLogic.Models.VatMetadata.SearchCriteria()
            {
                VatCode = searchCriteria.VatCode,
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
            SearchResult<BusinessLogic.Entities.VatMetadata> searchResult)
        {
            return new StaticPagedList<MetadataViewModel>(
                searchResult.Items.Select(x => new MetadataViewModel() 
                { 
                    Id = x.Id,
                    ParentCode = x.VatCode,
                    Key = x.Key,
                    Value = x.Value,
                    Description = _service.GetMetadata().FirstOrDefault(y => y.Key == x.Key)?.Description ?? "Unknown"
                }),
                searchResult.Page,
                searchResult.PageSize,
                searchResult.Count);
        }
    }
}