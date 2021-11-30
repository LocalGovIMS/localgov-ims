using System.Web;
using System.Web.Mvc;

namespace Admin.Classes.ViewHelpers
{
    public static class FormatHelper
    {
        public static IHtmlString ToCurrency(this HtmlHelper helper, decimal? value)
        {
            var formattedValue = decimal.Round((decimal)(value ?? 0), 2).ToString("N");
            return new MvcHtmlString(formattedValue);
        }
        public static IHtmlString ToMaxLength(this HtmlHelper helper, string value, int maxLength)
        {
            if (string.IsNullOrEmpty(value)) return new MvcHtmlString(string.Empty);

            var length = value.Length;

            var isOverMaxLength = length > maxLength;

            if (isOverMaxLength) length = maxLength;

            return new MvcHtmlString(value.Substring(0, length) + (isOverMaxLength ? "..." : ""));
        }
    }
}