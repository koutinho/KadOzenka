using Core.Register.LongProcessManagment;
using ObjectModel.Core.LongProcess;

namespace KadOzenka.Dal.CommonFunctions
{
	public class WorkerCommonWrapper : IWorkerCommonWrapper
	{
		public void SetProgress(OMQueue queue, long? progress)
		{
			WorkerCommon.SetProgress(queue, 0);
		}

		public void SetMessage(OMQueue queue, string message)
		{
			WorkerCommon.SetMessage(queue, message);
		}
	}


	public interface IWorkerCommonWrapper
	{
		void SetProgress(OMQueue queue, long? progress);

		void SetMessage(OMQueue queue, string message);
	}
}
