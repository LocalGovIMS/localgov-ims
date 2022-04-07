using BusinessLogic.Classes.Cryptography;
using BusinessLogic.Classes.Dependencies;
using BusinessLogic.Classes.Smtp;
using BusinessLogic.Classes.Strategies;
using BusinessLogic.Enums;
using BusinessLogic.ImportProcessing;
using BusinessLogic.Interfaces.Dependencies;
using BusinessLogic.Interfaces.Services;
using BusinessLogic.Interfaces.Services.Cryptography;
using BusinessLogic.Interfaces.Smtp;
using BusinessLogic.Interfaces.Strategies;
using BusinessLogic.Interfaces.Validators;
using BusinessLogic.Services;
using BusinessLogic.Validators;
using BusinessLogic.Validators.Payment;
using System;
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
                .RegisterType<IPaymentIntegrationService, PaymentIntegrationService>()
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
                .RegisterType<IVatService, VatService>()
                .RegisterType<IVatMetadataService, VatMetadataService>()
                .RegisterType<ICryptographyService, MD5CryptographyService>()
                .RegisterType<IAccountReferenceValidatorService, AccountReferenceValidatorService>()

                .RegisterType<IEmailService, EmailService>()
                .RegisterType<IEmailServiceDependencies, EmailServiceDependencies>()
                .RegisterType<IEmailFactory, EmailFactory>()

                .RegisterType<IAccountHolderStopMessageValidator, AccountHolderStopMessageValidator>()
                .RegisterType<IBasketValidator, BasketValidator>()
                .RegisterType<IEReturnDescriptionValidator, EReturnDescriptionValidator>()
                .RegisterType<IEReturnReferenceValidator, EReturnReferenceValidator>()
                .RegisterType<ITemplateRowValidator, TemplateRowValidator>()
                .RegisterType<ITransactionFeeValidator, TransactionFeeValidator>()
                .RegisterType<ITransactionJournalValidator, TransactionJournalValidator>()
                .RegisterType<ITransactionTransferValidator, TransactionTransferValidator>()
                .RegisterType<IRollbackTransactionJournalValidator, RollbackTransactionJournalValidator>()

                .RegisterType<ITransactionVatStrategy, TransactionVatStrategy>()
                .RegisterType<IJournalAllocationStrategy, SuspenseJournalAllocationStrategy>()
                .RegisterType<IApproveEReturnsStrategy, ApproveEReturnsStrategy>()

                .RegisterType<Clients.PaymentIntegrationClient.IClient, Clients.PaymentIntegrationClient.Client>()

                .RegisterType<IRuleEngine, RuleEngine>()
                .RegisterType<IOperations, Operations>()
                .RegisterType<IFileImporter, FileImporter>()
                .RegisterType<IImportProcessor, ImportProcessor>()
                .RegisterType<IProcessedTransactionModelBuilder, ProcessedTransactionModelBuilder>()

                .RegisterType<IPaymentValidationHandler, PaymentValidationHandler>()

                .RegisterType<IValidator<PaymentValidationArgs>, AmountValidator>(nameof(AmountValidator))
                .RegisterType<IValidator<PaymentValidationArgs>, ReferenceLengthValidator>(nameof(ReferenceLengthValidator))
                .RegisterType<IValidator<PaymentValidationArgs>, CharacterTypeValidator>(nameof(CharacterTypeValidator))
                .RegisterType<IValidator<PaymentValidationArgs>, AccountExistsValidator>(nameof(AccountExistsValidator))
                .RegisterType<IValidator<PaymentValidationArgs>, InputMaskValidator>(nameof(InputMaskValidator))
                .RegisterType<IValidator<PaymentValidationArgs>, RegexValidator>(nameof(RegexValidator))

                .RegisterFactory<Func<string, IValidator<PaymentValidationArgs>>>(c => new Func<string, IValidator<PaymentValidationArgs>>(name => c.Resolve<IValidator<PaymentValidationArgs>>(name)))

                .RegisterType<ICheckDigitStrategy, WeightedSumStrategy>(CheckDigitType.WeightedSum.ToString())
                .RegisterType<ICheckDigitStrategy, DynixLibraryStrategy>(CheckDigitType.DynixLibrary.ToString())

                .RegisterFactory<Func<CheckDigitType, ICheckDigitStrategy>>(c => new Func<CheckDigitType, ICheckDigitStrategy>(type => c.Resolve<ICheckDigitStrategy>(type.ToString())))
            ;
        }
    }
}
