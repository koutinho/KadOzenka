using KadOzenka.Dal.DataComparing.DataComparers;
using Serilog;

namespace KadOzenka.Dal.LongProcess.DataComparing
{
	public class TaskChangesDataComparingLongProcess : DataComparingLongProcess
	{
		public TaskChangesDataComparingLongProcess() : base(Log.ForContext<TaskChangesDataComparingLongProcess>(), new TaskChangesDataComparer()) { }
	}
}
