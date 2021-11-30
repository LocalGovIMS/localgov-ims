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
            StopMessages = new HashSet<StopMessage>();
            PendingTransactions = new HashSet<PendingTransaction>();
            ProcessedTransactions = new HashSet<ProcessedTransaction>();
            MetaData = new HashSet<FundMetaData>();
        }

        [Key]
        [StringLength(5)]
        public string FundCode { get; set; }

        [StringLength(50)]
        [Required]
        public string FundName { get; set; }

        [StringLength(2)]
        public string AccessLevel { get; set; }

        [StringLength(3)]
        public string ValidationReference { get; set; }

        [StringLength(2)]
        public string VatCode { get; set; }

        public decimal? MaximumAmount { get; set; }

        public bool NarrativeFlag { get; set; }

        public bool ExportToFund { get; set; }

        public bool ExportToLedger { get; set; }

        [StringLength(10)]
        public string FundExportFormat { get; set; }

        public bool UseGeneralLedgerCode { get; set; }

        [StringLength(20)]
        public string GeneralLedgerCode { get; set; }

        public bool OverPayAccount { get; set; }

        public bool AccountExist { get; set; }

        public bool AquireAddress { get; set; }

        [StringLength(50)]
        public string DisplayName { get; set; }

        public bool VatOverride { get; set; }

        public bool? LedgerDetail { get; set; }

        public bool Disabled { get; set; }

        public virtual ICollection<AccountHolder> AccountHolders { get; set; }

        public virtual ICollection<FundGroupFund> FundGroupFunds { get; set; }

        public virtual Vat Vat { get; set; }

        public virtual ICollection<StopMessage> StopMessages { get; set; }

        public virtual ICollection<PendingTransaction> PendingTransactions { get; set; }

        public virtual ICollection<ProcessedTransaction> ProcessedTransactions { get; set; }

        public virtual ICollection<FundMetaData> MetaData { get; set; }
    }
}
