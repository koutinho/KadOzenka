using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Core.Shared.Extensions;
using KadOzenka.Dal.LongProcess.MarketObjects.Settings;
using KadOzenka.Dal.OutliersChecking.Dto;
using MarketPlaceBusiness;
using MarketPlaceBusiness.Interfaces;
using ObjectModel.Directory;
using ObjectModel.Market;
using Serilog;

namespace KadOzenka.Dal.OutliersChecking
{
	public enum ObjectPropertyTypeDivision
	{
		[Description("Земельные участки")]
		LandArea,
		[Description("Здания и Помещения")]
		BuildingsAndPlacements,
		[Description("Сооружения, ОНС, ЕНК и иные ОН")]
		ConstructionsAndOthers
	}

	public class OutliersCheckingProcess
	{
		private static readonly ILogger Log = Serilog.Log.ForContext<OutliersCheckingProcess>();
		private readonly OutliersCheckingSettingsService _outliersCheckingSettingsService;
		private readonly OutliersCheckingReport _outliersCheckingReport;
		private Dictionary<string, OutliersCheckingSettingDto> _outliersCheckingSettings;
		private IMarketObjectsForOutliersCheckingService MarketObjectsService { get; set; }
		protected object Locked;

		public int TotalObjectsCount { get; private set; }
		public int CurrentHandledObjectsCount { get; private set; }
		public int ExcludedObjectsCount { get; private set; }
		public List<PropertyTypesCIPJS> PropertyTypes { get; private set; }

		public OutliersCheckingProcess()
		{
			_outliersCheckingSettingsService = new OutliersCheckingSettingsService();
			_outliersCheckingReport = new OutliersCheckingReport();
			MarketObjectsService = new MarketObjectsForOutliersCheckingService();
		}

		public long PerformOutliersChecking(MarketSegment? segment, List<ObjectPropertyTypeDivision> propertyTypes)
		{
			Log.Debug($"Старт процедуры Проверки на выбросы {(segment.HasValue ? $"для сегмента {segment.GetEnumDescription()}" : "для всех сегментов")}");

			SetPropertyTypes(propertyTypes);

			Log.Debug("Получение настроек коэффициентов");
			_outliersCheckingSettings = _outliersCheckingSettingsService.GetOutliersCheckingSettings()
				.ToDictionary(x => x.LocationName, x => x);

			Log.Debug("Получение объектов по сегментам");
			var objectsBySegments = MarketObjectsService.GetObjectsBySegments(PropertyTypes, segment);
			SetTotalObjectsCount(objectsBySegments);

			var segmentsForHandling = segment.HasValue 
				? new List<MarketSegment>{segment.Value} :
				System.Enum.GetValues(typeof(MarketSegment)).Cast<MarketSegment>().ToList();
			segmentsForHandling.Remove(MarketSegment.None);

			Locked = new object();
			foreach (var marketSegment in segmentsForHandling)
			{
				var objectsBySegment = objectsBySegments.ContainsKey(marketSegment) 
					? objectsBySegments[marketSegment] 
					: new List<OMCoreObject>();

				ProcessObjectsBySegment(marketSegment, objectsBySegment);
			}

			_outliersCheckingReport.SetStyleAndSorting();
			return _outliersCheckingReport.SaveReport();
		}

		private void SetPropertyTypes(List<ObjectPropertyTypeDivision> propertyTypeDivisions)
		{
			if(propertyTypeDivisions.IsEmpty())
				throw new Exception("Не заданы виды объектов недвижимости");

			var propertyTypes = new List<PropertyTypesCIPJS>();
			foreach (var propertyTypeDivision in propertyTypeDivisions)
			{
				switch (propertyTypeDivision)
				{
					case ObjectPropertyTypeDivision.LandArea:
						propertyTypes.Add(PropertyTypesCIPJS.LandArea);
						break;
					case ObjectPropertyTypeDivision.BuildingsAndPlacements:
						propertyTypes.AddRange(new[]{ PropertyTypesCIPJS.Buildings, PropertyTypesCIPJS.Placements });
						break;
					case ObjectPropertyTypeDivision.ConstructionsAndOthers:
						propertyTypes.Add(PropertyTypesCIPJS.OtherAndMore);
						break;
				}
			}

			PropertyTypes = propertyTypes;
		}

		private void ProcessObjectsBySegment(MarketSegment segmentCode, List<OMCoreObject> objectsBySegment)
		{
			Log.ForContext("ObjectsCountTotal", TotalObjectsCount)
				.ForContext("ObjectsCountCurrentHandled", CurrentHandledObjectsCount)
				.ForContext("ObjectsCountCurrentExcluded", ExcludedObjectsCount)
				.ForContext("ObjectsCountBySegment", objectsBySegment.Count)
				.Debug("Начата обработка сегмента '{MarketSegment}'", segmentCode.GetEnumDescription());

			var saleODealObjects = objectsBySegment.Where(x => x.DealType_Code == DealType.SaleDeal).ToList();
			var saleOSuggestionObjects = objectsBySegment.Where(x => x.DealType_Code == DealType.SaleSuggestion).ToList();
			var rentDealObjects = objectsBySegment.Where(x => x.DealType_Code == DealType.RentDeal).ToList();
			var rentSuggestionObjects = objectsBySegment.Where(x => x.DealType_Code == DealType.RentSuggestion).ToList();
			ProcessObjectsByDealType(segmentCode, DealType.SaleDeal, saleODealObjects);
			ProcessObjectsByDealType(segmentCode, DealType.SaleSuggestion, saleOSuggestionObjects);
			ProcessObjectsByDealType(segmentCode, DealType.RentDeal, rentDealObjects);
			ProcessObjectsByDealType(segmentCode, DealType.RentSuggestion, rentSuggestionObjects);

			Log.ForContext("ObjectsCountTotal", TotalObjectsCount)
				.ForContext("ObjectsCountCurrentHandled", CurrentHandledObjectsCount)
				.ForContext("ObjectsCountCurrentExcluded", ExcludedObjectsCount)
				.ForContext("ObjectsCountBySegment", objectsBySegment.Count)
				.Debug("Обработка сегмента '{MarketSegment}' завершена", segmentCode.GetEnumDescription());
		}

		private void ProcessObjectsByDealType(MarketSegment segmentCode, DealType dealType, List<OMCoreObject> objectsByDealType)
		{
			Log.ForContext("ObjectsCountTotal", TotalObjectsCount)
				.ForContext("ObjectsCountCurrentHandled", CurrentHandledObjectsCount)
				.ForContext("ObjectsCountCurrentExcluded", ExcludedObjectsCount)
				.ForContext("ObjectsCountByDealType", objectsByDealType.Count)
				.Debug("Начата обработка типа сделки '{DealType}' сегмента '{MarketSegment}'", dealType.GetEnumDescription(), segmentCode.GetEnumDescription());

			if (PropertyTypes.Contains(PropertyTypesCIPJS.LandArea))
			{
				var parcels = objectsByDealType.Where(x => x.PropertyTypesCIPJS_Code == PropertyTypesCIPJS.LandArea).ToList();
				ProcessObjectsByPropertyType(segmentCode, dealType, ObjectPropertyTypeDivision.LandArea, parcels);
			}

			if (PropertyTypes.Contains(PropertyTypesCIPJS.Placements) ||
			    PropertyTypes.Contains(PropertyTypesCIPJS.Buildings))
			{
				var buildingsAndPlacements = objectsByDealType.Where(x =>
					x.PropertyTypesCIPJS_Code == PropertyTypesCIPJS.Buildings ||
					x.PropertyTypesCIPJS_Code == PropertyTypesCIPJS.Placements).ToList();
				ProcessObjectsByPropertyType(segmentCode, dealType, ObjectPropertyTypeDivision.BuildingsAndPlacements, buildingsAndPlacements);
			}

			if (PropertyTypes.Contains(PropertyTypesCIPJS.OtherAndMore))
			{
				var constructionsAndOthers = objectsByDealType.Where(x => x.PropertyTypesCIPJS_Code == PropertyTypesCIPJS.OtherAndMore).ToList();
				ProcessObjectsByPropertyType(segmentCode, dealType, ObjectPropertyTypeDivision.ConstructionsAndOthers, constructionsAndOthers);
			}

			Log.ForContext("ObjectsCountTotal", TotalObjectsCount)
				.ForContext("ObjectsCountCurrentHandled", CurrentHandledObjectsCount)
				.ForContext("ObjectsCountCurrentExcluded", ExcludedObjectsCount)
				.ForContext("ObjectsCountByDealType", objectsByDealType.Count)
				.Debug("Обработка типа сделки '{DealType}' сегмента '{MarketSegment}' завершена", dealType.GetEnumDescription(), segmentCode.GetEnumDescription());
		}

		private void ProcessObjectsByPropertyType(MarketSegment segmentCode, DealType dealType, ObjectPropertyTypeDivision propertyTypeDivision, List<OMCoreObject> omCoreObjects)
		{
			_outliersCheckingReport.AddNewWorksheetForSegment(segmentCode, dealType, propertyTypeDivision);

			var objectsByLocationSliceList = ObjectsByLocationSliceDto.FromEntityList(omCoreObjects);
			Log.ForContext("ObjectsCountTotal", TotalObjectsCount)
				.ForContext("ObjectsCountCurrentHandled", CurrentHandledObjectsCount)
				.ForContext("ObjectsCountCurrentExcluded", ExcludedObjectsCount)
				.ForContext("ObjectsCountByPropertyType", omCoreObjects.Count)
				.Debug("Получено '{LocationSliceCount}'локаций для '{PropertyTypeDivision}' типа сделки '{DealType}' сегмента '{MarketSegment}'",
					objectsByLocationSliceList.Count, propertyTypeDivision.GetEnumDescription(), dealType.GetEnumDescription(), segmentCode.GetEnumDescription());

			var cancelTokenSource = new CancellationTokenSource();
			var options = new ParallelOptions
			{
				CancellationToken = cancelTokenSource.Token,
				MaxDegreeOfParallelism = 20
			};
			Parallel.ForEach(objectsByLocationSliceList, options, ProcessObjectsByLocation);

			Log.ForContext("ObjectsCountTotal", TotalObjectsCount)
				.ForContext("ObjectsCountCurrentHandled", CurrentHandledObjectsCount)
				.ForContext("ObjectsCountCurrentExcluded", ExcludedObjectsCount)
				.ForContext("ObjectsCountByPropertyType", omCoreObjects.Count)
				.Debug("Обработка для '{PropertyTypeDivision}' типа сделки '{DealType}' сегмента '{MarketSegment}' завершена",
					objectsByLocationSliceList.Count, propertyTypeDivision.GetEnumDescription(), dealType.GetEnumDescription(), segmentCode.GetEnumDescription());
		}

		private void SetTotalObjectsCount(Dictionary<MarketSegment, List<OMCoreObject>> objectsBySegments)
		{
			var maxObjectsCount = 0;
			foreach (var objectsBySegment in objectsBySegments)
			{
				maxObjectsCount += objectsBySegment.Value.Count;
			}

			TotalObjectsCount = maxObjectsCount;
		}

		private void ProcessObjectsByLocation(ObjectsByLocationSliceDto objectsByLocation)
		{
			var dealType = objectsByLocation.MarketObjects.First().DealType_Code;
			if (objectsByLocation.MarketObjects.Count == 1)
			{
				lock (Locked)
				{
					CurrentHandledObjectsCount++;
				}
				Log.ForContext("ObjectsCountByLocation", 1)
					.Verbose("Обработка локации '{LocationName}' для семента {MarketSegment}({DealType}) завершена",
						objectsByLocation.LocationName,
						objectsByLocation.MarketObjects.First().PropertyMarketSegment_Code.GetEnumDescription(), dealType.GetEnumDescription());

				return;
			}

			var reportRow = new OutliersCheckingReportRow(objectsByLocation.LocationName);
			var orderedObjects = objectsByLocation.MarketObjects.OrderBy(x => x.PricePerMeter).ToList();
			var lowerRangeMedian = GetMedianPricePerMeter(orderedObjects.Take(orderedObjects.Count / 2).ToList());
			var upperRangeMedian = GetMedianPricePerMeter(orderedObjects.TakeLast(orderedObjects.Count - orderedObjects.Count / 2).ToList());
			reportRow.SetMedians(lowerRangeMedian, upperRangeMedian);

			decimal lowerRangeLimit = lowerRangeMedian, upperRangeLimit = upperRangeMedian;
			if (_outliersCheckingSettings.TryGetValue(objectsByLocation.LocationName, out var setting))
			{
				if (setting.MinDeltaCoef.HasValue)
				{
					lowerRangeLimit *= setting.MinDeltaCoef.Value;
				}

				if (setting.MaxDeltaCoef.HasValue)
				{
					upperRangeLimit *= setting.MaxDeltaCoef.Value;
				}

				reportRow.SetDeltaCoefs(setting.MinDeltaCoef, setting.MaxDeltaCoef);
			}
			reportRow.SetLimits(lowerRangeLimit, upperRangeLimit);
			var excludedObjectsCountByLocation = 0;
			foreach (var omCoreObject in orderedObjects)
			{
				reportRow.SetMarketInfo(omCoreObject.Id, omCoreObject.CadastralNumber, omCoreObject.PricePerMeter.Value,
					omCoreObject.ProcessType_Code);

				if (omCoreObject.ProcessType_Code == ProcessStep.InProcess
				    && (omCoreObject.PricePerMeter < lowerRangeLimit || omCoreObject.PricePerMeter > upperRangeLimit))
				{
					omCoreObject.ProcessType_Code = ProcessStep.Excluded;
					omCoreObject.ExclusionStatus_Code = ExclusionStatus.UnacceptablePrice;
					omCoreObject.Save();

					excludedObjectsCountByLocation++;
				}
				reportRow.SetExcludedStatus(omCoreObject.ProcessType_Code == ProcessStep.Excluded);

				lock (Locked)
				{
					_outliersCheckingReport.AddRow(reportRow);
					CurrentHandledObjectsCount++;
					if (omCoreObject.ProcessType_Code == ProcessStep.Excluded)
						ExcludedObjectsCount++;
				}
			}

			Log.ForContext("LowerMedian", reportRow.LowerMedian)
				.ForContext("UpperMedian", reportRow.UpperMedian)
				.ForContext("MinDeltaCoef", reportRow.MinDeltaCoef)
				.ForContext("MaxDeltaCoef", reportRow.MaxDeltaCoef)
				.ForContext("LowerLimit", reportRow.LowerLimit)
				.ForContext("UpperLimit", reportRow.UpperLimit)
				.ForContext("ObjectsCountByLocation", objectsByLocation.MarketObjects.Count)
				.ForContext("ExcludedObjectsCountByLocation", excludedObjectsCountByLocation)
				.Verbose("Обработка локации '{LocationName}' для семента {MarketSegment}({DealType}) завершена",
					objectsByLocation.LocationName,
					objectsByLocation.MarketObjects.First().PropertyMarketSegment_Code.GetEnumDescription(),
					dealType.GetEnumDescription());
		}

		private decimal GetMedianPricePerMeter(List<OMCoreObject> orderedMarketObjects)
		{
			decimal medianPricePerMeter;
			if (orderedMarketObjects.Count == 1)
			{
				medianPricePerMeter = orderedMarketObjects.First().PricePerMeter.Value;
			}
			else
			{
				if (orderedMarketObjects.Count % 2 == 1)
				{
					medianPricePerMeter = orderedMarketObjects[orderedMarketObjects.Count / 2].PricePerMeter.Value;
				}
				else
				{
					medianPricePerMeter = (orderedMarketObjects[orderedMarketObjects.Count / 2 - 1].PricePerMeter.Value +
					                       orderedMarketObjects[orderedMarketObjects.Count / 2].PricePerMeter.Value) / 2;
				}
			}

			return medianPricePerMeter;
		}
	}
}
