using System.Collections.Generic;

namespace KadOzenka.Dal.Modeling.Dto
{
	public class CalculationSet
	{
		public List<string> AttributeNames { get; set; }
		public List<decimal> Coefficients { get; set; }

		public CalculationSet()
		{
			AttributeNames = new List<string>();
			Coefficients = new List<decimal>();
		}
	}
}
