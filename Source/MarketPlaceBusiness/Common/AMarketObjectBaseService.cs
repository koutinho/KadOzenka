using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using AutoMapper;
using Core.Register.RegisterEntities;
using MarketPlaceBusiness.Common.Dto;
using MarketPlaceBusiness.Common.Dto.AutoMapper;
using MarketPlaceBusiness.Common.Interfaces;
using MarketPlaceBusiness.Repositories;
using ObjectModel.Market;

namespace MarketPlaceBusiness.Common
{
	public abstract class AMarketObjectBaseService : IAMarketObjectBaseService
	{
		//"закладываемся" на то, что будем возвращать не OMCoreObject, а OMCoreObjectDto,
		//поэтому делаем отдельный метод в сервисе с вызовом репозитория
		protected IMarketObjectsRepository MarketObjectsRepository { get; }
		protected IMapper Mapper{ get; }

		
		//TODO inject via IoC
		protected AMarketObjectBaseService(IMarketObjectsRepository marketObjectsRepository = null, IMapper mapper = null)
		{
			Mapper = mapper ?? MapperSingleton.Get();

			MarketObjectsRepository = marketObjectsRepository ?? new MarketObjectsRepository();
		}

		public RegisterAttribute GetAttributeData<TResult>(Expression<Func<OMCoreObject, TResult>> property)
		{
			return OMCoreObject.GetAttributeData(property);
		}

		protected OMCoreObject GetById(long id, Expression<Func<OMCoreObject, object>> selectExpression = null)
		{
			return MarketObjectsRepository.GetById(id, selectExpression);
		}

		public MarketObjectDto GetMappedObjectById(long id, Expression<Func<OMCoreObject, object>> selectExpression = null)
		{
			var omCoreObject = GetById(id, selectExpression);
			var marketObjectDto = Mapper.Map<OMCoreObject, MarketObjectDto>(omCoreObject);

			return marketObjectDto;
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
