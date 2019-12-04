﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Core.Register.QuerySubsystem;
using Core.UI.Registers.Controllers;
using Core.UI.Registers.Services;
using KadOzenka.Web.Models.Prefilter;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ObjectModel.Market;
using Newtonsoft.Json;
using ObjectModel.Directory;
using Core.Shared.Extensions;
using KadOzenka.Web.Models.MarketObject;

namespace KadOzenka.Web.Controllers
{
    public class MapController : BaseController
    {
	    private readonly CoreUiService _coreUiService;

	    public string MarketObjectsRegisterViewId => "MarketObjects";

	    public MapController(CoreUiService coreUiService) => _coreUiService = coreUiService;

	    public ActionResult Index(long? objectId)
	    {
		    if (objectId.HasValue)
		    {
			    var marketObject = OMCoreObject
				    .Where(x => x.Id == objectId)
				    .SelectAll()
				    .ExecuteFirstOrDefault();

			    return View(MapObjectDto.OMMap(marketObject));
			}

		    return View(new MapObjectDto());
	    }

	    public JsonResult Objects(decimal? topLatitude, decimal? topLongitude, decimal? bottomLatitude,
		    decimal? bottomLongitude, int? mapZoom, int? minClusterZoom, int maxLoadedObjectsCount, string token,
		    long? objectId)
	    {
		    var query = OMCoreObject.Where(x =>
			    x.ProcessType_Code == ObjectModel.Directory.ProcessStep.InProcess &&
			    x.Market_Code != ObjectModel.Directory.MarketTypes.Rosreestr);
		    if (objectId.HasValue)
		    {
				PrepareQueryByObject(query, objectId.Value);
			}
		    else
		    {
			    PrepareQueryByUserFilter(query);
		    }

		    if (topLatitude.HasValue) query.And(x => x.Lat >= topLatitude.Value);
		    if (topLongitude.HasValue) query.And(x => x.Lng >= topLongitude.Value);
		    if (bottomLatitude.HasValue) query.And(x => x.Lat <= bottomLatitude.Value);
		    if (bottomLongitude.HasValue) query.And(x => x.Lng <= bottomLongitude.Value);
		    if (mapZoom < minClusterZoom) query.SetPackageSize(maxLoadedObjectsCount);
		    var point = new List<object>();
		    var analogItem = query.Select(x => new {x.Id, x.Lat, x.Lng, x.Category, x.Subcategory, x.PropertyType_Code})
			    .Execute().ToList();
		    analogItem.ForEach(x => point.Add(new
		    {
			    points = new[] {x.Lat, x.Lng}, type = FormType(x.Category, x.Subcategory, x.PropertyType_Code),
			    id = x.Id
		    }));

		    //  query.SetPackageSize(700).GroupBy(x => new { x.Lat.Round(2), x.Lng.Round(2) });//.ExecuteSelect(x => new{ x.Lat, x.Lng });
		    //  var analogItem = query.ExecuteSelect(x => new{ x.Lat.Avg(), x.Lng }).ToList();
		    //  analogItem.ForEach(x => point.Add(new { points = new[] { x.Lat, x.Lng } }));

		    return Json(new {token = token, arr = point});
	    }

	    public JsonResult RequiredInfo()
        {
            List<long> ids = JsonConvert.DeserializeObject<List<long>>(new StreamReader(HttpContext.Request.Body).ReadToEnd());
	        List<object> allData = new List<object>();
	        if (ids.Count > 0)
	        {
		        OMCoreObject.Where(x => ids.Contains(x.Id)).SelectAll().Execute().Take(ids.Count <= 20 ? ids.Count : 20).ToList().ForEach(x => {
			        allData.Add(new
			        {
                        type = FormType(x.Category, x.Subcategory, x.PropertyType_Code),
                        price = x.Price,
				        area = x.Area,
                        areaLand = x.AreaLand,
                        roomsCount = x.RoomsCount,
                        link = x.Url,
				        metro = x.Metro,
				        address = x.Address,
				        images = x.Images,
				        id = x.Id,
                        floor = x.FloorNumber,
                        floorCount = x.FloorsCount,
                        cadastralNumber = x.CadastralNumber,
                        parserTime = x.ParserTime?.ToString("dd.MM.yyyy"),
				        lastUpdateDate = x.LastDateUpdate?.ToString("dd.MM.yyyy")
			        });
		        });
			}

            return Json(allData);
        }

	    private void PrepareQueryByObject(QSQuery<OMCoreObject> query, long objectId)
	    {
		    var marketObject = OMCoreObject
			    .Where(x => x.Id == objectId)
			    .SelectAll()
			    .ExecuteFirstOrDefault();

		    if (marketObject == null)
		    {
			    throw new Exception($"Ошибка! Объекта аналога с идентификатором {objectId} не существует!");
		    }

			query.And(x =>
			    x.DealType_Code == marketObject.DealType_Code && x.PropertyType_Code == marketObject.PropertyType_Code);
	    }

		private void PrepareQueryByUserFilter(QSQuery<OMCoreObject> query)
		{
			var userFilter = _coreUiService.GetSearchFilter(MarketObjectsRegisterViewId);
			if (userFilter != null && !string.IsNullOrEmpty(userFilter.Condition))
			{
				var filters = JsonConvert.DeserializeObject<List<FilterModel>>(userFilter.Condition);

				if (filters.Any(f => f.Id == OMCoreObject.GetAttributeData(x => x.PropertyMarketSegment).Id))
				{
					var filter = filters.First(f =>
						f.Id == OMCoreObject.GetAttributeData(x => x.PropertyMarketSegment).Id);
					if (filter.ValueLongArrayCasted != null)
					{
						var list = filter.ValueLongArrayCasted.Select(y => ((MarketSegment)y).GetEnumDescription()).ToList();
						query.And(x => list.Contains(x.PropertyMarketSegment));
					}
				}

				if (filters.Any(f => f.Id == OMCoreObject.GetAttributeData(x => x.DealType).Id))
				{
					var filter = filters.First(f => f.Id == OMCoreObject.GetAttributeData(x => x.DealType).Id);
					if (filter.ValueLongArrayCasted != null)
					{
						var list = filter.ValueLongArrayCasted.Select(y => ((DealType)y).GetEnumDescription()).ToList();
						query.And(x => list.Contains(x.DealType));
					}
				}

				if (filters.Any(f => f.Id == OMCoreObject.GetAttributeData(x => x.PropertyType).Id))
				{
					var filter = filters.First(f => f.Id == OMCoreObject.GetAttributeData(x => x.PropertyType).Id);
					if (filter.ValueLongArrayCasted != null)
					{
						var list = filter.ValueLongArrayCasted.Select(y => ((PropertyTypes)y).GetEnumDescription()).ToList();
						query.And(x => list.Contains(x.PropertyType));
					}
				}

				if (filters.Any(f => f.Id == OMCoreObject.GetAttributeData(x => x.Price).Id))
				{
					var filter = filters.First(f => f.Id == OMCoreObject.GetAttributeData(x => x.Price).Id);
					if (filter.From.HasValue)
							query.And(x => x.Price >= filter.From.Value);
					if (filter.To.HasValue)
							query.And(x => x.Price <= filter.To.Value);
				}

				if (filters.Any(f => f.Id == OMCoreObject.GetAttributeData(x => x.Metro).Id))
				{
					var filter = filters.First(f => f.Id == OMCoreObject.GetAttributeData(x => x.Metro).Id);
					query.And(x => x.Metro.Contains(filter.ValueStringCasted));
				}
			}
		}

		private int FormType(string category, string subCategory, ObjectModel.Directory.PropertyTypes propertyType)
        {
            if (propertyType == ObjectModel.Directory.PropertyTypes.UncompletedBuilding) return 7;
            switch (category)
            {
                case "Коммерческая недвижимость":
                    switch (subCategory)
                    {
                        case "Складская": return 0;
                        case "Гараж": return 1;
                        case "Торговая": return 2;
                        case "Свободного назначения": return 3;
                        case "Офисная": return 4;
                        case "Готовый бизнес": return 5;
                        case "Производственная": return 6;
                        case "Здание": return 8;
                        default: return 9;
                    }
                case "Квартиры":
                    switch (subCategory)
                    {
                        case "Вторичное": return 10;
                        default: return 11;
                    }
                case "Комнаты":
                    switch (subCategory)
                    {
                        case "Вторичное": return 12;
                        default: return 13;
                    }
                case "Загородная недвижимость":
                    switch (subCategory)
                    {
                        case "Участок": return 14;
                        case "Таунхаус": return 15;
                        case "Дом": return 16;
                        default: return 17;
                    }
                default:
                    break;
            }
            return 18;
        }

    }
}