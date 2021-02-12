using System.Collections.Generic;
using System.ComponentModel;

namespace KadOzenka.Dal.LongProcess.Reports.CadastralCostDeterminationResults.Entities
{
	public class ReportLongProcessInputParameters
	{
		public List<long> TaskIds { get; set; }
		public ReportType Type { get; set; }
	}

	public enum ReportType
	{
		[Description("Результаты государственной кадастровой оценки объектов недвижимости")]
		State,
		[Description("Сведения о результатах определения кадастровой стоимости объектов недвижимости, кадастровая стоимость которых определена индивидуально")]
		Individual
	}
}
