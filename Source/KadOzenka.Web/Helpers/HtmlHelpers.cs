using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.Encodings.Web;
using Core.RefLib;
using Core.Shared.Misc;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Kendo.Mvc.UI;
using Kendo.Mvc.UI.Fluent;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
//using Microsoft.AspNetCore.Mvc.ViewFeatures.Internal;
using Microsoft.AspNetCore.Routing;
using ObjectModel.Core.Shared;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace KadOzenka.Web.Helpers
{
	public static class HtmlHelpers
	{
		public static IHtmlContent KendoDropDownListTreeWithButton<TModel, TValue>(this IHtmlHelper<TModel> html,
			Expression<Func<TModel, TValue>> expression, IEnumerable<DropDownTreeItemModel> data, string dataTextField = "Text",
			string dataValueField = "Value", FilterType filter = FilterType.Contains, bool useAddTag = false, string addFunction = "", double minLength = 3, bool isReadonly = false, string idSuffix = null, string idOverwrite =null)
		{
			var modelExpressionProvider = (ModelExpressionProvider)html.ViewContext.HttpContext.RequestServices.GetService(typeof(IModelExpressionProvider));
			var modelExplorer = modelExpressionProvider.CreateModelExpression(html.ViewData, expression);

			string htmlFieldName = GetExpressionText(html, expression);
			var name = html.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(htmlFieldName);
			if (!string.IsNullOrEmpty(idSuffix))
			{
				name = name + idSuffix;
			}

			var className = name.Replace(".", "_").Replace('[', '_').Replace(']', '_');

			var script = $"<script src='/js/dropdowns.js'></script><script>" +
			             $"init('{className}',{addFunction})</script>";
            List<DropDownTreeItemModel> dataSource = data.ToList();

			var clearTag = new TagBuilder("a");
			clearTag.AddCssClass("k-button");
			clearTag.AddCssClass("k-button-icon");
			clearTag.AddCssClass($"clear-button-{className}");
			clearTag.MergeAttribute("style", "margin-left: 2px; width: 35px; display: table-cell;");
			clearTag.InnerHtml.AppendHtml("<span class='k-icon k-i-close'></span>");

			var addTag = new TagBuilder("a");
			addTag.AddCssClass("k-button");
			addTag.AddCssClass("k-button-icon");
			addTag.AddCssClass($"add-button-{className}");
			addTag.MergeAttribute("style", "margin-left: 2px; width: 35px; display: table-cell;");
			addTag.InnerHtml.AppendHtml("<span class='fas fa-plus-circle'></span>");

			DropDownTreeBuilder dropDownTreeBuilder = html.Kendo().DropDownTree()
				.Name(idOverwrite ?? name)
				.Filter(filter)
				.BindTo(dataSource)
				.ClearButton(false)
				.Value(modelExplorer.Model?.ToString());

			var tooltip = html.Kendo().Tooltip()
				.For($"#{className}Wrapper")
				.Filter(".k-dropdowntree")
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
				foreach (var item in dataSource)
				{
					var selectedDataItem = item.Items.FirstOrDefault(x =>
						x.HasProperty(dataValueField) &&
						x.GetPropertyValue(dataValueField).ToString() == modelExplorer.Model.ToString());
					if (selectedDataItem != null)
					{
						tooltip = tooltip.Content(selectedDataItem.GetPropertyValue(dataTextField)?.ToString());
						break;
					}
				}
				
			}

			if (modelExplorer.Metadata != null && modelExplorer.Metadata.IsRequired)
			{
				dropDownBuilderHtmlAttributes.Add("required", "required");
			}
			dropDownTreeBuilder.HtmlAttributes(dropDownBuilderHtmlAttributes);

			if (isReadonly)
			{
				dropDownTreeBuilder = dropDownTreeBuilder.Enable(false);
				clearTag.AddCssClass("k-state-disabled");
			}

			var dropDownWrapper = new TagBuilder("div");
			dropDownWrapper.MergeAttribute("style", "display: table-cell;");
			dropDownWrapper.InnerHtml.AppendHtml(dropDownTreeBuilder);

			var allWrapper = new TagBuilder("div");
			allWrapper.MergeAttribute("style", "display: table; width: 100%; table-layout: fixed;");
			allWrapper.InnerHtml.AppendHtml(dropDownWrapper);
			allWrapper.InnerHtml.AppendHtml(clearTag);
			if (useAddTag)
			{
				allWrapper.InnerHtml.AppendHtml(addTag);
			}


			var autocompleteDiv = new TagBuilder("div");
			autocompleteDiv.MergeAttribute("id", $"{className}Wrapper");
			autocompleteDiv.AddCssClass("col-sm-12");
			autocompleteDiv.InnerHtml.AppendHtml(allWrapper);

			var container = new TagBuilder("div");
			container.AddCssClass("row");
			container.InnerHtml.AppendHtml(autocompleteDiv);
			container.InnerHtml.AppendHtml(tooltip);

			var writer = new StringWriter();
			container.WriteTo(writer, HtmlEncoder.Default);
			StringBuilder content = new StringBuilder();
			content.AppendLine(script);
			content.AppendLine(writer.ToString());

			return new HtmlString(content.ToString());
		}

		public static IHtmlContent KendoDropDownListWithAutocompleteFor<TModel, TValue>(this IHtmlHelper<TModel> html,
			Expression<Func<TModel, TValue>> expression, IEnumerable data, string dataTextField = "Text",
			string dataValueField = "Value", string filter = "contains", bool useAddTag = false, string addFunction = "", double minLength = 3, bool isReadonly = false, string idPrefix = null, string modelPrefix = null)
		{
			var modelExpressionProvider = (ModelExpressionProvider)html.ViewContext.HttpContext.RequestServices.GetService(typeof(IModelExpressionProvider));
			var modelExplorer = modelExpressionProvider.CreateModelExpression(html.ViewData, expression);

			string htmlFieldName = GetExpressionText(html, expression);
			var name = html.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(htmlFieldName);
		    if (!string.IsNullOrEmpty(modelPrefix))
		    {
		        name = modelPrefix + "." + name;
		    }
            var className = name.Replace(".", "_").Replace('[', '_').Replace(']', '_');
			if (!string.IsNullOrEmpty(idPrefix))
			{
				name = name + idPrefix;
				className = className + idPrefix;
			}

            var script = "<script>" +
						 $"function onCascade{className}(e) {{if($('#{className}Wrapper').data('kendoTooltip')){{ $('#{className}Wrapper').data('kendoTooltip').options.content = this.text(); $('#{className}Wrapper').data('kendoTooltip').refresh();}}}}" +
			             $"function clearField{className}() {{ $('input.{className}').data('kendoDropDownList').value('');}}" +
							$"$(document).ready(function(){{$('.add-button-{className}').on('click', {addFunction});}});" +
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
			clearTag.MergeAttribute("style", "margin-left: 2px; width: 35px; display: table-cell;");
			clearTag.InnerHtml.AppendHtml("<span class='k-icon k-i-close'></span>");

			var addTag = new TagBuilder("a");
			addTag.AddCssClass("k-button");
			addTag.AddCssClass("k-button-icon");
			addTag.AddCssClass($"add-button-{className}");
			addTag.MergeAttribute("style", "margin-left: 2px; width: 35px; display: table-cell;");
			addTag.InnerHtml.AppendHtml("<span class='fas fa-plus-circle'></span>");

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

			if (isReadonly)
			{
				dropDownBuilder = dropDownBuilder.Enable(false);
				clearTag.AddCssClass("k-state-disabled");
			}

			var dropDownWrapper = new TagBuilder("div");
			dropDownWrapper.MergeAttribute("style", "display: table-cell;");
			dropDownWrapper.InnerHtml.AppendHtml(dropDownBuilder);

			var allWrapper = new TagBuilder("div");
			allWrapper.MergeAttribute("style", "display: table; width: 100%; table-layout: fixed;");
			allWrapper.InnerHtml.AppendHtml(dropDownWrapper);
			allWrapper.InnerHtml.AppendHtml(clearTag);
			if (useAddTag)
			{
				allWrapper.InnerHtml.AppendHtml(addTag);
			}


			var autocompleteDiv = new TagBuilder("div");
			autocompleteDiv.MergeAttribute("id", $"{className}Wrapper");
			autocompleteDiv.AddCssClass("col-sm-12");
			autocompleteDiv.InnerHtml.AppendHtml(allWrapper);

			var container = new TagBuilder("div");
			container.AddCssClass("row");
			container.InnerHtml.AppendHtml(autocompleteDiv);
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
			var modelExpressionProvider = (ModelExpressionProvider)html.ViewContext.HttpContext.RequestServices.GetService(typeof(IModelExpressionProvider));
			var modelExplorer = modelExpressionProvider.CreateModelExpression(html.ViewData, expression);

			string htmlFieldName = GetExpressionText(html, expression);
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

		public static IHtmlContent KendoKoReferenceDropDownListFor<TModel, TValue>(this IHtmlHelper<TModel> html,
			Expression<Func<TModel, TValue>> expression,
			long? referenceId, string text = null, string value = null, bool isReadonly = true, bool withOptionLabel = true, string filter = "contains")
		{
			var modelExpressionProvider = (ModelExpressionProvider)html.ViewContext.HttpContext.RequestServices.GetService(typeof(IModelExpressionProvider));
			var modelExplorer = modelExpressionProvider.CreateModelExpression(html.ViewData, expression);

			List<OMReferenceItem> referenceItems = ReferencesCommon.GetItems(referenceId.GetValueOrDefault(-1), false);
			List<SelectListItem> dataSource = referenceItems
				.Select(s => new SelectListItem() {Value = s.ItemId.ToString(), Text = s.Value}).ToList();

			DropDownListBuilder element = html.Kendo().DropDownListFor(expression)
				.BindTo(dataSource)
				.NoDataTemplate("Ничего не найдено!")
				.Filter(filter)
				.Text(text)
				.Value(value);

			if (withOptionLabel)
				element.OptionLabel(new SelectListItem {Value = null, Text = "-"});

			RouteValueDictionary htmlAttributes = new RouteValueDictionary();
			{
				if (isReadonly) htmlAttributes.Add("readonly", "readonly");
			}
			element.HtmlAttributes(htmlAttributes);

			return element;
		}

		public static IHtmlContent RenderHelpButton<TModel>(this IHtmlHelper<TModel> html, bool bottomMargin=false)
		{
			var controller = html.ViewContext.RouteData.Values["controller"];
			var action = html.ViewContext.RouteData.Values["action"];
			if (html.ViewBag.UseMasterPage == true) { return HtmlString.Empty; }
			var str =
				String.Concat($@"<div class=""form-group""", bottomMargin ? "style=\"margin-bottom: 20px;\"" : String.Empty, $@">
						<div class=""col-sm-12"">
						<a style=""float: right"" href=""/Help/Help?CurrentLocation=/{controller}/{action}"" target=""_blank"">
							<span style=""font-size: 20px;"" class=""k-icon k-i-question""></span>
						</a>
					</div>
				</div>");
			return html.Raw(str);
		}
		public static string GetExpressionText<TModel, TResult>(
		   this IHtmlHelper<TModel> htmlHelper,
		   Expression<Func<TModel, TResult>> expression)
		{
			var expresionProvider = htmlHelper.ViewContext.HttpContext.RequestServices
			.GetService(typeof(ModelExpressionProvider)) as ModelExpressionProvider;
			return expresionProvider.GetExpressionText(expression);
		}

		public static Task RenderPartialFor<TModel, TProperty>(this IHtmlHelper<TModel> helper, string partialViewName, Expression<Func<TModel, TProperty>> expression, string idSuffix = null)
		{
			var name = GetExpressionText(helper, expression) + idSuffix;
			var modelExpressionProvider = (ModelExpressionProvider)helper.ViewContext.HttpContext.RequestServices.GetService(typeof(IModelExpressionProvider));
			var modelExplorer = modelExpressionProvider.CreateModelExpression(helper.ViewData, expression);
			object model = modelExplorer.Model;
			var viewData = new ViewDataDictionary(helper.ViewData);
			var htmlPrefix = viewData.TemplateInfo.HtmlFieldPrefix.Replace('[','_').Replace(']','_');
			viewData.TemplateInfo.HtmlFieldPrefix += !Equals(htmlPrefix, string.Empty) ? $".{name}" : name;

			return helper.RenderPartialAsync(partialViewName, model, viewData);
		}

		public static IHtmlContent KendoDropDownListEnumFor<TModel, TProperty>(this IHtmlHelper<TModel> helper,
			Expression<Func<TModel, TProperty>> expression)
		{
			return helper.Kendo().DropDownListFor(expression)
				.DataTextField("Text")
				.DataValueField("Value")
				.BindTo(helper.GetEnumSelectList(typeof(TProperty)));
		}
	}
}
