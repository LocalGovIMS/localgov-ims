using BusinessLogic.Classes;
using BusinessLogic.Entities;
using BusinessLogic.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogic.Extensions
{
    public static class PendingTransactionExtensions
    {
        public static ProcessedTransaction ToFeeTransaction(this List<PendingTransaction> transactions, PaymentResult paymentResult, Mop mop, string mopCode)
        {
            var transaction = transactions.First();

            var feeTransaction = new ProcessedTransaction()
            {
                TransactionReference = $"{transaction.InternalReference}_{transactions.Count + 1}",
                InternalReference = transaction.InternalReference,
                PspReference = paymentResult.PspReference,
                OfficeCode = transaction.OfficeCode,
                EntryDate = DateTime.Now,
                TransactionDate = DateTime.Now,
                UserCode = transaction.UserCode,
                FundCode = transaction.FundCode,
                MopCode = mopCode,
                Amount = paymentResult.Fee,
                VatCode = transaction.VatCode, // TODO - What should this be? Maybe pick it up from MOP Metadata and calculate the amount and rate from the VAT Code record
                VatAmount = 0, // TODO - What should this be?
                VatRate = 0 // TODO - What should this be?
            };

            if (mop.IsARechargeFee())
            {
                feeTransaction.AccountReference = transactions.AccountReference();
                feeTransaction.FundCode = transactions.FundCode();
            }
            else if (mop.IsACentralChargeFee())
            {
                feeTransaction.AccountReference = mop.GetMopMetaDataValue<string>(MopMetaDataKeys.FeeAccountReference);
                feeTransaction.FundCode = mop.GetMopMetaDataValue<string>(MopMetaDataKeys.FeeFundCode);
            }

            return feeTransaction;
        }

        public static string AccountReference(this List<PendingTransaction> transactions)
        {
            return transactions.First().AccountReference;
        }

        public static string FundCode(this List<PendingTransaction> transactions)
        {
            return transactions.First().FundCode;
        }

        public static string SuccessUrl(this List<PendingTransaction> transactions)
        {
            return transactions.First().SuccessUrl;
        }

        public static string FailUrl(this List<PendingTransaction> transactions)
        {
            return transactions.First().FailUrl;
        }

        public static string CancelUrl(this List<PendingTransaction> transactions)
        {
            return transactions.First().CancelUrl;
        }

        public static bool? Legacy(this List<PendingTransaction> transactions)
        {
            return transactions.First().Legacy;
        }
    }
}
