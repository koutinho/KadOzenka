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
using Core.Shared.Extensions;
using MarketPlaceBusiness;
using MarketPlaceBusiness.Common;
using MarketPlaceBusiness.Interfaces;
using AutoMapper;

namespace KadOzenka.Web.Controllers
{
	[Authorize]
	public class PrefilterController : BaseController
	{
		private readonly CoreUiService _service;
		private readonly RegistersService _registersService;
		private IMarketObjectService MarketObjectService { get; }
		private IMapper _mapper { get; }
		public string MarketObjectsRegisterViewId => "MarketObjects";


		public PrefilterController(CoreUiService service, RegistersService registersService,
			IMarketObjectService marketObjectService, IMapper mapper)
		{
			_service = service;
			_registersService = registersService;
			_mapper = mapper;
			MarketObjectService = marketObjectService;
		}

		[HttpGet]
		public IActionResult Prefilter()
		{
			return View();
		}

		[HttpGet]
		public ActionResult MarketSegmentList()
		{
			var marketSegmentReferenceId = Consts.PropertyMarketSegmentAttribute.ReferenceId;
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
			//DealType
			var dealTypeReferenceId = 110;
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
			var propertyTypeId = Consts.PropertyTypesCIPJSAttribute.ReferenceId;
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
			var c = new CoreUiController(_service, _registersService, _mapper);
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

                if (model.PropertyTypeItemIds?.Length > 0)
                {
                    var propertyType = Consts.PropertyTypesCIPJSAttribute;
                    var filterModel = new FilterModel
                    {
                        TypeControl = "value",
                        Type = "REFERENCE",
                        Text =
                            $"{propertyType.Name}: {string.Join(", ", model.PropertyTypeItemIds.Select(x => ((PropertyTypesCIPJS)x).GetEnumDescription()))}",
                        Value = model.PropertyTypeItemIds,
                        ReferenceId = propertyType.ReferenceId,
                        Id = propertyType.Id,
                    };
                    subFilters.Add(filterModel.ConvertToString());
                }

				if (model.MarketSegmentItemIds?.Length > 0)
				{
					var marketSegment = Consts.PropertyMarketSegmentAttribute;
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
				if (model.PriceTo.HasValue || model.PriceFrom.HasValue)
				{
					if (model.PriceFrom.HasValue && model.PriceTo.HasValue && model.PriceFrom > model.PriceTo)
					{
						throw new Exception("Задан некорректный диапазон цен");
					}

					var priceType = Consts.PriceAttribute;
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

				return "[" + string.Join(",", subFilters) + "]";
			}
		}

		private bool IsFilterEmpty(PrefilterDto model)
		{
			return model == null || (model.MarketSegmentItemIds?.Length == 0
			                         && model.DealTypeItemIds?.Length == 0
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
