﻿using Admin.Models.Shared;
using PagedList;

namespace Admin.Models.TransactionImportTypeImportProcessingRule
{
    public class ListViewModel
    {
        public StaticPagedList<BusinessLogic.Entities.TransactionImportTypeImportProcessingRule> Items { get; set; }
        public int Count { get; set; }
        public int Pages { get; set; }
        public int Page { get; set; }
        public SearchCriteria SearchCriteria { get; set; }
    }
}