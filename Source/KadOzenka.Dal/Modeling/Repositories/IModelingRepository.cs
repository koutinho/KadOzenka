using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using ObjectModel.KO;

namespace KadOzenka.Dal.Modeling.Repositories
{
	public interface IModelingRepository
	{
		OMModel GetModelById(long modelId, Expression<Func<OMModel, object>> selectExpression);

		List<OMModel> GetModelsByCondition(Expression<Func<OMModel, bool>> whereExpression,
			Expression<Func<OMModel, object>> selectExpression);

		OMModel GetActiveModelEntityByGroupId(long? groupId);

		int Save(OMModel model);
	}
}
