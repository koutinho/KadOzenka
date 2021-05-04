using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Core.Register.QuerySubsystem;
using MarketPlaceBusiness.Common;
using ObjectModel.Market;

namespace MarketPlaceBusiness
{
	public class MarketObjectService : IMarketObjectService
	{
		//"закладываемся" на то, что будем возвращать не OMCoreObject, а OMCoreObjectDto,
		//поэтому делаем отдельный метод в сервисе с вызовом репозитория
		private IMarketObjectsRepository MarketObjectsRepository { get; }

		
		//TODO inject via IoC
		public MarketObjectService(IMarketObjectsRepository marketObjectsRepository = null)
		{
			MarketObjectsRepository = marketObjectsRepository ?? new MarketObjectsRepository();
		}


		public List<OMCoreObject> GetObjectsByCondition(Expression<Func<OMCoreObject, bool>> whereExpression,
			Expression<Func<OMCoreObject, object>> selectExpression)
		{
			return MarketObjectsRepository.GetEntitiesByCondition(whereExpression, selectExpression);
		}
	}
}
