namespace BusinessLogic.Entities
{
    public partial class UserFundGroup
    {
        public int UserFundGroupId { get; set; }

        public int UserId { get; set; }

        public int FundGroupId { get; set; }

        public virtual FundGroup FundGroup { get; set; }

        public virtual User User { get; set; }
    }
}
