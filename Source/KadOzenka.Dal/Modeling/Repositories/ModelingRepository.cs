using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using ObjectModel.KO;

namespace KadOzenka.Dal.Modeling.Repositories
{
	public class ModelingRepository : IModelingRepository
	{
		public OMModel GetById(long id, Expression<Func<OMModel, object>> selectExpression)
		{
			return GetEntityByCondition(x => x.Id == id, selectExpression);
		}

		public OMModel GetEntityByCondition(Expression<Func<OMModel, bool>> whereExpression,
			Expression<Func<OMModel, object>> selectExpression)
		{
			var baseQuery = OMModel.Where(whereExpression);

			baseQuery = selectExpression == null
				? baseQuery.SelectAll()
				: baseQuery.Select(selectExpression);

			return baseQuery.ExecuteFirstOrDefault();
		}

		public List<OMModel> GetEntitiesByCondition(Expression<Func<OMModel, bool>> whereExpression,
			Expression<Func<OMModel, object>> selectExpression)
		{
			var baseQuery = OMModel.Where(whereExpression);

			baseQuery = selectExpression == null
				? baseQuery.SelectAll()
				: baseQuery.Select(selectExpression);

			return baseQuery.Execute();
		}

		public int Save(OMModel model)
		{
			return model.Save();
		}
	}
}
