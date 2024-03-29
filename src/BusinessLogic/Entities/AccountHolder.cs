namespace BusinessLogic.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public partial class AccountHolder
    {
        [Key]
        [StringLength(30)]
        public string AccountReference { get; set; }

        [StringLength(5)]
        public string FundCode { get; set; }

        public decimal? CurrentBalance { get; set; }

        public decimal? PeriodDebit { get; set; }

        [StringLength(10)]
        public string Title { get; set; }

        [StringLength(50)]
        public string Forename { get; set; }

        [StringLength(50)]
        public string Surname { get; set; }

        [StringLength(50)]
        public string SurnameSoundex { get; private set; }

        [StringLength(60)]
        public string AddressLine1 { get; set; }

        [StringLength(30)]
        public string AddressLine2 { get; set; }

        [StringLength(30)]
        public string AddressLine3 { get; set; }

        [StringLength(30)]
        public string AddressLine4 { get; set; }

        [StringLength(10)]
        public string Postcode { get; set; }

        public decimal? PeriodCredit { get; set; }

        [StringLength(10)]
        public string RecordType { get; set; }

        [StringLength(50)]
        public string UserField1 { get; set; }

        [StringLength(50)]
        public string UserField2 { get; set; }

        [StringLength(50)]
        public string UserField3 { get; set; }

        public int? FundMessageId { get; set; }

        public DateTime CreatedAt { get; set; }

        public int CreatedByUserId { get; set; }

        public User CreatedByUser { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public int? UpdatedByUserId { get; set; }

        public User UpdatedByUser { get; set; }

        public virtual Fund Fund { get; set; }

        public virtual FundMessage FundMessage { get; set; }
    }
}
