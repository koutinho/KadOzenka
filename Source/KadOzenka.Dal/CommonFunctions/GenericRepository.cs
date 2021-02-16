using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Core.ObjectModel;
using Core.Register.QuerySubsystem;

namespace KadOzenka.Dal.CommonFunctions
{
	public abstract class GenericRepository<TSource> : IGenericRepository<TSource> where TSource : OMBaseClass<TSource>, new()
	{
		protected abstract QSQuery<TSource> GetBaseQuery(Expression<Func<TSource, bool>> whereExpression);
		protected abstract Expression<Func<TSource, bool>> GetWhereByIdExpression(long id);

		public TSource GetById(long id, Expression<Func<TSource, object>> selectExpression)
		{
			return GetEntityByCondition(GetWhereByIdExpression(id), selectExpression);
		}

		public TSource GetEntityByCondition(Expression<Func<TSource, bool>> whereExpression,
			Expression<Func<TSource, object>> selectExpression)
		{
			var baseQuery = GetBaseQuery(whereExpression);

			baseQuery = selectExpression == null
				? baseQuery.SelectAll()
				: baseQuery.Select(selectExpression);

			return baseQuery.ExecuteFirstOrDefault();
		}

		public bool IsExists(Expression<Func<TSource, bool>> whereExpression)
		{
			var baseQuery = GetBaseQuery(whereExpression);

			return baseQuery.ExecuteExists();
		}

		public List<TSource> GetEntitiesByCondition(Expression<Func<TSource, bool>> whereExpression,
			Expression<Func<TSource, object>> selectExpression)
		{
			var baseQuery = GetBaseQuery(whereExpression);

			baseQuery = selectExpression == null
				? baseQuery.SelectAll()
				: baseQuery.Select(selectExpression);

			return baseQuery.Execute();
		}

		public int Save(TSource entity)
		{
			return entity.Save();
		}
	}
}
