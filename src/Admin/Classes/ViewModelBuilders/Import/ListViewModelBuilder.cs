using Admin.Models.Import;
using BusinessLogic.Interfaces.Services;
using BusinessLogic.Models.Shared;
using log4net;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc.Html;
using Web.Mvc;

namespace Admin.Classes.ViewModelBuilders.Import
{
    public class ListViewModelBuilder : BaseViewModelBuilder<ListViewModel, SearchCriteria>
    {
        private readonly IImportService _ImportService;
        private readonly IImportTypeService _ImportTypeService;
        public ListViewModelBuilder(ILog log
            , IImportService ImportService
            , IImportTypeService ImportTypeService)
            : base(log)
        {
            _ImportService = ImportService;
            _ImportTypeService = ImportTypeService;
        }

        protected override ListViewModel OnBuild()
        {
            return OnBuild(new SearchCriteria() { Page = 1 });
        }

        protected override ListViewModel OnBuild(SearchCriteria criteria)
        {
            var searchCriteria = new BusinessLogic.Models.Import.SearchCriteria()
            {
                DataType = criteria.DataType,
                ImportTypeId = criteria.ImportTypeId,
                StatusId = criteria.StatusId,
                StartDate = criteria.StartDate,
                EndDate = criteria.EndDate,
                Page = criteria.Page,
                PageSize = criteria.PageSize
            };

            var searchResult = _ImportService.Search(searchCriteria);

            criteria.DataTypes = GetDataTypes();
            criteria.ImportTypes = GetImportTypeList();
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

        private SelectList GetDataTypes()
        {
            return new SelectList(EnumHelper.GetSelectList(typeof(BusinessLogic.Enums.ImportDataTypeEnum)), false);
        }

        private SelectList GetImportTypeList()
        {
            var selectListItems = new List<SelectListItem>();
            var items = _ImportTypeService.GetAll().OrderBy(x => x.Name);

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
            return new SelectList(EnumHelper.GetSelectList(typeof(BusinessLogic.Enums.ImportStatusEnum)), false);
        }

        private StaticPagedList<BusinessLogic.Entities.Import> GetSearchResultAsPagedList(
            SearchResult<BusinessLogic.Entities.Import> searchResult)
        {
            return new StaticPagedList<BusinessLogic.Entities.Import>(
                searchResult.Items.OrderByDescending(x => x.CreatedDate),
                searchResult.Page,
                searchResult.PageSize,
                searchResult.Count);
        }
    }
}