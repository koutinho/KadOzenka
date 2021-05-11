using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Core.Register.QuerySubsystem;
using ObjectModel.Market;

namespace MarketPlaceBusiness.Common
{
	public class MarketObjectsRepository : IMarketObjectsRepository
	{
		public OMCoreObject GetById(long id, Expression<Func<OMCoreObject, object>> selectExpression)
		{
			return GetEntityByCondition(GetWhereByIdExpression(id), selectExpression);
		}

		public OMCoreObject GetEntityByCondition(Expression<Func<OMCoreObject, bool>> whereExpression,
			Expression<Func<OMCoreObject, object>> selectExpression)
		{
			var baseQuery = GetBaseQuery(whereExpression);

			baseQuery = selectExpression == null
				? baseQuery.SelectAll(false)
				: baseQuery.Select(selectExpression);

			return baseQuery.ExecuteFirstOrDefault();
		}

		public bool IsExists(Expression<Func<OMCoreObject, bool>> whereExpression)
		{
			var baseQuery = GetBaseQuery(whereExpression);

			return baseQuery.ExecuteExists();
		}

		public List<OMCoreObject> GetEntitiesByCondition(Expression<Func<OMCoreObject, bool>> whereExpression,
			Expression<Func<OMCoreObject, object>> selectExpression)
		{
			var baseQuery = GetBaseQuery(whereExpression);

			baseQuery = selectExpression == null
				? baseQuery.SelectAll()
				: baseQuery.Select(selectExpression);

			return baseQuery.Execute();
		}

		public int Save(OMCoreObject entity)
		{
			return entity.Save();
		}


		#region Support Methods

		protected QSQuery<OMCoreObject> GetBaseQuery(Expression<Func<OMCoreObject, bool>> whereExpression)
		{
			return OMCoreObject.Where(whereExpression);
		}

		protected Expression<Func<OMCoreObject, bool>> GetWhereByIdExpression(long id)
		{
			return x => x.Id == id;
		}

		#endregion
	}
}
