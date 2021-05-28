using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using ObjectModel.Market;

namespace MarketPlaceBusiness.Common
{
	public interface IMarketObjectsRepository
	{
		OMCoreObject GetById(long id, Expression<Func<OMCoreObject, object>> selectExpression);

		List<OMCoreObject> GetEntitiesByCondition(Expression<Func<OMCoreObject, bool>> whereExpression,
			Expression<Func<OMCoreObject, object>> selectExpression);

		OMCoreObject GetEntityByCondition(Expression<Func<OMCoreObject, bool>> whereExpression,
			Expression<Func<OMCoreObject, object>> selectExpression);

		bool IsExists(Expression<Func<OMCoreObject, bool>> whereExpression);

		int Save(OMCoreObject entity);
	}
}
