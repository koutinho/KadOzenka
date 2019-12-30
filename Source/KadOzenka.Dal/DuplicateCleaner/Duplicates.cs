using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;

using ObjectModel.Market;
using KadOzenka.Dal.Logger;

namespace KadOzenka.Dal.DuplicateCleaner
{

    public class Duplicates
    {

        private static double currentProgress = 0;
        private static bool inProgress = false;
        private double areaDelta = 0.01;
        private double priceDelta = 0.05;

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

        public Duplicates() { }

        public Duplicates(double areaDelta, double priceDelta)
        {
            AreaDelta = areaDelta;
            PriceDelta = priceDelta;
        }

        readonly List<OMCoreObject> AllObjects =
            OMCoreObject
                .Where(x => x.Market_Code != ObjectModel.Directory.MarketTypes.Rosreestr && 
                            x.LastDateUpdate != null &&
                            x.Price > 1 && (
                            x.ProcessType_Code == ObjectModel.Directory.ProcessStep.CadastralNumberStep ||
                            x.ProcessType_Code == ObjectModel.Directory.ProcessStep.InProcess ||
                            x.ExclusionStatus_Code == ObjectModel.Directory.ExclusionStatus.Duplicate))
                .Select(x => new { x.CadastralNumber, x.DealType_Code, x.PropertyType_Code, x.Subcategory, x.ExclusionStatus_Code, x.Price, x.Area, x.ParserTime, x.DealType })
                .Execute()
                .ToList();

        public void Detect(bool useTestTable = false)
        {
	        if (useTestTable)
	        {
		        var inputObjects = OMCoreObjectTest
			        .Where(x => true)
			        .Select(x => new { x.CadastralNumber, x.DealType_Code, x.PropertyType_Code, x.Subcategory, x.ExclusionStatus_Code, x.Price, x.Area, x.ParserTime, x.DealType })
			        .Execute()
			        .ToList()
					.Select(x => x.AsIMarketObject()).ToList();
		        PerformProc(inputObjects);
			}
	        else
	        {
		        var inputObjects = AllObjects.Select(x => x.AsIMarketObject()).ToList();
		        PerformProc(inputObjects);
			}
        }

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

	    private void PerformProc<T>(List<T> inputObjects) where T : IMarketObject
		{
			var objs = inputObjects.GroupBy(x => new { x.CadastralNumber, x.DealType_Code, x.PropertyType_Code, x.Subcategory }).Select(grp => grp.ToList()).ToList();
			var result = new List<List<T>>();
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
                ConsoleLog.WriteData("Проверка данных на дублинование", ICtr, ICur, ICor, IErr);
			});
            currentProgress = 0;
            inProgress = false;
            FormHistory(DateTime.Now, "Все сегменты", Convert.ToDecimal(AreaDelta), Convert.ToDecimal(PriceDelta), AllObjects.Count, ICor, IDup).Save();
            ConsoleLog.WriteFotter("Проверка данных на дублинование завершена");
		}

	    private List<List<T>> SplitListByPersent<T>(List<T> list) where T : IMarketObject
		{
            List<List<T>> result = new List<List<T>>();
			T FEL = list.ElementAt(0);
            int counter = 0;
            result.Add(new List<T>());
            list.ForEach(x =>
            {
                if (FEL.Area.GetValueOrDefault() >= x.Area.GetValueOrDefault() * Convert.ToDecimal(1 - AreaDelta) &&
                    FEL.Price.GetValueOrDefault() >= x.Price.GetValueOrDefault() * Convert.ToDecimal(1 - PriceDelta) &&
                    FEL.Price.GetValueOrDefault() <= x.Price.GetValueOrDefault() * Convert.ToDecimal(1 + PriceDelta))
                    result.ElementAt(counter).Add(x);
                else
                {
                    FEL = x;
                    counter++;
                    result.Add(new List<T>());
                    result.ElementAt(counter).Add(FEL);
                }
            });
            result.ForEach(x => x.Sort((y, z) => DateTime.Compare(z.ParserTime.GetValueOrDefault(), y.ParserTime.GetValueOrDefault())));
            return result;
        }

    }

}