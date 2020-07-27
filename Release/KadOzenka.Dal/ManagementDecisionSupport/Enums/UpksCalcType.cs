using System.ComponentModel;

namespace KadOzenka.Dal.ManagementDecisionSupport.Enums
{
	public enum UpksCalcType
	{
		[Description("Минимальное")]
		Min,
		[Description("Среднее (арифметическое)")]
		Average,
		[Description("Среднее (взвешенное)")]
		AverageWeight,
		[Description("Максимальное")]
		Max
	}
}
