using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace WebUI
{
    public static class LabelExtensions
    {
        public static MvcHtmlString Label(this HtmlHelper html, string expression, object htmlAttributes = null, string id = "", bool generatedId = false)
        {
            return LabelHelper(html, ModelMetadata.FromStringExpression(expression, html.ViewData), expression, htmlAttributes, id, generatedId);
        }

        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "This is an appropriate nesting of generic types")]
        public static MvcHtmlString LabelFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, object htmlAttributes, string id = "", bool generatedId = false)
        {
            return LabelHelper(html, ModelMetadata.FromLambdaExpression(expression, html.ViewData), ExpressionHelper.GetExpressionText(expression), htmlAttributes, id, generatedId);
        }

        internal static MvcHtmlString LabelHelper(HtmlHelper html, ModelMetadata metadata, string htmlFieldName, object htmlAttributes, string id, bool generatedId)
        {
            string labelText = metadata.DisplayName ?? metadata.PropertyName ?? htmlFieldName.Split('.').Last();
            if (String.IsNullOrEmpty(labelText))
            {
                return MvcHtmlString.Empty;
            }

            if (metadata.IsRequired)
                labelText += " <span class=\"text-danger\">*</span>";

            var label = new TagBuilder("label");
            if (!string.IsNullOrWhiteSpace(id))
            {
                label.Attributes.Add("id", id);
            }
            else //if (generatedId)
            {
                label.Attributes.Add("id", html.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldId(htmlFieldName) + "_Label");
            }

            label.Attributes.Add("for", html.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldId(htmlFieldName));
            label.InnerHtml=labelText;

            return MvcHtmlString.Create(label.ToString());
        }
    }

}