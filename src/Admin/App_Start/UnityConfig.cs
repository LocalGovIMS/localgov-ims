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

            container.RegisterType<IUnitOfWork, UnitOfWork>(new PerRequestLifetimeManager())
                .RegisterType<ISecurityContext, SecurityContext>(new PerRequestLifetimeManager())
                .RegisterType<IUserStore, UserStore>(new PerRequestLifetimeManager())
                .RegisterType<IncomeDbContext>(new PerRequestLifetimeManager());

            container.RegisterType<ApplicationUserManager>(new PerRequestLifetimeManager());
            container.RegisterType<ApplicationSignInManager>(new PerRequestLifetimeManager());
            container.RegisterType<IUserStore<PaymentsUser>, UserStore<PaymentsUser>>(new PerRequestLifetimeManager());
            container.RegisterType<IAuthenticationManager>(new InjectionFactory(x => HttpContext.Current.GetOwinContext().Authentication));
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

                .RegisterType<IModelBuilder<ViewModels.Fund.DetailsViewModel, string>, ViewModelBuilders.Fund.DetailsViewModelBuilder>()
                .RegisterType<IModelBuilder<ViewModels.Fund.EditViewModel, string>, ViewModelBuilders.Fund.EditViewModelBuilder>()
                .RegisterType<IModelBuilder<ViewModels.Fund.ListViewModel, ViewModels.Fund.SearchCriteria>, ViewModelBuilders.Fund.ListViewModelBuilder>()
                .RegisterType<IModelCommand<ViewModels.Fund.EditViewModel>, Commands.Fund.CreateCommand>("Create")
                .RegisterType<IModelCommand<ViewModels.Fund.EditViewModel>, Commands.Fund.EditCommand>("Edit")

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
                .RegisterType<IModelCommand<string>, Commands.Payment.RemoveCommand>("Remove")
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

                .RegisterType<IModelBuilder<ViewModels.SystemMessage.DetailsViewModel, int>, ViewModelBuilders.SystemMessage.DetailsViewModelBuilder>()
                .RegisterType<IModelBuilder<ViewModels.SystemMessage.EditViewModel, int>, ViewModelBuilders.SystemMessage.EditViewModelBuilder>()
                .RegisterType<IModelBuilder<IList<ViewModels.SystemMessage.DetailsViewModel>, int>, ViewModelBuilders.SystemMessage.ListViewModelBuilder>()
                .RegisterType<IModelCommand<ViewModels.SystemMessage.EditViewModel>, Commands.SystemMessage.CreateCommand>("Create")
                .RegisterType<IModelCommand<ViewModels.SystemMessage.EditViewModel>, Commands.SystemMessage.EditCommand>("Edit")

                .RegisterType<IModelBuilder<ViewModels.Template.ListViewModel, int>, ViewModelBuilders.Template.ListViewModelBuilder>()
                .RegisterType<IModelBuilder<ViewModels.Template.EditViewModel, int>, ViewModelBuilders.Template.EditViewModelBuilder>()
                .RegisterType<IModelCommand<ViewModels.Template.EditViewModel>, Commands.Template.CreateCommand>("Create")
                .RegisterType<IModelCommand<ViewModels.Template.EditViewModel>, Commands.Template.EditCommand>("Edit")

                .RegisterType<IModelBuilder<ViewModels.Transaction.ListViewModel, ViewModels.Transaction.SearchCriteria>, ViewModelBuilders.Transaction.ListViewModelBuilder>()
                .RegisterType<IModelBuilder<ViewModels.Transaction.DetailsViewModel, string>, ViewModelBuilders.Transaction.DetailsViewModelBuilder>()
                .RegisterType<IModelBuilder<ViewModels.Transaction.TransferViewModel, string>, ViewModelBuilders.Transaction.TransferViewModelBuilder>()
                .RegisterType<IModelBuilder<ViewModels.Transaction.RefundViewModel, string>, ViewModelBuilders.Transaction.RefundViewModelBuilder>()
                .RegisterType<IModelCommand<ViewModels.Transaction.TransferViewModel>, Commands.Transaction.TransferCommand>()
                .RegisterType<IModelCommand<string>, Commands.Transaction.UndoTransferCommand>("UndoTransfer")
                .RegisterType<IModelCommand<ViewModels.Transaction.RefundViewModel>, Commands.Transaction.RefundCommand>()
                .RegisterType<IModelCommand<TransferItem>, Commands.Transaction.ValidateTransferItemCommand>()
                .RegisterType<IModelCommand<ViewModels.Transaction.EmailReceiptViewModel>, Commands.Transaction.EmailReceiptCommand>()

                .RegisterType<IModelBuilder<ViewModels.Transfer.TransferViewModel, string>, ViewModelBuilders.Transfer.TransferViewModelBuilder>()
                .RegisterType<IModelCommand<ViewModels.Transfer.TransferViewModel>, Commands.Transfer.TransferCommand>()

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

                .RegisterType<IModelBuilder<ViewModels.Vat.DetailsViewModel, string>, ViewModelBuilders.Vat.DetailsViewModelBuilder>()
                .RegisterType<IModelBuilder<ViewModels.Vat.EditViewModel, string>, ViewModelBuilders.Vat.EditViewModelBuilder>()
                .RegisterType<IModelBuilder<IList<ViewModels.Vat.DetailsViewModel>, string>, ViewModelBuilders.Vat.ListViewModelBuilder>()
                .RegisterType<IModelCommand<ViewModels.Vat.EditViewModel>, Commands.Vat.CreateCommand>("Create")
                .RegisterType<IModelCommand<ViewModels.Vat.EditViewModel>, Commands.Vat.EditCommand>("Edit")

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
                ;
        }

        private static void RegisterControllerDependencies(IUnityContainer container)
        {
            container
                .RegisterType<IAccountHolderControllerDependencies, AccountHolderControllerDependencies>()
                .RegisterType<IHomeControllerDependencies, HomeControllerDependencies>()
                .RegisterType<IFundGroupControllerDependencies, FundGroupControllerDependencies>()
                .RegisterType<IEReturnControllerDependencies, EReturnControllerDependencies>()
                .RegisterType<IMethodOfPaymentControllerDependencies, MethodOfPaymentControllerDependencies>()
                .RegisterType<IMethodOfPaymentMetadataControllerDependencies, MethodOfPaymentMetadataControllerDependencies>()
                .RegisterType<IPaymentControllerDependencies, PaymentControllerDependencies>()
                .RegisterType<IRoleControllerDependencies, RoleControllerDependencies>()
                .RegisterType<ISuspenseControllerDependencies, SuspenseControllerDependencies>()
                .RegisterType<ISystemMessageControllerDependencies, SystemMessageControllerDependencies>()
                .RegisterType<ITemplateControllerDependencies, TemplateControllerDependencies>()
                .RegisterType<ITransactionControllerDependencies, TransactionControllerDependencies>()
                .RegisterType<ITransferControllerDependencies, TransferControllerDependencies>()
                .RegisterType<IUserControllerDependencies, UserControllerDependencies>()
                .RegisterType<IUserFundGroupControllerDependencies, UserFundGroupControllerDependencies>()
                .RegisterType<IUserRoleControllerDependencies, UserRoleControllerDependencies>()
                .RegisterType<IUserTemplateControllerDependencies, UserTemplateControllerDependencies>()
                .RegisterType<IVatControllerDependencies, VatControllerDependencies>()
                .RegisterType<IFundControllerDependencies, FundControllerDependencies>()
                .RegisterType<IValidationControllerDependencies, ValidationControllerDependencies>()
                .RegisterType<IImportProcessingRuleControllerDependencies, ImportProcessingRuleControllerDependencies>()
                .RegisterType<IImportProcessingRuleConditionControllerDependencies, ImportProcessingRuleConditionControllerDependencies>()
                .RegisterType<IImportProcessingRuleActionControllerDependencies, ImportProcessingRuleActionControllerDependencies>()
                .RegisterType<IOfficeControllerDependencies, OfficeControllerDependencies>();

        }

        private static void RegisterOther(IUnityContainer container)
        {
            container.RegisterType<IUrlResolver, UrlResolver>();
        }
    }
}