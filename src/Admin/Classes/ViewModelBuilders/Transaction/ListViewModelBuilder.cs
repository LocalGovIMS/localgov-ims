using Admin.Models.Transaction;
using BusinessLogic.Entities;
using BusinessLogic.Extensions;
using BusinessLogic.Interfaces.Services;
using BusinessLogic.Models.Shared;
using log4net;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using Web.Mvc;

namespace Admin.Classes.ViewModelBuilders.Transaction
{
    public class ListViewModelBuilder : BaseViewModelBuilder<ListViewModel, SearchCriteria>
    {
        private readonly ITransactionService _transactionService;
        private readonly IFundService _fundService;
        private readonly IMethodOfPaymentService _mopService;
        private readonly IUserService _userService;

        public ListViewModelBuilder(ILog log
            , ITransactionService transactionService
            , IFundService fundService
            , IMethodOfPaymentService mopService
            , IUserService userService)
            : base(log)
        {
            _transactionService = transactionService;
            _fundService = fundService;
            _mopService = mopService;
            _userService = userService;
        }

        protected override ListViewModel OnBuild()
        {
            var transactionCriteria = new BusinessLogic.Models.Transactions.SearchCriteria()
            {
                Page = 1,
                PageSize = 20
            };

            var searchResult = _transactionService.SearchTransactions(transactionCriteria);

            var searchCriteria = new SearchCriteria
            {
                Funds = GetFundsList(),
                Mops = GetMopList(),
                Users = GetUserList()
            };

            return new ListViewModel()
            {
                Transactions = GetSearchResultAsPagedList(searchResult),
                SearchCriteria = searchCriteria,
                Count = searchResult.Count,
                Pages = (int)Math.Ceiling((double)searchResult.Count / searchResult.PageSize),
                Page = searchResult.Page
            };
        }

        protected override ListViewModel OnBuild(SearchCriteria criteria)
        {
            var searchCriteria = new BusinessLogic.Models.Transactions.SearchCriteria()
            {
                AccountReference = criteria.AccountReference,
                AppReference = criteria.AppReference,
                InternalReference = criteria.InternalReference,
                Amount = criteria.Amount,
                FundCodes = criteria.FundCode == null ? null : new[] { criteria.FundCode },
                MopCodes = criteria.MopCode == null ? null : new[] { criteria.MopCode },
                Narrative = criteria.Narrative,
                ReceiptNumber = criteria.ReceiptNumber,
                StartDate = criteria.StartDate,
                EndDate = criteria.EndDate?.ToEndOfDay(),
                UserId = criteria.UserId,
                Page = criteria.Page == 0 ? 1 : criteria.Page,
                PageSize = criteria.PageSize,
                WildSearchAccountReference = criteria.WildSearchAccountReference,
                TransactionImportId = criteria.TransactionImportId,
                CardPrefix = criteria.CardPrefix,
                CardSuffix = criteria.CardSuffix
            };

            var searchResult = _transactionService.SearchTransactions(searchCriteria);

            criteria.Funds = GetFundsList();
            criteria.Mops = GetMopList();
            criteria.Users = GetUserList();

            return new ListViewModel()
            {
                Transactions = GetSearchResultAsPagedList(searchResult),
                SearchCriteria = criteria,
                Count = searchResult.Count,
                Pages = (int)Math.Ceiling((double)searchResult.Count / searchResult.PageSize),
                Page = searchResult.Page
            };
        }

        private SelectList GetFundsList()
        {
            var selectListItems = new List<SelectListItem>();
            var items = _fundService.GetAllFunds(true).OrderBy(x => x.FundName);

            foreach (var item in items)
            {
                selectListItems.Add(new SelectListItem()
                {
                    Value = item.FundCode,
                    Text = string.Format("{0} {1}", item.FundName, item.Disabled ? "(Disabled)" : string.Empty)
                });
            }

            return new SelectList(selectListItems, true);
        }

        private SelectList GetMopList()
        {
            var selectListItems = new List<SelectListItem>();
            var items = _mopService.GetAllMops(true).OrderBy(x => x.MopName);

            foreach (var item in items)
            {
                selectListItems.Add(new SelectListItem()
                {
                    Value = item.MopCode,
                    Text = string.Format("{0} {1}", item.MopName, item.Disabled ? "(Disabled)" : string.Empty)
                });
            }

            return new SelectList(selectListItems, true);
        }

        private SelectList GetUserList()
        {
            var selectListItems = new List<SelectListItem>();
            var items = _userService.GetAllUsers().OrderBy(x => x.DisplayName);

            foreach (var item in items)
            {
                selectListItems.Add(new SelectListItem()
                {
                    Value = item.UserId.ToString(),
                    Text = item.DisplayName
                });
            }

            return new SelectList(selectListItems, true);
        }

        private StaticPagedList<ProcessedTransaction> GetSearchResultAsPagedList(
            SearchResult<ProcessedTransaction> searchResult)
        {
            return new StaticPagedList<ProcessedTransaction>(
                searchResult.Items,
                searchResult.Page,
                searchResult.PageSize,
                searchResult.Count);
        }
    }
}