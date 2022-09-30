using BusinessLogic.Entities;
using BusinessLogic.Interfaces.Repositories;

namespace BusinessLogic.Interfaces.Persistence
{
    public interface IUnitOfWork
    {
        IAccountHolderRepository AccountHolders { get; }
        IAccountReferenceValidatorRepository AccountReferenceValidators { get; }
        IActivityLogRepository ActivityLogs { get; }
        ICheckDigitConfigurationRepository CheckDigitConfigurations { get; }
        IEReturnRepository EReturns { get; }
        IEReturnCashRepository EReturnCashes { get; }
        IEReturnChequeRepository EReturnCheques { get; }
        IEReturnStatusRepository EReturnStatuses { get; }
        IEReturnTypeRepository EReturnTypes { get; }
        IRepository<EReturnNote> EReturnNotes { get; }
        IFundRepository Funds { get; }
        IFundMetadataRepository FundMetadatas { get; }
        IFundGroupRepository FundGroups { get; }
        IFundGroupFundRepository FundGroupFunds { get; }
        IMethodOfPaymentRepository Mops { get; }
        IMethodOfPaymentMetadataRepository MopMetadatas { get; }
        IMetadataKeyRepository MetadataKeys { get; }
        IOfficeRepository Offices { get; }
        IPendingTransactionRepository PendingTransactions { get; }
        IRoleRepository Roles { get; }
        IFundMessageRepository FundMessages { get; }
        IFundMessageMetadataRepository FundMessageMetadata { get; }
        ISystemMessageRepository SystemMessages { get; }
        ISuspenseRepository Suspenses { get; }
        IRepository<SuspenseNote> SuspenseNotes { get; }
        ITemplateRepository Templates { get; }
        ITemplateRowRepository TemplateRows { get; }
        ITransactionRepository Transactions { get; }
        IUserRepository Users { get; }
        IUserFundGroupRepository UserFundGroups { get; }
        IUserRoleRepository UserRoles { get; }
        IUserTemplateRepository UserTemplates { get; }
        IUserMethodOfPaymentRepository UserMethodOfPayments { get; }
        IVatRepository Vats { get; }
        IVatMetadataRepository VatMetadatas { get; }
        IPaymentIntegrationRepository PaymentIntegrations { get; }
        IImportProcessingRuleRepository ImportProcessingRules { get; }
        IImportProcessingRuleConditionRepository ImportProcessingRuleConditions { get; }
        IImportProcessingRuleActionRepository ImportProcessingRuleActions { get; }
        IRepository<ImportProcessingRuleField> ImportProcessingRuleFields { get; }
        IRepository<ImportProcessingRuleOperator> ImportProcessingRuleOperators { get; }
        IFileImportRepository FileImports { get; }
        IImportRepository Imports { get; }
        IRepository<ImportRow> ImportRows { get; }
        IImportTypeRepository ImportTypes { get; }
        IImportTypeImportProcessingRuleRepository ImportTypeImportProcessingRules { get; }

        int Complete(int userId);
        int CompleteWithoutAudit(int userId);
        void ResetChanges();
    }
}