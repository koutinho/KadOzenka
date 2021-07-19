using System;
using System.Linq.Expressions;
using CommonSdks;
using Core.Register.QuerySubsystem;
using KadOzenka.Dal.CommonFunctions;
using ObjectModel.KO;

namespace KadOzenka.Dal.Modeling.Model.Repositories
{
	public class ModelingRepository : GenericRepository<OMModel>, IModelingRepository
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
