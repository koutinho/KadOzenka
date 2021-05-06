﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Core.Register.QuerySubsystem;
using Core.UI.Registers.Controllers;
using Core.UI.Registers.Services;
using KadOzenka.Web.Models.Prefilter;
using Microsoft.AspNetCore.Mvc;
using ObjectModel.Market;
using Newtonsoft.Json;
using ObjectModel.Directory;
using Core.Shared.Extensions;
using KadOzenka.Web.Models.MarketObject;
using ObjectModel.Core.Shared;
using KadOzenka.Dal.MapModeling;
using System.Globalization;
using KadOzenka.Web.Attributes;
using MarketPlaceBusiness;
using MarketPlaceBusiness.Interfaces;
using ObjectModel.SRD;

namespace KadOzenka.Web.Controllers
{
	public class MapController : BaseController
    {
        private readonly CoreUiService _coreUiService;
        private readonly RegistersService _registersService;

        private IMarketObjectsForMapService MarketObjectService { get; }
        public string MarketObjectsRegisterViewId => "MarketObjects";


        public MapController(CoreUiService coreUiService, RegistersService registersService,
	        IMarketObjectsForMapService marketObjectService)
        {
	        _coreUiService = coreUiService;
	        _registersService = registersService;
	        MarketObjectService = marketObjectService;
        }


        [SRDFunction(Tag = SRDCoreFunctions.MARKET_MAP)]
        public ActionResult Index(long? objectId)
        {
	        if (objectId.HasValue)
            {
                var marketObject = MarketObjectService.GetById(objectId.Value);
                return View(MapObjectDto.OMMap(marketObject));
            }
            return View(new MapObjectDto());
        }

        [SRDFunction(Tag = SRDCoreFunctions.MARKET_MAP)]
        public JsonResult Objects(decimal? topLatitude, decimal? topLongitude, decimal? bottomLatitude, decimal? bottomLongitude,
            int? mapZoom, int? minClusterZoom, int maxLoadedObjectsCount, int maxObjectsCount, string token, long? objectId, 
            string districts, string marketSource, string actualDate)
        {
            DateTime acD;
            var query = MarketObjectService.GetBaseQuery();
            if (objectId.HasValue) PrepareQueryByObject(query, objectId.Value);
            else PrepareQueryByUserFilter(query);
            if (!districts.IsEmpty()) query.And(x => x.District == districts);
            if (!marketSource.IsEmpty()) query.And(x => x.Market == marketSource);
            if (topLatitude.HasValue) query.And(x => x.Lat >= topLatitude.Value);
            if (topLongitude.HasValue) query.And(x => x.Lng >= topLongitude.Value);
            if (bottomLatitude.HasValue) query.And(x => x.Lat <= bottomLatitude.Value);
            if (bottomLongitude.HasValue) query.And(x => x.Lng <= bottomLongitude.Value);
            if (DateTime.TryParseExact(actualDate, "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out acD)) query.And(x => x.ParserTime <= acD);
            int size = query.ExecuteCount();
            if (mapZoom < minClusterZoom && size > maxObjectsCount) query.SetPackageSize(maxLoadedObjectsCount).OrderBy(x => x.Id);
            var point = new List<object>();
            var analogItem = query.Select(x => new { x.Id, x.Lat, x.Lng, x.PropertyMarketSegment, x.DealType, x.PropertyTypesCIPJS }).Execute().ToList();
            analogItem.ForEach(x => point.Add(new {
                points = new[] { x.Lat, x.Lng }, id = x.Id, segment = FormSegment(x.PropertyMarketSegment), dealType = FormDealType(x.DealType), propertyType = x.PropertyTypesCIPJS
            }));
            return Json(new { token = token, arr = point, allCount = size });
        }

        [SRDFunction(Tag = SRDCoreFunctions.MARKET_MAP)]
        public JsonResult HeatMapData(string colors, string actualDate)
        {
	        DateTime acD;
            string[] colorsArray = colors.Split(",");
            var query = MarketObjectService.GetBaseQuery();

            List<OMReferenceItem> allDistricts = OMReferenceItem.Where(x => x.ReferenceId == MarketObjectService.GetAttributeData(y => y.District).ReferenceId).Select(x => x.Value).Execute().ToList();
            List<OMReferenceItem> allRegions = OMReferenceItem.Where(x => x.ReferenceId == MarketObjectService.GetAttributeData(y => y.Neighborhood).ReferenceId).Select(x => x.Value).Execute().ToList();
            List<OMReferenceItem> allZones = OMReferenceItem.Where(x => x.ReferenceId == MarketObjectService.GetAttributeData(y => y.ZoneRegion).ReferenceId).Select(x => x.Value).Execute().ToList();
            List<string> allQuartals = OMQuartalDictionary.Where(x => true).Select(x => x.CadastralQuartal).Execute().Select(x => x.CadastralQuartal).ToList();

            PrepareQueryByUserFilter(query);

            if (DateTime.TryParseExact(actualDate, "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out acD)) query.And(x => x.ParserTime <= acD);

            var DistrictsData = query.Select(x => new { x.PricePerMeter, x.District, x.District_Code, x.Neighborhood, x.Neighborhood_Code, x.ZoneRegion, x.CadastralQuartal }).Execute().ToList();

            List<IGrouping<string, OMCoreObject>> districtList = DistrictsData.GroupBy(x => x.District).ToList();
            List<IGrouping<string, OMCoreObject>> regionList = DistrictsData.GroupBy(x => x.Neighborhood).ToList();
            List<IGrouping<string, OMCoreObject>> zoneList = DistrictsData.GroupBy(x => x.ZoneRegion).ToList();
            List<IGrouping<string, OMCoreObject>> quartalList = DistrictsData.GroupBy(x => x.CadastralQuartal).ToList();

            (List<(string name, string color, string counter)> ColoredData, List<(string min, string max)> MinMaxData) districtsData = 
                new MarketHeatMap().SetColors(new MarketHeatMap().GroupList(allDistricts, districtList), colorsArray);
            (List<(string name, string color, string counter)> ColoredData, List<(string min, string max)> MinMaxData) regionsData =
                new MarketHeatMap().SetColors(new MarketHeatMap().GroupList(allRegions, regionList), colorsArray);
            (List<(string name, string color, string counter)> ColoredData, List<(string min, string max)> MinMaxData) zonesData =
                new MarketHeatMap().SetColors(new MarketHeatMap().GroupList(allZones, zoneList), colorsArray);
            (List<(string name, string color, string counter)> ColoredData, List<(string min, string max)> MinMaxData) quartalsData =
	            new MarketHeatMap().SetColors(new MarketHeatMap().GroupList(allQuartals, quartalList), colorsArray);

            new MarketHeatMap().GenerateHeatMapQuartalInitialImages(quartalsData.ColoredData.Where(x => !x.name.IsEmpty())
	            ?.ToDictionary(x => x.name, x => x.color));

            return Json(new
            {
                districts = districtsData.ColoredData.Select(x => new { name = x.name, color = x.color, counter = x.counter }),
                districtsSteps = districtsData.MinMaxData.Select(x => new { min = x.min, max = x.max }),
                regions = regionsData.ColoredData.Select(x => new { name = x.name, color = x.color, counter = x.counter }),
                regionsSteps = regionsData.MinMaxData.Select(x => new { min = x.min, max = x.max }),
                zones = zonesData.ColoredData.Select(x => new { name = x.name, color = x.color, counter = x.counter }),
                zonesSteps = zonesData.MinMaxData.Select(x => new { min = x.min, max = x.max }),
                quartalsSteps = quartalsData.MinMaxData.Select(x => new { min = x.min, max = x.max })
            });
        }

        [SRDFunction(Tag = SRDCoreFunctions.MARKET_MAP)]
        public JsonResult RequiredInfo()
        {
            List<long> ids = JsonConvert.DeserializeObject<List<long>>(new StreamReader(HttpContext.Request.Body).ReadToEnd());
            List<object> allData = new List<object>();
            if (ids.Count > 0)
            {
	            MarketObjectService.GetByIds(ids).Take(ids.Count <= 20 ? ids.Count : 20).ToList().ForEach(x => {
                    allData.Add(new
                    {
                        segment = FormSegment(x.PropertyMarketSegment),
                        dealType = x.DealType,
                        propertyType = x.PropertyTypesCIPJS,
                        propertyTypeCode = x.PropertyTypesCIPJS_Code,
                        marketSegment = x.PropertyMarketSegment,
                        marketSegmentCode = x.PropertyMarketSegment_Code,
                        status = x.ProcessType,
                        statusCode = x.ProcessType_Code,
                        source = x.Market,
                        price = x.Price,
                        area = x.Area,
                        areaLand = x.AreaLand,
                        roomsCount = x.RoomsCount,
                        link = x.Url,
                        metro = x.Metro,
                        address = x.Address,
                        images = x.Images,
                        id = x.Id,
                        floorNumber = x.FloorNumber,
                        floorCount = x.FloorsCount,
                        cadastralNumber = x.CadastralNumber,
                        parserTime = x.ParserTime?.ToString("dd.MM.yyyy"),
                        lastUpdateDate = x.LastDateUpdate?.ToString("dd.MM.yyyy"),
                        lng = x.Lng,
                        lat = x.Lat,
                        entranceType = x.EntranceType,
                        qualityClassCode = x.QualityClass_Code,
                        qualityClass = x.QualityClass,
                        renovation = x.Renovation,
                        buildingLine = x.BuildingLine
                    });
                });
            }
            return Json(allData);
        }

        [SRDFunction(Tag = SRDCoreFunctions.MARKET_MAP)]
        public JsonResult FindFilters()
        {
            var userFilter = _coreUiService.GetSearchFilter(MarketObjectsRegisterViewId);
            var filters = (userFilter != null && userFilter.Condition != null) ? JsonConvert.DeserializeObject<List<FilterModel>>(userFilter.Condition) : null;
            string typeControl = "value", type = "REFERENCE";
            long[] propertyTypes = null, dealTypes = null, marketSegments = null;
            if (filters != null)
            {
                propertyTypes = filters.Where(x => x.ReferenceId == MarketObjectService.GetAttributeData(y => y.PropertyTypesCIPJS).ReferenceId).ToList().Select(x => x.ValueLongArrayCasted).FirstOrDefault();
                dealTypes = filters.Where(x => x.ReferenceId == MarketObjectService.GetAttributeData(y => y.DealType).ReferenceId).ToList().Select(x => x.ValueLongArrayCasted).FirstOrDefault();
                marketSegments = filters.Where(x => x.ReferenceId == MarketObjectService.GetAttributeData(y => y.PropertyMarketSegment).ReferenceId).ToList().Select(x => x.ValueLongArrayCasted).FirstOrDefault();
            }
            var propertyTypeList =
                OMReferenceItem.Where(x => x.ReferenceId == MarketObjectService.GetAttributeData(y => y.PropertyTypesCIPJS).ReferenceId)
                    .OrderBy(x => x.Value).SelectAll().Execute()
                    .OrderBy(x => int.Parse(x.Code)).Select(x => new {
                        Id = x.ItemId,
                        Code = x.Code,
                        Value = x.Value,
                        Name = x.Name,
                        Selected = propertyTypes == null ? false : propertyTypes.Contains(x.ItemId) ? true : false
                    });
            var commertialMarketSegmentList =
                OMReferenceItem.Where(x => x.ReferenceId == MarketObjectService.GetAttributeData(y => y.PropertyMarketSegment).ReferenceId)
                    .OrderBy(x => x.Value).SelectAll().Execute().Where(x => int.Parse(x.Code) < 100)
                    .OrderBy(x => int.Parse(x.Code)).Select(x => new {
                        Id = x.ItemId, 
                        Code = x.Code, 
                        Value = x.Value, 
                        Name = x.Name,
                        Selected = marketSegments == null ? false : marketSegments.Contains(x.ItemId) ? true : false
                    });
            var propertyMarketSegmentList =
                OMReferenceItem.Where(x => x.ReferenceId == MarketObjectService.GetAttributeData(y => y.PropertyMarketSegment).ReferenceId)
                    .OrderBy(x => x.Value).SelectAll().Execute().Where(x => int.Parse(x.Code) >= 100 && int.Parse(x.Code) < 200)
                    .OrderBy(x => int.Parse(x.Code)).Select(x => new {
                        Id = x.ItemId, 
                        Code = x.Code, 
                        Value = x.Value, 
                        Name = x.Name,
                        Selected = marketSegments == null ? false : marketSegments.Contains(x.ItemId) ? true : false
                    });
            var dealTypeList = OMReferenceItem.Where(x => x.ReferenceId == MarketObjectService.GetAttributeData(y => y.DealType).ReferenceId)
                    .OrderBy(x => x.Value).SelectAll().Execute()
                    .OrderBy(x => x.ItemId).Select(x => new {
                        Id = x.ItemId, 
                        Code = x.Code, 
                        Value = x.Value, 
                        Name = x.Name,
                        Selected = dealTypes == null ? false : dealTypes.Contains(x.ItemId) ? true : false
                    });
            var districtTypeList = OMReferenceItem.Where(x => x.ReferenceId == MarketObjectService.GetAttributeData(y => y.District).ReferenceId)
                    .OrderBy(x => x.Value).SelectAll().Execute()
                    .OrderBy(x => x.ItemId).Select(x => new { 
                        Id = x.ItemId,
                        Code = x.Code,
                        Value = x.Value,
                        Name = x.Name,
                        Selected = false
                    });
            var sourceTypeList = OMReferenceItem.Where(x => x.ReferenceId == MarketObjectService.GetAttributeData(y => y.Market).ReferenceId).And(x => x.ItemId <= (int) MarketTypes.Rosreestr)
                    .OrderBy(x => x.Value).SelectAll().Execute()
                    .OrderBy(x => x.ItemId).Select(x => new {
                        Id = x.ItemId,
                        Code = x.Code,
                        Value = x.Value,
                        Name = x.Name,
                        Selected = false
                    });
            return Json(new
            {
                propertyTypeFilter = new
                {
                    typeControl = typeControl,
                    type = type,
                    text = MarketObjectService.GetAttributeData(y => y.PropertyTypesCIPJS).Name,
                    propertyTypeList = propertyTypeList,
                    referenceId = MarketObjectService.GetAttributeData(y => y.PropertyTypesCIPJS).ReferenceId,
                    id = MarketObjectService.GetAttributeData(y => y.PropertyTypesCIPJS).Id
                },
                commertialMarketFilter = new
                {
                    typeControl = typeControl,
                    type = type,
                    text = MarketObjectService.GetAttributeData(y => y.PropertyMarketSegment).Name,
                    commertialMarketSegmentList = commertialMarketSegmentList,
                    referenceId = MarketObjectService.GetAttributeData(y => y.PropertyMarketSegment).ReferenceId,
                    id = MarketObjectService.GetAttributeData(y => y.PropertyMarketSegment).Id
                },
                propertyMarketFilter = new
                {
                    typeControl = typeControl,
                    type = type,
                    text = MarketObjectService.GetAttributeData(y => y.PropertyMarketSegment).Name,
                    propertyMarketSegmentList = propertyMarketSegmentList,
                    referenceId = MarketObjectService.GetAttributeData(y => y.PropertyMarketSegment).ReferenceId,
                    id = MarketObjectService.GetAttributeData(y => y.PropertyMarketSegment).Id,
                },
                dealTypeFilter = new
                {
                    typeControl = typeControl,
                    type = type,
                    text = MarketObjectService.GetAttributeData(y => y.DealType).Name,
                    dealTypeList = dealTypeList,
                    referenceId = MarketObjectService.GetAttributeData(y => y.DealType).ReferenceId,
                    id = MarketObjectService.GetAttributeData(y => y.DealType).Id
                },
                districtTypeFilter = new
                {
                    typeControl = typeControl,
                    type = type,
                    text = MarketObjectService.GetAttributeData(y => y.District).Name,
                    districtTypeList = districtTypeList,
                    referenceId = MarketObjectService.GetAttributeData(y => y.District).ReferenceId,
                    id = MarketObjectService.GetAttributeData(y => y.District).Id
                },
                sourceTypeFilter = new
                {
                    typeControl = typeControl,
                    type = type,
                    text = MarketObjectService.GetAttributeData(y => y.Market).Name,
                    sourceTypeList = sourceTypeList,
                    referenceId = MarketObjectService.GetAttributeData(y => y.Market).ReferenceId,
                    id = MarketObjectService.GetAttributeData(y => y.Market).Id
                }
            });
        }

        [SRDFunction(Tag = SRDCoreFunctions.MARKET_MAP)]
        public JsonResult SetFilters(string filter)
        {
            new CoreUiController(_coreUiService, _registersService).SaveSearchFilter(MarketObjectsRegisterViewId, filter);
            return Json(new { });
        }

        [SRDFunction(Tag = SRDCoreFunctions.MARKET_MAP)]
        public JsonResult GetAvaliableValues()
        {
            List<OMReferenceItem> CIPJSType = OMReferenceItem.Where(x => x.ReferenceId == MarketObjectService.GetAttributeData(y => y.PropertyTypesCIPJS).ReferenceId).SelectAll().Execute();
            List<OMReferenceItem> MarketSegment = OMReferenceItem.Where(x => x.ReferenceId == MarketObjectService.GetAttributeData(y => y.PropertyMarketSegment).ReferenceId).SelectAll().Execute();
            List<OMReferenceItem> status = OMReferenceItem.Where(x => x.ReferenceId == MarketObjectService.GetAttributeData(y => y.ProcessType_Code).ReferenceId).SelectAll().Execute();
            List<OMReferenceItem> qualityClass = OMReferenceItem.Where(x => x.ReferenceId == MarketObjectService.GetAttributeData(y => y.QualityClass_Code).ReferenceId).SelectAll().Execute();
            qualityClass.Add(new OMReferenceItem{ItemId = 0, Value = null});

            return Json(new 
            { 
                CIPJSType = CIPJSType.Select(x => new { code = x.ItemId, value = x.Value }), 
                MarketSegment = MarketSegment.Select(x => new { code = x.ItemId, value = x.Value }),
                Status = status.Select(x => new { code = x.ItemId, value = x.Value }),
                QualityClass = qualityClass.Select(x => new { code = x.ItemId, value = x.Value })
            });
        }

        [SRDFunction(Tag = SRDCoreFunctions.MARKET_MAP)]
        public JsonResult ChangeObject(MarketSaveObjectDto dto)
        {
            var obj = MarketObjectService.GetById(dto.Id.GetValueOrDefault());
            MarketSaveObjectDto.ToEntity(dto, ref obj);
            obj.Save();

            object result = new
            {
	            segment = FormSegment(obj.PropertyMarketSegment),
	            propertyType = obj.PropertyTypesCIPJS,
	            propertyTypeCode = obj.PropertyTypesCIPJS_Code,
	            marketSegment = obj.PropertyMarketSegment,
	            marketSegmentCode = obj.PropertyMarketSegment_Code,
	            status = obj.ProcessType,
	            statusCode = obj.ProcessType_Code,
	            id = obj.Id,
	            lng = obj.Lng,
	            lat = obj.Lat,
	            entranceType = obj.EntranceType,
	            qualityClassCode = obj.QualityClass_Code,
	            qualityClass = obj.QualityClass,
	            renovation = obj.Renovation,
	            buildingLine = obj.BuildingLine,
	            floorNumber = obj.FloorNumber,
	            floorCount = obj.FloorsCount
            };

            return Json(result);
        }

        [SRDFunction(Tag = SRDCoreFunctions.MARKET_MAP)]
        public ActionResult CadastralHeatMapTiles(int x, int y, int z)
        {
	        var file = new MarketHeatMap().GetHeatMapTile(x, y, z);
	        if (file == null)
		        return EmptyResponse();

	        return File(file, "image/png");
        }

        [SRDFunction(Tag = SRDCoreFunctions.MARKET_MAP)]
        public ActionResult CadastralTransparentTiles(int x, int y, int z)
        {
	        try { return base.File($@"~/MapImageLayer/QuartalLayer/{z}/{x}_{y}.png", "image/png"); }
	        catch (Exception) { return EmptyResponse(); }
        }

        private void PrepareQueryByObject(QSQuery<OMCoreObject> query, long objectId)
	    {
		    var marketObject = MarketObjectService.GetById(objectId);
            if (marketObject == null)throw new Exception($"Ошибка! Объекта аналога с идентификатором {objectId} не существует!");
            query.And(x => x.DealType_Code == marketObject.DealType_Code && x.PropertyMarketSegment_Code == marketObject.PropertyMarketSegment_Code);
	    }

		private void PrepareQueryByUserFilter(QSQuery<OMCoreObject> query)
		{
			var userFilter = _coreUiService.GetSearchFilter(MarketObjectsRegisterViewId);
			if (userFilter != null && !string.IsNullOrEmpty(userFilter.Condition))
			{
				var filters = JsonConvert.DeserializeObject<List<FilterModel>>(userFilter.Condition);
                if (filters.Any(f => f.Id == MarketObjectService.GetAttributeData(x => x.PropertyTypesCIPJS).Id))
                {
                    var filter = filters.First(f => f.Id == MarketObjectService.GetAttributeData(x => x.PropertyTypesCIPJS).Id);
                    if (filter.ValueLongArrayCasted != null)
                    {
                        var list = filter.ValueLongArrayCasted.Select(y => ((PropertyTypesCIPJS)y).GetEnumDescription()).ToList();
                        query.And(x => list.Contains(x.PropertyTypesCIPJS));
                    }
                }
                if (filters.Any(f => f.Id == MarketObjectService.GetAttributeData(x => x.PropertyMarketSegment).Id))
				{
					var filter = filters.First(f => f.Id == MarketObjectService.GetAttributeData(x => x.PropertyMarketSegment).Id);
					if (filter.ValueLongArrayCasted != null)
					{
						var list = filter.ValueLongArrayCasted.Select(y => ((MarketSegment)y).GetEnumDescription()).ToList();
						query.And(x => list.Contains(x.PropertyMarketSegment));
					}
				}
				if (filters.Any(f => f.Id == MarketObjectService.GetAttributeData(x => x.DealType).Id))
				{
					var filter = filters.First(f => f.Id == MarketObjectService.GetAttributeData(x => x.DealType).Id);
					if (filter.ValueLongArrayCasted != null)
					{
						var list = filter.ValueLongArrayCasted.Select(y => ((DealType)y).GetEnumDescription()).ToList();
						query.And(x => list.Contains(x.DealType));
					}
				}
				if (filters.Any(f => f.Id == MarketObjectService.GetAttributeData(x => x.Price).Id))
				{
					var filter = filters.First(f => f.Id == MarketObjectService.GetAttributeData(x => x.Price).Id);
					if (filter.From.HasValue) query.And(x => x.Price >= filter.From.Value);
					if (filter.To.HasValue) query.And(x => x.Price <= filter.To.Value);
				}
				if (filters.Any(f => f.Id == MarketObjectService.GetAttributeData(x => x.Metro).Id))
				{
					var filter = filters.First(f => f.Id == MarketObjectService.GetAttributeData(x => x.Metro).Id);
					query.And(x => x.Metro.Contains(filter.ValueStringCasted));
				}
			}
		}

        private int FormSegment(string marketSegment)
        {
            switch (marketSegment)
            {
                case "ИЖС": return 0;
                case "МЖС": return 1;
                case "Апартаменты": return 2;
                case "Санатории": return 3;
                case "Садоводческое, огородническое и дачное использование": return 4;
                case "Машиноместа": return 5;
                case "Гаражи": return 6;
                case "Гостиницы": return 7;
                case "Производство и склады": return 8;
                case "Офисы": return 9;
                case "Торговля": return 10;
                case "Общепит": return 11;
                case "Без сегмента": return 12;
            }
            return 13;
        }

        private int FormDealType(string dealType)
        {
            switch (dealType)
            {
                case "Предложение-продажа": return 0;
                case "Сделка купли-продажи": return 1;
                case "Предложение-аренда": return 2;
                case "Сделка-аренда": return 3;
            }
            return 0;
        }

    }
}