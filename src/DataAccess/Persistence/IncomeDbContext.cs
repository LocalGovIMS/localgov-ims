using BusinessLogic.Authentication.Identity;
using BusinessLogic.Entities;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Data.Entity;

namespace DataAccess.Persistence
{
    public partial class IncomeDbContext : IdentityDbContext<PaymentsUser>
    {
        public IncomeDbContext()
            : base("name=IncomeDb")
        {
        }

        public static IncomeDbContext Create()
        {
            return new IncomeDbContext();
        }

        public virtual DbSet<AccountHolder> AccountHolders { get; set; }
        public virtual DbSet<AccountValidation> AccountValidations { get; set; }
        public virtual DbSet<AccountValidationWeighting> Account_Validation_Weightings { get; set; }
        public virtual DbSet<ActivityLog> ActivityLogs { get; set; }
        public virtual DbSet<ApplicationLog> ApplicationLogs { get; set; }
        public virtual DbSet<EmailLog> EmailLogs { get; set; }
        public virtual DbSet<EReturn> EReturns { get; set; }
        public virtual DbSet<EReturnCash> EReturnCashes { get; set; }
        public virtual DbSet<EReturnCheque> EReturnCheques { get; set; }
        public virtual DbSet<EReturnStatus> EReturnStatus { get; set; }
        public virtual DbSet<EReturnType> EReturnTypes { get; set; }
        public virtual DbSet<FundGroup> FundGroups { get; set; }
        public virtual DbSet<FundGroupFund> FundGroupFunds { get; set; }
        public virtual DbSet<Fund> Funds { get; set; }
        public virtual DbSet<FundMetaData> FundMetadatas { get; set; }
        public virtual DbSet<Mop> MOPs { get; set; }
        public virtual DbSet<MopMetaData> MopMetadatas { get; set; }
        public virtual DbSet<Office> Offices { get; set; }
        public virtual DbSet<Role> ImsRoles { get; set; }
        public virtual DbSet<Suspense> Suspenses { get; set; }
        public virtual DbSet<SuspenseNote> SuspenseNotes { get; set; }
        public virtual DbSet<SuspenseProcessedTransaction> SuspenseProcessedTransactions { get; set; }
        public virtual DbSet<SystemMessage> SystemMessages { get; set; }
        public virtual DbSet<TemplateRow> TemplateRows { get; set; }
        public virtual DbSet<Template> Templates { get; set; }
        public virtual DbSet<TransactionNotification> TransactionNotifications { get; set; }
        public virtual DbSet<PendingTransaction> PendingTransactions { get; set; }
        public virtual DbSet<ProcessedTransaction> ProcessedTransactions { get; set; }
        public virtual DbSet<UserFundGroup> ImsUserFundGroups { get; set; }
        public virtual DbSet<UserPostPaymentMopCode> ImsUserPostPaymentMopCodes { get; set; }
        public virtual DbSet<UserRole> ImsUserRoles { get; set; }
        public virtual DbSet<User> ImsUsers { get; set; }
        public virtual DbSet<UserTemplate> ImsUserTemplates { get; set; }
        public virtual DbSet<Vat> VATs { get; set; }
        public virtual DbSet<VatMetaData> VatMetadatas { get; set; }
        public virtual DbSet<ScheduleLog> ScheduleLogs { get; set; }
        public virtual DbSet<StopMessage> Stop_Message { get; set; }
        public virtual DbSet<TransactionStatus> TransactionStatus { get; set; }
        public virtual DbSet<PaymentIntegration> PaymentServiceProviders { get; set; }
        public virtual DbSet<ImportProcessingRule> ImportProcessingRules { get; set; }
        public virtual DbSet<ImportProcessingRuleCondition> ImportProcessingRuleConditions { get; set; }
        public virtual DbSet<ImportProcessingRuleAction> ImportProcessingRuleActions { get; set; }
        public virtual DbSet<ImportProcessingRuleField> ImportProcessingRuleFields { get; set; }
        public virtual DbSet<ImportProcessingRuleOperator> ImportProcessingRuleOperators { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            SetupGeneralProperties(modelBuilder);
            SetupSpecificProperties(modelBuilder);
            SetupKeys(modelBuilder);
            SetupRelationships(modelBuilder);
            SetupIndexes(modelBuilder);
        }

        private void SetupGeneralProperties(DbModelBuilder modelBuilder)
        {
            modelBuilder.Properties<DateTime>()
                .Configure(c => c.HasColumnType("datetime2"));
        }

        private void SetupSpecificProperties(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Vat>()
                .Property(c => c.Percentage).HasPrecision(18, 3);
        }

        private void SetupKeys(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PendingTransaction>()
                .HasKey(x => x.Id, config => config.IsClustered(false));

            modelBuilder.Entity<ProcessedTransaction>()
                .HasKey(x => x.Id, config => config.IsClustered(false));

            modelBuilder.Entity<ScheduleLog>()
                .HasKey(x => x.Id, config => config.IsClustered(false));

            modelBuilder.Entity<StopMessage>()
                .HasKey(x => new { x.Id, x.FundCode });
        }

        private void SetupRelationships(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AccountHolder>()
                .HasOptional(p => p.StopMessage)
                .WithMany(c => c.AccountHolders)
                .HasForeignKey(p => new { p.StopMessageReference, p.FundCode });

            modelBuilder.Entity<AccountValidation>()
                .HasMany(e => e.AccountValidationWeightings)
                .WithRequired(e => e.AccountValidation)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<EReturn>()
                .HasMany(e => e.EReturnCashes)
                .WithRequired(e => e.EReturn)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<EReturn>()
                .HasMany(e => e.EReturnCheques)
                .WithRequired(e => e.EReturn)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<EReturnStatus>()
                .HasMany(e => e.EReturns)
                .WithRequired(e => e.EReturnStatus)
                .HasForeignKey(e => e.StatusId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<EReturnType>()
                .HasMany(e => e.EReturns)
                .WithRequired(e => e.EReturnType)
                .HasForeignKey(e => e.TypeId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<FundGroup>()
                .HasMany(e => e.UserFundGroups)
                .WithRequired(e => e.FundGroup)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<FundGroup>()
                .HasMany(e => e.FundGroupFunds)
                .WithRequired(e => e.FundGroup)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Fund>()
                .HasMany(e => e.FundGroupFunds)
                .WithRequired(e => e.Fund)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Fund>()
                .HasMany(e => e.StopMessages)
                .WithRequired(e => e.Fund)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Fund>()
                .HasMany(e => e.MetaData)
                .WithRequired(e => e.Fund)
                .HasForeignKey(e => e.FundCode)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Fund>()
                .HasMany(e => e.AccountHolders)
                .WithOptional(e => e.Fund) // HIGH: This satisfies the existing schema - but should it be WithRequired? The column is never empty in the DB
                .HasForeignKey(e => e.FundCode)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Mop>()
                .HasMany(e => e.UserPostPaymentMopCodes)
                .WithRequired(e => e.Mop)
                .HasForeignKey(e => e.MopCode)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Mop>()
                .HasMany(e => e.MetaData)
                .WithRequired(e => e.Mop)
                .HasForeignKey(e => e.MopCode)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Office>()
                .HasMany(e => e.PendingTransactions)
                .WithOptional(e => e.Office)
                .HasForeignKey(e => e.OfficeCode);

            modelBuilder.Entity<Office>()
                .HasMany(e => e.ProcessedTransactions)
                .WithOptional(e => e.Office)
                .HasForeignKey(e => e.OfficeCode);

            modelBuilder.Entity<Role>()
                .HasMany(e => e.UserRoles)
                .WithRequired(e => e.Role)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Suspense>()
                .HasMany(e => e.SuspenseProcessedTransactions)
                .WithRequired(e => e.Suspense)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Suspense>()
                .HasMany(e => e.SuspenseNotes)
                .WithRequired(e => e.Suspense)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Template>()
                .HasMany(e => e.EReturns)
                .WithRequired(e => e.Template)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Template>()
                .HasMany(e => e.TemplateRows)
                .WithRequired(e => e.Template)
                .HasForeignKey(e => e.TemplateId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Template>()
                .HasMany(e => e.UserTemplates)
                .WithRequired(e => e.Template)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ProcessedTransaction>()
                .HasMany(e => e.EmailLogs)
                .WithOptional(e => e.ProcessedTransaction)
                .HasForeignKey(e => e.TransactionProcessedId);

            modelBuilder.Entity<ProcessedTransaction>()
                .HasMany(e => e.SuspenseProcessedTransactions)
                .WithRequired(e => e.TransactionIn)
                .HasForeignKey(e => e.TransactionInId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ProcessedTransaction>()
                .HasMany(e => e.SuspenseProcessedTransactions1)
                .WithRequired(e => e.TransactionOut)
                .HasForeignKey(e => e.TransactionOutId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.ApprovedEReturns)
                .WithOptional(e => e.ApprovedByUser)
                .HasForeignKey(e => e.ApprovedByUserId);

            modelBuilder.Entity<User>()
                .HasMany(e => e.CreatedEReturns)
                .WithOptional(e => e.CreatedByUser)
                .HasForeignKey(e => e.CreatedByUserId);

            modelBuilder.Entity<User>()
                .HasMany(e => e.SubmittedEReturns)
                .WithOptional(e => e.SubmittedByUser)
                .HasForeignKey(e => e.SubmittedByUserId);

            modelBuilder.Entity<User>()
                .HasMany(e => e.ProcessedTransactions)
                .WithRequired(e => e.User)
                .HasForeignKey(e => e.UserCode)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.PendingTransactions)
                .WithRequired(e => e.User)
                .HasForeignKey(e => e.UserCode)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.SuspenseNotes)
                .WithRequired(e => e.CreatedByUser)
                .HasForeignKey(e => e.CreatedByUserId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.SuspenseProcessedTransactions)
                .WithRequired(e => e.CreatedByUser)
                .HasForeignKey(e => e.CreatedByUserId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.UserFundGroups)
                .WithRequired(e => e.User)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.UserPostPaymentMopCodes)
                .WithRequired(e => e.User)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.UserRoles)
                .WithRequired(e => e.User)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.UserTemplates)
                .WithRequired(e => e.User)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Vat>()
                .HasMany(e => e.TemplateRows)
                .WithRequired(e => e.VAT)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Vat>()
                .HasMany(e => e.MetaData)
                .WithRequired(e => e.Vat)
                .HasForeignKey(e => e.VatCode)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ImportProcessingRule>()
                .HasMany(e => e.Actions)
                .WithRequired(e => e.Rule)
                .HasForeignKey(e => e.ImportProcessingRuleId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ImportProcessingRule>()
                .HasMany(e => e.Conditions)
                .WithRequired(e => e.Rule)
                .HasForeignKey(e => e.ImportProcessingRuleId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ImportProcessingRuleField>()
                .HasMany(e => e.Conditions)
                .WithRequired(e => e.Field)
                .HasForeignKey(e => e.ImportProcessingRuleFieldId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ImportProcessingRuleField>()
                .HasMany(e => e.Actions)
                .WithRequired(e => e.Field)
                .HasForeignKey(e => e.ImportProcessingRuleFieldId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ImportProcessingRuleOperator>()
                .HasMany(e => e.Conditions)
                .WithRequired(e => e.Operator)
                .HasForeignKey(e => e.ImportProcessingRuleOperatorId)
                .WillCascadeOnDelete(false);
        }

        private void SetupIndexes(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AccountHolder>()
                .HasIndex(s => s.FundCode)
                .IsUnique(false);

            modelBuilder.Entity<FundGroupFund>()
                .HasIndex(s => new { s.FundGroupId, s.FundCode })
                .IsUnique(true);

            modelBuilder.Entity<Fund>()
                .HasIndex(s => new { s.FundCode, s.FundName });

            modelBuilder.Entity<PendingTransaction>()
                .HasIndex(s => new { s.TransactionReference })
                .IsUnique(true);

            modelBuilder.Entity<PendingTransaction>()
                .HasIndex(s => new { s.RefundReference, s.Processed });

            modelBuilder.Entity<PendingTransaction>()
                .HasIndex(r => r.TransactionDate)
                .IsClustered(true);

            modelBuilder.Entity<ProcessedTransaction>()
                .HasIndex(s => new { s.AccountReference });

            modelBuilder.Entity<ProcessedTransaction>()
                .HasIndex(s => new { s.Amount });

            modelBuilder.Entity<ProcessedTransaction>()
                .HasIndex(s => new { s.AppReference });

            modelBuilder.Entity<ProcessedTransaction>()
                .HasIndex(s => new { s.EntryDate })
                .IsClustered(true);

            modelBuilder.Entity<ProcessedTransaction>()
                .HasIndex(s => new { s.InternalReference });

            modelBuilder.Entity<ProcessedTransaction>()
                .HasIndex(s => new { s.PspReference });

            modelBuilder.Entity<ProcessedTransaction>()
                .HasIndex(s => new { s.TransactionReference });

            modelBuilder.Entity<UserFundGroup>()
                .HasIndex(s => new { s.UserId, s.FundGroupId })
                .IsUnique(true);

            modelBuilder.Entity<UserPostPaymentMopCode>()
                .HasIndex(s => new { s.UserId, s.MopCode })
                .IsUnique(true);

            modelBuilder.Entity<UserRole>()
                .HasIndex(s => new { s.UserId, s.RoleId })
                .IsUnique(true);

            modelBuilder.Entity<User>()
                .HasIndex(s => new { s.UserName, s.Disabled })
                .IsUnique(true);

            modelBuilder.Entity<ScheduleLog>()
                .HasIndex(r => r.JobRunTime)
                .IsClustered(true);
        }
    }
}
