using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using MarketPlaceBusiness.Common;
using ObjectModel.Market;

namespace MarketPlaceBusiness
{
	public interface IMarketObjectBaseService
	{
		OMCoreObject GetById(long id, Expression<Func<OMCoreObject, object>> selectExpression);

		List<OMCoreObject> GetObjectsByCondition(Expression<Func<OMCoreObject, bool>> whereExpression,
			Expression<Func<OMCoreObject, object>> selectExpression);
	}

	public abstract class MarketObjectBaseService : IMarketObjectBaseService
	{
		//"закладываемся" на то, что будем возвращать не OMCoreObject, а OMCoreObjectDto,
		//поэтому делаем отдельный метод в сервисе с вызовом репозитория
		protected IMarketObjectsRepository MarketObjectsRepository { get; }

		
		//TODO inject via IoC
		protected MarketObjectBaseService(IMarketObjectsRepository marketObjectsRepository = null)
		{
			MarketObjectsRepository = marketObjectsRepository ?? new MarketObjectsRepository();
		}


		public OMCoreObject GetById(long id, Expression<Func<OMCoreObject, object>> selectExpression)
		{
			return MarketObjectsRepository.GetById(id, selectExpression);
		}

		public List<OMCoreObject> GetObjectsByCondition(Expression<Func<OMCoreObject, bool>> whereExpression,
			Expression<Func<OMCoreObject, object>> selectExpression)
		{
			return MarketObjectsRepository.GetEntitiesByCondition(whereExpression, selectExpression);
		}
	}
}
