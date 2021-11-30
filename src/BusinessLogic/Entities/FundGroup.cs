namespace BusinessLogic.Entities
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class FundGroup
    {
        public FundGroup()
        {
            UserFundGroups = new HashSet<UserFundGroup>();
            FundGroupFunds = new HashSet<FundGroupFund>();
        }

        public int FundGroupId { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        public virtual ICollection<UserFundGroup> UserFundGroups { get; set; }

        public virtual ICollection<FundGroupFund> FundGroupFunds { get; set; }
    }
}
