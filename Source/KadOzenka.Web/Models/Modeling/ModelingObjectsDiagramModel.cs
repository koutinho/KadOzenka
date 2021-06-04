using ObjectModel.Modeling;

namespace KadOzenka.Web.Models.Modeling
{
	public class ModelingObjectsDiagramModel
	{
	    public long Id { get; set; }
	    public decimal Price { get; set; }


		public static ModelingObjectsDiagramModel ToModel(OMModelToMarketObjects obj)
		{
			return new()
			{
				Id = obj.Id,
				Price = obj.Price
			};
        }
    }
}
