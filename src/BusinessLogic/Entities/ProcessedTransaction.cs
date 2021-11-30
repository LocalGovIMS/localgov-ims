namespace BusinessLogic.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class ProcessedTransaction
    {
        public ProcessedTransaction()
        {
            EmailLogs = new HashSet<EmailLog>();
            SuspenseProcessedTransactions = new HashSet<SuspenseProcessedTransaction>();
            SuspenseProcessedTransactions1 = new HashSet<SuspenseProcessedTransaction>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(36)]
        public string TransactionReference { get; set; }

        [StringLength(36)]
        public string InternalReference { get; set; }

        [StringLength(16)]
        public string PspReference { get; set; }

        [StringLength(2)]
        public string OfficeCode { get; set; }

        public DateTime? EntryDate { get; set; }

        public DateTime? TransactionDate { get; set; }

        [StringLength(30)]
        public string AccountReference { get; set; }

        public int UserCode { get; set; }

        [StringLength(5)]
        public string FundCode { get; set; }

        [StringLength(5)]
        public string MopCode { get; set; }

        public decimal? Amount { get; set; }

        public decimal? VatAmount { get; set; }

        [StringLength(2)]
        public string VatCode { get; set; }

        [StringLength(100)]
        public string Narrative { get; set; }

        [StringLength(30)]
        public string BatchReference { get; set; }

        [StringLength(30)]
        public string MerchantNumber { get; set; }

        [StringLength(15)]
        public string AuthorisationCode { get; set; }

        [StringLength(100)]
        public string AppReference { get; set; }

        [StringLength(50)]
        public string CardHolderName { get; set; }

        [StringLength(100)]
        public string CardHolderBusinessName { get; set; }

        [StringLength(50)]
        public string CardHolderPremiseNumber { get; set; }

        [StringLength(100)]
        public string CardHolderPremiseName { get; set; }

        [StringLength(50)]
        public string CardHolderStreet { get; set; }

        [StringLength(50)]
        public string CardHolderArea { get; set; }

        [StringLength(50)]
        public string CardHolderTown { get; set; }

        [StringLength(50)]
        public string CardHolderCounty { get; set; }

        [StringLength(10)]
        public string CardHolderPostCode { get; set; }

        public float VatRate { get; set; }

        [StringLength(36)]
        public string RefundReference { get; set; }

        [StringLength(36)]
        public string TransferReference { get; set; }

        [StringLength(36)]
        public string TransferGuid { get; set; }

        [StringLength(36)]
        public string TransferRollbackGuid { get; set; }

        public bool ReceiptIssued { get; set; }

        public int? EReturnId { get; set; }

        [StringLength(50)]
        public string ChequeSortCode { get; set; }

        [StringLength(50)]
        public string ChequeAccountNumber { get; set; }

        [StringLength(50)]
        public string ChequeNumber { get; set; }

        [StringLength(50)]
        public string ChequeName { get; set; }

        [StringLength(13)]
        public string OriginalTransactionReference { get; set; }

        public virtual EReturn EReturn { get; set; }

        public virtual Fund Fund { get; set; }

        public virtual Mop Mop { get; set; }

        public virtual Office Office { get; set; }

        public virtual User User { get; set; }

        public virtual Vat Vat { get; set; }

        public virtual ICollection<EmailLog> EmailLogs { get; set; }

        public virtual ICollection<SuspenseProcessedTransaction> SuspenseProcessedTransactions { get; set; }

        public virtual ICollection<SuspenseProcessedTransaction> SuspenseProcessedTransactions1 { get; set; }
    }
}
