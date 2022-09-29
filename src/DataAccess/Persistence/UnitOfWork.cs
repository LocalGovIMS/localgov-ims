using BusinessLogic.Entities;
using BusinessLogic.Interfaces.Persistence;
using BusinessLogic.Interfaces.Repositories;
using BusinessLogic.Interfaces.Security;
using DataAccess.Interfaces;
using log4net;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace DataAccess.Persistence
{
    public sealed class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly IncomeDbContext _context;
        private readonly ISecurityContext _securityContext;
        private readonly IAuditLogger _auditLogger;
        private readonly ILog _log;

        #region Repositories

        public IAccountHolderRepository AccountHolders { get; private set; }
        public IAccountReferenceValidatorRepository AccountReferenceValidators { get; private set; }
        public IActivityLogRepository ActivityLogs { get; private set; }
        public ICheckDigitConfigurationRepository CheckDigitConfigurations { get; private set; }
        public IEReturnRepository EReturns { get; private set; }
        public IEReturnCashRepository EReturnCashes { get; private set; }
        public IEReturnChequeRepository EReturnCheques { get; private set; }
        public IEReturnStatusRepository EReturnStatuses { get; private set; }
        public IEReturnTypeRepository EReturnTypes { get; private set; }
        public IRepository<EReturnNote> EReturnNotes { get; private set; }
        public IFundRepository Funds { get; private set; }
        public IFundMetadataRepository FundMetadatas { get; private set; }
        public IFundGroupRepository FundGroups { get; private set; }
        public IFundGroupFundRepository FundGroupFunds { get; private set; }
        public IMethodOfPaymentRepository Mops { get; private set; }
        public IMethodOfPaymentMetadataRepository MopMetadatas { get; private set; }
        public IMethodOfPaymentMetadataKeyRepository MopMetadataKeys { get; private set; }
        public IOfficeRepository Offices { get; private set; }
        public IPendingTransactionRepository PendingTransactions { get; private set; }
        public IRoleRepository Roles { get; private set; }
        public IFundMessageRepository FundMessages { get; private set; }
        public IFundMessageMetadataRepository FundMessageMetadata { get; private set; }
        public ISystemMessageRepository SystemMessages { get; private set; }
        public ISuspenseRepository Suspenses { get; private set; }
        public IRepository<SuspenseNote> SuspenseNotes { get; private set; }
        public ITemplateRepository Templates { get; private set; }
        public ITemplateRowRepository TemplateRows { get; private set; }
        public ITransactionRepository Transactions { get; private set; }
        public IUserRepository Users { get; private set; }
        public IUserFundGroupRepository UserFundGroups { get; private set; }
        public IUserRoleRepository UserRoles { get; private set; }
        public IUserTemplateRepository UserTemplates { get; private set; }
        public IUserMethodOfPaymentRepository UserMethodOfPayments { get; private set; }
        public IVatRepository Vats { get; private set; }
        public IVatMetadataRepository VatMetadatas { get; private set; }
        public IPaymentIntegrationRepository PaymentIntegrations { get; private set; }
        public IImportProcessingRuleRepository ImportProcessingRules { get; private set; }
        public IImportProcessingRuleConditionRepository ImportProcessingRuleConditions { get; private set; }
        public IImportProcessingRuleActionRepository ImportProcessingRuleActions { get; private set; }
        public IRepository<ImportProcessingRuleField> ImportProcessingRuleFields { get; private set; }
        public IRepository<ImportProcessingRuleOperator> ImportProcessingRuleOperators { get; private set; }
        public IFileImportRepository FileImports { get; private set; }
        public IImportRepository Imports { get; private set; }
        public IRepository<ImportRow> ImportRows { get; private set; }
        public IImportTypeRepository ImportTypes { get; private set; }
        public IImportTypeImportProcessingRuleRepository ImportTypeImportProcessingRules { get; private set; }

        #endregion

        public UnitOfWork(IncomeDbContext context,
            ISecurityContext securityContext,
            IAccountHolderRepository accountHolderRepository,
            IAccountReferenceValidatorRepository accountReferenceValidatorRepository,
            IActivityLogRepository activityLogRepository,
            ICheckDigitConfigurationRepository checkDigitConfigurationRepository,
            IEReturnRepository eReturnRepository,
            IEReturnCashRepository eReturnCashesRepository,
            IEReturnChequeRepository eReturnChequesRepository,
            IEReturnStatusRepository eReturnStatusRepository,
            IEReturnTypeRepository eReturnTypeRepository,
            IRepository<EReturnNote> eReturnNotesRepository,
            IFundRepository fundRepository,
            IFundMetadataRepository fundMetadataRepository,
            IFundGroupRepository fundGroupRepository,
            IFundGroupFundRepository fundGroupFundRepository,
            IMethodOfPaymentRepository mopRepository,
            IMethodOfPaymentMetadataRepository mopMetadataRepository,
            IMethodOfPaymentMetadataKeyRepository mopMetadataKeyRepository,
            IOfficeRepository officeRepository,
            IPendingTransactionRepository pendingTransactionRepository,
            IRoleRepository roleRepository,
            IFundMessageRepository fundMessageRepository,
            IFundMessageMetadataRepository fundMessageMetadataRepository,
            ISystemMessageRepository systemMessageRepository,
            ISuspenseRepository suspenseRepository,
            IRepository<SuspenseNote> suspenseNoteRepository,
            ITemplateRepository templateRepository,
            ITemplateRowRepository templateRowRepository,
            ITransactionRepository transactionRepository,
            IUserRepository userRepository,
            IUserFundGroupRepository userFundGroupRepository,
            IUserRoleRepository userRoleRepository,
            IUserTemplateRepository userTemplateRepository,
            IUserMethodOfPaymentRepository userPostPaymentMopCodeRepository,
            IVatRepository vatRepository,
            IVatMetadataRepository VatMetadataRepository,
            IPaymentIntegrationRepository paymentIntegrationRepository,
            IImportProcessingRuleRepository importProcessingRuleRepository,
            IImportProcessingRuleConditionRepository importProcessingRuleConditionRepository,
            IImportProcessingRuleActionRepository importProcessingRuleActionRepository,
            IRepository<ImportProcessingRuleField> importProcessingRuleFieldRepository,
            IRepository<ImportProcessingRuleOperator> importProcessingRuleOperatorRepository,
            IFileImportRepository fileImports,
            IImportTypeRepository importTypes,
            IImportTypeImportProcessingRuleRepository importTypeImportProcessingRules,
            IImportRepository importsRepository,
            IRepository<ImportRow> importRowRepository,
            IAuditLogger auditLogger,
            ILog log)
        {
            _context = context;
            _securityContext = securityContext;

            AccountHolders = accountHolderRepository;
            AccountReferenceValidators = accountReferenceValidatorRepository;
            ActivityLogs = activityLogRepository;
            CheckDigitConfigurations = checkDigitConfigurationRepository;
            EReturns = eReturnRepository;
            EReturnCashes = eReturnCashesRepository;
            EReturnCheques = eReturnChequesRepository;
            EReturnStatuses = eReturnStatusRepository;
            EReturnTypes = eReturnTypeRepository;
            EReturnNotes = eReturnNotesRepository;
            Funds = fundRepository;
            FundMetadatas = fundMetadataRepository;
            FundGroups = fundGroupRepository;
            FundGroupFunds = fundGroupFundRepository;
            Mops = mopRepository;
            MopMetadatas = mopMetadataRepository;
            MopMetadataKeys = mopMetadataKeyRepository;
            Offices = officeRepository;
            PendingTransactions = pendingTransactionRepository;
            Roles = roleRepository;
            FundMessages = fundMessageRepository;
            FundMessageMetadata = fundMessageMetadataRepository;
            Suspenses = suspenseRepository;
            SuspenseNotes = suspenseNoteRepository;
            SystemMessages = systemMessageRepository;
            Templates = templateRepository;
            TemplateRows = templateRowRepository;
            Transactions = transactionRepository;
            Users = userRepository;
            UserFundGroups = userFundGroupRepository;
            UserRoles = userRoleRepository;
            UserTemplates = userTemplateRepository;
            UserMethodOfPayments = userPostPaymentMopCodeRepository;
            Vats = vatRepository;
            VatMetadatas = VatMetadataRepository;
            PaymentIntegrations = paymentIntegrationRepository;
            ImportProcessingRules = importProcessingRuleRepository;
            ImportProcessingRuleConditions = importProcessingRuleConditionRepository;
            ImportProcessingRuleActions = importProcessingRuleActionRepository;
            ImportProcessingRuleFields = importProcessingRuleFieldRepository;
            ImportProcessingRuleOperators = importProcessingRuleOperatorRepository;
            FileImports = fileImports;
            ImportTypes = importTypes;
            ImportTypeImportProcessingRules = importTypeImportProcessingRules;
            Imports = importsRepository;
            ImportRows = importRowRepository;

            _auditLogger = auditLogger;
            _log = log;

            ApplyFilters();
        }

        public int Complete(int userId)
        {
            try
            {
                _auditLogger.CreateAudit(_context, userId);
            }
            catch (Exception e)
            {
                _log.Error(e);
            }

            // Note: Could we wrap this in a try catch and call ResetChanges if there is a problem?
            // Means we don't have to handle it in the consuming code - it's all handled here.

            return _context.SaveChanges();
        }

        public int CompleteWithoutAudit(int userId)
        {
            // Note: Could we wrap this in a try catch and call ResetChanges if there is a problem?
            // Means we don't have to handle it in the consuming code - it's all handled here.

            return _context.SaveChanges();
        }

        // Note: See note above in Complete. We could potentially make this private and handle all rollbacks in this class, rather than consuming code.
        public void ResetChanges()
        {
            foreach (var change in _context.ChangeTracker.Entries())
            {
                switch (change.State)
                {
                    case EntityState.Modified:
                    case EntityState.Deleted:
                        change.State = EntityState.Modified;
                        change.State = EntityState.Unchanged;
                        break;
                    case EntityState.Added:
                        change.State = EntityState.Detached;
                        break;
                }
            }
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        #region Private methods

        private void ApplyFilters()
        {
            if (!HttpContext.Current.User.Identity.IsAuthenticated) return;

            // Super user - no filters!
            if (_securityContext.IsSuperUser) return;

            if (_securityContext.IsPublicUser)
            {
                // Filter out things here that public users can shouldn't see

                return;
            };

            // We only need to get this data once...
            var userAccessibleFunds = _securityContext.AccessibleFundCodes;
            var userAccessibleTemplates = _securityContext.AccessibleTemplates;

            // Now apply restrictions.
            // This idea here is that we're not repeating the restrictions in several places.
            // I didn't want to include them in the repository as this isn't repository 
            // specific code (you oculd argue it isn't UoW specific code - but it does 
            // seem to fit a little better in here).
            Transactions.AddFilter(x => userAccessibleFunds.Contains(x.FundCode));
            AccountHolders.AddFilter(x => userAccessibleFunds.Contains(x.FundCode));
            Funds.AddFilter(x => userAccessibleFunds.Contains(x.FundCode));
            FundGroupFunds.AddFilter(x => userAccessibleFunds.Contains(x.Fund.FundCode));

            if (!_securityContext.IsInRole(BusinessLogic.Security.Role.Finance) &&
                !_securityContext.IsInRole(BusinessLogic.Security.Role.ChequeProcess))
            {
                Templates.AddFilter(x => userAccessibleTemplates.Contains(x.Id.ToString()));
                EReturns.AddFilter(x => userAccessibleTemplates.Contains(x.Template.Id.ToString()));
            }
        }

        #endregion
    }
}