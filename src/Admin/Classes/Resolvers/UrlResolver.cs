using Admin.Interfaces.Resolvers;
using System;
using System.Configuration;
using System.Web;

namespace Admin.Classes.Resolvers
{
    public class UrlResolver : IUrlResolver
    {
        public string GetCurrentUrl()
        {
            var currentUrl = ConfigurationManager.AppSettings["Organisation.Website"];

            if (HttpContext.Current.Request.Url != null)
            {
                currentUrl = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority);

                if (!string.IsNullOrEmpty(HttpContext.Current.Request.ApplicationPath))
                {
                    currentUrl += HttpContext.Current.Request.ApplicationPath.TrimEnd('/');
                }
            }

            return currentUrl;
        }
    }
}