using System.Collections.Generic;

namespace ModelingBusiness.Objects
{
	public class ModelingObjectsStatistic
	{
		public int TotalCount { get; set; }
		public List<ObjectsByAttributeStatistic> ObjectsByAttributeStatistics { get; set; }
	}

	public class ObjectsByAttributeStatistic
	{
		public long AttributeId { get; set; }
		public string AttributeName { get; set; }
		public int Count { get; set; }
		public decimal? Percent { get; set; }
	}
}
