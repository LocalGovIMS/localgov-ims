using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Web.Mvc.Navigation;

namespace Admin.Classes.ViewHelpers
{
    public static class BreadcrumbHelper
    {
        public static IHtmlString Breadcrumb(this HtmlHelper helper, Stack<NavigationItem> navigationItems)
        {
            var output = new StringBuilder();
            var clone = new Stack<NavigationItem>(new NavigationManager().NavigationItems);
            var count = 0;
            var urlHelper = new UrlHelper();

            var link = new TagBuilder("a");
            link.MergeAttribute("href", HttpContext.Current.Request.ApplicationPath);
            link.InnerHtml = "Home";
            output.Append(link.ToString(TagRenderMode.Normal));

            try
            {
                foreach (var item in clone)
                {
                    count++;

                    var splitter = new TagBuilder("i");
                    splitter.MergeAttribute("class", "right chevron icon divider");
                    output.Append(splitter.ToString(TagRenderMode.Normal));

                    link = new TagBuilder("a");
                    link.MergeAttribute("class", "section");
                    link.MergeAttribute("href", item.Url);
                    link.InnerHtml = item.DisplayText ?? item.Url.Replace("/", "").TrimStart();
                    output.Append(link.ToString(TagRenderMode.Normal));
                }
            }
            catch (Exception)
            {

            }

            var wrapper = new TagBuilder("div");
            wrapper.MergeAttribute("class", "ui small breadcrumb");
            wrapper.InnerHtml = output.ToString();

            return new MvcHtmlString(wrapper.ToString());
        }
    }
}