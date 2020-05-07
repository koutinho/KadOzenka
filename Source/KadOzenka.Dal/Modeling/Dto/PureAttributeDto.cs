using System.Collections.Generic;

namespace KadOzenka.Dal.Modeling.Dto
{
	public class PureAttributeDto
	{
		public List<string> AttributeNames { get; set; }
		public List<decimal> Coefficients { get; set; }

		public PureAttributeDto()
		{
			AttributeNames = new List<string>();
			Coefficients = new List<decimal>();
		}
	}
}
