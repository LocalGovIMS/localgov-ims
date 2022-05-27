using BusinessLogic.Entities;
using BusinessLogic.Interfaces.Persistence;
using BusinessLogic.Interfaces.Repositories;
using DataAccess.Classes;
using DataAccess.Interfaces;
using DataAccess.Persistence;
using DataAccess.Repositories;
using Unity;

namespace DataAccess
{
    public class UnityConfig
    {
        public static void RegisterComponents(IUnityContainer container)
        {
            container
                .RegisterType<IAccountHolderRepository, AccountHolderRepository>()
                .RegisterType<IAccountReferenceValidatorRepository, AccountReferenceValidatorRepository>()
                .RegisterType<IActivityLogRepository, ActivityLogRepository>()
                .RegisterType<ICheckDigitConfigurationRepository, CheckDigitConfigurationRepository>()
                .RegisterType<IEReturnRepository, EReturnRepository>()
                .RegisterType<IEReturnCashRepository, EReturnCashRepository>()
                .RegisterType<IEReturnChequeRepository, EReturnChequeRepository>()
                .RegisterType<IEReturnStatusRepository, EReturnStatusRepository>()
                .RegisterType<IEReturnTypeRepository, EReturnTypeRepository>()
                .RegisterType<IFundGroupFundRepository, FundGroupFundRepository>()
                .RegisterType<IFundGroupRepository, FundGroupRepository>()
                .RegisterType<IFundRepository, FundRepository>()
                .RegisterType<IFundMetadataRepository, FundMetadataRepository>()
                .RegisterType<IMethodOfPaymentRepository, MethofOfPaymentRepository>()
                .RegisterType<IMethodOfPaymentMetadataRepository, MethodOfPaymentMetadataRepository>()
                .RegisterType<IOfficeRepository, OfficeRepository>()
                .RegisterType<IPendingTransactionRepository, PendingTransactionRepository>()
                .RegisterType<IRoleRepository, RoleRepository>()
                .RegisterType<IFundMessageRepository, FundMessageRepository>()
                .RegisterType<IFundMessageMetadataRepository, FundMessageMetadataRepository>()
                .RegisterType<ISuspenseRepository, SuspenseRepository>()
                .RegisterType<ISystemMessageRepository, SystemMessageRepository>()
                .RegisterType<ITemplateRepository, TemplateRepository>()
                .RegisterType<ITemplateRowRepository, TemplateRowRepository>()
                .RegisterType<ITransactionNotificationRepository, TransactionNotificationRepository>()
                .RegisterType<ITransactionRepository, TransactionRepository>()
                .RegisterType<IUserFundGroupRepository, UserFundGroupRepository>()
                .RegisterType<IUserRepository, UserRepository>()
                .RegisterType<IUserRoleRepository, UserRoleRepository>()
                .RegisterType<IUserMethodOfPaymentRepository, UserMethodOfPaymentRepository>()
                .RegisterType<IUserTemplateRepository, UserTemplateRepository>()
                .RegisterType<IVatRepository, VatRepository>()
                .RegisterType<IVatMetadataRepository, VatMetadataRepository>()
                .RegisterType<IRepository<EmailLog>, Repository<EmailLog>>()
                .RegisterType<IPaymentIntegrationRepository, PaymentIntegrationRepository>()
                .RegisterType<IImportProcessingRuleRepository, ImportProcessingRuleRepository>()
                .RegisterType<IImportProcessingRuleConditionRepository, ImportProcessingRuleConditionRepository>()
                .RegisterType<IImportProcessingRuleActionRepository, ImportProcessingRuleActionRepository>()
                .RegisterType<IFileImportRepository, FileImportRepository>()
                .RegisterType(typeof(IRepository<>), typeof(Repository<>))
                .RegisterType<IAuditLogger, AuditLogger>()
                ;

            container
                .RegisterType<IUnitOfWork, UnitOfWork>();
        }
    }
}
