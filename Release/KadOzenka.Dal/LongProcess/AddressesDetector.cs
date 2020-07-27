using System;
using System.Linq;
using System.Text;
using System.Threading;
using System.Collections.Generic;

using Core.Register.LongProcessManagment;
using ObjectModel.Gbu.GroupingAlgoritm;
using ObjectModel.Core.LongProcess;
using ObjectModel.Market;

namespace KadOzenka.Dal.LongProcess
{

    public class AddressesDetector : LongProcess
    {

        public const string LongProcessName = "AddressesDetector";

        public static void AddProcessToQueue() => LongProcessManager.AddTaskToQueue(LongProcessName, null, null);

        public static bool IsProcessAdditable() => OMQueue.Where(x => x.ProcessTypeId == 35 && x.EndDate == null).ExecuteCount() == 0;

        public override void StartProcess(OMProcessType processType, OMQueue processQueue, CancellationToken cancellationToken)
        {
            WorkerCommon.SetProgress(processQueue, 0);
            List<OMCoreObject> AllObjects =
            OMCoreObject.Where(x => x.ZoneRegion == null && (x.ProcessType_Code == ObjectModel.Directory.ProcessStep.InProcess || x.ProcessType_Code == ObjectModel.Directory.ProcessStep.Dealed))
                        .Select(x => new { x.District_Code, x.Neighborhood_Code, x.Neighborhood, x.ZoneRegion, x.Market_Code, x.Market, x.CadastralQuartal, x.Zone })
                        .Execute().ToList();
            List<OMQuartalDictionary> QuartalDictionary = OMQuartalDictionary.Where(x => true).SelectAll().Execute().ToList();
            int allCount = AllObjects.Count, current = 0;
            AllObjects.ForEach(x =>
            {
                OMQuartalDictionary dictionary = QuartalDictionary.Where(y => x.CadastralQuartal.Equals(y.CadastralQuartal)).FirstOrDefault();
                if (dictionary != null)
                {
                    x.District_Code = dictionary.District_Code;
                    x.Neighborhood_Code = dictionary.Region_Code;
                    x.ZoneRegion = dictionary.ZoneRegion;
                    x.Zone = dictionary.Zone;
                    x.Save();
                }
                WorkerCommon.SetProgress(processQueue, (int)((double)++current / allCount * 100));
            });
        }

    }

}