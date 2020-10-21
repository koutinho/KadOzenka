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
	public class OutliersCheckingService
	{
		private static readonly ILogger Log = Serilog.Log.ForContext<OutliersCheckingService>();
		private readonly OutliersCheckingSettingsService _outliersCheckingSettingsService;
		private Dictionary<string, OutliersCheckingSettingDto> _outliersCheckingSettings;
		protected object Locked;

		public OutliersCheckingService()
		{
			_outliersCheckingSettingsService = new OutliersCheckingSettingsService();
		}

		public void PerformOutliersChecking(MarketSegment? segment)
		{
			Log.Debug($"Старт процедуры Проверки на выбросы {(segment.HasValue ? $"для сегмента {segment.GetEnumDescription()}" : "для всех сегментов")}");
			Log.Debug("Получение настроек коэффициентов");
			_outliersCheckingSettings = _outliersCheckingSettingsService.GetOutliersCheckingSettings()
				.ToDictionary(x => x.LocationName, x => x);

			Log.Debug("Получение объектов по сегментам");
			var objectsBySegments = GetObjectsBySegments(segment);

			Locked = new object();
			foreach (var objectsBySegment in objectsBySegments)
			{
				Log.ForContext("ObjectsCountBySegment", objectsBySegment.Value.Count)
					.Debug("Начата обработка сегмента '{MarketSegment}'", objectsBySegment.Key.GetEnumDescription());

				var objectsByLocationSliceList = ObjectsByLocationSliceDto.FromEntityList(objectsBySegment.Value);

				var cancelTokenSource = new CancellationTokenSource();
				var options = new ParallelOptions
				{
					CancellationToken = cancelTokenSource.Token,
					MaxDegreeOfParallelism = 20
				};
				Parallel.ForEach(objectsByLocationSliceList, options, ProcessObjectsByLocation);

				Log.ForContext("ObjectsCountBySegment", objectsBySegment.Value.Count)
					.Debug("Обработка сегмента '{MarketSegment}' завершена", objectsBySegment.Key.GetEnumDescription());
			}
		}

		private Dictionary<MarketSegment, List<OMCoreObject>> GetObjectsBySegments(MarketSegment? segment)
		{
			return OMCoreObject.Where(x =>
					(x.ProcessType_Code == ProcessStep.InProcess || x.ProcessType_Code == ProcessStep.Dealed)
					&& x.PropertyMarketSegment != null
					&& x.Zone != null
					&& x.District != null
					&& x.Neighborhood != null
					&& (!segment.HasValue || x.PropertyMarketSegment_Code == segment))
				.Select(x => new
				{
					x.Id,
					x.CadastralNumber,
					x.Zone,
					x.District,
					x.District_Code,
					x.Neighborhood,
					x.Neighborhood_Code,
					x.PricePerMeter,
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
			if(objectsByLocation.MarketObjects.Count == 1)
				return;

			var orderedObjects = objectsByLocation.MarketObjects.OrderBy(x => x.PricePerMeter).ToList();
			var lowerRangeLimit = GetMedianPricePerMeter(orderedObjects.Take(orderedObjects.Count / 2).ToList());
			var upperRangeLimit = GetMedianPricePerMeter(orderedObjects.Take(orderedObjects.Count - orderedObjects.Count / 2).ToList());
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
			}

			foreach (var omCoreObject in orderedObjects)
			{
				if (omCoreObject.ProcessType_Code == ProcessStep.InProcess
				    && (omCoreObject.PricePerMeter < lowerRangeLimit || omCoreObject.PricePerMeter > upperRangeLimit))
				{
					omCoreObject.ProcessType_Code = ProcessStep.Excluded;
					omCoreObject.ExclusionStatus_Code = ExclusionStatus.UnacceptablePrice;
					omCoreObject.Save();
				}
			}
		}

		private decimal? GetMedianPricePerMeter(List<OMCoreObject> orderedMarketObjects)
		{
			decimal? medianPricePerMeter;
			if (orderedMarketObjects.Count % 2 == 0)
			{
				medianPricePerMeter = orderedMarketObjects[orderedMarketObjects.Count / 2].PricePerMeter;
			}
			else
			{
				medianPricePerMeter = (orderedMarketObjects[orderedMarketObjects.Count / 2].PricePerMeter +
				                       orderedMarketObjects[orderedMarketObjects.Count / 2 + 1].PricePerMeter) / 2;
			}

			return medianPricePerMeter;
		}
	}
}
