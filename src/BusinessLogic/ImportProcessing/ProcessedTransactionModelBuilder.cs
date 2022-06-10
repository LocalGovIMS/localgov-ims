using BusinessLogic.Models;
using System;

namespace BusinessLogic.ImportProcessing
{
    public class ProcessedTransactionModelBuilder : IProcessedTransactionModelBuilder
    {
        public ProcessedTransactionModel BuildFromCsvRow(string csvRow, int transactionImportId)
        {
            var fields = csvRow.Split(',');

            if (fields.Length != 15)
                throw new InvalidOperationException("The row data does not contain the correct number of fields");

            var model = new ProcessedTransactionModel();

            model.TransactionImportId = transactionImportId;
            model.Reference = GetValue(() => { return fields[0]; }, "Reference");
            model.InternalReference = GetValue(() => { return fields[1]; }, "Reference");
            model.PspReference = GetValue(() => { return fields[2]; }, "Reference");
            model.OfficeCode = GetValue(() => { return fields[3]; }, "Office Code");
            model.EntryDate = GetValue(() => { return Convert.ToDateTime(fields[4]); }, "Entry Date");
            model.TransactionDate = GetValue(() => { return Convert.ToDateTime(fields[5]); }, "Transaction Date");
            model.AccountReference = GetValue(() => { return fields[6]; }, "Account Reference");
            model.UserCode = GetValue(() => { return Convert.ToInt32(fields[7]); }, "User Code");
            model.FundCode = GetValue(() => { return fields[8]; }, "Fund Code");
            model.MopCode = GetValue(() => { return fields[9]; }, "Mop Code");
            model.Amount = GetValue(() => { return Convert.ToDecimal(fields[10]); }, "Amount");
            model.VatCode = GetValue(() => { return fields[11]; }, "Vat Code");
            model.VatRate = GetValue(() => { return Convert.ToSingle(fields[12]); }, "Vat Rate");
            model.VatAmount = GetValue(() => { return Convert.ToDecimal(fields[13]); }, "Vat Amount");
            model.Narrative = GetValue(() => { return fields[14]; }, "Narrative");

            return model;
        }

        private T GetValue<T>(Func<T> source, string fieldName)
        {
            try
            {
                return source();
            }
            catch
            {
                throw new InvalidCastException($"Unable to set the {fieldName} value");
            }
        }
    }
}
