﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Core.ObjectModel;

namespace KadOzenka.Dal.CommonFunctions
{
	public interface IGenericRepository<TSource> where TSource : OMBaseClass<TSource>, new()
	{
		TSource GetById(long id, Expression<Func<TSource, object>> selectExpression);

		List<TSource> GetByCondition(Expression<Func<TSource, bool>> whereExpression,
			Expression<Func<TSource, object>> selectExpression);

		int Save(TSource entity);
	}
}
