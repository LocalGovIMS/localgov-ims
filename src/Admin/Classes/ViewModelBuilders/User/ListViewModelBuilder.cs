using Admin.Models.User;
using BusinessLogic.Interfaces.Services;
using BusinessLogic.Models.Shared;
using log4net;
using PagedList;
using System;

namespace Admin.Classes.ViewModelBuilders.User
{
    public class ListViewModelBuilder : BaseViewModelBuilder<ListViewModel, SearchCriteria>
    {
        private readonly IUserService _userService;

        public ListViewModelBuilder(ILog log
            , IUserService userService)
            : base(log)
        {
            _userService = userService;
        }

        protected override ListViewModel OnBuild()
        {
            var searchCriteria = new SearchCriteria();
            var criteria = new BusinessLogic.Models.User.SearchCriteria();
            var searchResult = _userService.Search(criteria);

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
            var criteria = new BusinessLogic.Models.User.SearchCriteria()
            {
                UserName = searchCriteria.UserName,
                DisplayName = searchCriteria.DisplayName,
                Page = searchCriteria.Page,
                PageSize = 20
            };

            var searchResult = _userService.Search(criteria);

            return new ListViewModel()
            {
                Items = GetSearchResultAsPagedList(searchResult),
                SearchCriteria = searchCriteria,
                Count = searchResult.Count,
                Pages = (int)Math.Ceiling((double)searchResult.Count / searchResult.PageSize),
                Page = searchResult.Page
            };
        }

        private StaticPagedList<BusinessLogic.Entities.User> GetSearchResultAsPagedList(
            SearchResult<BusinessLogic.Entities.User> searchResult)
        {
            return new StaticPagedList<BusinessLogic.Entities.User>(
                searchResult.Items,
                searchResult.Page,
                searchResult.PageSize,
                searchResult.Count);
        }
    }
}