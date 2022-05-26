using Admin.Models.FundMetadata;
using Admin.Models.Shared;
using BusinessLogic.Interfaces.Services;
using BusinessLogic.Models.Shared;
using log4net;
using PagedList;
using System;
using System.Linq;

namespace Admin.Classes.ViewModelBuilders.FundMetadata
{
    public class ListViewModelBuilder : BaseViewModelBuilder<ListViewModel, SearchCriteria>
    {
        private readonly IFundMetadataService _service;

        public ListViewModelBuilder(ILog log
            , IFundMetadataService fundMetadataService)
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
            var criteria = new BusinessLogic.Models.FundMetadata.SearchCriteria()
            {
                FundCode = searchCriteria.FundCode,
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
            SearchResult<BusinessLogic.Entities.FundMetadata> searchResult)
        {
            return new StaticPagedList<MetadataViewModel>(
                searchResult.Items.Select(x => new MetadataViewModel() 
                { 
                    Id = x.Id,
                    ParentCode = x.FundCode,
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