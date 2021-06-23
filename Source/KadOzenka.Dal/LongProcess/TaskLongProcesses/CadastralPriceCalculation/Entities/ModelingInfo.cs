using System.Collections.Generic;

namespace KadOzenka.Dal.LongProcess.TaskLongProcesses.CadastralPriceCalculation.Entities
{
	public class ModelingInfo
	{
		public string Formula { get; set; }
		public List<FactorInfo> Factors { get; set; }
	}
}