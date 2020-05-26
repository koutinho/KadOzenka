using System.Collections.Generic;

namespace KadOzenka.Dal.Modeling.Dto
{
	public class ModelingModelDto
	{
		public long ModelId { get; set; }
		public string Name { get; set; }
		public long TourId { get; set; }
		public long TourYear { get; set; }
        public long GroupId { get; set; }
        public string GroupName { get; set; }
        public bool WasTrained { get; set; }
        public bool IsOksObjectType { get; set; }
        public List<ModelAttributeRelationDto> Attributes { get; set; }

        public ModelingModelDto()
		{
			Attributes = new List<ModelAttributeRelationDto>();
		}
	}
}
