using BusinessLogic.Entities;
using BusinessLogic.Interfaces.Repositories;
using BusinessLogic.Models.Suspense;
using DataAccess.Extensions;
using DataAccess.Persistence;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace DataAccess.Repositories
{
    public class SuspenseRepository : Repository<Suspense>, ISuspenseRepository
    {
        public SuspenseRepository(IncomeDbContext context) : base(context)
        {
            IncomeDbContext.Configuration.ProxyCreationEnabled = false;
            IncomeDbContext.Configuration.LazyLoadingEnabled = false;
        }

        public List<Suspense> Search(SearchCriteria criteria, out int resultCount)
        {
            var data = IncomeDbContext.Suspenses.AsQueryable();

            if (criteria.CreatedAtDateFrom.HasValue)
            {
                data = data.Where(x => x.CreatedAt >= criteria.CreatedAtDateFrom.Value);
            }

            if (criteria.CreatedAtDateTo.HasValue)
            {
                var transactionDateTo = criteria.CreatedAtDateTo.Value.AddDays(1);

                data = data.Where(x => x.CreatedAt < transactionDateTo);
            }

            if (!string.IsNullOrEmpty(criteria.AccountNumber))
            {
                data = data.Where(x => x.AccountNumber.Contains(criteria.AccountNumber));
            }

            if (!string.IsNullOrEmpty(criteria.Narrative))
            {
                data = data.Where(x => x.Narrative.Contains(criteria.Narrative));
            }

            if (!string.IsNullOrEmpty(criteria.BatchReference))
            {
                data = data.Where(x => x.BatchReference.Contains(criteria.BatchReference));
            }

            if (criteria.Amount.HasValue)
            {
                data = data.Where(x => x.Amount == criteria.Amount.Value);
            }

            if (!criteria.ShowAllocated) // If we don't want to include allocated suspense items - add this filter.
            {
                data = data.Where(x => x.Amount - x.SuspenseProcessedTransactions.Sum(y => y.Amount) > 0 || x.SuspenseProcessedTransactions.Any() == false);
            }
            else
            {
                data = data.Where(x => x.Amount == x.SuspenseProcessedTransactions.Sum(y => y.Amount) && x.SuspenseProcessedTransactions.Any());
            }

            if (criteria.PageSize == 0) criteria.PageSize = 20;
            if (criteria.Page == 0) criteria.Page = 1;

            data = data.ApplyFilters(Filters);

            resultCount = data.Count();

            data = data
                .OrderByDescending(x => DbFunctions.TruncateTime(x.CreatedAt))
                .ThenByDescending(x => x.Amount - (x.SuspenseProcessedTransactions.Any() == false ? 0 : x.SuspenseProcessedTransactions.Sum(y => y.Amount)))
                .Skip((criteria.Page - 1) * criteria.PageSize)
                .Take(criteria.PageSize)
                .Include(x => x.SuspenseProcessedTransactions);

            return data.ToList();
        }

        public Suspense GetSuspense(int id)
        {
            var item = IncomeDbContext.Suspenses
                .AsQueryable()
                .Where(x => x.Id == id)
                .ApplyFilters(Filters)
                .Include(x => x.SuspenseProcessedTransactions)
                .Include(s => s.SuspenseProcessedTransactions.Select(y => y.TransactionIn))
                .Include(s => s.SuspenseProcessedTransactions.Select(y => y.TransactionOut))
                .FirstOrDefault();

            return item;
        }

        public List<Suspense> GetSuspensesBeingProcessed(Guid processId)
        {
            return IncomeDbContext.Suspenses
                .AsQueryable()
                .Where(x => x.ProcessId == processId.ToString())
                .ApplyFilters(Filters)
                .Include(x => x.SuspenseProcessedTransactions)
                .ToList();
        }

        public void Lock(List<int> items, Guid processId)
        {
            var itemsToLock = IncomeDbContext.Suspenses
                .Where(x => items.Contains(x.Id) && x.ProcessId == null)
                .ToList();

            foreach (var x in itemsToLock)
            {
                x.ProcessId = processId.ToString();
            }
        }

        public void Unlock(Guid processId)
        {
            var itemsToUnlock = IncomeDbContext.Suspenses
                .Where(x => x.ProcessId == processId.ToString())
                .ToList();

            foreach (var x in itemsToUnlock)
            {
                x.ProcessId = null;
            }
        }
    }
}