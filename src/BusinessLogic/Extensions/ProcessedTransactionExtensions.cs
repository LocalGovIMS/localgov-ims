using BusinessLogic.Entities;
using System.Collections.Generic;
using System.Linq;

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
    }
}
