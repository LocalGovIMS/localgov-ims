using BusinessLogic.Entities;
using BusinessLogic.Interfaces.Repositories;
using BusinessLogic.Models.EReturns;
using DataAccess.Extensions;
using DataAccess.Persistence;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace DataAccess.Repositories
{
    public class EReturnRepository : Repository<EReturn>, IEReturnRepository
    {
        public EReturnRepository(IncomeDbContext context) : base(context)
        {
            IncomeDbContext.Configuration.ProxyCreationEnabled = false;
            IncomeDbContext.Configuration.LazyLoadingEnabled = false;
        }

        public List<EReturn> Search(SearchCriteria criteria, out int resultCount)
        {
            var eReturns = IncomeDbContext.EReturns.AsQueryable();

            if (criteria.StartDate != null)
            {
                eReturns = eReturns.Where(x => x.CreatedAt >= criteria.StartDate);
            }

            if (criteria.EndDate != null)
            {
                DateTime endDate = ((DateTime)criteria.EndDate).AddDays(1);

                eReturns = eReturns.Where(x => x.CreatedAt < endDate);
            }

            if (criteria.Type.HasValue)
            {
                eReturns = eReturns.Where(x => x.TypeId == (int)criteria.Type.Value);
            }

            if (criteria.Name != null)
            {
                eReturns = eReturns.Where(x => x.EReturnCheques.Any(y => y.Name == criteria.Name));
            }

            if (!string.IsNullOrWhiteSpace(criteria.EReturnNumber))
            {
                eReturns = eReturns.Where(x => x.EReturnNo.Contains(criteria.EReturnNumber));
            }

            // Either get data by the status we're looking for, or exclude deleted. Means folks can search for deleted records then. 
            if (criteria.StatusId.HasValue)
            {
                eReturns = eReturns.Where(x => x.StatusId == criteria.StatusId.Value);
            }
            else
            {
                eReturns = eReturns.Where(x => x.StatusId != (int)BusinessLogic.Enums.EReturnStatus.Deleted);
                eReturns = eReturns.Where(x => x.StatusId != (int)BusinessLogic.Enums.EReturnStatus.Voided);
            }

            if (criteria.TemplateId.HasValue)
            {
                eReturns = eReturns.Where(x => x.Template.Id == criteria.TemplateId.Value);
            }

            if (criteria.Amount != null)
            {
                eReturns = eReturns.Where(x =>
                    (x.TypeId == (int)BusinessLogic.Enums.EReturnType.Cash && x.EReturnCashes.Sum(y => y.Total) == criteria.Amount)
                    ||
                    (x.TypeId == (int)BusinessLogic.Enums.EReturnType.Cheque && x.EReturnCheques.Sum(y => y.Amount) == criteria.Amount)
                 );
            }

            if (criteria.PageSize == 0) criteria.PageSize = 20;
            if (criteria.Page == 0) criteria.Page = 1;

            eReturns = eReturns.ApplyFilters(Filters);

            resultCount = eReturns.Count();

            eReturns = eReturns
                .OrderByDescending(x => x.CreatedAt)
                .Skip((criteria.Page - 1) * criteria.PageSize)
                .Take(criteria.PageSize)
                .Include(x => x.EReturnCashes)
                .Include(x => x.EReturnCheques)
                .Include(x => x.EReturnStatus)
                .Include(x => x.EReturnType)
                .Include(x => x.Template)
                .Include(x => x.ApprovedByUser)
                .Include(x => x.CreatedByUser)
                .Include(x => x.SubmittedByUser);

            return eReturns.ToList();
        }

        public EReturn GetEReturn(int id)
        {
            var item = IncomeDbContext.EReturns.AsQueryable()
                .Where(x => x.Id == id)
                .ApplyFilters(Filters)
                .Include(x => x.PendingTransactions)
                .Include(x => x.ProcessedTransactions)
                .Include(x => x.EReturnCashes)
                .Include(x => x.EReturnCheques)
                .Include(x => x.EReturnStatus)
                .Include(x => x.EReturnType)
                .Include(x => x.Template)
                .Include(x => x.Template.TemplateRows)
                .Include(x => x.ApprovedByUser)
                .Include(x => x.CreatedByUser)
                .Include(x => x.SubmittedByUser)
                .FirstOrDefault();

            return item;
        }

        public void SetEReturnNo(EReturn item)
        {
            var eReturn = IncomeDbContext.EReturns.AsQueryable()
                .Where(x => x.Id == item.Id && x.EReturnNo == null)
                .ApplyFilters(Filters)
                .FirstOrDefault();

            eReturn.EReturnNo = "R" + item.Id;
        }

        public void Update(EReturn item)
        {
            var entity = IncomeDbContext.EReturns.AsQueryable()
                .Where(x => x.Id == item.Id)
                .ApplyFilters(Filters)
                .FirstOrDefault();

            // Only update the status if it's New - after that it should get updated by actions such as submitting/approving (which will be developed later)
            if (entity.StatusId == (int)BusinessLogic.Enums.EReturnStatus.New) entity.StatusId = (int)BusinessLogic.Enums.EReturnStatus.InProgress;
        }

        public void Delete(EReturn item)
        {
            // If we can't get the item - either it doesn't exist, or we don't have access to it, so return.
            var existingItem = GetEReturn(item.Id);
            if (existingItem == null) return;

            var cashToDelete = item.EReturnCashes.ToList();
            foreach (var a in cashToDelete)
            {
                IncomeDbContext.EReturnCashes.Remove(a);
            }

            var chequesToDelete = item.EReturnCheques.ToList();
            foreach (var a in chequesToDelete)
            {
                IncomeDbContext.EReturnCheques.Remove(a);
            }

            var transactionsToDelete = item.PendingTransactions.ToList();
            foreach (var a in transactionsToDelete)
            {
                IncomeDbContext.PendingTransactions.Remove(a);
            }

            IncomeDbContext.EReturns.Remove(item);
        }

        public List<EReturn> GetEReturnsBeingProcessed(Guid processId)
        {
            var query = IncomeDbContext.EReturns.Where(x => x.ProcessId == processId.ToString())
                .ApplyFilters(Filters);

            query.Include(x => x.PendingTransactions).Load();
            query.Include(x => x.ProcessedTransactions).Load();

            query
                .Include(x => x.EReturnCashes)
                .Include(x => x.EReturnCheques)
                .Load();

            query.Include(x => x.EReturnStatus)
                .Include(x => x.EReturnType)
                .Include(x => x.Template)
                .Include(x => x.ApprovedByUser)
                .Include(x => x.CreatedByUser)
                .Include(x => x.SubmittedByUser)
                .Load();

            return query.ToList();
        }

        public void Lock(List<int> items, Guid processId)
        {
            var itemsToLock = IncomeDbContext.EReturns
                .Where(x => items.Contains(x.Id) && x.ProcessId == null)
                .ApplyFilters(Filters)
                .ToList();

            foreach (var x in itemsToLock)
            {
                x.ProcessId = processId.ToString();
            }
        }

        public void Unlock(Guid processId)
        {
            var itemsToUnlock = IncomeDbContext.EReturns
                .Where(x => x.ProcessId == processId.ToString())
                .ApplyFilters(Filters)
                .ToList();

            foreach (var x in itemsToUnlock)
            {
                x.ProcessId = null;
            }
        }
    }
}