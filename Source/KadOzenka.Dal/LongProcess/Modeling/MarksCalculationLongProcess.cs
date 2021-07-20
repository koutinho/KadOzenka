using System;
using System.Threading;
using Core.Register.LongProcessManagment;
using KadOzenka.Dal.LongProcess.Common;
using ObjectModel.Core.LongProcess;
using ObjectModel.KO;

namespace KadOzenka.Dal.LongProcess.Modeling
{
    public class MarksCalculationLongProcess : LongProcess
    {
	    private static long ProcessId => 100;
		

		public static void AddProcessToQueue(long modelId)
        {
	        if (modelId == 0)
		        throw new Exception("Не передан ИД модели");

			var isProcessExists = new LongProcessService().HasActiveProcessInQueue(ProcessId, modelId);
	        if (isProcessExists)
		        throw new Exception("Процесс расчета меток уже находится в очереди");

			LongProcessManager.AddTaskToQueue(nameof(MarksCalculationLongProcess), objectId: modelId, registerId: OMModel.GetRegisterId());
        }


        public override void StartProcess(OMProcessType processType, OMQueue processQueue, CancellationToken cancellationToken)
		{
			
		}
    }
}
