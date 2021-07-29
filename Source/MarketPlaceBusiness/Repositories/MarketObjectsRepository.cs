using System;
using System.Linq.Expressions;
using CommonSdks.Repositories;
using Core.Register.QuerySubsystem;
using ObjectModel.Market;

namespace MarketPlaceBusiness.Repositories
{
	public class MarketObjectsRepository : GenericRepository<OMCoreObject>, IMarketObjectsRepository
	{
		protected override QSQuery<OMCoreObject> GetBaseQuery(Expression<Func<OMCoreObject, bool>> whereExpression)
		{
			return OMCoreObject.Where(whereExpression);
		}

		protected override Expression<Func<OMCoreObject, bool>> GetWhereByIdExpression(long id)
		{
			return x => x.Id == id;
		}
	}
}
