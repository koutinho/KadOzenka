using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Core.Register.RegisterEntities;
using MarketPlaceBusiness.Common.Dto;
using ObjectModel.Market;

namespace MarketPlaceBusiness.Common.Interfaces
{
	public interface IAMarketObjectBaseService
	{
		//TODO убран временно, т.к. лучше пользоваться поиском по ИД с маппером
		//OMCoreObject GetById(long id, Expression<Func<OMCoreObject, object>> selectExpression = null);

		MarketObjectDto GetMappedObjectById(long id, Expression<Func<OMCoreObject, object>> selectExpression = null);

		List<OMCoreObject> GetByIds(List<long> ids, Expression<Func<OMCoreObject, object>> selectExpression = null);

		List<OMCoreObject> GetObjectsByCondition(Expression<Func<OMCoreObject, bool>> whereExpression,
			Expression<Func<OMCoreObject, object>> selectExpression = null);

		RegisterAttribute GetAttributeData<TResult>(Expression<Func<OMCoreObject, TResult>> property);
	}
}