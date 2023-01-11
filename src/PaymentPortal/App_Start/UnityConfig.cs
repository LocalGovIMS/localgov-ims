using BusinessLogic.Interfaces.Persistence;
using BusinessLogic.Interfaces.Security;
using BusinessLogic.Security;
using DataAccess.Persistence;
using PaymentPortal.Controllers;
using PaymentPortal.Security;
using System;
using System.Web.Mvc;
using Unity;
using Unity.AspNet.Mvc;
using Unity.log4net;

namespace PaymentPortal
{
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

            // container.RegisterType<IProductRepository, ProductRepository>();

            container.RegisterType<ISecurityContext, SecurityContext>(new PerRequestLifetimeManager());
            container.RegisterType<IUserStore, UserStore>(new PerRequestLifetimeManager());
            container.RegisterType<IncomeDbContext>(new PerRequestLifetimeManager());

            RegisterControllerDependencies(container);

            BusinessLogic.UnityConfig.RegisterComponents(container);
            DataAccess.UnityConfig.RegisterComponents(container);

            log4net.Config.XmlConfigurator.Configure();
            container.AddNewExtension<Log4NetExtension>();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }

        private static void RegisterControllerDependencies(IUnityContainer container)
        {
            container.RegisterType<IPaymentControllerDependencies, PaymentControllerDependencies>();
        }
    }
}