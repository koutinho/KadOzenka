using ObjectModel.Market;
using ObjectModel.Directory;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using Core.Register.QuerySubsystem;
using Core.Shared.Extensions;

namespace KadOzenka.Dal.Correction
{
	public class CorrectionByStage
	{
		public void MakeCorrection()
		{
			List<string> cadNumExcludeList = null;

			//средняя цена подвальных помещений
			var objsBasement = OMCoreObject.Where(x => x.DealType_Code == DealType.SaleSuggestion
				&& x.CadastralNumber != null
				&& x.FloorNumber < 0)
				.GroupBy(x => new { x.CadastralNumber, x.PropertyMarketSegment_Code })
				.ExecuteSelect(x => new
				{
					x.CadastralNumber,
					Segment = x.PropertyMarketSegment_Code,
					Price = x.Avg(y => y.Price)
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
					Price = x.Avg(y => y.Price)
				});
				
			//здания, в которых есть и подземные, и надземные помещения
			var avgPrice = objsBasement.Join(objsStage,
				x => new { x.CadastralNumber, x.Segment },
				y => new { y.CadastralNumber, y.Segment },
				(x, y) => new { x.CadastralNumber, x.Segment, Price = Math.Round((decimal)x.Price / (decimal)y.Price, 4) }).ToList();

			foreach (var obj in avgPrice)
			{
				SaveHistory(obj.CadastralNumber, obj.Segment, obj.Price);
			}

			//среднее по сегменту
			Dictionary<MarketSegment, decimal> avgKoeff = avgPrice.GroupBy(x => x.Segment)
				.ToDictionary(g => g.Key, g => g.Average(x => x.Price));

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
			var resObjs = basements.Join(avgPrice,
				x => new { x.CadastralNumber, Segment = x.PropertyMarketSegment_Code },
				y => new { y.CadastralNumber, Segment = y.Segment },
				(basement, avg) => new OMCoreObject
				{
					Id = basement.Id,
					CadastralNumber = basement.CadastralNumber,
					PropertyMarketSegment_Code = basement.PropertyMarketSegment_Code,
					Price = basement.Price,
					PriceAfterCorrectionByRooms = basement.PriceAfterCorrectionByRooms
				}).ToList();

			//перемножить средний коэффициент на стоимость подвальных помещений
			foreach (var obj in resObjs)
			{
				if (!avgKoeff.ContainsKey(obj.PropertyMarketSegment_Code))
				{
					continue;
				}
				decimal thisSegmentKoeff = avgKoeff[obj.PropertyMarketSegment_Code];
				decimal? thisPrice = obj.PriceAfterCorrectionByRooms ?? obj.Price;
				obj.PriceAfterCorrectionByStage = Math.Round(thisPrice.GetValueOrDefault() * thisSegmentKoeff, 2);
				obj.Save();
			}
		}

		private void SaveHistory(string buildingCadastralNumber, MarketSegment segment, decimal coefficient)
		{
			OMPriceCorrectionByStageHistory history = new OMPriceCorrectionByStageHistory
			{
				BuildingCadastralNumber = buildingCadastralNumber,
				MarketSegment_Code = segment,
				ChangingDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1),
				StageCoefficient = coefficient
			};
			history.Save();
		}
	}
}
