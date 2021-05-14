using System;
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
using AutoMapper;
using KadOzenka.Web.Attributes;
using MarketPlaceBusiness;
using MarketPlaceBusiness.Common;
using MarketPlaceBusiness.Dto;
using MarketPlaceBusiness.Interfaces;
using ObjectModel.SRD;

namespace KadOzenka.Web.Controllers
{
	public class MapController : BaseController
    {
        private readonly CoreUiService _coreUiService;
        private readonly RegistersService _registersService;

        private IMarketObjectsForMapService MarketObjectService { get; }
        private IMapper Mapper { get; }
        public string MarketObjectsRegisterViewId => "MarketObjects";


        public MapController(CoreUiService coreUiService, RegistersService registersService,
	        IMarketObjectsForMapService marketObjectService, IMapper mapper)
        {
	        _coreUiService = coreUiService;
	        _registersService = registersService;
	        MarketObjectService = marketObjectService;
	        Mapper = mapper;
        }


        [SRDFunction(Tag = SRDCoreFunctions.MARKET_MAP)]
        public ActionResult Index(long? objectId)
        {
	        if (objectId.HasValue)
            {
                var marketObject = MarketObjectService.GetMappedObjectById(objectId.Value);
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

            var allDistricts = new List<OMReferenceItem>();
            List<OMReferenceItem> allRegions = new List<OMReferenceItem>();
            List<OMReferenceItem> allZones = new List<OMReferenceItem>();
            List<string> allQuartals = OMQuartalDictionary.Where(x => true).Select(x => x.CadastralQuartal).Execute().Select(x => x.CadastralQuartal).ToList();

            PrepareQueryByUserFilter(query);

            if (DateTime.TryParseExact(actualDate, "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out acD)) query.And(x => x.ParserTime <= acD);

            var DistrictsData = query.Select(x => new { x.PricePerMeter }).Execute().ToList();

            var districtList = new List<IGrouping<string, OMCoreObject>>();
            var regionList = new List<IGrouping<string, OMCoreObject>>();
            var zoneList = new List<IGrouping<string, OMCoreObject>>();
            var quartalList = new List<IGrouping<string, OMCoreObject>>();

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
                        status = string.Empty,
                        statusCode = string.Empty,
                        source = x.Market,
                        price = x.Price,
                        area = x.Area,
                        areaLand = (decimal?) null,
                        roomsCount = (int?) null,
                        link = string.Empty,
                        metro = string.Empty,
                        address = x.Address,
                        images = string.Empty,
                        id = x.Id,
                        floorNumber = x.FloorNumber,
                        floorCount = x.FloorsCount,
                        cadastralNumber = x.CadastralNumber,
                        parserTime = x.ParserTime?.ToString("dd.MM.yyyy"),
                        lastUpdateDate = (DateTime?)null,
                        lng = x.Lng,
                        lat = x.Lat,
                        entranceType = string.Empty,
                        qualityClassCode = x.QualityClass_Code,
                        qualityClass = x.QualityClass,
                        renovation = string.Empty,
                        buildingLine = string.Empty
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
                propertyTypes = filters.Where(x => x.ReferenceId == Consts.PropertyTypesCIPJSAttribute.ReferenceId).ToList().Select(x => x.ValueLongArrayCasted).FirstOrDefault();
                dealTypes = filters.Where(x => x.ReferenceId == Consts.DealTypeAttribute.ReferenceId).ToList().Select(x => x.ValueLongArrayCasted).FirstOrDefault();
                marketSegments = filters.Where(x => x.ReferenceId == Consts.PropertyMarketSegmentAttribute.ReferenceId).ToList().Select(x => x.ValueLongArrayCasted).FirstOrDefault();
            }
            var propertyTypeList =
                OMReferenceItem.Where(x => x.ReferenceId == Consts.PropertyTypesCIPJSAttribute.ReferenceId)
                    .OrderBy(x => x.Value).SelectAll().Execute()
                    .OrderBy(x => int.Parse(x.Code)).Select(x => new {
                        Id = x.ItemId,
                        Code = x.Code,
                        Value = x.Value,
                        Name = x.Name,
                        Selected = propertyTypes == null ? false : propertyTypes.Contains(x.ItemId) ? true : false
                    });
            var commertialMarketSegmentList =
                OMReferenceItem.Where(x => x.ReferenceId == Consts.PropertyMarketSegmentAttribute.ReferenceId)
                    .OrderBy(x => x.Value).SelectAll().Execute().Where(x => int.Parse(x.Code) < 100)
                    .OrderBy(x => int.Parse(x.Code)).Select(x => new {
                        Id = x.ItemId, 
                        Code = x.Code, 
                        Value = x.Value, 
                        Name = x.Name,
                        Selected = marketSegments == null ? false : marketSegments.Contains(x.ItemId) ? true : false
                    });
            var propertyMarketSegmentList =
                OMReferenceItem.Where(x => x.ReferenceId == Consts.PropertyMarketSegmentAttribute.ReferenceId)
                    .OrderBy(x => x.Value).SelectAll().Execute().Where(x => int.Parse(x.Code) >= 100 && int.Parse(x.Code) < 200)
                    .OrderBy(x => int.Parse(x.Code)).Select(x => new {
                        Id = x.ItemId, 
                        Code = x.Code, 
                        Value = x.Value, 
                        Name = x.Name,
                        Selected = marketSegments == null ? false : marketSegments.Contains(x.ItemId) ? true : false
                    });
            var dealTypeList = OMReferenceItem.Where(x => x.ReferenceId == Consts.DealTypeAttribute.ReferenceId)
                    .OrderBy(x => x.Value).SelectAll().Execute()
                    .OrderBy(x => x.ItemId).Select(x => new {
                        Id = x.ItemId, 
                        Code = x.Code, 
                        Value = x.Value, 
                        Name = x.Name,
                        Selected = dealTypes == null ? false : dealTypes.Contains(x.ItemId) ? true : false
                    });
            var sourceTypeList = OMReferenceItem.Where(x => x.ReferenceId == Consts.MarketAttribute.ReferenceId).And(x => x.ItemId <= (int) MarketTypes.Rosreestr)
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
                    text = Consts.PropertyTypesCIPJSAttribute.Name,
                    propertyTypeList = propertyTypeList,
                    referenceId = Consts.PropertyTypesCIPJSAttribute.ReferenceId,
                    id = Consts.PropertyTypesCIPJSAttribute.Id
                },
                commertialMarketFilter = new
                {
                    typeControl = typeControl,
                    type = type,
                    text = Consts.PropertyMarketSegmentAttribute.Name,
                    commertialMarketSegmentList = commertialMarketSegmentList,
                    referenceId = Consts.PropertyMarketSegmentAttribute.ReferenceId,
                    id = Consts.PropertyMarketSegmentAttribute.Id
                },
                propertyMarketFilter = new
                {
                    typeControl = typeControl,
                    type = type,
                    text = Consts.PropertyMarketSegmentAttribute.Name,
                    propertyMarketSegmentList = propertyMarketSegmentList,
                    referenceId = Consts.PropertyMarketSegmentAttribute.ReferenceId,
                    id = Consts.PropertyMarketSegmentAttribute.Id,
                },
                dealTypeFilter = new
                {
                    typeControl = typeControl,
                    type = type,
                    text = Consts.DealTypeAttribute.Name,
                    dealTypeList = dealTypeList,
                    referenceId = Consts.DealTypeAttribute.ReferenceId,
                    id = Consts.DealTypeAttribute.Id
                },
                districtTypeFilter = new
                {
                    typeControl = string.Empty,
                    type = type,
                    text = string.Empty,
                    districtTypeList = new List<OMReferenceItem>(),
                    referenceId = 0,
                    id = 0
                },
                sourceTypeFilter = new
                {
                    typeControl = typeControl,
                    type = type,
                    text = Consts.MarketAttribute.Name,
                    sourceTypeList = sourceTypeList,
                    referenceId = Consts.MarketAttribute.ReferenceId,
                    id = Consts.MarketAttribute.Id
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
            List<OMReferenceItem> CIPJSType = OMReferenceItem.Where(x => x.ReferenceId == Consts.PropertyTypesCIPJSAttribute.ReferenceId).SelectAll().Execute();
            List<OMReferenceItem> MarketSegment = OMReferenceItem.Where(x => x.ReferenceId == Consts.PropertyMarketSegmentAttribute.ReferenceId).SelectAll().Execute();
            List<OMReferenceItem> status = new List<OMReferenceItem>();
            List<OMReferenceItem> qualityClass = OMReferenceItem.Where(x => x.ReferenceId == Consts.QualityClassCodeAttribute.ReferenceId).SelectAll().Execute();
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
	        var marketObjectDto = Mapper.Map<MarketSaveObjectDto, MarketObjectDto>(dto);

	        MarketObjectService.UpdateInfoFromCard(marketObjectDto);

            object result = new
            {
	            segment = FormSegment(marketObjectDto.PropertyMarketSegment),
	            propertyType = marketObjectDto.PropertyTypesCIPJS,
	            propertyTypeCode = marketObjectDto.PropertyTypesCIPJS_Code,
	            marketSegment = marketObjectDto.PropertyMarketSegment,
	            marketSegmentCode = marketObjectDto.PropertyMarketSegment_Code,
	            status = string.Empty,
	            statusCode = string.Empty,
	            id = marketObjectDto.Id,
	            lng = marketObjectDto.Lng,
	            lat = marketObjectDto.Lat,
	            entranceType = string.Empty,
	            qualityClassCode = marketObjectDto.QualityClass_Code,
	            qualityClass = marketObjectDto.QualityClass,
	            //TODO будет справочник
                renovation = string.Empty,
                //TODO будет справочник
	            buildingLine = string.Empty,
	            floorNumber = marketObjectDto.FloorNumber,
	            floorCount = marketObjectDto.FloorsCount
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
		    var marketObject = MarketObjectService.GetMappedObjectById(objectId);
            if (marketObject == null)throw new Exception($"Ошибка! Объекта аналога с идентификатором {objectId} не существует!");
            query.And(x => x.DealType_Code == marketObject.DealType_Code && x.PropertyMarketSegment_Code == marketObject.PropertyMarketSegment_Code);
	    }

		private void PrepareQueryByUserFilter(QSQuery<OMCoreObject> query)
		{
			var userFilter = _coreUiService.GetSearchFilter(MarketObjectsRegisterViewId);
			if (userFilter != null && !string.IsNullOrEmpty(userFilter.Condition))
			{
				var filters = JsonConvert.DeserializeObject<List<FilterModel>>(userFilter.Condition);
                if (filters.Any(f => f.Id == Consts.PropertyTypesCIPJSAttribute.Id))
                {
                    var filter = filters.First(f => f.Id == Consts.PropertyTypesCIPJSAttribute.Id);
                    if (filter.ValueLongArrayCasted != null)
                    {
                        var list = filter.ValueLongArrayCasted.Select(y => ((PropertyTypesCIPJS)y).GetEnumDescription()).ToList();
                        query.And(x => list.Contains(x.PropertyTypesCIPJS));
                    }
                }
                if (filters.Any(f => f.Id == Consts.PropertyMarketSegmentAttribute.Id))
				{
					var filter = filters.First(f => f.Id == Consts.PropertyMarketSegmentAttribute.Id);
					if (filter.ValueLongArrayCasted != null)
					{
						var list = filter.ValueLongArrayCasted.Select(y => ((MarketSegment)y).GetEnumDescription()).ToList();
						query.And(x => list.Contains(x.PropertyMarketSegment));
					}
				}
				if (filters.Any(f => f.Id == Consts.DealTypeAttribute.Id))
				{
					var filter = filters.First(f => f.Id == Consts.DealTypeAttribute.Id);
					if (filter.ValueLongArrayCasted != null)
					{
						var list = filter.ValueLongArrayCasted.Select(y => ((DealType)y).GetEnumDescription()).ToList();
						query.And(x => list.Contains(x.DealType));
					}
				}
				if (filters.Any(f => f.Id == Consts.PriceAttributeId))
				{
					var filter = filters.First(f => f.Id == Consts.PriceAttributeId);
					if (filter.From.HasValue) query.And(x => x.Price >= filter.From.Value);
					if (filter.To.HasValue) query.And(x => x.Price <= filter.To.Value);
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