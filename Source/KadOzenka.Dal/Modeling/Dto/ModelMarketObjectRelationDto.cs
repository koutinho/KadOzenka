using System.Collections.Generic;

namespace KadOzenka.Dal.Modeling.Dto
{
	public class ModelMarketObjectRelationDto
	{
		public long Id { get; set; }
		public string CadastralNumber { get; set; }
		public decimal Price { get; set; }
		public bool IsExcluded { get; set; }
        public List<ModelAttributeDto> Coefficients { get; set; }
	}
}
