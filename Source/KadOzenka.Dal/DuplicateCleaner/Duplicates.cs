﻿using System;
using System.Linq;
using System.Collections.Generic;
using ObjectModel.Market;
using KadOzenka.Dal.Logger;
using Newtonsoft.Json;
using ObjectModel.Directory;
using Core.Shared.Extensions;
using MarketPlaceBusiness;
using MarketPlaceBusiness.Interfaces;

namespace KadOzenka.Dal.DuplicateCleaner
{
	public class Duplicates
    {
	    private static double currentProgress = 0;
        private static bool inProgress = false;
        private double areaDelta = 0.01;
        private double priceDelta = 0.05;
        private int selectedMarket = 0;

        public double AreaDelta
        {
            get => areaDelta;
            set => areaDelta = value >= 1 ? value / 100 : value;
        }

        public double PriceDelta
        {
            get => priceDelta;
            set => priceDelta = value >= 1 ? value / 100 : value;
        }

        public int SelectedMarket
        {
            get => selectedMarket;
            set => selectedMarket = value;
        }

        public static double CurrentProgress
        {
            get => currentProgress;
            set => currentProgress = value;
        }

        public static bool InProgress
        {
            get => inProgress;
            set => inProgress = value;
        }

        public IMarketObjectService MarketObjectService { get; set; }

        public Duplicates()
        {
	        MarketObjectService = new MarketObjectService();
        }

        public Duplicates(double areaDelta, double priceDelta, int selectedMarket) : this()
        {
            AreaDelta = areaDelta;
            PriceDelta = priceDelta;
            SelectedMarket = selectedMarket;
        }

        public void Detect(bool logData = true) => PerformProc(logData);

        private OMDuplicatesHistory FormHistory(DateTime dateTime, string marketSegment, decimal areaDelta, decimal priceDelta, int commonCount, int inProgressCount, int duplicateObjects)
        {
            OMDuplicatesHistory result = new OMDuplicatesHistory();
            result.CheckDate = dateTime;
            result.MarketSegment = marketSegment;
            result.AreaDelta = areaDelta;
            result.PriceDelta = priceDelta;
            result.CommonCount = commonCount;
            result.InProgressCount = inProgressCount;
            result.DuplicateObjects = duplicateObjects;
            return result;
        }

	    private void PerformProc(bool logData = true)
		{
			var inputObjects = MarketObjectService.GetObjectsForDuplicatesChecking();

            var objs = inputObjects.GroupBy(x => new { x.CadastralNumber, x.DealType_Code, x.PropertyTypesCIPJS_Code, x.PropertyMarketSegment_Code }).Select(grp => grp.ToList()).ToList();
			var result = new List<List<OMCoreObject>>();
			objs.ForEach(x => result.AddRange(SplitListByPersent(x.OrderBy(y => y.Area).ToList())));
			int ICur = 0, ICor = 0, IErr = 0, ICtr = result.Count, IDup = 0;
			result.ForEach(x =>
			{
				try
				{
					x.First().ProcessType_Code = ObjectModel.Directory.ProcessStep.InProcess;
					x.First().ExclusionStatus_Code = ObjectModel.Directory.ExclusionStatus.None;
					x.Skip(1).ToList().ForEach(y =>
					{
						y.ProcessType_Code = ObjectModel.Directory.ProcessStep.Excluded;
						y.ExclusionStatus_Code = ObjectModel.Directory.ExclusionStatus.Duplicate;
                        IDup++;
                    });
					x.ForEach(y => y.Save());
					ICor++;
				}
				catch (Exception) { IErr++; }
				ICur++;
                currentProgress = ((double)ICur) / ICtr * 100;
                new WebSocket.SocketPool().BroadCastMessage(GetCurrentProgress());
                if(logData)  ConsoleLog.WriteData("Проверка данных на дублинование", ICtr, ICur, ICor, IErr);
			});
            currentProgress = 0;
            inProgress = false;
            FormHistory(DateTime.Now, "Все сегменты", Convert.ToDecimal(AreaDelta), Convert.ToDecimal(PriceDelta), inputObjects.Count, ICor, IDup).Save();
            new WebSocket.SocketPool().BroadCastMessage(GetCurrentProgress());
            ConsoleLog.WriteFotter("Проверка данных на дублинование завершена");
		}

        private List<List<OMCoreObject>> SplitListByPersent(List<OMCoreObject> list)
		{
            List<List<OMCoreObject>> buffer = new(), result = new();
            var FEL = list.ElementAt(0);
            int counter = 0;
            buffer.Add(new List<OMCoreObject>());
            list.ForEach(x =>
            {
                if (FEL.Area.GetValueOrDefault() >= x.Area.GetValueOrDefault() * Convert.ToDecimal(1 - AreaDelta) &&
                    FEL.Price.GetValueOrDefault() >= x.Price.GetValueOrDefault() * Convert.ToDecimal(1 - PriceDelta) &&
                    FEL.Price.GetValueOrDefault() <= x.Price.GetValueOrDefault() * Convert.ToDecimal(1 + PriceDelta))
                    buffer.ElementAt(counter).Add(x);
                else
                {
                    FEL = x;
                    counter++;
                    buffer.Add(new List<OMCoreObject>());
                    buffer.ElementAt(counter).Add(FEL);
                }
            });
            buffer.ForEach(x => result.Add(x.OrderBy(y => y.Market_Code != (MarketTypes)SelectedMarket).ThenByDescending(y => y.ParserTime.GetValueOrDefault()).ToList()));
            return result;
        }

        public static string GetCurrentProgress()
        {
            OMDuplicatesHistory history = OMDuplicatesHistory.Where(x => x.AreaDelta != null && x.PriceDelta != null).SelectAll().OrderByDescending(x => x.CheckDate).ExecuteFirstOrDefault();
            return JsonConvert.SerializeObject(new
            {
                checkDate = history == null ? null : history.CheckDate?.ToString("yyyy.MM.dd HH:mm:ss"),
                marketSegment = history == null ? null : history.MarketSegment,
                areaDelta = history == null ? null : $"{(int)(history.AreaDelta * 100)}&nbsp;%",
                priceDelta = history == null ? null : $"{(int)(history.PriceDelta * 100)}&nbsp;%",
                commonCount = history == null ? null : history.CommonCount,
                inProgressCount = history == null ? null : history.InProgressCount,
                duplicateCount = history == null ? null : history.DuplicateObjects,
                currentProgress = CurrentProgress,
                inProgress = InProgress
            });
        }

        public static string GetListOfMarkets()
        {
            List<object> result = new List<object>();
            foreach (MarketTypes marketType in MarketTypes.GetValues(typeof(MarketTypes)).Cast<MarketTypes>().Take(4))
                result.Add(new { value=(int)marketType, description=marketType.GetEnumDescription()});
            return JsonConvert.SerializeObject(new { ListOfMarkets = result });
        }

    }

}