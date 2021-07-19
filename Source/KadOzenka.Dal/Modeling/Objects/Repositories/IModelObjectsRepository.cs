using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using CommonSdks;
using KadOzenka.Dal.CommonFunctions;
using KadOzenka.Dal.Modeling.Modeling.Entities;
using KadOzenka.Dal.Modeling.Objects.Entities;
using ObjectModel.Modeling;

namespace KadOzenka.Dal.Modeling.Objects.Repositories
{
	public interface IModelObjectsRepository : IGenericRepository<OMModelToMarketObjects>
	{
		bool AreIncludedModelObjectsExist(long? modelId, IncludedObjectsMode mode);

		List<OMModelToMarketObjects> GetIncludedModelObjects(long modelId, IncludedObjectsMode mode,
			Expression<Func<OMModelToMarketObjects, object>> selectExpression = null);

		List<OMModelToMarketObjects> GetIncludedObjectsForTraining(long modelId, TrainingSampleType mode, Expression<Func<OMModelToMarketObjects, object>> selectExpression = null);
	}
}
