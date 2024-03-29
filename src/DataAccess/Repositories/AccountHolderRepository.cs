﻿using BusinessLogic.Entities;
using BusinessLogic.Interfaces.Repositories;
using BusinessLogic.Models.AccountHolder;
using DataAccess.Extensions;
using DataAccess.Persistence;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace DataAccess.Repositories
{
    public class AccountHolderRepository : Repository<AccountHolder>, IAccountHolderRepository
    {
        public AccountHolderRepository(IncomeDbContext context) : base(context)
        {
            context.Configuration.ProxyCreationEnabled = false;
            context.Configuration.LazyLoadingEnabled = false;
        }

        public AccountHolder GetByAccountReferenceSimple(string accountReference)
        {
            return IncomeDbContext.AccountHolders
                .ApplyFilters(Filters)
                .FirstOrDefault(x => x.AccountReference == accountReference);
        }

        public AccountHolder GetByAccountReference(string accountReference)
        {
            return IncomeDbContext.AccountHolders
                .Include(x => x.Fund)
                .Include(x => x.FundMessage)
                .Include(x => x.FundMessage.Metadata)
                .Include(x => x.CreatedByUser)
                .Include(x => x.UpdatedByUser)
                .ApplyFilters(Filters)
                .FirstOrDefault(x => x.AccountReference == accountReference);
        }

        public AccountHolder GetByAccountReference(string accountReference, string fundCode)
        {
            return IncomeDbContext.AccountHolders
                .Include(x => x.Fund)
                .Include(x => x.FundMessage)
                .Include(x => x.FundMessage.Metadata)
                .Include(x => x.CreatedByUser)
                .Include(x => x.UpdatedByUser)
                .ApplyFilters(Filters)
                .FirstOrDefault(x => x.AccountReference == accountReference && x.FundCode == fundCode);
        }

        public List<AccountHolder> Search(SearchCriteria searchCriteria, out int resultCount)
        {
            var items = IncomeDbContext.AccountHolders.AsQueryable();

            items = items.ApplyFilters(Filters);

            if (!string.IsNullOrWhiteSpace(searchCriteria.AccountReference))
            {
                items = items.Where(x => x.AccountReference.Contains(searchCriteria.AccountReference));
            }

            if (!string.IsNullOrWhiteSpace(searchCriteria.FundCode))
            {
                items = items.Where(x => x.FundCode == searchCriteria.FundCode);
            }

            if (!string.IsNullOrWhiteSpace(searchCriteria.Address))
            {
                items = items.Where(x => x.AddressLine1.Contains(searchCriteria.Address)
                    || x.AddressLine2.Contains(searchCriteria.Address)
                    || x.AddressLine3.Contains(searchCriteria.Address)
                    || x.AddressLine4.Contains(searchCriteria.Address));
            }

            if (!string.IsNullOrWhiteSpace(searchCriteria.PostCode))
            {
                items = items.Where(x => x.Postcode.Contains(searchCriteria.PostCode));
            }

            if (!string.IsNullOrWhiteSpace(searchCriteria.Surname))
            {
                items = items.Where(x => x.Surname.Contains(searchCriteria.Surname));
            }

            if (searchCriteria.PageSize == 0) searchCriteria.PageSize = 20;
            if (searchCriteria.Page == 0) searchCriteria.Page = 1;

            resultCount = items.Count();

            items = items
                .OrderByDescending(x => x.AccountReference)
                .Skip((searchCriteria.Page - 1) * searchCriteria.PageSize)
                .Take(searchCriteria.PageSize);

            var returnVal = items.Select(x =>
               new
               {
                   Account_Reference = x.AccountReference,
                   Title = x.Title,
                   Forename = x.Forename,
                   Surname = x.Surname,
                   Address_Line1 = x.AddressLine1,
                   Address_Line2 = x.AddressLine2,
                   Address_Line3 = x.AddressLine3,
                   Address_Line4 = x.AddressLine4,
                   Postcode = x.Postcode,
                   Current_Balance = x.CurrentBalance
               }).ToList();

            return returnVal.Select(x =>
                new AccountHolder
                {
                    AccountReference = x.Account_Reference,
                    Title = x.Title,
                    Forename = x.Forename,
                    Surname = x.Surname,
                    AddressLine1 = x.Address_Line1,
                    AddressLine2 = x.Address_Line2,
                    AddressLine3 = x.Address_Line3,
                    AddressLine4 = x.Address_Line4,
                    Postcode = x.Postcode,
                    CurrentBalance = x.Current_Balance
                }).ToList();
        }

        public List<AccountHolder> Search(SearchCriteria searchCriteria)
        {
            int resultCount;
            return Search(searchCriteria, out resultCount);
        }
    }
}