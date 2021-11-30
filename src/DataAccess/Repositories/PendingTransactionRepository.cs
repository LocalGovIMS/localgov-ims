using BusinessLogic.Entities;
using BusinessLogic.Interfaces.Repositories;
using DataAccess.Extensions;
using DataAccess.Persistence;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace DataAccess.Repositories
{
    public class PendingTransactionRepository : Repository<PendingTransaction>, IPendingTransactionRepository
    {
        public PendingTransactionRepository(IncomeDbContext context) : base(context)
        {
        }

        public IEnumerable<PendingTransaction> GetByInternalReference(string reference)
        {
            return IncomeDbContext.PendingTransactions
                .Where(x => x.InternalReference == reference)
                .ApplyFilters(Filters)
                .ToList();
        }

        public IEnumerable<PendingTransaction> GetByRefundReference(string reference)
        {
            return IncomeDbContext.PendingTransactions
                .Where(x => x.RefundReference == reference)
                .Include(x => x.Fund)
                .ApplyFilters(Filters)
                .ToList();
        }

        public IEnumerable<PendingTransaction> GetPendingRefunds(string transactionReference)
        {
            return IncomeDbContext.PendingTransactions
                .Where(x => x.RefundReference == transactionReference && x.Amount < 0 && x.Processed != true)
                .ApplyFilters(Filters)
                .ToList();
        }

        public IEnumerable<PendingTransaction> GetFailedRefunds(string transactionReference)
        {
            return IncomeDbContext.PendingTransactions
                .Where(x => x.RefundReference == transactionReference && x.StatusId == (int)BusinessLogic.Enums.TransactionStatus.Failed)
                .ApplyFilters(Filters)
                .ToList();
        }

        public void Update(List<PendingTransaction> items, int eReturnId)
        {
            var existingItems = IncomeDbContext.PendingTransactions
                .AsQueryable()
                .Where(x => x.EReturnId == eReturnId)
                .ToList();

            foreach (var item in existingItems)
            {
                var updateItem = items.FirstOrDefault(x => x.Id == item.Id);
                if (updateItem == null) continue;

                item.Amount = updateItem.Amount;
                item.Narrative = updateItem.Narrative;
                item.VatCode = updateItem.VatCode;
                item.AccountReference = updateItem.AccountReference;
            }
        }
    }
}