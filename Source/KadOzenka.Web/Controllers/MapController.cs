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
using KadOzenka.Web.Attributes;
using ObjectModel.SRD;

namespace KadOzenka.Web.Controllers
{

    public class MapController : BaseController
    {
        private readonly CoreUiService _coreUiService;
        private readonly RegistersService _registersService;

        public string MarketObjectsRegisterViewId => "MarketObjects";

        public MapController(CoreUiService coreUiService, RegistersService registersService)
        {
	        _coreUiService = coreUiService;
            _registersService = registersService;
        }

        [SRDFunction(Tag = SRDCoreFunctions.MARKET_MAP)]
        public ActionResult Index(long? objectId)
        {
	        if (objectId.HasValue)
            {
                var marketObject = OMCoreObject.Where(x => x.Id == objectId).SelectAll().ExecuteFirstOrDefault();
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
            var query = OMCoreObject
                .Where(x =>
                    (x.ProcessType_Code == ProcessStep.InProcess || x.ProcessType_Code == ProcessStep.Dealed) &&
                    x.Lng != null && x.Lat != null &&
                    (x.LastDateUpdate != null || x.Market_Code == MarketTypes.Rosreestr));
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
            var query = OMCoreObject
                .Where(x => (x.ProcessType_Code == ProcessStep.InProcess || x.ProcessType_Code == ProcessStep.Dealed) &&
                             x.Lng != null &&
                             x.Lat != null &&
                             (x.LastDateUpdate != null || x.Market_Code == MarketTypes.Rosreestr));

            List<OMReferenceItem> allDistricts = OMReferenceItem.Where(x => x.ReferenceId == OMCoreObject.GetAttributeData(y => y.District).ReferenceId).Select(x => x.Value).Execute().ToList();
            List<OMReferenceItem> allRegions = OMReferenceItem.Where(x => x.ReferenceId == OMCoreObject.GetAttributeData(y => y.Neighborhood).ReferenceId).Select(x => x.Value).Execute().ToList();
            List<OMReferenceItem> allZones = OMReferenceItem.Where(x => x.ReferenceId == OMCoreObject.GetAttributeData(y => y.ZoneRegion).ReferenceId).Select(x => x.Value).Execute().ToList();
            List<string> allQuartals = OMQuartalDictionary.Where(x => true).Select(x => x.CadastralQuartal).Execute().Select(x => x.CadastralQuartal).ToList();

            PrepareQueryByUserFilter(query);

            if (DateTime.TryParseExact(actualDate, "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out acD)) query.And(x => x.ParserTime <= acD);

            List<OMCoreObject> DistrictsData = query.Select(x => new { x.PricePerMeter, x.District, x.District_Code, x.Neighborhood, x.Neighborhood_Code, x.ZoneRegion, x.CadastralQuartal }).Execute().ToList();

            List<IGrouping<string, OMCoreObject>> districtList = DistrictsData.GroupBy(x => x.District).ToList();
            List<IGrouping<string, OMCoreObject>> regionList = DistrictsData.GroupBy(x => x.Neighborhood).ToList();
            List<IGrouping<string, OMCoreObject>> zoneList = DistrictsData.GroupBy(x => x.ZoneRegion).ToList();
            List<IGrouping<string, OMCoreObject>> quartalList = DistrictsData.GroupBy(x => x.CadastralQuartal).ToList();

            (List<(string name, string color, string counter)> ColoredData, List<(string min, string max)> MinMaxData) districtsData = 
                new HeatMap().SetColors(new HeatMap().GroupList(allDistricts, districtList), colorsArray);
            (List<(string name, string color, string counter)> ColoredData, List<(string min, string max)> MinMaxData) regionsData =
                new HeatMap().SetColors(new HeatMap().GroupList(allRegions, regionList), colorsArray);
            (List<(string name, string color, string counter)> ColoredData, List<(string min, string max)> MinMaxData) zonesData =
                new HeatMap().SetColors(new HeatMap().GroupList(allZones, zoneList), colorsArray);
            (List<(string name, string color, string counter)> ColoredData, List<(string min, string max)> MinMaxData) quartalsData =
	            new HeatMap().SetColors(new HeatMap().GroupList(allQuartals, quartalList), colorsArray);

            new HeatMap().GenerateHeatMapQuartalInitialImages(quartalsData.ColoredData);

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
                OMCoreObject.Where(x => ids.Contains(x.Id)).SelectAll().Execute().Take(ids.Count <= 20 ? ids.Count : 20).ToList().ForEach(x => {
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
                        floor = x.FloorNumber,
                        floorCount = x.FloorsCount,
                        cadastralNumber = x.CadastralNumber,
                        parserTime = x.ParserTime?.ToString("dd.MM.yyyy"),
                        lastUpdateDate = x.LastDateUpdate?.ToString("dd.MM.yyyy"),
                        lng = x.Lng,
                        lat = x.Lat
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
                propertyTypes = filters.Where(x => x.ReferenceId == OMCoreObject.GetAttributeData(y => y.PropertyTypesCIPJS).ReferenceId).ToList().Select(x => x.ValueLongArrayCasted).FirstOrDefault();
                dealTypes = filters.Where(x => x.ReferenceId == OMCoreObject.GetAttributeData(y => y.DealType).ReferenceId).ToList().Select(x => x.ValueLongArrayCasted).FirstOrDefault();
                marketSegments = filters.Where(x => x.ReferenceId == OMCoreObject.GetAttributeData(y => y.PropertyMarketSegment).ReferenceId).ToList().Select(x => x.ValueLongArrayCasted).FirstOrDefault();
            }
            var propertyTypeList =
                OMReferenceItem.Where(x => x.ReferenceId == OMCoreObject.GetAttributeData(y => y.PropertyTypesCIPJS).ReferenceId)
                    .OrderBy(x => x.Value).SelectAll().Execute()
                    .OrderBy(x => int.Parse(x.Code)).Select(x => new {
                        Id = x.ItemId,
                        Code = x.Code,
                        Value = x.Value,
                        Name = x.Name,
                        Selected = propertyTypes == null ? false : propertyTypes.Contains(x.ItemId) ? true : false
                    });
            var commertialMarketSegmentList =
                OMReferenceItem.Where(x => x.ReferenceId == OMCoreObject.GetAttributeData(y => y.PropertyMarketSegment).ReferenceId)
                    .OrderBy(x => x.Value).SelectAll().Execute().Where(x => int.Parse(x.Code) < 100)
                    .OrderBy(x => int.Parse(x.Code)).Select(x => new {
                        Id = x.ItemId, 
                        Code = x.Code, 
                        Value = x.Value, 
                        Name = x.Name,
                        Selected = marketSegments == null ? false : marketSegments.Contains(x.ItemId) ? true : false
                    });
            var propertyMarketSegmentList =
                OMReferenceItem.Where(x => x.ReferenceId == OMCoreObject.GetAttributeData(y => y.PropertyMarketSegment).ReferenceId)
                    .OrderBy(x => x.Value).SelectAll().Execute().Where(x => int.Parse(x.Code) >= 100 && int.Parse(x.Code) < 200)
                    .OrderBy(x => int.Parse(x.Code)).Select(x => new {
                        Id = x.ItemId, 
                        Code = x.Code, 
                        Value = x.Value, 
                        Name = x.Name,
                        Selected = marketSegments == null ? false : marketSegments.Contains(x.ItemId) ? true : false
                    });
            var dealTypeList = OMReferenceItem.Where(x => x.ReferenceId == OMCoreObject.GetAttributeData(y => y.DealType).ReferenceId)
                    .OrderBy(x => x.Value).SelectAll().Execute()
                    .OrderBy(x => x.ItemId).Select(x => new {
                        Id = x.ItemId, 
                        Code = x.Code, 
                        Value = x.Value, 
                        Name = x.Name,
                        Selected = dealTypes == null ? false : dealTypes.Contains(x.ItemId) ? true : false
                    });
            var districtTypeList = OMReferenceItem.Where(x => x.ReferenceId == OMCoreObject.GetAttributeData(y => y.District).ReferenceId)
                    .OrderBy(x => x.Value).SelectAll().Execute()
                    .OrderBy(x => x.ItemId).Select(x => new { 
                        Id = x.ItemId,
                        Code = x.Code,
                        Value = x.Value,
                        Name = x.Name,
                        Selected = false
                    });
            var sourceTypeList = OMReferenceItem.Where(x => x.ReferenceId == OMCoreObject.GetAttributeData(y => y.Market).ReferenceId).And(x => x.ItemId <= (int) MarketTypes.Rosreestr)
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
                    text = OMCoreObject.GetAttributeData(y => y.PropertyTypesCIPJS).Name,
                    propertyTypeList = propertyTypeList,
                    referenceId = OMCoreObject.GetAttributeData(y => y.PropertyTypesCIPJS).ReferenceId,
                    id = OMCoreObject.GetAttributeData(y => y.PropertyTypesCIPJS).Id
                },
                commertialMarketFilter = new
                {
                    typeControl = typeControl,
                    type = type,
                    text = OMCoreObject.GetAttributeData(y => y.PropertyMarketSegment).Name,
                    commertialMarketSegmentList = commertialMarketSegmentList,
                    referenceId = OMCoreObject.GetAttributeData(y => y.PropertyMarketSegment).ReferenceId,
                    id = OMCoreObject.GetAttributeData(y => y.PropertyMarketSegment).Id
                },
                propertyMarketFilter = new
                {
                    typeControl = typeControl,
                    type = type,
                    text = OMCoreObject.GetAttributeData(y => y.PropertyMarketSegment).Name,
                    propertyMarketSegmentList = propertyMarketSegmentList,
                    referenceId = OMCoreObject.GetAttributeData(y => y.PropertyMarketSegment).ReferenceId,
                    id = OMCoreObject.GetAttributeData(y => y.PropertyMarketSegment).Id,
                },
                dealTypeFilter = new
                {
                    typeControl = typeControl,
                    type = type,
                    text = OMCoreObject.GetAttributeData(y => y.DealType).Name,
                    dealTypeList = dealTypeList,
                    referenceId = OMCoreObject.GetAttributeData(y => y.DealType).ReferenceId,
                    id = OMCoreObject.GetAttributeData(y => y.DealType).Id
                },
                districtTypeFilter = new
                {
                    typeControl = typeControl,
                    type = type,
                    text = OMCoreObject.GetAttributeData(y => y.District).Name,
                    districtTypeList = districtTypeList,
                    referenceId = OMCoreObject.GetAttributeData(y => y.District).ReferenceId,
                    id = OMCoreObject.GetAttributeData(y => y.District).Id
                },
                sourceTypeFilter = new
                {
                    typeControl = typeControl,
                    type = type,
                    text = OMCoreObject.GetAttributeData(y => y.Market).Name,
                    sourceTypeList = sourceTypeList,
                    referenceId = OMCoreObject.GetAttributeData(y => y.Market).ReferenceId,
                    id = OMCoreObject.GetAttributeData(y => y.Market).Id
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
            List<OMReferenceItem> CIPJSType = OMReferenceItem.Where(x => x.ReferenceId == OMCoreObject.GetAttributeData(y => y.PropertyTypesCIPJS).ReferenceId).SelectAll().Execute();
            List<OMReferenceItem> MarketSegment = OMReferenceItem.Where(x => x.ReferenceId == OMCoreObject.GetAttributeData(y => y.PropertyMarketSegment).ReferenceId).SelectAll().Execute();
            List<OMReferenceItem> status = OMReferenceItem.Where(x => x.ReferenceId == OMCoreObject.GetAttributeData(y => y.ProcessType_Code).ReferenceId).SelectAll().Execute();
            return Json(new 
            { 
                CIPJSType = CIPJSType.Select(x => new { code = x.ItemId, value = x.Value }), 
                MarketSegment = MarketSegment.Select(x => new { code = x.ItemId, value = x.Value }),
                Status = status.Select(x => new { code = x.ItemId, value = x.Value }) 
            });
        }

        [SRDFunction(Tag = SRDCoreFunctions.MARKET_MAP)]
        public JsonResult ChangeObject(long? id, decimal? lng, decimal? lat, long? propertyTypeCode, long? marketSegmentCode, long? statusCode)
        {
            OMCoreObject obj = OMCoreObject.Where(x => x.Id == id).SelectAll().ExecuteFirstOrDefault();
            obj.Lng = lng;
            obj.Lat = lat;
            obj.PropertyTypesCIPJS_Code = (PropertyTypesCIPJS) propertyTypeCode;
            obj.PropertyMarketSegment_Code = (MarketSegment) marketSegmentCode;
            obj.ProcessType_Code = (ProcessStep) statusCode;
            obj.Save();
            object result = OMCoreObject.Where(x => x.Id == id).SelectAll().Execute().Select(x => {
                return new
                {
                    segment = FormSegment(x.PropertyMarketSegment),
                    propertyType = x.PropertyTypesCIPJS,
                    propertyTypeCode = x.PropertyTypesCIPJS_Code,
                    marketSegment = x.PropertyMarketSegment,
                    marketSegmentCode = x.PropertyMarketSegment_Code,
                    status = x.ProcessType,
                    statusCode = x.ProcessType_Code,
                    id = x.Id,
                    lng = x.Lng,
                    lat = x.Lat
                };
            }).FirstOrDefault();
            return Json(result);
        }

        [SRDFunction(Tag = SRDCoreFunctions.MARKET_MAP)]
        public ActionResult CadastralHeatMapTiles(int x, int y, int z)
        {
	        var file = new HeatMap().GetHeatMapTile(x, y, z);
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
		    var marketObject = OMCoreObject.Where(x => x.Id == objectId).SelectAll().ExecuteFirstOrDefault();
            if (marketObject == null)throw new Exception($"Ошибка! Объекта аналога с идентификатором {objectId} не существует!");
            query.And(x => x.DealType_Code == marketObject.DealType_Code && x.PropertyMarketSegment_Code == marketObject.PropertyMarketSegment_Code);
	    }

		private void PrepareQueryByUserFilter(QSQuery<OMCoreObject> query)
		{
			var userFilter = _coreUiService.GetSearchFilter(MarketObjectsRegisterViewId);
			if (userFilter != null && !string.IsNullOrEmpty(userFilter.Condition))
			{
				var filters = JsonConvert.DeserializeObject<List<FilterModel>>(userFilter.Condition);
                if (filters.Any(f => f.Id == OMCoreObject.GetAttributeData(x => x.PropertyTypesCIPJS).Id))
                {
                    var filter = filters.First(f => f.Id == OMCoreObject.GetAttributeData(x => x.PropertyTypesCIPJS).Id);
                    if (filter.ValueLongArrayCasted != null)
                    {
                        var list = filter.ValueLongArrayCasted.Select(y => ((PropertyTypesCIPJS)y).GetEnumDescription()).ToList();
                        query.And(x => list.Contains(x.PropertyTypesCIPJS));
                    }
                }
                if (filters.Any(f => f.Id == OMCoreObject.GetAttributeData(x => x.PropertyMarketSegment).Id))
				{
					var filter = filters.First(f => f.Id == OMCoreObject.GetAttributeData(x => x.PropertyMarketSegment).Id);
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
				if (filters.Any(f => f.Id == OMCoreObject.GetAttributeData(x => x.Price).Id))
				{
					var filter = filters.First(f => f.Id == OMCoreObject.GetAttributeData(x => x.Price).Id);
					if (filter.From.HasValue) query.And(x => x.Price >= filter.From.Value);
					if (filter.To.HasValue) query.And(x => x.Price <= filter.To.Value);
				}
				if (filters.Any(f => f.Id == OMCoreObject.GetAttributeData(x => x.Metro).Id))
				{
					var filter = filters.First(f => f.Id == OMCoreObject.GetAttributeData(x => x.Metro).Id);
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