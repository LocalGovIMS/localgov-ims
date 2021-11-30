using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Web.Mvc;

namespace PaymentPortal
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            ModelBinderProviders.BinderProviders.Add(new XmlModelBinderProvider());
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}