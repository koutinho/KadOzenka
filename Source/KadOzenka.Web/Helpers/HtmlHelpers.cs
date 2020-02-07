using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.Encodings.Web;
using Core.Shared.Misc;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Kendo.Mvc.UI;
using Kendo.Mvc.UI.Fluent;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc.ViewFeatures.Internal;
using Microsoft.AspNetCore.Routing;

namespace KadOzenka.Web.Helpers
{
	public static class HtmlHelpers
	{
		public static IHtmlContent KendoDropDownListWithAutocompleteFor<TModel, TValue>(this IHtmlHelper<TModel> html,
			Expression<Func<TModel, TValue>> expression, IEnumerable data, string dataTextField = "Text",
			string dataValueField = "Value", string filter = "contains", double minLength = 3)
		{
			ModelExplorer modelExplorer =
				ExpressionMetadataProvider.FromLambdaExpression(expression, html.ViewData, html.MetadataProvider);

			var htmlFieldName = ExpressionHelper.GetExpressionText(expression);
			var name = html.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(htmlFieldName);
			var className = name.Replace(".", "_");

			var script = "<script>" +
						 $"function onCascade{className}(e) {{if($('#{className}Wrapper').data('kendoTooltip')){{ $('#{className}Wrapper').data('kendoTooltip').options.content = this.text(); $('#{className}Wrapper').data('kendoTooltip').refresh();}}}}" +
			             $"function clearField{className}() {{ $('input.{className}').data('kendoDropDownList').value('');}}" +
							$"$(document).ready(function(){{$('.clear-button-{className}').on('click', clearField{className});}});" +
							"</script>";

			var dataList = data.Cast<object>().ToList();
			var dataSource = dataList.Select(x => new SelectListItem(x.GetPropertyValue(dataTextField)?.ToString(),
				x.GetPropertyValue(dataValueField)?.ToString())).ToList();
			var emptyItem = new SelectListItem("", null);
			dataSource.Insert(0, emptyItem);

			var clearTag = new TagBuilder("a");
			clearTag.AddCssClass("k-button");
			clearTag.AddCssClass("k-button-icon");
			clearTag.AddCssClass($"clear-button-{className}");
			clearTag.InnerHtml.AppendHtml("<span class='k-icon k-i-close'></span>");

			DropDownListBuilder dropDownBuilder = html.Kendo().DropDownList()
				.Name(name)
				.Filter(filter)
				.BindTo(dataSource)
				.OptionLabel(null)
				.Events(x =>
					x.Cascade($"onCascade{className}")
				)
				.Value(modelExplorer.Model?.ToString());

			var tooltip = html.Kendo().Tooltip()
				.For($"#{className}Wrapper")
				.Filter(".k-dropdown")
				.Iframe(true)
				.Position(TooltipPosition.Top)
				.AutoHide(true);

			var dropDownBuilderHtmlAttributes = new RouteValueDictionary
			{
				{ "style", "width: 100%;" },
				{ "class", $"{className}" },
			};

			if (modelExplorer.Model != null)
			{
				var selectedDataItem = dataList.FirstOrDefault(x =>
					x.HasProperty(dataValueField) &&
					x.GetPropertyValue(dataValueField).ToString() == modelExplorer.Model.ToString());
				if (selectedDataItem != null)
				{
					tooltip = tooltip.Content(selectedDataItem.GetPropertyValue(dataTextField)?.ToString());
				}
			}

			if (modelExplorer.Metadata != null && modelExplorer.Metadata.IsRequired)
			{
				dropDownBuilderHtmlAttributes.Add("required", "required");
			}
			dropDownBuilder.HtmlAttributes(dropDownBuilderHtmlAttributes);

			var autocompleteDiv = new TagBuilder("div");
			autocompleteDiv.MergeAttribute("id", $"{className}Wrapper");
			autocompleteDiv.AddCssClass("col-sm-11");
			autocompleteDiv.InnerHtml.AppendHtml(dropDownBuilder);

			var wrapButtonDiv = new TagBuilder("div");
			wrapButtonDiv.AddCssClass("col-sm-1");
			wrapButtonDiv.InnerHtml.AppendHtml(clearTag);

			var container = new TagBuilder("div");
			container.AddCssClass("row");
			container.MergeAttribute("style", "display: flex;");
			container.InnerHtml.AppendHtml(autocompleteDiv);
			container.InnerHtml.AppendHtml(wrapButtonDiv);
			container.InnerHtml.AppendHtml(tooltip);

			var writer = new StringWriter();
			container.WriteTo(writer, HtmlEncoder.Default);
			StringBuilder content = new StringBuilder();
			content.AppendLine(script);
			content.AppendLine(writer.ToString());

			return new HtmlString(content.ToString());
		}

		public static IHtmlContent KendoAutocompleteWithClearButtonFor<TModel, TValue>(this IHtmlHelper<TModel> html,
			Expression<Func<TModel, TValue>> expression, IEnumerable data, string dataTextField = "Text",
			string dataValueField = "Value", string filter = "contains", double minLength = 3)
		{
			ModelExplorer modelExplorer =
				ExpressionMetadataProvider.FromLambdaExpression(expression, html.ViewData, html.MetadataProvider);

			var htmlFieldName = ExpressionHelper.GetExpressionText(expression);
			var name = html.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(htmlFieldName);
			var textName = html.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(htmlFieldName) + "Text";

			var script = "<script>" +
							$"var valid{textName}; var id{textName} = '{textName}';" +
							$"function onOpen{textName}() {{if(this.element.attr('id') === id{textName}) valid{textName} = false;}}" +
							$"function onClose{textName}() {{if(this.element.attr('id') === id{textName} && !valid{textName}) {{this.value(''); $('#{textName}').prop('title', ''); $('#{name}').val(null);}}}}" +
							$"function onSelect{textName}(e) {{if(this.element.attr('id') === id{textName}) {{valid{textName} = true; $('#{textName}').prop('title', e.dataItem.{dataTextField}); $('#{name}').val(e.dataItem.{dataValueField});}}}}" +
							$"function clearField{textName}() {{ $('#{textName}').val(''); $('#{textName}').prop('title', ''); $('#{name}').val(null);}}" +
							$"$(document).ready(function(){{$('.clear-button-{textName}').on('click', clearField{textName});}});" +
							"</script>";

			var dataList = data.Cast<object>().ToList();

			var clearTag = new TagBuilder("a");
			clearTag.AddCssClass("k-button");
			clearTag.AddCssClass("k-button-icon");
			clearTag.AddCssClass($"clear-button-{textName}");
			clearTag.InnerHtml.AppendHtml("<span class='k-icon k-i-close'></span>");

			var hiddenInputTag = new TagBuilder("input");
			hiddenInputTag.Attributes.Add("type", "hidden");
			hiddenInputTag.Attributes.Add("name", name);
			hiddenInputTag.Attributes.Add("id", name);

			var autoCompleteBuilder = html.Kendo().AutoComplete()
				.Name(textName)
				.DataTextField(dataTextField)
				.Filter(filter)
				.MinLength(minLength)
				.ClearButton(false)
				.Events(x =>
					x.Close($"onClose{textName}")
						.Select($"onSelect{textName}")
						.Open($"onOpen{textName}")
				)
				.BindTo(dataList);

			var autoCompleteHtmlAttributes = new RouteValueDictionary
			{
				{ "style", "width: 100%;" },
				{ "class", "autocomplete-field" },
				{ "title", "" }
			};

			if (modelExplorer.Model != null)
			{
				var selectedDataItem = dataList.FirstOrDefault(x =>
					x.HasProperty(dataValueField) &&
					x.GetPropertyValue(dataValueField).ToString() == modelExplorer.Model.ToString());
				if (selectedDataItem != null)
				{
					autoCompleteBuilder = autoCompleteBuilder.Value(selectedDataItem.GetPropertyValue(dataTextField)?.ToString());
					hiddenInputTag.Attributes.Add("value", selectedDataItem.GetPropertyValue(dataValueField)?.ToString());
					autoCompleteHtmlAttributes["title"] = selectedDataItem.GetPropertyValue(dataTextField)?.ToString();
				}
			}

			if (modelExplorer.Metadata != null && modelExplorer.Metadata.IsRequired)
			{
				autoCompleteHtmlAttributes.Add("required", "required");
			}
			autoCompleteBuilder.HtmlAttributes(autoCompleteHtmlAttributes);

			var autocompleteDiv = new TagBuilder("div");
			autocompleteDiv.AddCssClass("col-sm-11");
			autocompleteDiv.InnerHtml.AppendHtml(autoCompleteBuilder);
			autocompleteDiv.InnerHtml.AppendHtml(hiddenInputTag);

			var wrapButtonDiv = new TagBuilder("div");
			wrapButtonDiv.AddCssClass("col-sm-1");
			wrapButtonDiv.InnerHtml.AppendHtml(clearTag);

			var container = new TagBuilder("div");
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
