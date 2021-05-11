using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Core.Register.RegisterEntities;
using ObjectModel.Market;

namespace MarketPlaceBusiness.Interfaces
{
	public interface IAMarketObjectBaseService
	{
		OMCoreObject GetById(long id, Expression<Func<OMCoreObject, object>> selectExpression = null);

		List<OMCoreObject> GetByIds(List<long> ids, Expression<Func<OMCoreObject, object>> selectExpression = null);

		List<OMCoreObject> GetObjectsByCondition(Expression<Func<OMCoreObject, bool>> whereExpression,
			Expression<Func<OMCoreObject, object>> selectExpression = null);

		RegisterAttribute GetAttributeData<TResult>(Expression<Func<OMCoreObject, TResult>> property);
	}
}