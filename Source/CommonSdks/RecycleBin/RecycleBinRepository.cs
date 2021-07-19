using System;
using System.Linq.Expressions;
using Core.Register.QuerySubsystem;
using ObjectModel.Common;

namespace CommonSdks.RecycleBin
{
	public class RecycleBinRepository : GenericRepository<OMRecycleBin>, IRecycleBinRepository
	{
		protected override QSQuery<OMRecycleBin> GetBaseQuery(Expression<Func<OMRecycleBin, bool>> whereExpression)
		{
			return OMRecycleBin.Where(whereExpression);
		}

		protected override Expression<Func<OMRecycleBin, bool>> GetWhereByIdExpression(long id)
		{
			return x => x.EventId == id;
		}
	}
}
