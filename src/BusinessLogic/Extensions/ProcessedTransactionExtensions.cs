using BusinessLogic.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessLogic.Extensions
{
    public static class ProcessedTransactionExtensions
    {
        public static bool IsATransferTransaction(this List<ProcessedTransaction> transactions)
        {
            if (transactions == null || !transactions.Any()) return false;
            if (transactions.Count() > 1) return false;

            var transaction = transactions.FirstOrDefault();

            return transaction.Mop.IsAJournal() || transaction.Mop.IsAJournalReallocation();
        }

        public static List<ProcessedTransaction> GetProcessedTransactions(this List<ProcessedTransaction> transactions)
        {
            if (transactions == null || !transactions.Any()) return new List<ProcessedTransaction>();

            return transactions
                .Where(x => x.IsProcessed())
                .ToList();
        }

        public static bool IsProcessed(this ProcessedTransaction transaction)
        {
            if (transaction == null) return false;

            return !transaction.Mop.IsAJournal() && !transaction.Mop.IsATransferOut();
        }

        public static bool IsRefundable(this ProcessedTransaction transaction)
        {
            if (transaction == null) return false;

            return transaction.Mop.IsARefundablePayment();
        }

        public static bool IsACreditNote(this ProcessedTransaction transaction)
        {
            if (transaction == null) return false;

            return transaction.Mop.IsAJournalReallocation() && transaction.Amount < 0;
        }

        public static bool IsACredit(this ProcessedTransaction transaction)
        {
            if (transaction == null) return false;

            return !transaction.Mop.IsAJournal() && !transaction.Mop.IsATransferOut();
        }

        public static bool IsADebit(this ProcessedTransaction transaction)
        {
            if (transaction == null) return false;

            return !transaction.Mop.IsAJournalReallocation() && !transaction.Mop.IsATransferIn();
        }

        public static bool IsCreatable(this ProcessedTransaction transaction)
        {
            if (transaction == null) return false;
            if (string.IsNullOrEmpty(transaction.FundCode)) return false;
            if (string.IsNullOrWhiteSpace(transaction.FundCode)) return false;

            return true;
        }

        public static Entities.Suspense ToSuspense(this ProcessedTransaction transaction)
        {
            return new Entities.Suspense()
            {
                AccountNumber = transaction.AccountReference,
                Amount = transaction.Amount ?? 0,
                CreatedAt = DateTime.Now,
                Narrative = transaction.Narrative,
                TransactionDate = transaction.TransactionDate ?? DateTime.Now,
                ImportId = transaction.ImportId
            };
        }
    }
}
