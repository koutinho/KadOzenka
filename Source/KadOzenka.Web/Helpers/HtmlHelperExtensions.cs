using System;
using System.IO;
using System.Linq.Expressions;
using System.Text;
using System.Text.Encodings.Web;
using Core.Shared.Extensions;
using Kendo.Mvc.UI;
using Kendo.Mvc.UI.Fluent;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc.ViewFeatures.Internal;
using Microsoft.AspNetCore.Routing;

namespace CIPJS.Helpers
{
	public static class HtmlHelperExtensions
	{
		public static IHtmlContent AutoCompleteWithEditButton<TModel, TValue>(this IHtmlHelper<TModel> html,
	   Expression<Func<TModel, TValue>> expression, string textField, string controllerName, string methodName, string clearEvent = null, string editEvent = null, string selectEvent = null,
	   double minLength = 3, bool isReadonly = true, bool editMode = true, string validMes = null, bool allowInsert = false, bool clearButton = false)
		{
			ModelExplorer modelExplorer = ExpressionMetadataProvider.FromLambdaExpression(expression, html.ViewData, html.MetadataProvider);
			string htmlFieldName = ExpressionHelper.GetExpressionText(expression);


			string name = html.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(htmlFieldName);
			string script = "<script>" +
							$"var valid{name}; var id = '{name}';" +
			                $"function getDataSearch() {{ return {{searchText: $('#{name}').val()}}}}" +
							$"function onSelect(e) {{if(this.element.attr('id') === id) valid{name} = true; if('{selectEvent}'){selectEvent}.call({{e}});}}" +
							$"function onClose() {{if(this.element.attr('id') === id && !valid{name}) this.value('');}}" +
							$"function onOpen() {{if(this.element.attr('id') === id) valid{name} = false;}}" +
							$"function clearField() {{$('#{name}').val(''); if('{clearEvent}'){clearEvent}.call();}}" +
							$"$(document).ready(function(){{$('.clear-button-{name}').on('click', clearField);}});" +
							$"$(document).ready(function(){{$('.edit-button-{name}').on('click', function() {{if('{editEvent}'){editEvent}.call();}});}});" +
							"</script>";


			AutoCompleteBuilder element = html.Kendo().AutoComplete()
				.Name(name)
				.DataTextField(textField)
				.Filter("contains")
				.MinLength(minLength)
				.ClearButton(clearButton)
				.Events(x =>
					x.Close("onClose")
					.Select("onSelect")
					.Open("onOpen")
					)
				.DataSource(source =>
				{
					source.Read(read => { read.Action(methodName, controllerName).Data("getDataSearch"); })
						.ServerFiltering(true);
				})
				.Value(modelExplorer.Model?.ToString());

			RouteValueDictionary htmlAttributes = new RouteValueDictionary();
			htmlAttributes.Add("style", "width: 100%;");
			htmlAttributes.Add("class", "autocomplete-field");

			if (isReadonly) htmlAttributes.Add("readonly", "readonly");
			if (editMode) htmlAttributes.Add("editmode", "true");
			if (validMes.IsNotEmpty()) htmlAttributes.Add("validationmessage", validMes);

			if (modelExplorer.Metadata != null && modelExplorer.Metadata.IsRequired)
			{
				htmlAttributes.Add("required", "required");
			}
			element.HtmlAttributes(htmlAttributes);


			TagBuilder clearTag = new TagBuilder("a");
			clearTag.AddCssClass("k-button");
			clearTag.AddCssClass("k-button-icon");
			clearTag.AddCssClass($"clear-button-{name}");
			clearTag.InnerHtml.AppendHtml("<span class='k-icon k-i-close'></span>");

			TagBuilder editTag = new TagBuilder("a");
			editTag.AddCssClass("k-button");
			editTag.AddCssClass("k-button-icon");
			editTag.AddCssClass($"edit-button-{name}");
			editTag.InnerHtml.AppendHtml("<span class='k-icon k-i-edit'></span>");

			TagBuilder aotocomplitDiv = new TagBuilder("div");
			aotocomplitDiv.AddCssClass("col-sm-10");
			aotocomplitDiv.InnerHtml.AppendHtml(element);

			TagBuilder wrapButtonDiv = new TagBuilder("div");
			wrapButtonDiv.AddCssClass("col-sm-2");
			wrapButtonDiv.InnerHtml.AppendHtml(clearTag);
			wrapButtonDiv.InnerHtml.AppendHtml(editTag);

			TagBuilder container = new TagBuilder("div");
			container.AddCssClass("row");
			container.InnerHtml.AppendHtml(aotocomplitDiv);
			container.InnerHtml.AppendHtml(wrapButtonDiv);


			var writer = new StringWriter();
			container.WriteTo(writer, HtmlEncoder.Default);
			StringBuilder content = new StringBuilder();
			content.AppendLine(script);
			content.AppendLine(writer.ToString());

			return new HtmlString(content.ToString());
		}
	}
}