using System.Collections.Generic;
using ObjectModel.Directory;

namespace KadOzenka.Dal.Modeling.Dto
{
	public class ModelingModelDto
	{
		public long ModelId { get; set; }
		public string Name { get; set; }
		public long TourId { get; set; }
		public long TourYear { get; set; }
		public MarketSegment MarketSegment { get; set; }
		public List<ModelAttributeDto> Attributes { get; set; }

		public ModelingModelDto()
		{
			Attributes = new List<ModelAttributeDto>();
		}
	}
}
