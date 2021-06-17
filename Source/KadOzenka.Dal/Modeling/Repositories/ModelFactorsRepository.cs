using System;
using System.Linq.Expressions;
using Core.Register.QuerySubsystem;
using KadOzenka.Dal.CommonFunctions;
using ObjectModel.KO;

namespace KadOzenka.Dal.Modeling.Repositories
{
	public class ModelFactorsRepository : GenericRepository<OMModelFactor>, IModelFactorsRepository
	{
		protected override QSQuery<OMModelFactor> GetBaseQuery(Expression<Func<OMModelFactor, bool>> whereExpression)
		{
			return OMModelFactor.Where(whereExpression);
		}

		protected override Expression<Func<OMModelFactor, bool>> GetWhereByIdExpression(long id)
		{
			return x => x.Id == id;
		}
	}
}
