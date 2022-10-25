using System;
using System.ComponentModel.DataAnnotations;

namespace Api.Controllers.PendingTransactions
{
    public class PendingTransactionModel
    {
        public int Id { get; private set; }

        public string Reference { get; private set; }

        public string InternalReference { get; private set; }

        [StringLength(2)]
        public string OfficeCode { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? TransactionDate { get; set; }

        [StringLength(30)]
        public string AccountReference { get; set; }

        public int UserCode { get; set; }

        [StringLength(5)]
        public string FundCode { get; set; }

        [StringLength(5)]
        public string MopCode { get; set; }

        public decimal? Amount { get; set; }

        [StringLength(2)]
        public string VatCode { get; set; }

        public float VatRate { get; private set; }

        public decimal? VatAmount { get; private set; }

        [StringLength(100)]
        public string Narrative { get; set; }

        [StringLength(100)]
        public string ExternalReference { get; set; } // Reference from calling app

        [StringLength(50)]
        public string PayeeName { get; set; }

        [StringLength(50)]
        public string PayeeAddressLine1 { get; set; }

        [StringLength(50)]
        public string PayeeAddressLine2 { get; set; }

        [StringLength(50)]
        public string PayeeAddressLine3 { get; set; }

        [StringLength(50)]
        public string PayeeAddressLine4 { get; set; }

        [StringLength(10)]
        public string PayeePostCode { get; set; }

        public string SuccessUrl { get; set; }

        [StringLength(255)]
        public string CancelUrl { get; set; }

        [StringLength(255)]
        public string FailUrl { get; set; }

        public DateTime? ExpiryDate { get; set; }

        public int StatusId { get; private set; }

        public PendingTransactionModel() { }

        public PendingTransactionModel(BusinessLogic.Entities.PendingTransaction source)
        {
            Id = source.Id;
            Reference = source.TransactionReference;
            InternalReference = source.InternalReference;
            OfficeCode = source.OfficeCode;
            CreatedDate = source.EntryDate;
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
            ExternalReference = source.AppReference;
            PayeeName = source.CardHolderName;
            PayeeAddressLine1 = source.CardHolderAddressLine1;
            PayeeAddressLine2 = source.CardHolderAddressLine2;
            PayeeAddressLine3 = source.CardHolderAddressLine3;
            PayeeAddressLine4 = source.CardHolderAddressLine4;
            PayeePostCode = source.CardHolderPostCode;
            SuccessUrl = source.SuccessUrl;
            CancelUrl = source.CancelUrl;
            FailUrl = source.FailUrl;
            ExpiryDate = source.ExpiryDate;
            StatusId = source.StatusId;
        }

        public BusinessLogic.Entities.PendingTransaction GetPendingTransaction()
        {
            return new BusinessLogic.Entities.PendingTransaction()
            {
                OfficeCode = OfficeCode,
                EntryDate = CreatedDate,
                TransactionDate = TransactionDate,
                AccountReference = AccountReference,
                UserCode = UserCode,
                FundCode = FundCode,
                MopCode = MopCode,
                Amount = Amount,
                VatCode = VatCode,
                Narrative = Narrative,
                AppReference = ExternalReference,
                CardHolderName = PayeeName,
                CardHolderAddressLine1 = PayeeAddressLine1,
                CardHolderAddressLine2 = PayeeAddressLine2,
                CardHolderAddressLine3 = PayeeAddressLine3,
                CardHolderAddressLine4 = PayeeAddressLine4,
                CardHolderPostCode = PayeePostCode,
                SuccessUrl = SuccessUrl,
                CancelUrl = CancelUrl,
                FailUrl = FailUrl,
                Legacy = false,
                Processed = false,
                ExpiryDate = ExpiryDate
            };
        }
    }
}