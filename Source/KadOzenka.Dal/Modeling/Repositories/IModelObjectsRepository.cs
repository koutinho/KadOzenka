using System.Collections.Generic;
using KadOzenka.Dal.CommonFunctions;
using ObjectModel.Modeling;

namespace KadOzenka.Dal.Modeling.Repositories
{
	public interface IModelObjectsRepository : IGenericRepository<OMModelToMarketObjects>
	{
		bool AreIncludedModelObjectsExist(long? modelId, IncludedObjectsMode mode);

		List<OMModelToMarketObjects> GetIncludedModelObjects(long modelId, IncludedObjectsMode mode);
	}
}
