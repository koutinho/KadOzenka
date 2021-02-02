using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Core.Register.QuerySubsystem;
using ObjectModel.KO;

namespace KadOzenka.Dal.Modeling.Repositories
{
	public class ModelingRepository : IModelingRepository
	{
		public OMModel GetActiveModelEntityByGroupId(long? groupId)
		{
			return OMModel.Where(x => x.GroupId == groupId && x.IsActive.Coalesce(false) == true).SelectAll()
				.ExecuteFirstOrDefault();
		}

		public OMModel GetById(long id, Expression<Func<OMModel, object>> selectExpression)
		{
			var baseQuery = OMModel.Where(x => x.Id == id);
			
			baseQuery = selectExpression == null 
				? baseQuery.SelectAll() 
				: baseQuery.Select(selectExpression);

			return baseQuery.ExecuteFirstOrDefault();
		}

		public List<OMModel> GetByCondition(Expression<Func<OMModel, bool>> whereExpression,
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


		#region Support Methods

		

		#endregion
	}
}
