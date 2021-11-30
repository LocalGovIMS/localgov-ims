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

        [StringLength(30)]
        public string BatchReference { get; set; }

        [StringLength(100)]
        public string ExternalReference { get; set; } // Reference from calling app

        [StringLength(50)]
        public string PayeeName { get; set; }

        [StringLength(100)]
        public string PayeeBusinessName { get; set; }

        [StringLength(50)]
        public string PayeePremiseNumber { get; set; }

        [StringLength(100)]
        public string PayeePremiseName { get; set; }

        [StringLength(50)]
        public string PayeeStreet { get; set; }

        [StringLength(50)]
        public string PayeeArea { get; set; }

        [StringLength(50)]
        public string PayeeTown { get; set; }

        [StringLength(50)]
        public string PayeeCounty { get; set; }

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
            BatchReference = source.BatchReference;
            ExternalReference = source.AppReference;
            PayeeName = source.CardHolderName;
            PayeeBusinessName = source.CardHolderBusinessName;
            PayeePremiseNumber = source.CardHolderPremiseNumber;
            PayeePremiseName = source.CardHolderPremiseName;
            PayeeStreet = source.CardHolderStreet;
            PayeeArea = source.CardHolderArea;
            PayeeTown = source.CardHolderTown;
            PayeeCounty = source.CardHolderCounty;
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
                BatchReference = BatchReference,
                AppReference = ExternalReference,
                CardHolderName = PayeeName,
                CardHolderBusinessName = PayeeBusinessName,
                CardHolderPremiseNumber = PayeePremiseNumber,
                CardHolderPremiseName = PayeePremiseName,
                CardHolderStreet = PayeeStreet,
                CardHolderArea = PayeeArea,
                CardHolderTown = PayeeTown,
                CardHolderCounty = PayeeCounty,
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