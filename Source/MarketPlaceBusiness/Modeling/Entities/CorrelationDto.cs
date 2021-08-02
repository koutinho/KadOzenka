using System.Collections.Generic;

namespace MarketPlaceBusiness.Modeling.Entities
{
	public class CorrelationDto
	{
		public List<string> AttributeNames { get; set; }
		public List<List<decimal?>> Coefficients { get; set; }

		public CorrelationDto()
		{
			AttributeNames = new List<string>();
			Coefficients = new List<List<decimal?>>();
		}
	}
}
