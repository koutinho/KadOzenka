using System.Collections.Generic;
using KadOzenka.Dal.Modeling.Modeling.Entities;
using KadOzenka.Dal.Modeling.Objects.Entities;

namespace KadOzenka.Web.Models.Modeling
{
	public class ModelingObjectsDiagramModel
	{
		public long ModelId{ get; set; }
		public TrainingSampleType TrainingSampleType { get; set; }
	}

	public class ObjectsInfoForDiagram
	{
		public decimal Average { get; set; }
		public decimal Delta { get; set; }
		public List<ObjectInfo> Info { get; set; }

		public ObjectsInfoForDiagram()
		{
			Info = new List<ObjectInfo>();
		}
	}

	public class ObjectInfo
	{
		public long Id { get; set; }
		public decimal Price { get; set; }
	}
}
