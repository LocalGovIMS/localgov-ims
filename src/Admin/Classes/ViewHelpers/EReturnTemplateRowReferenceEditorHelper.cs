using BusinessLogic.Entities;
using System;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Admin.Classes.ViewHelpers
{
    public static class EReturnTemplateRowReferenceEditorHelper
    {
        public static IHtmlString EReturnTemplateRowReferenceEditor(
            this HtmlHelper helper,
            TemplateRow templateRow,
            string reference,
            int item)
        {
            var output = new StringBuilder();

            if (!templateRow.ReferenceOverride)
            {
                output.Append(reference);

                var hidden = new TagBuilder("input");
                hidden.MergeAttribute("name", string.Format("Transactions[{0}].Reference", item));
                hidden.MergeAttribute("id", string.Format("Transactions_{0}__Reference", item));
                hidden.MergeAttribute("type", "hidden");
                hidden.MergeAttribute("value", reference);
                output.Append(hidden.ToString(TagRenderMode.Normal));

                return new MvcHtmlString(output.ToString());
            }

            var wildcardPosition = templateRow.Reference.IndexOf("*", StringComparison.Ordinal);
            var wildcardLength = templateRow.Reference.Length - wildcardPosition;

            if (wildcardPosition >= 0)
            {
                var hidden = new TagBuilder("input");
                hidden.MergeAttribute("name", string.Format("Transactions[{0}].Reference", item));
                hidden.MergeAttribute("id", string.Format("Transactions_{0}__Reference", item));
                hidden.MergeAttribute("type", "hidden");
                hidden.MergeAttribute("value", reference);
                output.Append(hidden.ToString(TagRenderMode.Normal));

                var label = new TagBuilder("div");
                label.MergeAttribute("class", "form-label");
                label.InnerHtml = reference.Substring(0, wildcardPosition);
                output.Append(label.ToString(TagRenderMode.Normal));

                var input = new TagBuilder("input");
                input.MergeAttribute("name", string.Format("ReferenceWildcard[{0}]", item));
                input.MergeAttribute("data-prefix", reference.Substring(0, wildcardPosition));
                input.MergeAttribute("data-index", item.ToString());
                input.MergeAttribute("size", wildcardLength.ToString());
                input.MergeAttribute("max-length", wildcardLength.ToString());
                input.MergeAttribute("class", "wildcard-value form-control");
                input.MergeAttribute("data-minlength", wildcardLength.ToString());
                input.MergeAttribute("value", reference.Substring(wildcardPosition));
                input.MergeAttribute("aria-labelledby", "Reference"); // TODO: Should pass this in as a parameter

                output.Append(input.ToString(TagRenderMode.Normal));

                var wrapper = new TagBuilder("div");
                wrapper.MergeAttribute("class", "ui labeled input");
                wrapper.InnerHtml = output.ToString();

                return new MvcHtmlString(wrapper.ToString());
            }
            else
            {
                var input = new TagBuilder("input");
                input.MergeAttribute("name", string.Format("Transactions[{0}].Reference", item));
                input.MergeAttribute("id", string.Format("Transactions_{0}__Reference", item));
                input.MergeAttribute("type", "text");
                input.MergeAttribute("max-length", "11");
                input.MergeAttribute("class", "form-control");
                input.MergeAttribute("value", reference);
                input.MergeAttribute("aria-labelledby", "Reference"); // TODO: Should pass this in as a parameter

                input.InnerHtml = output.ToString();

                return new MvcHtmlString(input.ToString());
            }
        }
    }
}
