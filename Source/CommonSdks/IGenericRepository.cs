using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Core.ObjectModel;

namespace CommonSdks
{
	public interface IGenericRepository<TSource> where TSource : OMBaseClass<TSource>, new()
	{
		TSource GetById(long id, Expression<Func<TSource, object>> selectExpression);

		List<TSource> GetEntitiesByCondition(Expression<Func<TSource, bool>> whereExpression,
			Expression<Func<TSource, object>> selectExpression);

		TSource GetEntityByCondition(Expression<Func<TSource, bool>> whereExpression,
			Expression<Func<TSource, object>> selectExpression);

		bool IsExists(Expression<Func<TSource, bool>> whereExpression);
		
		int ExecuteCount(Expression<Func<TSource, bool>> whereExpression);

		int Save(TSource entity);
	}
}
