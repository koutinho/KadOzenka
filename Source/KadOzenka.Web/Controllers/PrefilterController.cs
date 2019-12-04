using System;
using System.Collections.Generic;
using System.Linq;
using Core.Register.RegisterEntities;
using Core.UI.Registers.Controllers;
using Core.UI.Registers.Services;
using KadOzenka.Web.Models.Prefilter;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ObjectModel.Core.Shared;
using ObjectModel.Directory;
using ObjectModel.Market;
using Core.Shared.Extensions;

namespace KadOzenka.Web.Controllers
{
	[Authorize]
	public class PrefilterController : BaseController
	{
		private readonly CoreUiService _service;

		public string MarketObjectsRegisterViewId => "MarketObjects";

		public PrefilterController(CoreUiService service)
		{
			_service = service;
		}

		[HttpGet]
		public IActionResult Prefilter()
		{
			return View();
		}

		[HttpGet]
		public ActionResult MarketSegmentList()
		{
			var marketSegmentReferenceId = OMCoreObject
				.GetAttributeData(x => x.PropertyMarketSegment)
				.ReferenceId;
			var marketSegmentList = OMReferenceItem
				.Where(x => x.ReferenceId == marketSegmentReferenceId)
				.OrderBy(x => x.Value)
				.SelectAll()
				.Execute()
				.Select( x => new { Id = x.ItemId, Name = x.Value, Code = x.Code });

			return Content(JsonConvert.SerializeObject(marketSegmentList), "application/json");
		}

		[HttpGet]
		public ActionResult DealTypeList()
		{
			var dealTypeReferenceId = OMCoreObject
				.GetAttributeData(x => x.DealType)
				.ReferenceId;
			var dealTypeList = OMReferenceItem
				.Where(x => x.ReferenceId == dealTypeReferenceId)
				.OrderBy(x => x.Value)
				.SelectAll()
				.Execute()
				.Select(x => new { Id = x.ItemId, Name = x.Value, Code = x.Code });

			return Content(JsonConvert.SerializeObject(dealTypeList), "application/json");
		}

		[HttpGet]
		public ActionResult PropertyTypeList()
		{
			var propertyTypeId = OMCoreObject
				.GetAttributeData(x => x.PropertyType)
				.ReferenceId;
			var propertyTypeList = OMReferenceItem
				.Where(x => x.ReferenceId == propertyTypeId)
				.OrderBy(x => x.Value)
				.SelectAll()
				.Execute()
				.Select(x => new { Id = x.ItemId, Name = x.Value, Code = x.Code });

			return Content(JsonConvert.SerializeObject(propertyTypeList), "application/json");
		}

		[HttpPost]
		public ActionResult ConfigureSearchFilter(PrefilterDto model)
		{
			var filter = CreateConditionFilter(model);
			var c = new CoreUiController(_service);
			c.SaveSearchFilter(MarketObjectsRegisterViewId, filter);

			return NoContent();
		}

		private string CreateConditionFilter(PrefilterDto model)
		{
			if (IsFilterEmpty(model))
			{
				return null;
			}
			else
			{
				var subFilters = new List<string>();

				if (model.MarketSegmentItemIds?.Length > 0)
				{
					var marketSegment = OMCoreObject
						.GetAttributeData(x => x.PropertyMarketSegment);
					var filterModel = new FilterModel
					{
						TypeControl = "value",
						Type = "REFERENCE",
						Text =
							$"{marketSegment.Name}: {string.Join(", ", model.MarketSegmentItemIds.Select(x => ((MarketSegment)x).GetEnumDescription()))}",
						Value = model.MarketSegmentItemIds,
						ReferenceId = marketSegment.ReferenceId,
						Id = marketSegment.Id,
					};
					subFilters.Add(filterModel.ConvertToString());
				}
				if (model.DealTypeItemIds?.Length > 0)
				{
					var dealType = OMCoreObject
						.GetAttributeData(x => x.DealType);
					var filterModel = new FilterModel
					{
						TypeControl = "value",
						Type = "REFERENCE",
						Text =
							$"{dealType.Name}: {string.Join(", ", model.DealTypeItemIds.Select(x => ((DealType) x).GetEnumDescription()))}",
						Value = model.DealTypeItemIds,
						ReferenceId = dealType.ReferenceId,
						Id = dealType.Id,
					};
					subFilters.Add(filterModel.ConvertToString());
				}
				if (model.PropertyTypeItemIds?.Length > 0)
				{
					var propertyType = OMCoreObject
						.GetAttributeData(x => x.PropertyType);
					var filterModel = new FilterModel
					{
						TypeControl = "value",
						Type = "REFERENCE",
						Text =
							$"{propertyType.Name}: {string.Join(", ", model.PropertyTypeItemIds.Select(x => ((PropertyTypes)x).GetEnumDescription()))}",
						Value = model.PropertyTypeItemIds,
						ReferenceId = propertyType.ReferenceId,
						Id = propertyType.Id,
					};
					subFilters.Add(filterModel.ConvertToString());
				}
				if (model.PriceTo.HasValue || model.PriceFrom.HasValue)
				{
					if (model.PriceFrom.HasValue && model.PriceTo.HasValue && model.PriceFrom > model.PriceTo)
					{
						throw new Exception("Задан некорректный диапазон цен");
					}

					var priceType = OMCoreObject
						.GetAttributeData(x => x.Price);
					var filterModel = new FilterModel
					{
						TypeControl = "range",
						Type = "INTEGER",
						Text = GetTextForPriceFilter(model, priceType),
						From = model.PriceFrom,
						To = model.PriceTo,
						Id = priceType.Id,
					};
					subFilters.Add(filterModel.ConvertToString());
				}
				if (!string.IsNullOrEmpty(model.Metro))
				{
					var metroType = OMCoreObject
						.GetAttributeData(x => x.Metro);

					var filterModel = new FilterModel
					{
						TypeControl = "value",
						Type = "STRING",
						Text = $"{metroType.Name}: Содержит {model.Metro} ",
						Value = model.Metro,
						Id = metroType.Id,
					};
					subFilters.Add(filterModel.ConvertToString());
				}

				return "[" + string.Join(",", subFilters) + "]";
			}
		}

		private bool IsFilterEmpty(PrefilterDto model)
		{
			return model == null || (model.MarketSegmentItemIds?.Length == 0
			                         && model.DealTypeItemIds?.Length == 0
			                         && model.PropertyTypeItemIds?.Length == 0
			                         && !model.PriceTo.HasValue
			                         && !model.PriceFrom.HasValue
			                         && string.IsNullOrEmpty(model.Metro));
		}

		private string GetTextForPriceFilter(PrefilterDto model, RegisterAttribute priceType)
		{
			string text;
			if (model.PriceFrom.HasValue && model.PriceTo.HasValue)
			{
				text = $"{priceType.Name}: с {model.PriceFrom.ToString()} до {model.PriceTo.ToString()}";
			}
			else if (model.PriceFrom.HasValue)
			{
				text = $"{priceType.Name}: Больше или равно {model.PriceFrom.ToString()}";
			}
			else
			{
				text = $"{priceType.Name}: Меньше или равно {model.PriceTo.ToString()}";
			}

			return text;
		}
	}
}
