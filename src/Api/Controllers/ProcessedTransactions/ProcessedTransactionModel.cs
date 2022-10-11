using System;

namespace Api.Controllers.ProcessedTransactions
{
    public class ProcessedTransactionModel
    {
        public string Reference { get; set; }
        public string InternalReference { get; set; }
        public string PspReference { get; set; }
        public string OfficeCode { get; set; }
        public DateTime? EntryDate { get; set; }
        public DateTime? TransactionDate { get; set; }
        public string AccountReference { get; set; }
        public int UserCode { get; set; }
        public string FundCode { get; set; }
        public string MopCode { get; set; }
        public decimal? Amount { get; set; }
        public string VatCode { get; set; }
        public float VatRate { get; set; } // TODO: When we insert a pending transaction this field is calculated from the VatCode - is there a reason this isn't?
        public decimal? VatAmount { get; set; } // TODO: When we insert a pending transaction this field is calculated from the VatCode - is there a reason this isn't?
        public string Narrative { get; set; }
        public int? ImportId { get; set; } // TODO: Should this really be here?

        public ProcessedTransactionModel() { }

        public ProcessedTransactionModel(BusinessLogic.Entities.ProcessedTransaction source)
        {
            Reference = source.TransactionReference;
            InternalReference = source.InternalReference;
            PspReference = source.PspReference;
            OfficeCode = source.OfficeCode;
            EntryDate = source.EntryDate;
            TransactionDate = source.TransactionDate;
            AccountReference = source.AccountReference;
            UserCode = source.UserCode;
            FundCode = source.FundCode;
            MopCode = source.MopCode;
            Amount = source.Amount;
            VatCode = source.VatCode;
            VatRate = source.VatRate;
            VatAmount = source.VatAmount;
            Narrative = source.Narrative;
            ImportId = source.ImportId;
        }

        public ProcessedTransactionModel(BusinessLogic.Models.Transactions.SearchResultItem source)
        {
            Reference = source.TransactionReference;
            InternalReference = source.InternalReference;
            PspReference = source.PspReference;
            OfficeCode = source.OfficeCode;
            EntryDate = source.EntryDate;
            TransactionDate = source.TransactionDate;
            AccountReference = source.AccountReference;
            UserCode = source.UserCode;
            FundCode = source.FundCode;
            MopCode = source.MopCode;
            Amount = source.Amount;
            VatCode = source.VatCode;
            VatRate = source.VatRate;
            VatAmount = source.VatAmount;
            Narrative = source.Narrative;
            ImportId = source.ImportId;
        }

        public BusinessLogic.Entities.ProcessedTransaction GetProcessedTransaction()
        {
            return new BusinessLogic.Entities.ProcessedTransaction()
            {
                TransactionReference = "Unknown", // TODO: Should be populated in the service?
                InternalReference = "Unknown", // TODO: Should be populated in the service?
                PspReference = PspReference,
                OfficeCode = OfficeCode,
                EntryDate = DateTime.Now, // TODO: Should be populated in the service?
                TransactionDate = TransactionDate,
                AccountReference = AccountReference,
                UserCode = UserCode, // TODO: Should be populated in the service?
                FundCode = FundCode,
                MopCode = MopCode,
                Amount = Amount,
                VatCode = VatCode,
                VatRate = VatRate, // TODO: Should be populated in the service?
                VatAmount = VatAmount, // TODO: Should be populated in the service?
                Narrative = Narrative,
                ImportId = ImportId
            };
        }
    }
}