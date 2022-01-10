using BusinessLogic.Classes.Cryptography;
using BusinessLogic.Classes.Dependencies;
using BusinessLogic.Classes.Formatters;
using BusinessLogic.Classes.Smtp;
using BusinessLogic.Classes.Strategies;
using BusinessLogic.ImportProcessing;
using BusinessLogic.Interfaces.Dependencies;
using BusinessLogic.Interfaces.Formatters;
using BusinessLogic.Interfaces.Services;
using BusinessLogic.Interfaces.Services.Cryptography;
using BusinessLogic.Interfaces.Smtp;
using BusinessLogic.Interfaces.Strategies;
using BusinessLogic.Interfaces.Validators;
using BusinessLogic.Services;
using BusinessLogic.Validators;
using System.Diagnostics.CodeAnalysis;
using Unity;

namespace BusinessLogic
{
    [ExcludeFromCodeCoverage]
    public class UnityConfig
    {
        public static void RegisterComponents(IUnityContainer container)
        {
            container
                .RegisterType<IAccountHolderService, AccountHolderService>()
                .RegisterType<IEReturnService, EReturnService>()
                .RegisterType<IEReturnStatusService, EReturnStatusService>()
                .RegisterType<IEReturnTypeService, EReturnTypeService>()
                .RegisterType<IFundGroupService, FundGroupService>()
                .RegisterType<IFundService, FundService>()
                .RegisterType<IFundMetadataService, FundMetadataService>()
                .RegisterType<IImportService, ImportService>()
                .RegisterType<IImportProcessingRuleService, ImportProcessingRuleService>()
                .RegisterType<IImportProcessingRuleConditionService, ImportProcessingRuleConditionService>()
                .RegisterType<IImportProcessingRuleActionService, ImportProcessingRuleActionService>()
                .RegisterType<IImportProcessingRuleFieldService, ImportProcessingRuleFieldService>()
                .RegisterType<IImportProcessingRuleOperatorService, ImportProcessingRuleOperatorService>()
                .RegisterType<IMaintenanceService, MaintenanceService>()
                .RegisterType<INotificationService, NotificationService>()
                .RegisterType<IOfficeService, OfficeService>()
                .RegisterType<IMethodOfPaymentService, MethodOfPaymentService>()
                .RegisterType<IMethodOfPaymentMetadataService, MethodOfPaymentMetadataService>()
                .RegisterType<IPaymentService, PaymentService>()
                .RegisterType<IRefundService, RefundService>()
                .RegisterType<IRoleService, RoleService>()
                .RegisterType<ISuspenseService, SuspenseService>()
                .RegisterType<ISuspenseNoteService, SuspenseNoteService>()
                .RegisterType<IStopMessageService, StopMessageService>()
                .RegisterType<ISystemMessageService, SystemMessageService>()
                .RegisterType<ITemplateService, TemplateService>()
                .RegisterType<ITemplateRowService, TemplateRowService>()
                .RegisterType<ITransactionService, TransactionService>()
                .RegisterType<ITransactionJournalService, TransactionJournalService>()
                .RegisterType<ITransferService, TransferService>()
                .RegisterType<IUserFundGroupService, UserFundGroupService>()
                .RegisterType<IUserRoleService, UserRoleService>()
                .RegisterType<IUserService, UserService>()
                .RegisterType<IUserTemplateService, UserTemplateService>()
                .RegisterType<IUserPostPaymentMopCodeService, UserPostPaymentMopCodeService>()
                .RegisterType<IValidationService, ValidationService>()
                .RegisterType<IVatService, VatService>()
                .RegisterType<IVatMetadataService, VatMetadataService>()
                .RegisterType<ICryptographyService, MD5CryptographyService>()

                .RegisterType<IEmailService, EmailService>()
                .RegisterType<IEmailServiceDependencies, EmailServiceDependencies>()
                .RegisterType<IEmailFactory, EmailFactory>()

                .RegisterType<IAccountHolderStopMessageValidator, AccountHolderStopMessageValidator>()
                .RegisterType<IAccountReferenceValidator, AccountReferenceValidator>()
                .RegisterType<IBasketValidator, BasketValidator>()
                .RegisterType<IEReturnDescriptionValidator, EReturnDescriptionValidator>()
                .RegisterType<IEReturnReferenceValidator, EReturnReferenceValidator>()
                .RegisterType<ITemplateRowValidator, TemplateRowValidator>()
                .RegisterType<ITransactionJournalValidator, TransactionJournalValidator>()
                .RegisterType<ITransactionTransferValidator, TransactionTransferValidator>()
                .RegisterType<IRollbackTransactionJournalValidator, RollbackTransactionJournalValidator>()

                .RegisterType<IAccountReferenceFormatter, AccountReferenceFormatter>()

                .RegisterType<ITransactionVatStrategy, TransactionVatStrategy>()
                .RegisterType<IJournalAllocationStrategy, SuspenseJournalAllocationStrategy>()
                .RegisterType<IApproveEReturnsStrategy, ApproveEReturnsStrategy>()

                .RegisterType<Clients.PaymentIntegrationClient.IClient, Clients.PaymentIntegrationClient.Client>()

                .RegisterType<IRuleEngine, RuleEngine>()
                .RegisterType<IOperations, Operations>()
                .RegisterType<IFileImporter, FileImporter>()
                .RegisterType<IImportProcessor, ImportProcessor>()
                .RegisterType<IProcessedTransactionModelBuilder, ProcessedTransactionModelBuilder>()
                ;
        }
    }
}
