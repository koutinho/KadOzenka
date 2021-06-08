using KadOzenka.Dal.Modeling.Entities;

namespace KadOzenka.Web.Models.Modeling
{
	public class ModelingObjectsDiagramModel
	{
		public long ModelId{ get; set; }
		public TrainingSampleType TrainingSampleType { get; set; }
	}


	public class ObjectInfoForDiagram
	{
		public long Id { get; set; }
		public decimal Price { get; set; }
	}
}
