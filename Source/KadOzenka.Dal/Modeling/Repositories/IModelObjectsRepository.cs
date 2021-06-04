using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using KadOzenka.Dal.CommonFunctions;
using ObjectModel.Modeling;

namespace KadOzenka.Dal.Modeling.Repositories
{
	public interface IModelObjectsRepository : IGenericRepository<OMModelToMarketObjects>
	{
		bool AreIncludedModelObjectsExist(long? modelId, IncludedObjectsMode mode);

		List<OMModelToMarketObjects> GetIncludedModelObjects(long modelId, IncludedObjectsMode mode,
			Expression<Func<OMModelToMarketObjects, object>> selectExpression = null);
	}
}
