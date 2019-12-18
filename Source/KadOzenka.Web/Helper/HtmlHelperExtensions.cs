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

namespace CIPJS.Helper
{
	public static class HtmlHelperExtensions
	{
		public static IHtmlContent TestAutoCompleteWithEditButtonFor<TModel, TValue>(this IHtmlHelper<TModel> html,
Expression<Func<TModel, TValue>> expression, string textField, string controllerName, string methodName, string clearEvent = null, string searchEvent = null,
string selectEvent = null, string editEvent = null, string addEvent = null,
double minLength = 3, bool isReadonly = true, bool isAdditionalButton = false, bool editMode = true, string validMes = null, bool clearButton = false)
		{
			ModelExplorer modelExplorer = ExpressionMetadataProvider.FromLambdaExpression(expression, html.ViewData, html.MetadataProvider);
			string htmlFieldName = ExpressionHelper.GetExpressionText(expression);


			string name = html.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(htmlFieldName);

			string onSelect = $"function onSelect{name}(e) {{if(this.element.attr('id') === id) valid{name} = true;}}";
			if (selectEvent != null) onSelect = 
				$"function onSelect{name}(e) {{if(this.element.attr('id') === id) valid{name} = true; if('{selectEvent}'){selectEvent}.call({{e}});}}";

			string clearField =
				$"function clearField{name}() {{$('#{name}').val('');}}";
			if(clearEvent != null) clearField =
				$"function clearField{name}() {{$('#{name}').val(''); if('{clearEvent}'){clearEvent}.call();}}";

			string onSearch = "";
			if (searchEvent != null)
				onSearch = $"$(document).ready(function(){{$('.search-button-{name}').on('click', function() {{if('{searchEvent}'){searchEvent}.call();}});}});";

			string onEdit = "";
			if (editEvent != null)
				onEdit = $"$(document).ready(function(){{$('.edit-button-{name}').on('click', function() {{if('{editEvent}'){editEvent}.call();}});}});";

			string onAdd = "";
			if (addEvent != null)
				onAdd =
					$"$(document).ready(function(){{$('.add-button-{name}').on('click', function() {{if('{addEvent}'){addEvent}.call();}});}});";

			string script = "<script>" +
							$"var valid{name}; var id = '{name}';" +
							$"function getDataSearch{name}() {{ return {{searchText: $('#{name}').val()}}}}" + onSelect +
							$"function onClose{name}() {{if(this.element.attr('id') === id && !valid{name}) this.value('');}}" +
							$"function onOpen{name}() {{if(this.element.attr('id') === id) valid{name} = false;}}" + clearField +
							$"$(document).ready(function(){{$('.clear-button-{name}').on('click', clearField{name});}});" + 
							onSearch + 
							onEdit +
							onAdd +
							"</script>";

			AutoCompleteBuilder element = html.Kendo().AutoComplete()
				.Name(name)
				.DataTextField(textField)
				.Filter("contains")
				.MinLength(minLength)
				.ClearButton(clearButton)
				.Events(x =>
					x.Close($"onClose{name}")
					.Select($"onSelect{name}")
					.Open($"onOpen{name}")
					)
				.DataSource(source =>
				{
					source.Read(read => { read.Action(methodName, controllerName).Data($"getDataSearch{name}"); })
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

			TagBuilder searchTag = new TagBuilder("a");
			searchTag.AddCssClass("k-button");
			searchTag.AddCssClass("k-button-icon");
			searchTag.AddCssClass($"search-button-{name}");
			searchTag.InnerHtml.AppendHtml("<span class='k-icon k-i-search'></span>");

			TagBuilder editTag = new TagBuilder("a");
			editTag.AddCssClass("k-button");
			editTag.AddCssClass("k-button-icon");
			editTag.AddCssClass($"edit-button-{name}");
			editTag.InnerHtml.AppendHtml("<span class='k-icon k-i-edit'></span>");

			TagBuilder addTag = new TagBuilder("a");
			addTag.AddCssClass("k-button");
			addTag.AddCssClass("k-button-icon");
			addTag.AddCssClass($"add-button-{name}");
			addTag.InnerHtml.AppendHtml("<span class='k-icon k-i-plus'></span>");

			TagBuilder autocompleteDiv = new TagBuilder("div");
			autocompleteDiv.AddCssClass(isAdditionalButton ? "col-sm-9" : "col-sm-10");
			autocompleteDiv.InnerHtml.AppendHtml(element);

			TagBuilder wrapButtonDiv = new TagBuilder("div");
			wrapButtonDiv.AddCssClass(isAdditionalButton ? "col-sm-3" : "col-sm-2");
			wrapButtonDiv.InnerHtml.AppendHtml(clearTag);
			wrapButtonDiv.InnerHtml.AppendHtml(searchTag);
			if (isAdditionalButton)
			{
				wrapButtonDiv.InnerHtml.AppendHtml(editTag);
				wrapButtonDiv.InnerHtml.AppendHtml(addTag);
			}

			TagBuilder container = new TagBuilder("div");
			container.AddCssClass("row");
			container.InnerHtml.AppendHtml(autocompleteDiv);
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