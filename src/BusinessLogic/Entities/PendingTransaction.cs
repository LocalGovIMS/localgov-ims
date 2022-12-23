namespace BusinessLogic.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public partial class PendingTransaction
    {
        public int Id { get; set; }

        [Required]
        [StringLength(36)]
        public string TransactionReference { get; set; }

        [StringLength(36)]
        public string InternalReference { get; set; }

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

        [StringLength(15)]
        public string AuthorisationCode { get; set; }

        [StringLength(100)]
        public string AppReference { get; set; }

        [StringLength(50)]
        public string CardHolderName { get; set; }

        [StringLength(50)]
        public string CardHolderAddressLine1 { get; set; }

        [StringLength(50)]
        public string CardHolderAddressLine2 { get; set; }

        [StringLength(50)]
        public string CardHolderAddressLine3 { get; set; }

        [StringLength(50)]
        public string CardHolderAddressLine4 { get; set; }

        [StringLength(10)]
        public string CardHolderPostCode { get; set; }

        public float VatRate { get; set; }

        [StringLength(255)]
        public string SuccessUrl { get; set; }

        [StringLength(255)]
        public string CancelUrl { get; set; }

        [StringLength(255)]
        public string FailUrl { get; set; }

        [StringLength(36)]
        public string RefundReference { get; set; }

        public bool? Processed { get; set; }

        public DateTime? ExpiryDate { get; set; }

        public int? EReturnId { get; set; }

        public int? TemplateRowId { get; set; }

        public int StatusId { get; set; }

        [StringLength(50)]
        public string CallRecordingSource { get; set; }

        [StringLength(50)]
        public string CallRecordingUserName { get; set; }

        public virtual EReturn EReturn { get; set; }

        public virtual Fund Fund { get; set; }

        public virtual Mop Mop { get; set; }

        public virtual Office Office { get; set; }

        public virtual User User { get; set; }

        public virtual Vat Vat { get; set; }
    }
}
