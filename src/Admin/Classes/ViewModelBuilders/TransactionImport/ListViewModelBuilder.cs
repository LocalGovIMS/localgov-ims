using Admin.Models.TransactionImport;
using BusinessLogic.Interfaces.Services;
using BusinessLogic.Models.Shared;
using log4net;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc.Html;
using Web.Mvc;

namespace Admin.Classes.ViewModelBuilders.TransactionImport
{
    public class ListViewModelBuilder : BaseViewModelBuilder<ListViewModel, SearchCriteria>
    {
        private readonly ITransactionImportService _transactionImportService;
        private readonly ITransactionImportTypeService _transactionImportTypeService;
        public ListViewModelBuilder(ILog log
            , ITransactionImportService transactionImportService
            , ITransactionImportTypeService transactionImportTypeService)
            : base(log)
        {
            _transactionImportService = transactionImportService;
            _transactionImportTypeService = transactionImportTypeService;
        }

        protected override ListViewModel OnBuild()
        {
            return OnBuild(new SearchCriteria() { Page = 1 });
        }

        protected override ListViewModel OnBuild(SearchCriteria criteria)
        {
            var searchCriteria = new BusinessLogic.Models.TransactionImport.SearchCriteria()
            {
                TransactionImportTypeId = criteria.TransactionImportTypeId,
                StatusId = criteria.StatusId,
                StartDate = criteria.StartDate,
                EndDate = criteria.EndDate,
                Page = criteria.Page,
                PageSize = criteria.PageSize
            };

            var searchResult = _transactionImportService.Search(searchCriteria);

            criteria.TransactionImportTypes = GetTransactionTypeList();
            criteria.Statuses = GetStatuses();

            return new ListViewModel()
            {
                Items = GetSearchResultAsPagedList(searchResult),
                SearchCriteria = criteria,
                Count = searchResult.Count,
                Pages = (int)Math.Ceiling((double)searchResult.Count / searchResult.PageSize),
                Page = searchResult.Page
            };
        }

        private SelectList GetTransactionTypeList()
        {
            var selectListItems = new List<SelectListItem>();
            var items = _transactionImportTypeService.GetAll().OrderBy(x => x.Name);

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

        private SelectList GetStatuses()
        {
            return new SelectList(EnumHelper.GetSelectList(typeof(BusinessLogic.Enums.TransactionImportStatusEnum)), false);
        }

        private StaticPagedList<BusinessLogic.Entities.TransactionImport> GetSearchResultAsPagedList(
            SearchResult<BusinessLogic.Entities.TransactionImport> searchResult)
        {
            return new StaticPagedList<BusinessLogic.Entities.TransactionImport>(
                searchResult.Items.OrderByDescending(x => x.CreatedDate),
                searchResult.Page,
                searchResult.PageSize,
                searchResult.Count);
        }
    }
}