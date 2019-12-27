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

        public static double currentPersent = 0;
        public static bool inProgress = false;

        readonly List<OMCoreObject> AllObjects =
            OMCoreObject
                .Where(x => x.Market_Code != ObjectModel.Directory.MarketTypes.Rosreestr && x.Price > 1  && (
                            x.ProcessType_Code == ObjectModel.Directory.ProcessStep.CadastralNumberStep ||
                            x.ProcessType_Code == ObjectModel.Directory.ProcessStep.InProcess ||
                            x.ExclusionStatus_Code == ObjectModel.Directory.ExclusionStatus.Duplicate))
                .Select(x => new { x.CadastralNumber, x.DealType_Code, x.PropertyType_Code, x.Subcategory, x.ExclusionStatus_Code, x.Price, x.Area, x.ParserTime, x.DealType })
                .Execute()
                .ToList();

        public void Detect(bool useTestTable = false, double area = 0.01, double price = 0.05)
        {
	        if (useTestTable)
	        {
		        var inputObjects = OMCoreObjectTest
			        .Where(x => true)
			        .Select(x => new { x.CadastralNumber, x.DealType_Code, x.PropertyType_Code, x.Subcategory, x.ExclusionStatus_Code, x.Price, x.Area, x.ParserTime, x.DealType })
			        .Execute()
			        .ToList()
					.Select(x => x.AsIMarketObject()).ToList();
		        PerformProc(inputObjects, area, price);
			}
	        else
	        {
		        var inputObjects = AllObjects
			        .Select(x => x.AsIMarketObject()).ToList();
		        PerformProc(inputObjects, area, price);
			}
        }

	    private void PerformProc<T>(List<T> inputObjects, double area, double price) where T : IMarketObject
		{
			var objs = inputObjects.GroupBy(x => new { x.CadastralNumber, x.DealType_Code, x.PropertyType_Code, x.Subcategory }).Select(grp => grp.ToList()).ToList();
			var result = new List<List<T>>();
			objs.ForEach(x => result.AddRange(SplitListByPersent(x.OrderBy(y => y.Area).ToList(), area, price)));
			int ICur = 0, ICor = 0, IErr = 0, ICtr = result.Count;
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
					});
					x.ForEach(y => y.Save());
					ICor++;
				}
				catch (Exception) { IErr++; }
				ICur++;
                currentPersent = ((double)ICur) / ICtr * 100;
                ConsoleLog.WriteData("Проверка данных на дублинование", ICtr, ICur, ICor, IErr);
			});
            currentPersent = 0;
            inProgress = false;
            ConsoleLog.WriteFotter("Проверка данных на дублинование завершена");
		}

	    private List<List<T>> SplitListByPersent<T>(List<T> list, double area, double price) where T : IMarketObject
		{
            List<List<T>> result = new List<List<T>>();
			T FEL = list.ElementAt(0);
            int counter = 0;
            result.Add(new List<T>());
            list.ForEach(x =>
            {
                if (FEL.Area.GetValueOrDefault() >= x.Area.GetValueOrDefault() * Convert.ToDecimal(1 - area) &&
                    FEL.Price.GetValueOrDefault() >= x.Price.GetValueOrDefault() * Convert.ToDecimal(1 - price) &&
                    FEL.Price.GetValueOrDefault() <= x.Price.GetValueOrDefault() * Convert.ToDecimal(1 + price))
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