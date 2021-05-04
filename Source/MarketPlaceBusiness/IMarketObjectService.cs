using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using ObjectModel.Market;

namespace MarketPlaceBusiness
{
	public interface IMarketObjectService
	{
		List<OMCoreObject> GetObjectsByCondition(Expression<Func<OMCoreObject, bool>> whereExpression,
			Expression<Func<OMCoreObject, object>> selectExpression);
	}
}