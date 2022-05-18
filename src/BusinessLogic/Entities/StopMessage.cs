namespace BusinessLogic.Entities
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class StopMessage
    {
        public StopMessage()
        {
            AccountHolders = new HashSet<AccountHolder>();
        }

        public int Id { get; set; }

        [StringLength(5)]
        public string FundCode { get; set; }

        [StringLength(100)]
        public string Message { get; set; }

        public virtual Fund Fund { get; set; }

        public virtual ICollection<AccountHolder> AccountHolders { get; set; }
        public virtual ICollection<StopMessageMetadata> Metadata { get; set; }
    }
}
