using System.Collections.Generic;
using KadOzenka.Dal.CommonFunctions;
using ObjectModel.Modeling;

namespace KadOzenka.Dal.Modeling.Repositories
{
	public interface IModelObjectsRepository : IGenericRepository<OMModelToMarketObjects>
	{
		bool AreIncludedModelObjectsExist(long? modelId, bool isForTraining);

		List<OMModelToMarketObjects> GetIncludedModelObjects(long modelId, bool isForTraining);
	}
}
