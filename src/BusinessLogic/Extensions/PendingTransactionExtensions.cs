using BusinessLogic.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogic.Extensions
{
    public static class PendingTransactionExtensions
    {
        public static ProcessedTransaction ToFeeTransaction(this List<PendingTransaction> transactions, string pspReference, decimal fee, string mopCode)
        {
            var transaction = transactions.First();

            return new ProcessedTransaction()
            {
                TransactionReference = $"{transaction.InternalReference}_{transactions.Count + 1}",
                InternalReference = transaction.InternalReference,
                PspReference = pspReference,
                OfficeCode = transaction.OfficeCode,
                EntryDate = DateTime.Now,
                TransactionDate = DateTime.Now,
                //AccountReference = transaction.AccountReference,
                UserCode = transaction.UserCode,
                FundCode = transaction.FundCode,
                MopCode = mopCode,
                Amount = fee,
                VatCode = transaction.VatCode, // TODO - What should this be? Maybe pick it up from MOP Metadata and calculate the amount and rate from the VAT Code record
                VatAmount = 0, // TODO - What should this be?
                VatRate = 0 // TODO - What should this be?
            };
        }

    }
}
