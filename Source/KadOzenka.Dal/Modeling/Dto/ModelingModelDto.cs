using System.Collections.Generic;
using ObjectModel.Directory;

namespace KadOzenka.Dal.Modeling.Dto
{
	public class Attributes
	{
		public long? AttributeId { get; set; }
		public string AttributeName { get; set; }
		public long? DictionaryId { get; set; }
		public string DictionaryName { get; set; }
		public bool IsNormalized => DictionaryId != null;
	}

	 public class ModelingModelDto
	{
		public long ModelId { get; set; }
		public string Name { get; set; }
		public long TourId { get; set; }
		public MarketSegment MarketSegment { get; set; }
		public List<Attributes> Attributes { get; set; }

		public ModelingModelDto()
		{
			Attributes = new List<Attributes>();
		}
	}
}
