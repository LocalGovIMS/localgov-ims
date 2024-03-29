namespace BusinessLogic.Entities
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Fund
    {
        public Fund()
        {
            AccountHolders = new HashSet<AccountHolder>();
            FundGroupFunds = new HashSet<FundGroupFund>();
            FundMessages = new HashSet<FundMessage>();
            PendingTransactions = new HashSet<PendingTransaction>();
            ProcessedTransactions = new HashSet<ProcessedTransaction>();
            Metadata = new HashSet<FundMetadata>();
        }

        [Key]
        [StringLength(5)]
        public string FundCode { get; set; }

        [StringLength(50)]
        [Required]
        public string FundName { get; set; }

        public int? AccountReferenceValidatorId { get; set; }

        [StringLength(2)]
        public string VatCode { get; set; }

        public decimal? MaximumAmount { get; set; }

        public bool OverPayAccount { get; set; }

        public bool AccountExist { get; set; }

        public bool AquireAddress { get; set; }

        [StringLength(50)]
        public string DisplayName { get; set; }

        public bool VatOverride { get; set; }

        public bool Disabled { get; set; }

        public virtual ICollection<AccountHolder> AccountHolders { get; set; }

        public virtual ICollection<FundGroupFund> FundGroupFunds { get; set; }

        public virtual Vat Vat { get; set; }

        public virtual ICollection<FundMessage> FundMessages { get; set; }

        public virtual ICollection<PendingTransaction> PendingTransactions { get; set; }

        public virtual ICollection<ProcessedTransaction> ProcessedTransactions { get; set; }

        public virtual ICollection<FundMetadata> Metadata { get; set; }

        public virtual AccountReferenceValidator AccountReferenceValidator { get; set; }
    }
}
