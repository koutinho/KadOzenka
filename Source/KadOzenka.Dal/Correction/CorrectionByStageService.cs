using ObjectModel.Market;
using ObjectModel.Directory;
using System.Linq;
using System;
using System.Collections.Generic;
using Core.Shared.Extensions;
using KadOzenka.Dal.Correction.Dto;
using Core.Register.LongProcessManagment;
using MarketPlaceBusiness;
using MarketPlaceBusiness.Interfaces.Corrections;
using ObjectModel.Core.LongProcess;
using ObjectModel.Directory.MarketObjects;

namespace KadOzenka.Dal.Correction
{
	public class CorrectionByStageService : CorrectionBaseService
	{
		readonly OMQueue processQueue;
		public List<MarketSegment> CalculatedMarketSegments => new List<MarketSegment>() { MarketSegment.Office, MarketSegment.Trading, MarketSegment.MZHS };
		public IMarketObjectsForCorrectionByStage MarketObjectsService { get; }

		public CorrectionByStageService(OMQueue queue)
		{
			processQueue = queue;
			MarketObjectsService = new MarketObjectsForCorrectionsService();
		}

		public CorrectionByStageService()
		{
		    CorrectionSettingsService = new CorrectionSettingsService();
        }

		public void MakeCorrection(DateTime date)
		{
			WorkerCommon.SetProgress(processQueue, 0);

			date = new DateTime(date.Year, date.Month, 1);

			//средняя цена подвальных помещений
			var objsBasement = MarketObjectsService.GetObjects(false, CalculatedMarketSegments);

			//средняя цена надземных помещений
			var objsStage = MarketObjectsService.GetObjects(true, CalculatedMarketSegments);

			objsBasement = objsBasement.Where(x => x.Price > 0).ToList();
			objsStage = objsStage.Where(x => x.Price > 0).ToList();

			//здания, в которых есть и подземные, и надземные помещения
			var ratioPrice = objsBasement.Join(objsStage,
				x => new { x.CadastralNumber, x.Segment },
				y => new { y.CadastralNumber, y.Segment },
				(x, y) => new
				{
					x.CadastralNumber,
					x.Segment,
					Price = Math.Round((decimal)x.Price / (decimal)y.Price, 4)
				}).ToList();
			
			WorkerCommon.SetProgress(processQueue, 20);

			//проверка, что данный период обрабатывался ранее
			bool thisPeriodExists = OMPriceCorrectionByStageHistory.Where(x => x.ChangingDate == date && CalculatedMarketSegments.Contains(x.MarketSegment_Code))
				.Select(x => x.Id).ExecuteExists();

			List<CadSegment> excludedList;

			if (thisPeriodExists)
			{
				//сохраним исключенные элементы на заданную дату
				excludedList = OMPriceCorrectionByStageHistory.Where(x => x.ChangingDate == date && x.IsExcluded == true && CalculatedMarketSegments.Contains(x.MarketSegment_Code))
					.SelectAll(false).Execute()
					.Select(x => new CadSegment
					{
						CadastralNumber = x.BuildingCadastralNumber,
						Segment = x.MarketSegment_Code
					}).ToList();

				//удалим и перезапишем историю на заданную дату
				DeleteHistory(date);
			}
			else
			{
				//берем данные предыдущего периода
				DateTime prevDate = date.AddMonths(-1);
				excludedList = OMPriceCorrectionByStageHistory.Where(x => x.ChangingDate == prevDate && x.IsExcluded == true && CalculatedMarketSegments.Contains(x.MarketSegment_Code))
					.SelectAll(false).Execute()
					.Select(x => new CadSegment
					{
						CadastralNumber = x.BuildingCadastralNumber,
						Segment = x.MarketSegment_Code
					}).ToList();
			}

			foreach (var obj in ratioPrice)
			{
				bool isExcluded = excludedList.Any(x => x.CadastralNumber == obj.CadastralNumber && x.Segment == obj.Segment);
				SaveHistory(date, obj.CadastralNumber, obj.Segment, obj.Price, isExcluded);
			}
			
			WorkerCommon.SetProgress(processQueue, 60);

            //здания, по которым производится расчет на заданную дату
		    var settings = CorrectionSettingsService.GetCorrectionSettings(CorrectionTypes.CorrectionByStage);
		    var ratioPriceNotExcluded = OMPriceCorrectionByStageHistory.Where(x =>
		            x.ChangingDate == date
		            && (x.IsExcluded == false || x.IsExcluded == null)
		            && CalculatedMarketSegments.Contains(x.MarketSegment_Code)
		            && ((!settings.LowerLimitForCoefficient.HasValue || x.StageCoefficient >= settings.LowerLimitForCoefficient.Value)
		                && (!settings.UpperLimitForCoefficient.HasValue || x.StageCoefficient <= settings.UpperLimitForCoefficient.Value)))
		        .SelectAll(false).Execute();

            //среднее по сегменту
            Dictionary<MarketSegment, decimal> avgCoeff = ratioPriceNotExcluded.GroupBy(x => x.MarketSegment_Code)
				.ToDictionary(g => g.Key, g => g.Average(x => x.StageCoefficient));

			//все подвальные помещения
			var basements = MarketObjectsService.GetBasementObjects(CalculatedMarketSegments);

			//из них отобраны те, по которым делался расчет
			var resObjs = basements.Join(ratioPriceNotExcluded,
				x => new { CadastralNumber = x.CadastralNumber, Segment = x.PropertyMarketSegment_Code },
				y => new { CadastralNumber = y.BuildingCadastralNumber, Segment = y.MarketSegment_Code },
				(basement, avg) => new OMCoreObject
				{
					Id = basement.Id,
					CadastralNumber = basement.CadastralNumber,
					PropertyMarketSegment_Code = basement.PropertyMarketSegment_Code,
					Price = basement.Price,
					PriceAfterCorrectionByRooms = basement.PriceAfterCorrectionByRooms
				}).ToList();

			//обнуляем предыдущие значения
			basements.ForEach(basement =>
			{
				basement.PriceAfterCorrectionByStage = null;
				basement.Save();
			});

			WorkerCommon.SetProgress(processQueue, 75);

			//перемножаем средний коэффициент на стоимость подвальных помещений
			foreach (var obj in resObjs)
			{
				if (!avgCoeff.ContainsKey(obj.PropertyMarketSegment_Code))
				{
					continue;
				}
				decimal thisSegmentKoeff = avgCoeff[obj.PropertyMarketSegment_Code];
				decimal thisPrice = obj.PriceAfterCorrectionByRooms ?? obj.Price ?? 0;
				obj.PriceAfterCorrectionByStage = Math.Round(thisPrice * thisSegmentKoeff, 2);
				obj.Save();
			}

			WorkerCommon.SetProgress(processQueue, 100);
		}

	    public bool IsCoefIncludedInCalculationLimit(decimal? coefficient)
	    {
	        var settings = CorrectionSettingsService.GetCorrectionSettings(CorrectionTypes.CorrectionByStage);
	        var result = (!settings.LowerLimitForCoefficient.HasValue || coefficient >= settings.LowerLimitForCoefficient.Value)
	                     && (!settings.UpperLimitForCoefficient.HasValue || coefficient <= settings.UpperLimitForCoefficient.Value);

	        return result;
	    }

        private void SaveHistory(DateTime date, string buildingCadastralNumber, MarketSegment segment, decimal coefficient, bool isExcluded)
		{
			OMPriceCorrectionByStageHistory history = new OMPriceCorrectionByStageHistory
			{
				BuildingCadastralNumber = buildingCadastralNumber,
				MarketSegment_Code = segment,
				IsExcluded = isExcluded,
				ChangingDate = date,				
				StageCoefficient = coefficient
			};
			history.Save();
		}

		private void DeleteHistory(DateTime date)
		{
			var historyList = OMPriceCorrectionByStageHistory.Where(x => x.ChangingDate == date)
				.Select(x => x.Id).Execute();

			foreach (var history in historyList)
			{
				history.Destroy();
			}
		}

		public List<CorrectionByStageHistoryDto> GetGeneralHistory(long marketSegmentCode)
		{
		    if (!CalculatedMarketSegments.Contains((MarketSegment)marketSegmentCode))
		    {
		        throw new Exception($"Данная корректировка определяется только для сегментов: {string.Join(", ", CalculatedMarketSegments.Select(x => x.GetEnumDescription()).ToList())}");
		    }
		    var settings = CorrectionSettingsService.GetCorrectionSettings(CorrectionTypes.CorrectionByStage);

            return OMPriceCorrectionByStageHistory.Where(x => x.MarketSegment_Code == (MarketSegment)marketSegmentCode
                                                              && (x.IsExcluded == false || x.IsExcluded == null) 
                                                              && ((!settings.LowerLimitForCoefficient.HasValue || x.StageCoefficient >= settings.LowerLimitForCoefficient.Value)
                                                                  && (!settings.UpperLimitForCoefficient.HasValue || x.StageCoefficient <= settings.UpperLimitForCoefficient.Value)))
				.OrderByDescending(x => x.ChangingDate)
				.SelectAll().Execute().GroupBy(x => x.ChangingDate).Select(
					group => new CorrectionByStageHistoryDto
					{
						Date = group.Key,
						StageCoefficient = Math.Round(group.ToList().Average(x => x.StageCoefficient),	4)
					}).ToList();
		}

		public List<CorrectionByStageHistoryDto> GetDetailedHistory(long marketSegmentCode, DateTime date)
		{
		    if (!CalculatedMarketSegments.Contains((MarketSegment)marketSegmentCode))
		    {
		        throw new Exception($"Данная корректировка определяется только для сегментов: {string.Join(", ", CalculatedMarketSegments.Select(x => x.GetEnumDescription()).ToList())}");
		    }

            return OMPriceCorrectionByStageHistory.Where(x =>
					x.MarketSegment_Code == (MarketSegment)marketSegmentCode && x.ChangingDate == date)
				.OrderBy(x => x.BuildingCadastralNumber)
				.SelectAll().Execute().Select(
					x => new CorrectionByStageHistoryDto
					{
						Id = x.Id,
						BuildingCadastralNumber = x.BuildingCadastralNumber,
						StageCoefficient = x.StageCoefficient,
						IsExcludedFromCalculation = x.IsExcluded.GetValueOrDefault()
					}).ToList();
		}

		public bool ChangeBuildingsStatusInCalculation(List<CorrectionByStageHistoryDto> historyRecords)
		{
			if (historyRecords.Count == 0)
				return false;

			var isDataUpdated = false;
			historyRecords.ForEach(record =>
			{
				var recordFromDb = OMPriceCorrectionByStageHistory.Where(x => x.Id == record.Id).SelectAll().ExecuteFirstOrDefault();
				if (recordFromDb == null)
					return;

				if (recordFromDb.IsExcluded != record.IsExcludedFromCalculation)
				{
					isDataUpdated = true;

					recordFromDb.IsExcluded = record.IsExcludedFromCalculation;
					recordFromDb.Save();
				}
			});

			return isDataUpdated;
		}

	}

	class CadSegment
	{
		public string CadastralNumber { get; set; }
		public MarketSegment Segment { get; set; }
	}
}
