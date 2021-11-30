namespace BusinessLogic.Entities
{
    using System.ComponentModel.DataAnnotations;

    public partial class FundGroupFund
    {
        public int FundGroupFundId { get; set; }

        public int FundGroupId { get; set; }

        [Required]
        [StringLength(5)]
        public string FundCode { get; set; }

        public virtual FundGroup FundGroup { get; set; }

        public virtual Fund Fund { get; set; }
    }
}
