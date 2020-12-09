
using KadOzenka.Dal.DataComparing.DataComparers.CadastralCostDataComparer;
using Serilog;

namespace KadOzenka.Dal.LongProcess.DataComparing
{
	public class CadastralCostDataComparingLongProcess : DataComparingLongProcess
	{
		public CadastralCostDataComparingLongProcess() : base(Log.ForContext<CadastralCostDataComparingLongProcess>(), new CadastralCostDataComparer()) { }
	}
}
