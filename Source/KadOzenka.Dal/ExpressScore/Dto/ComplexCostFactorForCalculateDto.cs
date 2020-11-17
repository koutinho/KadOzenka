namespace KadOzenka.Dal.ExpressScore.Dto
{
	public class ComplexCostFactorForCalculateDto : ComplexCostFactor
	{
		public ComplexCostFactorForCalculateDto(ComplexCostFactor complexCostFactor, dynamic defaultValue): base(complexCostFactor)
		{
			DefaultValue = defaultValue;
		}
		public dynamic DefaultValue { get; set; }
	}
}