using System;
using System.Text;

namespace BusinessLogic.Extensions
{
    public static class SearchResultItemExtensions
    {
        public static string ToExportString(this Models.Transactions.SearchResultItem transaction, string delimiter)
        {
            if (transaction == null) return string.Empty;

            var exportString = new StringBuilder();

            exportString.Append((transaction.TransactionReference ?? string.Empty) + delimiter);
            exportString.Append((transaction.InternalReference ?? string.Empty) + delimiter);
            exportString.Append((transaction.PspReference ?? string.Empty) + delimiter);
            exportString.Append((transaction.OfficeCode ?? string.Empty) + delimiter);
            exportString.Append((transaction.EntryDate?.ToString("yyyy-MM-dd hh:mm:ss.fffffff") ?? string.Empty) + delimiter);
            exportString.Append((transaction.TransactionDate?.ToString("yyyy-MM-dd hh:mm:ss.fffffff") ?? string.Empty) + delimiter);
            exportString.Append((transaction.AccountReference ?? string.Empty) + delimiter);
            exportString.Append(Convert.ToString(transaction.UserCode) + delimiter);
            exportString.Append((transaction.FundCode ?? string.Empty) + delimiter);
            exportString.Append((transaction.MopCode ?? string.Empty) + delimiter);
            exportString.Append(Convert.ToString(transaction.Amount) + delimiter);
            exportString.Append((transaction.VatCode ?? string.Empty) + delimiter);
            exportString.Append(Convert.ToString(transaction.VatRate) + delimiter);
            exportString.Append(Convert.ToString(transaction.VatAmount) + delimiter);
            exportString.Append(transaction.Narrative);

            return exportString.ToString();
        }
    }
}
