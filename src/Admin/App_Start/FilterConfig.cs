using Admin.Classes.Attributes.Security;
using System.Diagnostics.CodeAnalysis;
using System.Web.Mvc;

namespace Admin
{
    [ExcludeFromCodeCoverage]
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new UserDisabledCheckActionFilterAttribute());
        }
    }
}
