using System.Linq.Expressions;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Assessment03.Utilities;

public static class IHtmlHelperExtension
{
    public static IHtmlContent CustomDropDown<TModel, TResult>(this IHtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TResult>> expression, IEnumerable<SelectListItem> selectList, string? optionLabel = null)
    {
        return new HtmlContentBuilder()
            .AppendHtml(htmlHelper.LabelFor(expression, htmlHelper.DisplayNameFor(expression)))
            .AppendHtml(htmlHelper.DropDownListFor(expression, selectList, optionLabel, new { @class = "form-control" }))
            .AppendHtml(htmlHelper.ValidationMessageFor(expression, null, new { @class = "text-danger" }));
    }

    public static IHtmlContent CustomTextBox<TModel, TResult>(this IHtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TResult>> expression)
    {
        return new HtmlContentBuilder()
            .AppendHtml(htmlHelper.LabelFor(expression, htmlHelper.DisplayNameFor(expression)))
            .AppendHtml(htmlHelper.TextBoxFor(expression, new { @class = "form-control" }))
            .AppendHtml(htmlHelper.ValidationMessageFor(expression, null, new { @class = "text-danger" }));
    }
}