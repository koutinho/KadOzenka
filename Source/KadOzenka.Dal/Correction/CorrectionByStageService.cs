using ObjectModel.Market;
using ObjectModel.Directory;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using Core.Register.QuerySubsystem;
using Core.Shared.Extensions;
using KadOzenka.Dal.Correction.Dto;
using System.Transactions;

namespace KadOzenka.Dal.Correction
{
	public class CorrectionByStageService
	{
		public void MakeCorrection(DateTime date)
		{			
			//TODO: для нового периода подтягивать данные старого периода по исключенным зданиям

			date = new DateTime(date.Year, date.Month, 1);

			//средняя цена подвальных помещений
			var objsBasement = OMCoreObject.Where(x => x.DealType_Code == DealType.SaleSuggestion
				&& x.CadastralNumber != null
				&& x.FloorNumber < 0)
				.GroupBy(x => new { x.CadastralNumber, x.PropertyMarketSegment_Code })
				.ExecuteSelect(x => new
				{
					x.CadastralNumber,
					Segment = x.PropertyMarketSegment_Code,
					Price = x.Avg(y => y.PriceAfterCorrectionByRooms ?? y.Price).Round(4)
				});

			//средняя цена надземных помещений
			var objsStage = OMCoreObject.Where(x => x.DealType_Code == DealType.SaleSuggestion
				&& x.CadastralNumber != null
				&& x.FloorNumber >= 0)
				.GroupBy(x => new { x.CadastralNumber, x.PropertyMarketSegment_Code })
				.ExecuteSelect(x => new
				{
					x.CadastralNumber,
					Segment = x.PropertyMarketSegment_Code,
					Price = x.Avg(y => y.PriceAfterCorrectionByRooms ?? y.Price).Round(4)
				});

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

			//перезапишем данные таблицы коэффициентов
			//сохраним исключенные элементы на заданную дату
			var excludedList = OMPriceCorrectionByStageHistory.Where(x => x.ChangingDate == date && x.IsExcluded == true)
				.SelectAll(false).Execute()
				.Select(x => new { CadastralNumber = x.BuildingCadastralNumber, Segment = x.MarketSegment_Code });

			//удалим и перезапишем историю на заданную дату
			DeleteHistory(date);

			foreach (var obj in ratioPrice)
			{
				bool isExcluded = excludedList.Any(x => x.CadastralNumber == obj.CadastralNumber && x.Segment == obj.Segment);
				SaveHistory(date, obj.CadastralNumber, obj.Segment, obj.Price, isExcluded);
			}

			//здания, по которым производится расчет на заданную дату
			var ratioPriceNotExcluded = OMPriceCorrectionByStageHistory.Where(x => x.ChangingDate == date && x.IsExcluded != true)
				.SelectAll(false).Execute();

			//среднее по сегменту
			Dictionary<MarketSegment, decimal> avgCoeff = ratioPriceNotExcluded.GroupBy(x => x.MarketSegment_Code)
				.ToDictionary(g => g.Key, g => g.Average(x => x.StageCoefficient));

			//все подвальные помещения
			var basements = OMCoreObject
				.Where(x => x.DealType_Code == DealType.SaleSuggestion
					&& x.CadastralNumber != null
					&& x.FloorNumber < 0)
				.Select(x => x.CadastralNumber)
				.Select(x => x.PropertyMarketSegment_Code)
				.Select(x => x.Price)
				.Select(x => x.PriceAfterCorrectionByRooms)
				.Execute();

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
			return OMPriceCorrectionByStageHistory.Where(x => x.MarketSegment_Code == (MarketSegment)marketSegmentCode)
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
}
