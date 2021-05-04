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
		//TODO вынести в константы
		public static readonly long RegisterId = OMCoreObject.GetRegisterId();
		//TODO KOMO-33: если будет много колонок, подумать над отдельным методом
		public static readonly QSColumn PrimaryKeyColumn = OMCoreObject.GetColumn(x => x.Id);
		public static readonly long PriceAttributeId = OMCoreObject.GetColumnAttributeId(x => x.Price);

		//"закладываемся" на то, что будем возвращать не OMCoreObject, а OMCoreObjectDto,
		//поэтому делаем отдельный метод в сервисе с вызовом репозитория
		private IMarketObjectsRepository MarketObjectsRepository { get; set; }

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
