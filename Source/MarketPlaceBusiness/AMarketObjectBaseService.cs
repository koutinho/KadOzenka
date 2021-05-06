using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Core.Register.RegisterEntities;
using MarketPlaceBusiness.Common;
using MarketPlaceBusiness.Interfaces;
using ObjectModel.Market;

namespace MarketPlaceBusiness
{
	public abstract class AMarketObjectBaseService : IAMarketObjectBaseService
	{
		//"закладываемся" на то, что будем возвращать не OMCoreObject, а OMCoreObjectDto,
		//поэтому делаем отдельный метод в сервисе с вызовом репозитория
		protected IMarketObjectsRepository MarketObjectsRepository { get; }

		
		//TODO inject via IoC
		protected AMarketObjectBaseService(IMarketObjectsRepository marketObjectsRepository = null)
		{
			MarketObjectsRepository = marketObjectsRepository ?? new MarketObjectsRepository();
		}

		public RegisterAttribute GetAttributeData<TResult>(Expression<Func<OMCoreObject, TResult>> property)
		{
			return OMCoreObject.GetAttributeData(property);
		}

		public OMCoreObject GetById(long id, Expression<Func<OMCoreObject, object>> selectExpression = null)
		{
			return MarketObjectsRepository.GetById(id, selectExpression);
		}

		public List<OMCoreObject> GetByIds(List<long> ids, Expression<Func<OMCoreObject, object>> selectExpression = null)
		{
			Expression<Func<OMCoreObject, bool>> whereExpression = x => ids.Contains(x.Id);
			return MarketObjectsRepository.GetEntitiesByCondition(whereExpression, selectExpression);
		}

		public List<OMCoreObject> GetObjectsByCondition(Expression<Func<OMCoreObject, bool>> whereExpression,
			Expression<Func<OMCoreObject, object>> selectExpression)
		{
			return MarketObjectsRepository.GetEntitiesByCondition(whereExpression, selectExpression);
		}
	}
}
