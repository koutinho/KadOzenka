using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Core.Shared.Extensions;
using KadOzenka.Dal.OutliersChecking.Dto;
using ObjectModel.Directory;
using ObjectModel.Market;
using Serilog;

namespace KadOzenka.Dal.OutliersChecking
{
	public class OutliersCheckingProcess
	{
		private static readonly ILogger Log = Serilog.Log.ForContext<OutliersCheckingProcess>();
		private readonly OutliersCheckingSettingsService _outliersCheckingSettingsService;
		private readonly OutliersCheckingReport _outliersCheckingReport;
		private Dictionary<string, OutliersCheckingSettingDto> _outliersCheckingSettings;
		protected object Locked;

		public int TotalObjectsCount { get; private set; }
		public int CurrentHandledObjectsCount { get; private set; }
		public int ExcludedObjectsCount { get; private set; }

		public OutliersCheckingProcess()
		{
			_outliersCheckingSettingsService = new OutliersCheckingSettingsService();
			_outliersCheckingReport = new OutliersCheckingReport();
		}

		public long PerformOutliersChecking(MarketSegment? segment)
		{
			Log.Debug($"Старт процедуры Проверки на выбросы {(segment.HasValue ? $"для сегмента {segment.GetEnumDescription()}" : "для всех сегментов")}");
			Log.Debug("Получение настроек коэффициентов");
			_outliersCheckingSettings = _outliersCheckingSettingsService.GetOutliersCheckingSettings()
				.ToDictionary(x => x.LocationName, x => x);

			Log.Debug("Получение объектов по сегментам");
			var objectsBySegments = GetObjectsBySegments(segment);
			SetTotalObjectsCount(objectsBySegments);

			Locked = new object();
			foreach (var objectsBySegment in objectsBySegments)
			{
				var segmentCode = objectsBySegment.Key;
				var saleObjects = objectsBySegment.Value.Where(x =>
					x.DealType_Code == DealType.SaleDeal || x.DealType_Code == DealType.SaleSuggestion).ToList();
				var rentObjects = objectsBySegment.Value.Where(x =>
					x.DealType_Code == DealType.RentDeal || x.DealType_Code == DealType.RentSuggestion).ToList();

				ProcessObjectsBySegmentAndDealType(segmentCode, DealType.SaleDeal, saleObjects);
				ProcessObjectsBySegmentAndDealType(segmentCode, DealType.RentDeal, rentObjects);
			}

			_outliersCheckingReport.SetStyleAndSorting();
			return _outliersCheckingReport.SaveReport();
		}

		private void ProcessObjectsBySegmentAndDealType(MarketSegment segmentCode, DealType dealType, List<OMCoreObject> objectsBySegment)
		{
			_outliersCheckingReport.AddNewWorksheetForSegment(segmentCode, dealType);
			var objectsByLocationSliceList = ObjectsByLocationSliceDto.FromEntityList(objectsBySegment);
			Log.ForContext("ObjectsCountTotal", TotalObjectsCount)
				.ForContext("ObjectsCountCurrentHandled", CurrentHandledObjectsCount)
				.ForContext("ObjectsCountCurrentExcluded", ExcludedObjectsCount)
				.ForContext("ObjectsCountBySegment", objectsBySegment.Count)
				.Debug("Получено '{LocationSliceCount}'локаций для сегмента '{MarketSegment}'({DealType})",
					objectsByLocationSliceList.Count, segmentCode.GetEnumDescription(), dealType == DealType.SaleDeal || dealType == DealType.SaleSuggestion ? "Продажа" : "Аренда");

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
				.ForContext("ObjectsCountBySegment", objectsBySegment.Count)
				.Debug("Обработка сегмента '{MarketSegment}'({DealType}) завершена", segmentCode.GetEnumDescription(), dealType == DealType.SaleDeal || dealType == DealType.SaleSuggestion ? "Продажа" : "Аренда");
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

		private Dictionary<MarketSegment, List<OMCoreObject>> GetObjectsBySegments(MarketSegment? segment)
		{
			var query = OMCoreObject.Where(x =>
				(x.ProcessType_Code == ProcessStep.InProcess || x.ProcessType_Code == ProcessStep.Dealed)
				&& x.PropertyMarketSegment != null
				&& x.PricePerMeter != null
				&& x.Zone != null
				&& x.District != null
				&& x.Neighborhood != null);

			if (segment.HasValue)
				query.And(x => x.PropertyMarketSegment_Code == segment.Value);

			return query
				.Select(x => new
				{
					x.Id,
					x.CadastralNumber,
					x.Zone,
					x.District,
					x.District_Code,
					x.DealType,
					x.DealType_Code,
					x.Neighborhood,
					x.Neighborhood_Code,
					x.PricePerMeter,
					x.PropertyMarketSegment,
					x.PropertyMarketSegment_Code,
					x.ProcessType_Code,
					x.ExclusionStatus_Code
				})
				.Execute()
				.GroupBy(x => x.PropertyMarketSegment_Code)
				.ToDictionary(x => x.Key, x => x.ToList());
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
						objectsByLocation.MarketObjects.First().PropertyMarketSegment_Code.GetEnumDescription(), dealType == DealType.SaleDeal || dealType == DealType.SaleSuggestion ? "Продажа" : "Аренда");

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
					dealType == DealType.SaleDeal || dealType == DealType.SaleSuggestion ? "Продажа" : "Аренда");
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
