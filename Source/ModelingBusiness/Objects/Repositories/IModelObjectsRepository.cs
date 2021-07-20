using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using CommonSdks;
using CommonSdks.Repositories;
using ModelingBusiness.Objects.Entities;
using ObjectModel.Modeling;

namespace ModelingBusiness.Objects.Repositories
{
	public interface IModelObjectsRepository : IGenericRepository<OMModelToMarketObjects>
	{
		bool AreIncludedModelObjectsExist(long? modelId, IncludedObjectsMode mode);

		List<OMModelToMarketObjects> GetIncludedModelObjects(long modelId, IncludedObjectsMode mode,
			Expression<Func<OMModelToMarketObjects, object>> selectExpression = null);

		List<OMModelToMarketObjects> GetIncludedObjectsForTraining(long modelId, TrainingSampleType mode, Expression<Func<OMModelToMarketObjects, object>> selectExpression = null);
	}
}
