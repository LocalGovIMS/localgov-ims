using Admin.Models.EReturn;
using BusinessLogic.Interfaces.Services;
using BusinessLogic.Models;
using BusinessLogic.Models.Shared;
using log4net;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using SelectList = Web.Mvc.SelectList;

namespace Admin.Classes.ViewModelBuilders.EReturn
{
    public class ListViewModelBuilder : BaseViewModelBuilder<ListViewModel, SearchCriteria>
    {
        private readonly IEReturnService _eReturnService;
        private readonly IEReturnStatusService _eReturnStatusService;
        private readonly IEReturnTypeService _eReturnTypeService;
        private readonly ITemplateService _templateService;

        public ListViewModelBuilder(ILog log
            , IEReturnService eReturnService
            , IEReturnStatusService eReturnStatusService
            , IEReturnTypeService eReturnTypeService
            , ITemplateService templateService
            ) : base(log)
        {
            _eReturnService = eReturnService;
            _eReturnStatusService = eReturnStatusService;
            _eReturnTypeService = eReturnTypeService;
            _templateService = templateService;
        }

        protected override ListViewModel OnBuild()
        {
            var searchCriteria = new SearchCriteria()
            {
                Statuses = GetStatusList(),
                Types = GetTypeList(),
                Templates = GetTemplateList()
            };

            var criteria = new BusinessLogic.Models.EReturns.SearchCriteria();

            var eReturnSearchResult = _eReturnService.SearchTransactions(criteria);

            return new ListViewModel()
            {
                EReturns = GetSearchResultAsPagedList(eReturnSearchResult),
                SearchCriteria = searchCriteria,
                Count = eReturnSearchResult.Count,
                Pages = (int)Math.Ceiling((double)eReturnSearchResult.Count / eReturnSearchResult.PageSize),
                Page = eReturnSearchResult.Page
            };
        }

        protected override ListViewModel OnBuild(SearchCriteria criteria)
        {
            var searchCriteria = new BusinessLogic.Models.EReturns.SearchCriteria()
            {
                Amount = criteria.Amount,
                StartDate = criteria.StartDate,
                EndDate = criteria.EndDate,
                EReturnNumber = criteria.Reference,
                StatusId = criteria.StatusId,
                Type = criteria.EReturnType,
                TemplateId = criteria.TemplateId,
                Page = criteria.Page,
                PageSize = 20
            };

            var eReturnSearchResult = _eReturnService.SearchTransactions(searchCriteria);

            criteria.Statuses = GetStatusList();
            criteria.Types = GetTypeList();
            criteria.Templates = GetTemplateList();

            return new ListViewModel()
            {
                EReturns = GetSearchResultAsPagedList(eReturnSearchResult),
                SearchCriteria = criteria,
                Count = eReturnSearchResult.Count,
                Pages = (int)Math.Ceiling((double)eReturnSearchResult.Count / eReturnSearchResult.PageSize),
                Page = eReturnSearchResult.Page
            };
        }

        private StaticPagedList<EReturnWrapper> GetSearchResultAsPagedList(
            SearchResult<EReturnWrapper> searchResult)
        {
            return new StaticPagedList<EReturnWrapper>(
                searchResult.Items,
                searchResult.Page,
                searchResult.PageSize,
                searchResult.Count);
        }

        private SelectList GetStatusList()
        {
            var selectListItems = new List<SelectListItem>();
            var items = _eReturnStatusService.GetAllEReturnStatuses().OrderBy(x => x.Id);

            foreach (var item in items)
            {
                selectListItems.Add(new SelectListItem()
                {
                    Value = item.Id.ToString(),
                    Text = item.DisplayName
                });
            }

            return new SelectList(selectListItems, true);
        }

        private SelectList GetTypeList()
        {
            var selectListItems = new List<SelectListItem>();
            var items = _eReturnTypeService.GetAllEReturnTypes().OrderBy(x => x.DisplayName);

            foreach (var item in items)
            {
                selectListItems.Add(new SelectListItem()
                {
                    Value = item.Id.ToString(),
                    Text = item.DisplayName
                });
            }

            return new SelectList(selectListItems, true);
        }

        private SelectList GetTemplateList()
        {
            var selectListItems = new List<SelectListItem>();
            var items = _templateService.GetAllTemplates().OrderBy(x => x.Name);

            foreach (var item in items)
            {
                selectListItems.Add(new SelectListItem()
                {
                    Value = item.Id.ToString(),
                    Text = item.Name
                });
            }

            return new SelectList(selectListItems, true);
        }
    }
}