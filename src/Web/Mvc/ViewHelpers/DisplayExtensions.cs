using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace Web.Mvc.ViewHelpers
{
    public static class DisplayExtensions
    {
        public static MvcHtmlString DisplayWithIdFor<TModel, TValue>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TValue>> expression, object htmlAttributes, string wrapperTag = "span")
        {
            var id = helper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldId(ExpressionHelper.GetExpressionText(expression));

            if (htmlAttributes != null)
            {
                var tag = new TagBuilder(wrapperTag);
                tag.MergeAttributes(HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes) as IDictionary<string, object>);
                tag.Attributes.Add("id", id);
                tag.SetInnerText(HttpUtility.HtmlDecode(helper.DisplayFor(expression).ToString()));

                return MvcHtmlString.Create(tag.ToString(TagRenderMode.Normal));
            }
            else
            {
                return MvcHtmlString.Create(string.Format("<{0} id=\"{1}\">{2}</{0}>", wrapperTag, id, HttpUtility.HtmlDecode(helper.DisplayFor(expression).ToString())));
            }
        }
    }
}
