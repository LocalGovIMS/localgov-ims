﻿using BusinessLogic.Entities;
using BusinessLogic.Extensions;
using BusinessLogic.Interfaces.Repositories;
using BusinessLogic.Models.Transactions;
using DataAccess.Extensions;
using DataAccess.Persistence;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace DataAccess.Repositories
{
    public class TransactionRepository : Repository<ProcessedTransaction>, ITransactionRepository
    {
        public TransactionRepository(IncomeDbContext context) : base(context)
        {
            IncomeDbContext.Configuration.ProxyCreationEnabled = false;
            IncomeDbContext.Configuration.LazyLoadingEnabled = false;
        }

        public IEnumerable<ProcessedTransaction> GetByInternalReference(string reference)
        {
            return IncomeDbContext.ProcessedTransactions
                .Where(x => x.InternalReference == reference)
                .ApplyFilters(Filters)
                .ToList();
        }

        public IEnumerable<ProcessedTransaction> GetByPspReference(string pspReference)
        {
            return IncomeDbContext.ProcessedTransactions
                .Where(x => x.PspReference == pspReference)
                .Include(x => x.Fund)
                .Include(x => x.Mop)
                .Include(x => x.Mop.Metadata)
                .Include(x => x.Mop.Metadata.Select(y => y.MetadataKey))
                .Include(x => x.Vat)
                .Include(x => x.User)
                .Include(x => x.SuspenseProcessedTransactions)
                .Include(x => x.SuspenseProcessedTransactions1)
                .ApplyFilters(Filters)
                .ToList();
        }

        public ProcessedTransaction GetByTransactionReference(string reference)
        {
            return IncomeDbContext.ProcessedTransactions
                .Where(x => x.TransactionReference == reference)
                .Include(x => x.Fund)
                .Include(x => x.Mop)
                .Include(x => x.Mop.Metadata)
                .Include(x => x.Mop.Metadata.Select(y => y.MetadataKey))
                .ApplyFilters(Filters)
                .FirstOrDefault();
        }

        public ProcessedTransaction GetByAppReference(string appReference)
        {
            return IncomeDbContext.ProcessedTransactions
                .Where(x => x.AppReference == appReference)
                .Include(x => x.Fund)
                .Include(x => x.Mop)
                .Include(x => x.Mop.Metadata)
                .Include(x => x.Mop.Metadata.Select(y => y.MetadataKey))
                .Include(x => x.Vat)
                .ApplyFilters(Filters)
                .FirstOrDefault();
        }

        public IEnumerable<ProcessedTransaction> GetProcessedRefunds(string reference)
        {
            // This is a slight quirk - but it means a filtered index can be used, which is quicker.
            // Try removing it and turning the Where into and '=' and you'll notice a change if you
            // run the SQL profiler.
            var array = new[] { reference };

            return IncomeDbContext.ProcessedTransactions
                .Where(x => array.Contains(x.RefundReference))
                .Include(x => x.Fund)
                .Include(x => x.User)
                .Include(x => x.Mop)
                .Include(x => x.Mop.Metadata)
                .Include(x => x.Mop.Metadata.Select(y => y.MetadataKey))
                .ApplyFilters(Filters)
                .ToList();
        }

        public IEnumerable<ProcessedTransaction> GetTransfers(string transferGuid)
        {
            return IncomeDbContext.ProcessedTransactions
                .Where(x => x.TransferGuid == transferGuid)
                .Include(x => x.Fund)
                .Include(x => x.User)
                .Include(x => x.Mop)
                .Include(x => x.Mop.Metadata)
                .Include(x => x.Mop.Metadata.Select(y => y.MetadataKey))
                .ApplyFilters(Filters)
                .ToList();
        }

        public IEnumerable<ProcessedTransaction> GetJournalsForTransactions(IEnumerable<ProcessedTransaction> transactions)
        {
            var journals = new List<ProcessedTransaction>();

            var trans = transactions.Select(t => t.TransactionReference).ToArray();

            var items = IncomeDbContext.ProcessedTransactions
                .Where(x => trans.Contains(x.TransferReference))
                .Include(x => x.Fund)
                .Include(x => x.User)
                .Include(x => x.Mop)
                .Include(x => x.Mop.Metadata)
                .Include(x => x.Mop.Metadata.Select(y => y.MetadataKey))
                .ApplyFilters(Filters)
                .ToList();

            return items;
        }

        public List<string> GetJournalsForTransactions(string[] transactionReferences)
        {
            var items = IncomeDbContext.ProcessedTransactions
                .Where(x => transactionReferences.Contains(x.TransferReference))
                .Include(x => x.Mop)
                .Include(x => x.Mop.Metadata)
                .Include(x => x.Mop.Metadata.Select(y => y.MetadataKey))
                .ApplyFilters(Filters)
                .Select(x => x.TransferReference)
                .Distinct()
                .ToList();

            return items;
        }

        public List<SearchResultItem> Search(SearchCriteria criteria, out int resultCount)
        {
            var transactions = IncomeDbContext.ProcessedTransactions.AsQueryable();

            if (!string.IsNullOrWhiteSpace(criteria.ReceiptNumber))
            {
                transactions = transactions.Where(x => x.PspReference == criteria.ReceiptNumber);
            }

            if (criteria.Amount != null)
            {
                transactions = transactions.Where(x => x.Amount == criteria.Amount);
            }

            if (!string.IsNullOrWhiteSpace(criteria.AccountReference))
            {
                if (criteria.WildSearchAccountReference)
                {
                    transactions = transactions.Where(x => x.AccountReference.Contains(criteria.AccountReference));
                }
                else
                {
                    transactions = transactions.Where(x => x.AccountReference == criteria.AccountReference);
                }
            }

            if (!string.IsNullOrWhiteSpace(criteria.AppReference))
            {
                transactions = transactions.Where(x => x.AppReference == criteria.AppReference);
            }

            if (!criteria.FundCodes.IsNullOrEmpty())
            {
                transactions = transactions.Where(x => criteria.FundCodes.Contains(x.FundCode));
            }

            if (!criteria.MopCodes.IsNullOrEmpty())
            {
                transactions = transactions.Where(x => criteria.MopCodes.Contains(x.MopCode));
            }

            if (criteria.UserId.HasValue)
            {
                transactions = transactions.Where(x => x.UserCode == criteria.UserId.Value);
            }

            if (!string.IsNullOrWhiteSpace(criteria.InternalReference))
            {
                transactions = transactions.Where(x => x.InternalReference == criteria.InternalReference);
            }

            if (!string.IsNullOrWhiteSpace(criteria.Narrative))
            {
                transactions = transactions.Where(x => x.Narrative.Contains(criteria.Narrative));
            }

            if (criteria.StartDate != null)
            {
                transactions = transactions.Where(x => x.EntryDate >= criteria.StartDate);
            }

            if (criteria.EndDate != null)
            {
                transactions = transactions.Where(x => x.EntryDate <= criteria.EndDate);
            }

            if (criteria.ImportId.HasValue)
            {
                transactions = transactions.Where(x => x.ImportId == criteria.ImportId.Value);
            }

            if (!string.IsNullOrWhiteSpace(criteria.CardPrefix))
            {
                transactions = transactions.Where(x => x.CardPrefix.StartsWith(criteria.CardPrefix));
            }

            if (!string.IsNullOrWhiteSpace(criteria.CardSuffix))
            {
                transactions = transactions.Where(x => x.CardSuffix.StartsWith(criteria.CardSuffix));
            }

            if (!string.IsNullOrWhiteSpace(criteria.PspReference))
            {
                transactions = transactions.Where(x => x.PspReference == criteria.PspReference);
            }

            transactions = transactions.ApplyFilters(Filters);

            resultCount = transactions.Count();

            if (criteria.ApplyPaging)
            {

                if (criteria.PageSize == 0) criteria.PageSize = 20;
                if (criteria.Page == 0) criteria.Page = 1;

                transactions = transactions
                    .OrderByDescending(x => x.EntryDate)
                    .Skip((criteria.Page - 1) * criteria.PageSize)
                    .Take(criteria.PageSize);
            }

            var returnVal = transactions.Select(x =>
               new
               {
                   TransactionReference = x.TransactionReference,
                   AccountReference = x.AccountReference,
                   InternalReference = x.InternalReference,
                   Amount = x.Amount,
                   FundCode = x.FundCode,
                   MopCode = x.MopCode,
                   Narrative = x.Narrative,
                   PspReference = x.PspReference,
                   EntryDate = x.EntryDate,
                   AppReference = x.AppReference,
                   AddressLine1 = x.CardHolderAddressLine1,
                   AddressLine2 = x.CardHolderAddressLine2,
                   AddressLine3 = x.CardHolderAddressLine3,
                   AddressLine4 = x.CardHolderAddressLine4,
                   CardHolderPostCode = x.CardHolderPostCode,
                   OfficeCode = x.OfficeCode,
                   TransactionDate = x.TransactionDate,
                   UserCode = x.UserCode,
                   VatCode = x.VatCode,
                   VatRate = x.VatRate,
                   VatAmount = x.VatAmount,
                   ImportId = x.ImportId
               }).ToList();

            var funds = IncomeDbContext.Funds.ToList();
            var mops = IncomeDbContext.MOPs.ToList();
            var journals = GetJournalsForTransactions(returnVal.Select(x => x.TransactionReference).ToArray());
                
            return returnVal.Select(x =>
                new SearchResultItem
                {
                    TransactionReference = x.TransactionReference,
                    AccountReference = x.AccountReference ?? string.Empty,
                    Amount = x.Amount,
                    FundCode = x.FundCode,
                    Fund = x.FundCode == null ? null : funds.FirstOrDefault(y => y.FundCode == x.FundCode),
                    MopCode = x.MopCode,
                    Mop = x.MopCode == null ? null : mops.FirstOrDefault(y => y.MopCode == x.MopCode),
                    Narrative = x.Narrative,
                    PspReference = x.PspReference,
                    EntryDate = x.EntryDate,
                    AppReference = x.AppReference,
                    InternalReference = x.InternalReference,
                    CardHolderAddressLine1 = x.AddressLine1,
                    CardHolderAddressLine2 = x.AddressLine2,
                    CardHolderAddressLine3 = x.AddressLine3,
                    CardHolderAddressLine4 = x.AddressLine4,
                    CardHolderPostCode = x.CardHolderPostCode,
                    OfficeCode = x.OfficeCode,
                    TransactionDate = x.TransactionDate,
                    UserCode = x.UserCode,
                    VatCode = x.VatCode,
                    VatRate = x.VatRate,
                    VatAmount = x.VatAmount,
                    ImportId = x.ImportId,
                    HasTransfers = journals.Contains(x.TransactionReference)
                }).ToList();
        }

        public List<SearchResultItem> Search(SearchCriteria criteria)
        {
            int resultCount;

            return Search(criteria, out resultCount);
        }
    }
}