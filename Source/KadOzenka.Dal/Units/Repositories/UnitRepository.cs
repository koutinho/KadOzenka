using System;
using System.Linq.Expressions;
using Core.Register.QuerySubsystem;
using KadOzenka.Dal.CommonFunctions;
using ObjectModel.KO;

namespace KadOzenka.Dal.Units.Repositories
{
	public class UnitRepository : GenericRepository<OMUnit>, IUnitRepository
	{
		protected override QSQuery<OMUnit> GetBaseQuery(Expression<Func<OMUnit, bool>> whereExpression)
		{
			return OMUnit.Where(whereExpression);
		}

		protected override Expression<Func<OMUnit, bool>> GetWhereByIdExpression(long id)
		{
			return x => x.Id == id;
		}
	}
}
