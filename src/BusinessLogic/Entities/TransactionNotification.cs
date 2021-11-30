namespace BusinessLogic.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public partial class TransactionNotification
    {
        public int Id { get; set; }

        [StringLength(100)]
        public string MerchantReference { get; set; }

        [StringLength(100)]
        public string EventCode { get; set; }

        [StringLength(1000)]
        public string OriginalReference { get; set; }

        [StringLength(1000)]
        public string PspReference { get; set; }

        public DateTime? EventDate { get; set; }

        [StringLength(100)]
        public string PaymentMethod { get; set; }

        public decimal? Amount { get; set; }

        public bool Success { get; set; }

        [StringLength(1000)]
        public string Reason { get; set; }

        [StringLength(100)]
        public string Operations { get; set; }

        public bool Live { get; set; }

        public bool Processed { get; set; }

        [StringLength(100)]
        public string MerchantAccountCode { get; set; }
    }
}
