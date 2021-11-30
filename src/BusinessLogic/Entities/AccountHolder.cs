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
        public string SurnameSoundex { get; set; }

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

        [StringLength(100)]
        public string StopMessageReference { get; set; }

        public DateTime? LastUpdated { get; set; }

        public virtual Fund Fund { get; set; }

        public virtual StopMessage StopMessage { get; set; }
    }
}
