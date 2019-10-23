using System.Collections.Generic;
using System.Linq;
using Core.UI.Registers.Controllers;
using Core.UI.Registers.Services;
using KadOzenka.Web.Models.Prefilter;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ObjectModel.Core.Shared;
using ObjectModel.Market;

namespace KadOzenka.Web.Controllers
{
	[Authorize]
	public class PrefilterController : BaseController
	{
		private readonly CoreUiService _service;

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
			c.SaveSearchFilter("MarketObjects", filter);

			return NoContent();
		}

		private string CreateConditionFilter(PrefilterDto model)
		{
			if(model == null || (!model.PropertyMarketSegmentItemId.HasValue && !model.DealTypeItemId.HasValue && !model.PropertyTypeItemId.HasValue
			                     && !model.PriceTo.HasValue && !model.PriceFrom.HasValue && string.IsNullOrEmpty(model.Metro)))
			{
				return null;
			}
			else
			{
				var subFilters = new List<string>();

				if (model.PropertyMarketSegmentItemId.HasValue)
				{
					var marketSegment = OMCoreObject
						.GetAttributeData(x => x.PropertyMarketSegment);
					subFilters.Add("{" +
					               $"\"typeControl\":\"value\"," +
					               $"\"type\":\"REFERENCE\"," +
					               $"\"text\":\"{marketSegment.Name}: {model.PropertyMarketSegmentValue}\"," +
					               $"\"value\":[{model.PropertyMarketSegmentItemId.Value}]," +
					               $"\"referenceId\":{marketSegment.ReferenceId}," +
					               $"\"id\":{marketSegment.Id}" +
					               "}");
				}
				if (model.DealTypeItemId.HasValue)
				{
					var dealType = OMCoreObject
						.GetAttributeData(x => x.DealType);
					subFilters.Add("{" +
					               $"\"typeControl\":\"value\"," +
					               $"\"type\":\"REFERENCE\"," +
					               $"\"text\":\"{dealType.Name}: {model.DealTypeValue}\"," +
					               $"\"value\":[{model.DealTypeItemId.Value}]," +
					               $"\"referenceId\":{dealType.ReferenceId}," +
					               $"\"id\":{dealType.Id}" +
					               "}");
				}
				if (model.PropertyTypeItemId.HasValue)
				{
					var propertyType = OMCoreObject
						.GetAttributeData(x => x.PropertyType);
					subFilters.Add("{" +
					               $"\"typeControl\":\"value\"," +
					               $"\"type\":\"REFERENCE\"," +
					               $"\"text\":\"{propertyType.Name}: {model.PropertyTypeValue}\"," +
					               $"\"value\":[{model.PropertyTypeItemId.Value}]," +
					               $"\"referenceId\":{propertyType.ReferenceId}," +
					               $"\"id\":{propertyType.Id}" +
					               "}");
				}
				if (model.PriceTo.HasValue || model.PriceFrom.HasValue)
				{
					var priceType = OMCoreObject
						.GetAttributeData(x => x.Price);

					string text = null;
					if (model.PriceFrom.HasValue && model.PriceTo.HasValue)
					{
						text = $"{priceType.Name}: с {model.PriceFrom.ToString()} до {model.PriceTo.ToString()}";
					}else if (model.PriceFrom.HasValue)
					{
						text = $"{priceType.Name}: Больше или равно {model.PriceFrom.ToString()}";
					}
					else
					{
						text = $"{priceType.Name}: Меньше или равно {model.PriceTo.ToString()}";
					}

					subFilters.Add("{" +
					               $"\"typeControl\":\"range\"," +
					               $"\"type\":\"INTEGER\"," +
					               $"\"text\":\"{text}\"," +
					               $"\"from\":\"{(model.PriceFrom.HasValue ? model.PriceFrom.Value.ToString() : string.Empty)}\"," +
					               $"\"to\":\"{(model.PriceTo.HasValue ? model.PriceTo.Value.ToString() : string.Empty)}\"," +
					               $"\"id\":{priceType.Id}" +
					               "}");
				}

				if (!string.IsNullOrEmpty(model.Metro))
				{
					var metroType = OMCoreObject
						.GetAttributeData(x => x.Metro);
					subFilters.Add("{" +
					               $"\"typeControl\":\"value\"," +
					               $"\"type\":\"STRING\"," +
					               $"\"text\":\"{metroType.Name}: Содержит {model.Metro} \"," +
					               $"\"value\":\"{model.Metro}\"," +
					               $"\"id\":{metroType.Id}" +
					               "}");
				}

				return "[" + string.Join(",", subFilters) + "]";
			}
		}
	}
}
