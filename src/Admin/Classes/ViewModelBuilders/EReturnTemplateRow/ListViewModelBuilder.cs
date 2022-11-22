using Admin.Models.EReturnTemplateRow;
using BusinessLogic.Interfaces.Services;
using BusinessLogic.Models.Shared;
using log4net;
using PagedList;
using System;
using System.Linq;

namespace Admin.Classes.ViewModelBuilders.EReturnTemplateRow
{
    public class ListViewModelBuilder : BaseViewModelBuilder<ListViewModel, SearchCriteria>
    {
        private readonly IEReturnTemplateRowService _service;

        public ListViewModelBuilder(ILog log
            , IEReturnTemplateRowService service)
            : base(log)
        {
            _service = service;
        }

        protected override ListViewModel OnBuild()
        {
            return OnBuild(new SearchCriteria());
        }

        protected override ListViewModel OnBuild(SearchCriteria searchCriteria)
        {
            var criteria = new BusinessLogic.Models.EReturnTemplateRow.SearchCriteria()
            {
                EReturnTemplateId = searchCriteria.EReturnTemplateId,
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

        private StaticPagedList<DetailsViewModel> GetSearchResultAsPagedList(
            SearchResult<BusinessLogic.Entities.TemplateRow> searchResult)
        {
            return new StaticPagedList<DetailsViewModel>(
                searchResult.Items.Select(x => new DetailsViewModel() 
                { 
                    Id = x.Id,
                    EReturnTemplateId = x.TemplateId,
                    Reference = x.Reference,
                    ReferenceOverride = x.ReferenceOverride,
                    VatCode = x.VatCode,
                    VatOverride = x.VatOverride,
                    Description = x.Description,
                    DescriptionOverride = x.DescriptionOverride
                }),
                searchResult.Page,
                searchResult.PageSize,
                searchResult.Count);
        }
    }
}