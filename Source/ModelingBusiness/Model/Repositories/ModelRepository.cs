using System;
using System.Linq.Expressions;
using CommonSdks;
using Core.Register.QuerySubsystem;
using ObjectModel.KO;

namespace ModelingBusiness.Model.Repositories
{
	public class ModelRepository : GenericRepository<OMModel>, IModelRepository
	{
		protected override QSQuery<OMModel> GetBaseQuery(Expression<Func<OMModel, bool>> whereExpression)
		{
			return OMModel.Where(whereExpression);
		}

		protected override Expression<Func<OMModel, bool>> GetWhereByIdExpression(long id)
		{
			return x => x.Id == id;
		}
	}
}
