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
using BusinessLogic.Suspense.JournalAllocation;
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
                .RegisterType<IEReturnTemplateService, EReturnTemplateService>()
                .RegisterType<IEReturnTemplateRowService, EReturnTemplateRowService>()
                .RegisterType<IEReturnTypeService, EReturnTypeService>()
                .RegisterType<IEReturnNoteService, EReturnNoteService>()
                .RegisterType<IFundGroupService, FundGroupService>()
                .RegisterType<IFundService, FundService>()
                .RegisterType<IFundMetadataService, FundMetadataService>()
                .RegisterType<IFileImportService, FileImportService>()
                .RegisterType<IImportProcessingRuleService, ImportProcessingRuleService>()
                .RegisterType<IImportProcessingRuleConditionService, ImportProcessingRuleConditionService>()
                .RegisterType<IImportProcessingRuleActionService, ImportProcessingRuleActionService>()
                .RegisterType<IImportProcessingRuleFieldService, ImportProcessingRuleFieldService>()
                .RegisterType<IImportProcessingRuleOperatorService, ImportProcessingRuleOperatorService>()
                .RegisterType<IOfficeService, OfficeService>()
                .RegisterType<IMethodOfPaymentService, MethodOfPaymentService>()
                .RegisterType<IMethodOfPaymentMetadataService, MethodOfPaymentMetadataService>()
                .RegisterType<IMetadataKeyService, MetadataKeyService>()
                .RegisterType<IPaymentService, PaymentService>()
                .RegisterType<IPaymentIntegrationService, PaymentIntegrationService>()
                .RegisterType<IRefundService, RefundService>()
                .RegisterType<IRoleService, RoleService>()
                .RegisterType<ISuspenseService, SuspenseService>()
                .RegisterType<ISuspenseJournalService, SuspenseJournalService>()
                .RegisterType<ISuspenseNoteService, SuspenseNoteService>()
                .RegisterType<IFundMessageService, FundMessageService>()
                .RegisterType<IFundMessageMetadataService, FundMessageMetadataService>()
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
                .RegisterType<IUserMethodOfPaymentService, UserMethodOfPaymentService>()
                .RegisterType<IVatService, VatService>()
                .RegisterType<IVatMetadataService, VatMetadataService>()
                .RegisterType<ICryptographyService, MD5CryptographyService>()
                .RegisterType<IAccountReferenceValidatorService, AccountReferenceValidatorService>()
                .RegisterType<ICheckDigitConfigurationService, CheckDigitConfigurationService>()
                .RegisterType<IImportTypeService, ImportTypeService>()
                .RegisterType<IImportTypeImportProcessingRuleService, ImportTypeImportProcessingRuleService>()
                .RegisterType<IImportService, ImportService>()

                .RegisterType<IEmailService, EmailService>()
                .RegisterType<IEmailServiceDependencies, EmailServiceDependencies>()
                .RegisterType<IEmailFactory, EmailFactory>()

                .RegisterType<IAccountHolderFundMessageValidator, AccountHolderFundMessageValidator>()
                .RegisterType<IBasketValidator, BasketValidator>()
                .RegisterType<IEReturnDescriptionValidator, EReturnDescriptionValidator>()
                .RegisterType<IEReturnReferenceValidator, EReturnReferenceValidator>()
                .RegisterType<ITemplateRowValidator, TemplateRowValidator>()
                .RegisterType<ITransactionFeeValidator, TransactionFeeValidator>()
                .RegisterType<ITransactionJournalValidator, TransactionJournalValidator>()
                .RegisterType<ITransactionTransferValidator, TransactionTransferValidator>()
                .RegisterType<IRollbackTransactionJournalValidator, RollbackTransactionJournalValidator>()

                .RegisterType<ITransactionVatStrategy, TransactionVatStrategy>()
                .RegisterType<IJournalAllocationStrategy, CombinedTransactionJournalAllocationStrategy>()
                .RegisterType<IJournalAllocationStrategyValidator, JournalAllocationStrategyValidator>()
                .RegisterType<IApproveEReturnsStrategy, ApproveEReturnsStrategy>()

                .RegisterType<Clients.PaymentIntegrationClient.IClient, Clients.PaymentIntegrationClient.Client>()

                .RegisterType<IRuleEngine, RuleEngine>()
                .RegisterType<IOperations, Operations>()
                .RegisterType<IFileImporter, FileImporter>()
                .RegisterType<IFileImportProcessor, FileImportProcessor>()

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

                .RegisterType<IImportProcessor, ImportProcessor>()

                .RegisterType<IImportInitialisationStrategy, TransactionImportInitialisationStrategy>(ImportDataTypeEnum.Transaction.ToString())
                .RegisterFactory<Func<string, IImportInitialisationStrategy>>(c => new Func<string, IImportInitialisationStrategy>(name => { try { return c.Resolve<IImportInitialisationStrategy>(name); } catch { return null; } }))

                .RegisterType<IImportProcessingStrategy, TransactionImportProcessingStrategy>(ImportDataTypeEnum.Transaction.ToString())
                .RegisterType<IImportProcessingStrategy, AccountHolderImportProcessingStrategy>(ImportDataTypeEnum.AccountHolder.ToString())
                .RegisterFactory<Func<string, IImportProcessingStrategy>>(c => new Func<string, IImportProcessingStrategy>(name => c.Resolve<IImportProcessingStrategy>(name)))

                .RegisterType<IValidator<ImportProcessingStrategyValidatorArgs>, TransactionImportProcessingStrategyValidator>(ImportDataTypeEnum.Transaction.ToString())
                .RegisterType<IValidator<ImportProcessingStrategyValidatorArgs>, AccountHolderImportProcessingStrategyValidator>(ImportDataTypeEnum.AccountHolder.ToString())
                .RegisterFactory<Func<string, IValidator<ImportProcessingStrategyValidatorArgs>>>(c => new Func<string, IValidator<ImportProcessingStrategyValidatorArgs>>(name => c.Resolve<IValidator<ImportProcessingStrategyValidatorArgs>>(name)))
            ;
        }
    }
}
