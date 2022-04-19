﻿using Admin.Models.CheckDigitConfiguration;
using BusinessLogic.Interfaces.Services;
using BusinessLogic.Models.Shared;
using log4net;
using PagedList;
using System;

namespace Admin.Classes.ViewModelBuilders.CheckDigitConfiguration
{
    public class ListViewModelBuilder : BaseViewModelBuilder<ListViewModel, SearchCriteria>
    {
        private readonly ICheckDigitConfigurationService _service;

        public ListViewModelBuilder(ILog log
            , ICheckDigitConfigurationService checkDigitConfigurationService)
            : base(log)
        {
            _service = checkDigitConfigurationService;
        }

        protected override ListViewModel OnBuild()
        {
            var searchCriteria = new SearchCriteria();

            return OnBuild(searchCriteria);
        }

        protected override ListViewModel OnBuild(SearchCriteria searchCriteria)
        {
            var criteria = new BusinessLogic.Models.CheckDigitConfiguration.SearchCriteria()
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

        private StaticPagedList<BusinessLogic.Entities.CheckDigitConfiguration> GetSearchResultAsPagedList(
            SearchResult<BusinessLogic.Entities.CheckDigitConfiguration> searchResult)
        {
            return new StaticPagedList<BusinessLogic.Entities.CheckDigitConfiguration>(
                searchResult.Items,
                searchResult.Page,
                searchResult.PageSize,
                searchResult.Count);
        }
    }
}