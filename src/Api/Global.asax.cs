using MessagePack;
using MessagePack.Resolvers;
using System.Web.Http;

namespace Api
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            MessagePackSerializer.DefaultOptions = new MessagePackSerializerOptions(CompositeResolver.Create(new IFormatterResolver[]
            {
                // This can solve DateTime time zone problem
                NativeDateTimeResolver.Instance,
                ContractlessStandardResolver.Instance
            }));
        }
    }
}
