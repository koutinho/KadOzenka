using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KadOzenka.BlFrontEnd.Logger;
using ObjectModel.Market;

namespace KadOzenka.BlFrontEnd.ClearDuplicates
{
    class DuplicateCleaner
    {
        readonly List<OMCoreObject> AllObjects =
            OMCoreObject
                .Where(x => x.Market_Code != ObjectModel.Directory.MarketTypes.Rosreestr && (
                            x.ProcessType_Code == ObjectModel.Directory.ProcessStep.CadastralNumberStep || 
                            x.ProcessType_Code == ObjectModel.Directory.ProcessStep.InProcess || 
                            x.ExclusionStatus_Code == ObjectModel.Directory.ExclusionStatus.Duplicate))
                .Select(x => new {x.CadastralNumber, x.DealType_Code, x.PropertyType_Code, x.ExclusionStatus_Code, x.Price, x.Area, x.ParserTime, x.DealType})
                .Execute()
                .ToList();

        public void Launch()
        {
            List<List<OMCoreObject>> objs = AllObjects.GroupBy(x => new {x.CadastralNumber, x.DealType_Code, x.PropertyType_Code}).Select(grp => grp.ToList()).ToList();
            List<List<OMCoreObject>> result = new List<List<OMCoreObject>>();
            objs.ForEach(x => result.AddRange(SplitListByPersent(x.OrderBy(y => y.Area).ToList())));
            int ICur = 0, ICor = 0, IErr = 0, ICtr = result.Count;
            result.ForEach(x => 
            {
                try
                {
                    x.First().ProcessType_Code = ObjectModel.Directory.ProcessStep.InProcess;
                    x.Skip(1).ToList().ForEach(y => y.ExclusionStatus_Code = ObjectModel.Directory.ExclusionStatus.Duplicate);
                    x.ForEach(y => y.Save());
                    ICor++;
                }
                catch (Exception) { IErr++; }
                ICur++;
                LogData.WriteData("Проверка данных на дублинование", ICtr, ICur, ICor, IErr);
            });
            LogData.WriteFotter("Проверка данных на дублинование завершена");
        }

        private List<List<OMCoreObject>> SplitListByPersent(List<OMCoreObject> list)
        {
            List<List<OMCoreObject>> result = new List<List<OMCoreObject>>();
            OMCoreObject FEL = list.ElementAt(0);
            int counter = 0;
            result.Add(new List<OMCoreObject>());
            list.ForEach(x => 
            {
                if (FEL.Area.GetValueOrDefault() >= x.Area.GetValueOrDefault() * 0.99m && 
                    FEL.Price.GetValueOrDefault() >= x.Price.GetValueOrDefault() * 0.95m && 
                    FEL.Price.GetValueOrDefault() <= x.Price.GetValueOrDefault() * 1.05m)
                result.ElementAt(counter).Add(x);
                else
                {
                    FEL = x;
                    counter++;
                    result.Add(new List<OMCoreObject>());
                    result.ElementAt(counter).Add(FEL);
                }
            });
            result.ForEach(x => x.Sort((y, z) => DateTime.Compare(z.ParserTime.GetValueOrDefault(), y.ParserTime.GetValueOrDefault())));
            return result;
        }

    }
}