using System;
using System.Collections.Generic;
using System.Linq;
using ObjectModel.Market;

namespace KadOzenka.Web.Models.MarketObject
{
	public class PriceHistoryChartModel
	{
		public DateTime ChangingDate { get; set; }
		public long PriceValue { get; set; }
		public long? ScreenshotId { get; set; }
		public bool IsPriceUp { get; set; }
		public bool IsPriceDown { get; set; }

		public static List<PriceHistoryChartModel> FromEntities(List<OMPriceHistory> priceHistories, List<OMScreenshots> screenshots)
		{
			var priceHistoryChartModels = new List<PriceHistoryChartModel>();
			var orderedHistories = priceHistories.OrderBy(x => x.ChangingDate).ToList();
			for (var i = 0; i < orderedHistories.Count(); i++)
			{
				var screenshot = screenshots.FirstOrDefault(x => x.CreationDate == orderedHistories[i].ChangingDate);
				var priceHistoryChartModel = new PriceHistoryChartModel
				{
					ChangingDate = orderedHistories[i].ChangingDate,
					PriceValue = orderedHistories[i].PriceValueTo,
					ScreenshotId = screenshot?.Id,
					IsPriceUp = i > 0 && orderedHistories[i - 1].PriceValueTo < orderedHistories[i].PriceValueTo 
						? true
						: false,
					IsPriceDown = i > 0 && orderedHistories[i - 1].PriceValueTo > orderedHistories[i].PriceValueTo
						? true
						: false
				};

				priceHistoryChartModels.Add(priceHistoryChartModel);
			}

			return priceHistoryChartModels;
		}
	}
}
