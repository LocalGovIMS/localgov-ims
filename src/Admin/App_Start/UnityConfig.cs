using Admin.Classes.Models;
using Admin.Classes.Resolvers;
using Admin.Classes.Security;
using Admin.Controllers;
using Admin.Interfaces.Commands;
using Admin.Interfaces.ModelBuilders;
using Admin.Interfaces.Resolvers;
using BusinessLogic.Authentication.Identity;
using BusinessLogic.Interfaces.Persistence;
using BusinessLogic.Interfaces.Security;
using BusinessLogic.Models;
using BusinessLogic.Security;
using DataAccess.Persistence;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO.Abstractions;
using System.Web;
using System.Web.Mvc;
using Unity;
using Unity.AspNet.Mvc;
using Unity.Injection;
using Unity.log4net;
using Commands = Admin.Classes.Commands;
using ViewModelBuilders = Admin.Classes.ViewModelBuilders;
using ViewModels = Admin.Models;

namespace Admin
{
    [ExcludeFromCodeCoverage]
    public static class UnityConfig
    {
        #region Unity Container
        private static Lazy<IUnityContainer> container =
          new Lazy<IUnityContainer>(() =>
          {
              var container = new UnityContainer();
              RegisterTypes(container);
              return container;
          });

        /// <summary>
        /// Configured Unity Container.
        /// </summary>
        public static IUnityContainer Container => container.Value;
        #endregion

        /// <summary>
        /// Registers the type mappings with the Unity container.
        /// </summary>
        /// <param name="container">The unity container to configure.</param>
        /// <remarks>
        /// There is no need to register concrete types such as controllers or
        /// API controllers (unless you want to change the defaults), as Unity
        /// allows resolving a concrete type even if it was not previously
        /// registered.
        /// </remarks>
        public static void RegisterTypes(IUnityContainer container)
        {
            // NOTE: To load from web.config uncomment the line below.
            // Make sure to add a Unity.Configuration to the using statements.
            // container.LoadConfiguration();

            container.RegisterType<ISecurityContext, SecurityContext>(new PerRequestLifetimeManager());
            container.RegisterType<IUserStore, UserStore>(new PerRequestLifetimeManager());
            container.RegisterType<IncomeDbContext>(new PerRequestLifetimeManager());

            container.RegisterType<ApplicationUserManager>(new PerRequestLifetimeManager());
            container.RegisterType<ApplicationSignInManager>(new PerRequestLifetimeManager());
            container.RegisterType<IUserStore<PaymentsUser>, UserStore<PaymentsUser>>(new PerRequestLifetimeManager());
            container.RegisterFactory<IAuthenticationManager>(c => HttpContext.Current.GetOwinContext().Authentication);
            container.RegisterType<IRoleStore<IdentityRole, string>, RoleStore<IdentityRole>>(new InjectionConstructor(typeof(IncomeDbContext)));

            RegisterBuildersAndCommands(container);
            RegisterControllerDependencies(container);
            RegisterOther(container);

            BusinessLogic.UnityConfig.RegisterComponents(container);
            DataAccess.UnityConfig.RegisterComponents(container);

            log4net.Config.XmlConfigurator.Configure();
            container.AddNewExtension<Log4NetExtension>();
            container.AddExtension(new Diagnostic());
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }

        private static void RegisterBuildersAndCommands(IUnityContainer container)
        {
            container

                .RegisterType<IModelBuilder<ViewModels.AccountHolder.ListViewModel, ViewModels.AccountHolder.SearchCriteria>, ViewModelBuilders.AccountHolder.ListViewModelBuilder>()
                .RegisterType<IModelBuilder<ViewModels.AccountHolder.DetailsViewModel, ViewModelBuilders.AccountHolder.DetailsViewModelBuilderArgs>, ViewModelBuilders.AccountHolder.DetailsViewModelBuilder>()
                .RegisterType<IModelBuilder<ViewModels.AccountHolder.EditViewModel, string>, ViewModelBuilders.AccountHolder.EditViewModelBuilder>()
                .RegisterType<IModelCommand<ViewModels.AccountHolder.LookupViewModel>, Commands.AccountHolder.LookupCommand>("Lookup")
                .RegisterType<IModelCommand<ViewModels.AccountHolder.EditViewModel>, Commands.AccountHolder.CreateCommand>("Create")
                .RegisterType<IModelCommand<ViewModels.AccountHolder.EditViewModel>, Commands.AccountHolder.EditCommand>("Edit")

                .RegisterType<IModelBuilder<ViewModels.FundGroup.DetailsViewModel, int>, ViewModelBuilders.FundGroup.DetailsViewModelBuilder>()
                .RegisterType<IModelBuilder<ViewModels.FundGroup.EditViewModel, int>, ViewModelBuilders.FundGroup.CreateViewModelBuilder>("Create")
                .RegisterType<IModelBuilder<ViewModels.FundGroup.EditViewModel, int>, ViewModelBuilders.FundGroup.EditViewModelBuilder>("Edit")
                .RegisterType<IModelBuilder<IList<ViewModels.FundGroup.DetailsViewModel>, int>, ViewModelBuilders.FundGroup.ListViewModelBuilder>()
                .RegisterType<IModelCommand<ViewModels.FundGroup.EditViewModel>, Commands.FundGroup.CreateCommand>("Create")
                .RegisterType<IModelCommand<ViewModels.FundGroup.EditViewModel>, Commands.FundGroup.EditCommand>("Edit")
                .RegisterType<IModelCommand<int>, Commands.FundGroup.DeleteCommand>("FundGroup.Command.Delete")

                .RegisterType<IModelBuilder<ViewModels.EReturn.ListViewModel, ViewModels.EReturn.SearchCriteria>, ViewModelBuilders.EReturn.ListViewModelBuilder>()
                .RegisterType<IModelBuilder<ViewModels.EReturn.CreateViewModel, int>, ViewModelBuilders.EReturn.CreateViewModelBuilder>()
                .RegisterType<IModelBuilder<ViewModels.EReturn.EditViewModel, int>, ViewModelBuilders.EReturn.DetailsViewModelBuilder>("EReturn.ViewModelBuilder.Details")
                .RegisterType<IModelBuilder<ViewModels.EReturn.EditViewModel, int>, ViewModelBuilders.EReturn.EditViewModelBuilder>("EReturn.ViewModelBuilder.Edit")
                .RegisterType<IModelCommand<ViewModels.EReturn.EditViewModel>, Commands.EReturn.EditCommand>("EReturn.Command.Edit")
                .RegisterType<IModelCommand<ViewModels.EReturn.EditViewModel>, Commands.EReturn.ApproverEditCommand>("EReturn.Command.ApproverEdit")
                .RegisterType<IModelCommand<ViewModels.EReturn.CreateViewModel>, Commands.EReturn.CreateCommand>()
                .RegisterType<IModelCommand<int>, Commands.EReturn.DeleteCommand>("EReturn.Command.Delete")
                .RegisterType<IModelCommand<int>, Commands.EReturn.SubmitCommand>("EReturn.Command.Submit")
                .RegisterType<IModelCommand<ViewModels.EReturn.ApproveViewModel>, Commands.EReturn.ApproveCommand>()

                .RegisterType<IModelBuilder<ViewModels.EReturnNote.ListViewModel, int>, ViewModelBuilders.EReturnNote.ListViewModelBuilder>()
                .RegisterType<IModelCommand<ViewModels.EReturnNote.EditViewModel>, Commands.EReturnNote.CreateCommand>()

                .RegisterType<IModelBuilder<ViewModels.EReturnTemplate.DetailsViewModel, int>, ViewModelBuilders.EReturnTemplate.DetailsViewModelBuilder>()
                .RegisterType<IModelBuilder<ViewModels.EReturnTemplate.EditViewModel, int>, ViewModelBuilders.EReturnTemplate.EditViewModelBuilder>()
                .RegisterType<IModelBuilder<ViewModels.EReturnTemplate.ListViewModel, ViewModels.EReturnTemplate.SearchCriteria>, ViewModelBuilders.EReturnTemplate.ListViewModelBuilder>()
                .RegisterType<IModelCommand<ViewModels.EReturnTemplate.EditViewModel>, Commands.EReturnTemplate.CreateCommand>("Create")
                .RegisterType<IModelCommand<ViewModels.EReturnTemplate.EditViewModel>, Commands.EReturnTemplate.EditCommand>("Edit")

                .RegisterType<IModelBuilder<ViewModels.EReturnTemplateRow.DetailsViewModel, int>, ViewModelBuilders.EReturnTemplateRow.DetailsViewModelBuilder>()
                .RegisterType<IModelBuilder<ViewModels.EReturnTemplateRow.EditViewModel, ViewModelBuilders.EReturnTemplateRow.CreateViewModelBuilderArgs>, ViewModelBuilders.EReturnTemplateRow.CreateViewModelBuilder>()
                .RegisterType<IModelBuilder<ViewModels.EReturnTemplateRow.EditViewModel, int>, ViewModelBuilders.EReturnTemplateRow.EditViewModelBuilder>()
                .RegisterType<IModelBuilder<ViewModels.EReturnTemplateRow.ListViewModel, ViewModels.EReturnTemplateRow.SearchCriteria>, ViewModelBuilders.EReturnTemplateRow.ListViewModelBuilder>()
                .RegisterType<IModelCommand<ViewModels.EReturnTemplateRow.EditViewModel>, Commands.EReturnTemplateRow.CreateCommand>("Create")
                .RegisterType<IModelCommand<ViewModels.EReturnTemplateRow.EditViewModel>, Commands.EReturnTemplateRow.EditCommand>("Edit")
                .RegisterType<IModelCommand<int>, Commands.EReturnTemplateRow.DeleteCommand>("EReturnTemplateRow.Command.Delete")

                .RegisterType<IModelBuilder<ViewModels.Fund.DetailsViewModel, string>, ViewModelBuilders.Fund.DetailsViewModelBuilder>()
                .RegisterType<IModelBuilder<ViewModels.Fund.EditViewModel, string>, ViewModelBuilders.Fund.EditViewModelBuilder>()
                .RegisterType<IModelBuilder<ViewModels.Fund.ListViewModel, ViewModels.Fund.SearchCriteria>, ViewModelBuilders.Fund.ListViewModelBuilder>()
                .RegisterType<IModelCommand<ViewModels.Fund.EditViewModel>, Commands.Fund.CreateCommand>("Create")
                .RegisterType<IModelCommand<ViewModels.Fund.EditViewModel>, Commands.Fund.EditCommand>("Edit")

                .RegisterType<IModelBuilder<ViewModels.FundMetadata.DetailsViewModel, int>, ViewModelBuilders.FundMetadata.DetailsViewModelBuilder>()
                .RegisterType<IModelBuilder<ViewModels.FundMetadata.EditViewModel, ViewModelBuilders.FundMetadata.CreateViewModelBuilderArgs>, ViewModelBuilders.FundMetadata.CreateViewModelBuilder>()
                .RegisterType<IModelBuilder<ViewModels.FundMetadata.EditViewModel, int>, ViewModelBuilders.FundMetadata.EditViewModelBuilder>()
                .RegisterType<IModelBuilder<ViewModels.FundMetadata.ListViewModel, ViewModels.FundMetadata.SearchCriteria>, ViewModelBuilders.FundMetadata.ListViewModelBuilder>()
                .RegisterType<IModelCommand<ViewModels.FundMetadata.EditViewModel>, Commands.FundMetadata.CreateCommand>("Create")
                .RegisterType<IModelCommand<ViewModels.FundMetadata.EditViewModel>, Commands.FundMetadata.EditCommand>("Edit")
                .RegisterType<IModelCommand<int>, Commands.FundMetadata.DeleteCommand>("FundMetadata.Command.Delete")

                .RegisterType<IModelBuilder<ViewModels.FundMessage.DetailsViewModel, int>, ViewModelBuilders.FundMessage.DetailsViewModelBuilder>()
                .RegisterType<IModelBuilder<ViewModels.FundMessage.EditViewModel, int>, ViewModelBuilders.FundMessage.EditViewModelBuilder>()
                .RegisterType<IModelBuilder<ViewModels.FundMessage.ListViewModel, ViewModels.FundMessage.SearchCriteria>, ViewModelBuilders.FundMessage.ListViewModelBuilder>()
                .RegisterType<IModelCommand<ViewModels.FundMessage.EditViewModel>, Commands.FundMessage.CreateCommand>("Create")
                .RegisterType<IModelCommand<ViewModels.FundMessage.EditViewModel>, Commands.FundMessage.EditCommand>("Edit")

                .RegisterType<IModelBuilder<ViewModels.FundMessageMetadata.DetailsViewModel, int>, ViewModelBuilders.FundMessageMetadata.DetailsViewModelBuilder>()
                .RegisterType<IModelBuilder<ViewModels.FundMessageMetadata.EditViewModel, ViewModelBuilders.FundMessageMetadata.CreateViewModelBuilderArgs>, ViewModelBuilders.FundMessageMetadata.CreateViewModelBuilder>()
                .RegisterType<IModelBuilder<ViewModels.FundMessageMetadata.EditViewModel, int>, ViewModelBuilders.FundMessageMetadata.EditViewModelBuilder>()
                .RegisterType<IModelBuilder<ViewModels.FundMessageMetadata.ListViewModel, ViewModels.FundMessageMetadata.SearchCriteria>, ViewModelBuilders.FundMessageMetadata.ListViewModelBuilder>()
                .RegisterType<IModelCommand<ViewModels.FundMessageMetadata.EditViewModel>, Commands.FundMessageMetadata.CreateCommand>("Create")
                .RegisterType<IModelCommand<ViewModels.FundMessageMetadata.EditViewModel>, Commands.FundMessageMetadata.EditCommand>("Edit")
                .RegisterType<IModelCommand<int>, Commands.FundMessageMetadata.DeleteCommand>("FundMessageMetadata.Command.Delete")

                .RegisterType<IModelBuilder<IList<ViewModels.SystemMessage.DetailsViewModel>, string>, ViewModelBuilders.Home.ListViewModelBuilder>()

                .RegisterType<IModelBuilder<ViewModels.MethodOfPayment.DetailsViewModel, string>, ViewModelBuilders.MethodOfPayment.DetailsViewModelBuilder>()
                .RegisterType<IModelBuilder<ViewModels.MethodOfPayment.EditViewModel, string>, ViewModelBuilders.MethodOfPayment.EditViewModelBuilder>()
                .RegisterType<IModelBuilder<IList<ViewModels.MethodOfPayment.DetailsViewModel>, string>, ViewModelBuilders.MethodOfPayment.ListViewModelBuilder>()
                .RegisterType<IModelCommand<ViewModels.MethodOfPayment.EditViewModel>, Commands.MethodOfPayment.CreateCommand>("Create")
                .RegisterType<IModelCommand<ViewModels.MethodOfPayment.EditViewModel>, Commands.MethodOfPayment.EditCommand>("Edit")

                .RegisterType<IModelBuilder<ViewModels.MethodOfPaymentMetadata.DetailsViewModel, int>, ViewModelBuilders.MethodOfPaymentMetadata.DetailsViewModelBuilder>()
                .RegisterType<IModelBuilder<ViewModels.MethodOfPaymentMetadata.EditViewModel, ViewModelBuilders.MethodOfPaymentMetadata.CreateViewModelBuilderArgs>, ViewModelBuilders.MethodOfPaymentMetadata.CreateViewModelBuilder>()
                .RegisterType<IModelBuilder<ViewModels.MethodOfPaymentMetadata.EditViewModel, int>, ViewModelBuilders.MethodOfPaymentMetadata.EditViewModelBuilder>()
                .RegisterType<IModelBuilder<ViewModels.MethodOfPaymentMetadata.ListViewModel, ViewModels.MethodOfPaymentMetadata.SearchCriteria>, ViewModelBuilders.MethodOfPaymentMetadata.ListViewModelBuilder>()
                .RegisterType<IModelCommand<ViewModels.MethodOfPaymentMetadata.EditViewModel>, Commands.MethodOfPaymentMetadata.CreateCommand>("Create")
                .RegisterType<IModelCommand<ViewModels.MethodOfPaymentMetadata.EditViewModel>, Commands.MethodOfPaymentMetadata.EditCommand>("Edit")
                .RegisterType<IModelCommand<int>, Commands.MethodOfPaymentMetadata.DeleteCommand>("MethodOfPaymentMetadata.Command.Delete")

                .RegisterType<IModelBuilder<ViewModels.Payment.IndexViewModel, ViewModels.Payment.IndexViewModel>, ViewModelBuilders.Payment.IndexViewModelBuilder>()
                .RegisterType<IModelCommand<ViewModels.Payment.IndexViewModel>, Commands.Payment.AddCommand>("Add")
                .RegisterType<IModelCommand<Guid>, Commands.Payment.RemoveCommand>("Remove")
                .RegisterType<IModelCommand<string>, Commands.Payment.EmptyBasketCommand>("EmptyBasket")
                .RegisterType<IModelCommand<ViewModels.Payment.IndexViewModel>, Commands.Payment.CheckAddressCommand>("CheckAddress")
                .RegisterType<IModelCommand<ViewModels.Payment.IndexViewModel>, Commands.Payment.CreatePaymentsCommand>("CreatePayments")
                .RegisterType<IModelCommand<ViewModels.Payment.IndexViewModel>, Commands.Payment.SetAddressCommand>("SetAddress")
                .RegisterType<IModelCommand<ProcessPaymentCommandAgrs>, Commands.Payment.ProcessPaymentCommand>()

                .RegisterType<IModelBuilder<ViewModels.Role.DetailsViewModel, int>, ViewModelBuilders.Role.DetailsViewModelBuilder>()
                .RegisterType<IModelBuilder<ViewModels.Role.EditViewModel, int>, ViewModelBuilders.Role.EditViewModelBuilder>()
                .RegisterType<IModelBuilder<IList<ViewModels.Role.DetailsViewModel>, int>, ViewModelBuilders.Role.ListViewModelBuilder>()
                .RegisterType<IModelCommand<ViewModels.Role.EditViewModel>, Commands.Role.EditCommand>()

                .RegisterType<IModelBuilder<ViewModels.Suspense.DetailsViewModel, int>, ViewModelBuilders.Suspense.DetailsViewModelBuilder>()
                .RegisterType<IModelBuilder<ViewModels.Suspense.ListViewModel, ViewModels.Suspense.SearchCriteria>, ViewModelBuilders.Suspense.ListViewModelBuilder>()
                .RegisterType<IModelBuilder<ViewModels.Suspense.JournalViewModel, string>, ViewModelBuilders.Suspense.JournalViewModelBuilder>()
                .RegisterType<IModelCommand<ViewModels.Suspense.JournalViewModel>, Commands.Suspense.JournalCommand>()
                .RegisterType<IModelCommand<ViewModels.Suspense.SaveNoteViewModel>, Commands.Suspense.SaveNoteCommand>()

                .RegisterType<IModelBuilder<ViewModels.SuspenseNote.ListViewModel, int>, ViewModelBuilders.SuspenseNote.ListViewModelBuilder>()
                .RegisterType<IModelCommand<ViewModels.SuspenseNote.EditViewModel>, Commands.SuspenseNote.CreateCommand>()

                .RegisterType<IModelBuilder<ViewModels.SystemMessage.DetailsViewModel, int>, ViewModelBuilders.SystemMessage.DetailsViewModelBuilder>()
                .RegisterType<IModelBuilder<ViewModels.SystemMessage.EditViewModel, int>, ViewModelBuilders.SystemMessage.EditViewModelBuilder>()
                .RegisterType<IModelBuilder<IList<ViewModels.SystemMessage.DetailsViewModel>, int>, ViewModelBuilders.SystemMessage.ListViewModelBuilder>()
                .RegisterType<IModelCommand<ViewModels.SystemMessage.EditViewModel>, Commands.SystemMessage.CreateCommand>("Create")
                .RegisterType<IModelCommand<ViewModels.SystemMessage.EditViewModel>, Commands.SystemMessage.EditCommand>("Edit")

                .RegisterType<IModelBuilder<ViewModels.Transaction.ListViewModel, ViewModels.Transaction.SearchCriteria>, ViewModelBuilders.Transaction.ListViewModelBuilder>()
                .RegisterType<IModelBuilder<ViewModels.Transaction.DetailsViewModel, string>, ViewModelBuilders.Transaction.DetailsViewModelBuilder>()
                .RegisterType<IModelBuilder<ViewModels.Transaction.TransferViewModel, string>, ViewModelBuilders.Transaction.TransferViewModelBuilder>()
                .RegisterType<IModelBuilder<ViewModels.Transaction.RefundViewModel, string>, ViewModelBuilders.Transaction.RefundViewModelBuilder>()
                .RegisterType<IModelCommand<ViewModels.Transaction.TransferViewModel>, Commands.Transaction.TransferCommand>()
                .RegisterType<IModelCommand<string>, Commands.Transaction.UndoTransferCommand>("UndoTransfer")
                .RegisterType<IModelCommand<ViewModels.Transaction.RefundViewModel>, Commands.Transaction.RefundCommand>()
                .RegisterType<IModelCommand<ViewModels.Transaction.EmailReceiptViewModel>, Commands.Transaction.EmailReceiptCommand>()
                .RegisterType<IModelCommand<Commands.Transaction.CreateCsvFileForExportCommandArgs>, Commands.Transaction.CreateCsvFileForExportCommand>()

                .RegisterType<IModelBuilder<ViewModels.Transfer.TransferViewModel, string>, ViewModelBuilders.Transfer.TransferViewModelBuilder>()
                .RegisterType<IModelCommand<ViewModels.Transfer.TransferViewModel>, Commands.Transfer.TransferCommand>()

                .RegisterType<IModelCommand<TransferItem>, Commands.Validation.ValidateTransferItemCommand>()

                .RegisterType<IModelBuilder<ViewModels.User.DetailsViewModel, int>, ViewModelBuilders.User.DetailsViewModelBuilder>()
                .RegisterType<IModelBuilder<ViewModels.User.EditViewModel, int>, ViewModelBuilders.User.EditViewModelBuilder>()
                .RegisterType<IModelBuilder<ViewModels.User.ListViewModel, ViewModels.User.SearchCriteria>, ViewModelBuilders.User.ListViewModelBuilder>()
                .RegisterType<IModelCommand<ViewModels.User.EditViewModel>, Commands.User.CreateCommand>("Create")
                .RegisterType<IModelCommand<ViewModels.User.EditViewModel>, Commands.User.EditCommand>("Edit")

                .RegisterType<IModelBuilder<ViewModels.Shared.BasicListViewModel, int>, ViewModelBuilders.UserRole.BasicListViewModelBuilder>("UserRole.ViewModelBuider.BasicList")
                .RegisterType<IModelBuilder<ViewModels.UserRole.EditViewModel, int>, ViewModelBuilders.UserRole.EditViewModelBuilder>()
                .RegisterType<IModelCommand<ViewModels.UserRole.EditViewModel>, Commands.UserRole.EditCommand>()

                .RegisterType<IModelBuilder<ViewModels.Shared.BasicListViewModel, int>, ViewModelBuilders.UserFundGroup.BasicListViewModelBuilder>("UserFundGroup.ViewModelBuider.BasicList")
                .RegisterType<IModelBuilder<ViewModels.UserFundGroup.EditViewModel, int>, ViewModelBuilders.UserFundGroup.EditViewModelBuilder>()
                .RegisterType<IModelCommand<ViewModels.UserFundGroup.EditViewModel>, Commands.UserFundGroup.EditCommand>()

                .RegisterType<IModelBuilder<ViewModels.Shared.BasicListViewModel, int>, ViewModelBuilders.UserTemplate.BasicListViewModelBuilder>("UserTemplate.ViewModelBuider.BasicList")
                .RegisterType<IModelBuilder<ViewModels.UserTemplate.EditViewModel, int>, ViewModelBuilders.UserTemplate.EditViewModelBuilder>()
                .RegisterType<IModelCommand<ViewModels.UserTemplate.EditViewModel>, Commands.UserTemplate.EditCommand>()

                .RegisterType<IModelBuilder<ViewModels.Shared.BasicListViewModel, int>, ViewModelBuilders.UserMethodOfPayment.BasicListViewModelBuilder>("UserMethodOfPayment.ViewModelBuider.BasicList")
                .RegisterType<IModelBuilder<ViewModels.UserMethodOfPayment.EditViewModel, int>, ViewModelBuilders.UserMethodOfPayment.EditViewModelBuilder>()
                .RegisterType<IModelCommand<ViewModels.UserMethodOfPayment.EditViewModel>, Commands.UserMethodOfPayment.EditCommand>()

                .RegisterType<IModelBuilder<ViewModels.Vat.DetailsViewModel, string>, ViewModelBuilders.Vat.DetailsViewModelBuilder>()
                .RegisterType<IModelBuilder<ViewModels.Vat.EditViewModel, string>, ViewModelBuilders.Vat.EditViewModelBuilder>()
                .RegisterType<IModelBuilder<IList<ViewModels.Vat.DetailsViewModel>, string>, ViewModelBuilders.Vat.ListViewModelBuilder>()
                .RegisterType<IModelCommand<ViewModels.Vat.EditViewModel>, Commands.Vat.CreateCommand>("Create")
                .RegisterType<IModelCommand<ViewModels.Vat.EditViewModel>, Commands.Vat.EditCommand>("Edit")

                .RegisterType<IModelBuilder<ViewModels.VatMetadata.DetailsViewModel, int>, ViewModelBuilders.VatMetadata.DetailsViewModelBuilder>()
                .RegisterType<IModelBuilder<ViewModels.VatMetadata.EditViewModel, ViewModelBuilders.VatMetadata.CreateViewModelBuilderArgs>, ViewModelBuilders.VatMetadata.CreateViewModelBuilder>()
                .RegisterType<IModelBuilder<ViewModels.VatMetadata.EditViewModel, int>, ViewModelBuilders.VatMetadata.EditViewModelBuilder>()
                .RegisterType<IModelBuilder<ViewModels.VatMetadata.ListViewModel, ViewModels.VatMetadata.SearchCriteria>, ViewModelBuilders.VatMetadata.ListViewModelBuilder>()
                .RegisterType<IModelCommand<ViewModels.VatMetadata.EditViewModel>, Commands.VatMetadata.CreateCommand>("Create")
                .RegisterType<IModelCommand<ViewModels.VatMetadata.EditViewModel>, Commands.VatMetadata.EditCommand>("Edit")
                .RegisterType<IModelCommand<int>, Commands.VatMetadata.DeleteCommand>("VatMetadata.Command.Delete")

                .RegisterType<IModelBuilder<ViewModels.Office.DetailsViewModel, string>, ViewModelBuilders.Office.DetailsViewModelBuilder>()
                .RegisterType<IModelBuilder<ViewModels.Office.EditViewModel, string>, ViewModelBuilders.Office.EditViewModelBuilder>()
                .RegisterType<IModelBuilder<IList<ViewModels.Office.DetailsViewModel>, string>, ViewModelBuilders.Office.ListViewModelBuilder>()
                .RegisterType<IModelCommand<ViewModels.Office.EditViewModel>, Commands.Office.CreateCommand>("Create")
                .RegisterType<IModelCommand<ViewModels.Office.EditViewModel>, Commands.Office.EditCommand>("Edit")

                .RegisterType<IModelBuilder<ViewModels.ImportProcessingRule.DetailsViewModel, int>, ViewModelBuilders.ImportProcessingRule.DetailsViewModelBuilder>()
                .RegisterType<IModelBuilder<ViewModels.ImportProcessingRule.EditViewModel, int>, ViewModelBuilders.ImportProcessingRule.EditViewModelBuilder>()
                .RegisterType<IModelBuilder<ViewModels.ImportProcessingRule.ListViewModel, ViewModels.ImportProcessingRule.SearchCriteria>, ViewModelBuilders.ImportProcessingRule.ListViewModelBuilder>()
                .RegisterType<IModelCommand<ViewModels.ImportProcessingRule.EditViewModel>, Commands.ImportProcessingRule.CreateCommand>("Create")
                .RegisterType<IModelCommand<ViewModels.ImportProcessingRule.EditViewModel>, Commands.ImportProcessingRule.EditCommand>("Edit")
                .RegisterType<IModelCommand<Commands.ImportProcessingRule.ChangeStatusCommandArgs>, Commands.ImportProcessingRule.ChangeStatusCommand>()

                .RegisterType<IModelBuilder<ViewModels.ImportProcessingRuleCondition.DetailsViewModel, int>, ViewModelBuilders.ImportProcessingRuleCondition.DetailsViewModelBuilder>()
                .RegisterType<IModelBuilder<ViewModels.ImportProcessingRuleCondition.EditViewModel, ViewModelBuilders.ImportProcessingRuleCondition.CreateViewModelBuilderArgs>, ViewModelBuilders.ImportProcessingRuleCondition.CreateViewModelBuilder>()
                .RegisterType<IModelBuilder<ViewModels.ImportProcessingRuleCondition.EditViewModel, int>, ViewModelBuilders.ImportProcessingRuleCondition.EditViewModelBuilder>()
                .RegisterType<IModelBuilder<ViewModels.ImportProcessingRuleCondition.ListViewModel, ViewModels.ImportProcessingRuleCondition.SearchCriteria>, ViewModelBuilders.ImportProcessingRuleCondition.ListViewModelBuilder>()
                .RegisterType<IModelCommand<ViewModels.ImportProcessingRuleCondition.EditViewModel>, Commands.ImportProcessingRuleCondition.CreateCommand>("Create")
                .RegisterType<IModelCommand<ViewModels.ImportProcessingRuleCondition.EditViewModel>, Commands.ImportProcessingRuleCondition.EditCommand>("Edit")
                .RegisterType<IModelCommand<int>, Commands.ImportProcessingRuleCondition.DeleteCommand>("ImportProcessingRuleCondition.Command.Delete")

                .RegisterType<IModelBuilder<ViewModels.ImportProcessingRuleAction.DetailsViewModel, int>, ViewModelBuilders.ImportProcessingRuleAction.DetailsViewModelBuilder>()
                .RegisterType<IModelBuilder<ViewModels.ImportProcessingRuleAction.EditViewModel, ViewModelBuilders.ImportProcessingRuleAction.CreateViewModelBuilderArgs>, ViewModelBuilders.ImportProcessingRuleAction.CreateViewModelBuilder>()
                .RegisterType<IModelBuilder<ViewModels.ImportProcessingRuleAction.EditViewModel, int>, ViewModelBuilders.ImportProcessingRuleAction.EditViewModelBuilder>()
                .RegisterType<IModelBuilder<ViewModels.ImportProcessingRuleAction.ListViewModel, ViewModels.ImportProcessingRuleAction.SearchCriteria>, ViewModelBuilders.ImportProcessingRuleAction.ListViewModelBuilder>()
                .RegisterType<IModelCommand<ViewModels.ImportProcessingRuleAction.EditViewModel>, Commands.ImportProcessingRuleAction.CreateCommand>("Create")
                .RegisterType<IModelCommand<ViewModels.ImportProcessingRuleAction.EditViewModel>, Commands.ImportProcessingRuleAction.EditCommand>("Edit")
                .RegisterType<IModelCommand<int>, Commands.ImportProcessingRuleAction.DeleteCommand>("ImportProcessingRuleAction.Command.Delete")

                .RegisterType<IModelCommand<Commands.FileImport.SaveCommandArgs>, Commands.FileImport.SaveCommand>()
                .RegisterType<IModelCommand<int>, Commands.FileImport.ProcessCommand>("FileImport.Command.Process")

                .RegisterType<IModelBuilder<ViewModels.PaymentIntegration.DetailsViewModel, int>, ViewModelBuilders.PaymentIntegration.DetailsViewModelBuilder>()
                .RegisterType<IModelBuilder<ViewModels.PaymentIntegration.EditViewModel, int>, ViewModelBuilders.PaymentIntegration.EditViewModelBuilder>()
                .RegisterType<IModelBuilder<IList<ViewModels.PaymentIntegration.DetailsViewModel>, int>, ViewModelBuilders.PaymentIntegration.ListViewModelBuilder>()
                .RegisterType<IModelCommand<ViewModels.PaymentIntegration.EditViewModel>, Commands.PaymentIntegration.CreateCommand>("Create")
                .RegisterType<IModelCommand<ViewModels.PaymentIntegration.EditViewModel>, Commands.PaymentIntegration.EditCommand>("Edit")

                .RegisterType<IModelBuilder<ViewModels.CheckDigitConfiguration.DetailsViewModel, int>, ViewModelBuilders.CheckDigitConfiguration.DetailsViewModelBuilder>()
                .RegisterType<IModelBuilder<ViewModels.CheckDigitConfiguration.EditViewModel, int>, ViewModelBuilders.CheckDigitConfiguration.EditViewModelBuilder>("Create")
                .RegisterType<IModelBuilder<ViewModels.CheckDigitConfiguration.EditViewModel, int>, ViewModelBuilders.CheckDigitConfiguration.EditViewModelBuilder>("Edit")
                .RegisterType<IModelBuilder<ViewModels.CheckDigitConfiguration.ListViewModel, ViewModels.CheckDigitConfiguration.SearchCriteria>, ViewModelBuilders.CheckDigitConfiguration.ListViewModelBuilder>()
                .RegisterType<IModelCommand<ViewModels.CheckDigitConfiguration.EditViewModel>, Commands.CheckDigitConfiguration.CreateCommand>("Create")
                .RegisterType<IModelCommand<ViewModels.CheckDigitConfiguration.EditViewModel>, Commands.CheckDigitConfiguration.EditCommand>("Edit")
                .RegisterType<IModelCommand<int>, Commands.CheckDigitConfiguration.DeleteCommand>("CheckDigitConfiguration.Command.Delete")

                .RegisterType<IModelBuilder<ViewModels.AccountReferenceValidator.DetailsViewModel, int>, ViewModelBuilders.AccountReferenceValidator.DetailsViewModelBuilder>()
                .RegisterType<IModelBuilder<ViewModels.AccountReferenceValidator.EditViewModel, int>, ViewModelBuilders.AccountReferenceValidator.EditViewModelBuilder>("Create")
                .RegisterType<IModelBuilder<ViewModels.AccountReferenceValidator.EditViewModel, int>, ViewModelBuilders.AccountReferenceValidator.EditViewModelBuilder>("Edit")
                .RegisterType<IModelBuilder<ViewModels.AccountReferenceValidator.ListViewModel, ViewModels.AccountReferenceValidator.SearchCriteria>, ViewModelBuilders.AccountReferenceValidator.ListViewModelBuilder>()
                .RegisterType<IModelCommand<ViewModels.AccountReferenceValidator.EditViewModel>, Commands.AccountReferenceValidator.CreateCommand>("Create")
                .RegisterType<IModelCommand<ViewModels.AccountReferenceValidator.EditViewModel>, Commands.AccountReferenceValidator.EditCommand>("Edit")
                .RegisterType<IModelCommand<int>, Commands.AccountReferenceValidator.DeleteCommand>("AccountReferenceValidator.Command.Delete")

                .RegisterType<IModelBuilder<ViewModels.ImportType.DetailsViewModel, int>, ViewModelBuilders.ImportType.DetailsViewModelBuilder>()
                .RegisterType<IModelBuilder<ViewModels.ImportType.EditViewModel, int>, ViewModelBuilders.ImportType.EditViewModelBuilder>()
                .RegisterType<IModelBuilder<ViewModels.ImportType.ListViewModel, ViewModels.ImportType.SearchCriteria>, ViewModelBuilders.ImportType.ListViewModelBuilder>()
                .RegisterType<IModelCommand<ViewModels.ImportType.EditViewModel>, Commands.ImportType.CreateCommand>("Create")
                .RegisterType<IModelCommand<ViewModels.ImportType.EditViewModel>, Commands.ImportType.EditCommand>("Edit")

                .RegisterType<IModelBuilder<ViewModels.ImportTypeImportProcessingRule.DetailsViewModel, int>, ViewModelBuilders.ImportTypeImportProcessingRule.DetailsViewModelBuilder>()
                .RegisterType<IModelBuilder<ViewModels.ImportTypeImportProcessingRule.EditViewModel, int>, ViewModelBuilders.ImportTypeImportProcessingRule.CreateViewModelBuilder>("Create")
                .RegisterType<IModelBuilder<ViewModels.ImportTypeImportProcessingRule.EditViewModel, int>, ViewModelBuilders.ImportTypeImportProcessingRule.EditViewModelBuilder>("Edit")
                .RegisterType<IModelBuilder<ViewModels.ImportTypeImportProcessingRule.ListViewModel, ViewModels.ImportTypeImportProcessingRule.SearchCriteria>, ViewModelBuilders.ImportTypeImportProcessingRule.ListViewModelBuilder>()
                .RegisterType<IModelCommand<ViewModels.ImportTypeImportProcessingRule.EditViewModel>, Commands.ImportTypeImportProcessingRule.CreateCommand>("Create")
                .RegisterType<IModelCommand<ViewModels.ImportTypeImportProcessingRule.EditViewModel>, Commands.ImportTypeImportProcessingRule.EditCommand>("Edit")
                .RegisterType<IModelCommand<int>, Commands.ImportTypeImportProcessingRule.DeleteCommand>("ImportTypeImportProcessingRule.Command.Delete")

                .RegisterType<IModelBuilder<ViewModels.ImportProcessingRuleImportType.DetailsViewModel, int>, ViewModelBuilders.ImportProcessingRuleImportType.DetailsViewModelBuilder>()
                .RegisterType<IModelBuilder<ViewModels.ImportProcessingRuleImportType.EditViewModel, int>, ViewModelBuilders.ImportProcessingRuleImportType.CreateViewModelBuilder>("Create")
                .RegisterType<IModelBuilder<ViewModels.ImportProcessingRuleImportType.EditViewModel, int>, ViewModelBuilders.ImportProcessingRuleImportType.EditViewModelBuilder>("Edit")
                .RegisterType<IModelBuilder<ViewModels.ImportProcessingRuleImportType.ListViewModel, ViewModels.ImportProcessingRuleImportType.SearchCriteria>, ViewModelBuilders.ImportProcessingRuleImportType.ListViewModelBuilder>()
                .RegisterType<IModelCommand<ViewModels.ImportProcessingRuleImportType.EditViewModel>, Commands.ImportProcessingRuleImportType.CreateCommand>("Create")
                .RegisterType<IModelCommand<ViewModels.ImportProcessingRuleImportType.EditViewModel>, Commands.ImportProcessingRuleImportType.EditCommand>("Edit")
                .RegisterType<IModelCommand<int>, Commands.ImportProcessingRuleImportType.DeleteCommand>("ImportProcessingRuleImportType.Command.Delete")

                .RegisterType<IModelBuilder<ViewModels.Import.DetailsViewModel, int>, ViewModelBuilders.Import.DetailsViewModelBuilder>()
                .RegisterType<IModelBuilder<ViewModels.Import.ListViewModel, ViewModels.Import.SearchCriteria>, ViewModelBuilders.Import.ListViewModelBuilder>()

                .RegisterType<IModelBuilder<ViewModels.MetadataKey.DetailsViewModel, int>, ViewModelBuilders.MetadataKey.DetailsViewModelBuilder>()
                .RegisterType<IModelBuilder<ViewModels.MetadataKey.EditViewModel, int>, ViewModelBuilders.MetadataKey.EditViewModelBuilder>()
                .RegisterType<IModelBuilder<ViewModels.MetadataKey.ListViewModel, ViewModels.MetadataKey.SearchCriteria>, ViewModelBuilders.MetadataKey.ListViewModelBuilder>()
                .RegisterType<IModelCommand<ViewModels.MetadataKey.EditViewModel>, Commands.MetadataKey.CreateCommand>("Create")
                .RegisterType<IModelCommand<ViewModels.MetadataKey.EditViewModel>, Commands.MetadataKey.EditCommand>("Edit")
                .RegisterType<IModelCommand<int>, Commands.MetadataKey.DeleteCommand>("MetadataKey.Command.Delete")
                ;
        }

        private static void RegisterControllerDependencies(IUnityContainer container)
        {
            container
                .RegisterType<IAccountHolderControllerDependencies, AccountHolderControllerDependencies>()
                .RegisterType<IAccountReferenceValidatorControllerDependencies, AccountReferenceValidatorControllerDependencies>()
                .RegisterType<IHomeControllerDependencies, HomeControllerDependencies>()
                .RegisterType<IFundGroupControllerDependencies, FundGroupControllerDependencies>()
                .RegisterType<IEReturnControllerDependencies, EReturnControllerDependencies>()
                .RegisterType<IEReturnTemplateControllerDependencies, EReturnTemplateControllerDependencies>()
                .RegisterType<IEReturnTemplateRowControllerDependencies, EReturnTemplateRowControllerDependencies>()
                .RegisterType<IEReturnNoteControllerDependencies, EReturnNoteControllerDependencies>()
                .RegisterType<IMethodOfPaymentControllerDependencies, MethodOfPaymentControllerDependencies>()
                .RegisterType<IMethodOfPaymentMetadataControllerDependencies, MethodOfPaymentMetadataControllerDependencies>()
                .RegisterType<IPaymentControllerDependencies, PaymentControllerDependencies>()
                .RegisterType<IRoleControllerDependencies, RoleControllerDependencies>()
                .RegisterType<ISuspenseControllerDependencies, SuspenseControllerDependencies>()
                .RegisterType<ISuspenseNoteControllerDependencies, SuspenseNoteControllerDependencies>()
                .RegisterType<ISystemMessageControllerDependencies, SystemMessageControllerDependencies>()
                .RegisterType<ITransactionControllerDependencies, TransactionControllerDependencies>()
                .RegisterType<ITransferControllerDependencies, TransferControllerDependencies>()
                .RegisterType<IUserControllerDependencies, UserControllerDependencies>()
                .RegisterType<IUserFundGroupControllerDependencies, UserFundGroupControllerDependencies>()
                .RegisterType<IUserRoleControllerDependencies, UserRoleControllerDependencies>()
                .RegisterType<IUserTemplateControllerDependencies, UserTemplateControllerDependencies>()
                .RegisterType<IVatControllerDependencies, VatControllerDependencies>()
                .RegisterType<IVatMetadataControllerDependencies, VatMetadataControllerDependencies>()
                .RegisterType<IFundControllerDependencies, FundControllerDependencies>()
                .RegisterType<IFundMetadataControllerDependencies, FundMetadataControllerDependencies>()
                .RegisterType<IFundMessageControllerDependencies, FundMessageControllerDependencies>()
                .RegisterType<IFundMessageMetadataControllerDependencies, FundMessageMetadataControllerDependencies>()
                .RegisterType<IValidationControllerDependencies, ValidationControllerDependencies>()
                .RegisterType<IImportProcessingRuleControllerDependencies, ImportProcessingRuleControllerDependencies>()
                .RegisterType<IImportProcessingRuleConditionControllerDependencies, ImportProcessingRuleConditionControllerDependencies>()
                .RegisterType<IImportProcessingRuleActionControllerDependencies, ImportProcessingRuleActionControllerDependencies>()
                .RegisterType<IOfficeControllerDependencies, OfficeControllerDependencies>()
                .RegisterType<IFileImportControllerDependencies, FileImportControllerDependencies>()
                .RegisterType<IPaymentIntegrationControllerDependencies, PaymentIntegrationControllerDependencies>()
                .RegisterType<IUserMethodOfPaymentControllerDependencies, UserMethodOfPaymentControllerDependencies>()
                .RegisterType<IImportTypeControllerDependencies, ImportTypeControllerDependencies>()
                .RegisterType<IImportTypeImportProcessingRuleControllerDependencies, ImportTypeImportProcessingRuleControllerDependencies>()
                .RegisterType<IImportProcessingRuleImportTypeControllerDependencies, ImportProcessingRuleImportTypeControllerDependencies>()
                .RegisterType<IImportControllerDependencies, ImportControllerDependencies>()
                .RegisterType<IMetadataKeyControllerDependencies, MetadataKeyControllerDependencies>();
        }

        private static void RegisterOther(IUnityContainer container)
        {
            container.RegisterType<IUrlResolver, UrlResolver>();
            container.RegisterType<IFileSystem, FileSystem>();
        }
    }
}