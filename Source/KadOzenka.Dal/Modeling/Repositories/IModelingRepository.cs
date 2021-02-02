using System;
using System.Linq.Expressions;
using ObjectModel.KO;

namespace KadOzenka.Dal.Modeling.Repositories
{
	public interface IModelingRepository
	{
		OMModel GetModelById(long modelId, Expression<Func<OMModel, object>> selectExpression);

		OMModel GetActiveModelEntityByGroupId(long? groupId);
	}
}
