using System;
using System.Linq.Expressions;
using CommonSdks;
using Core.Register.QuerySubsystem;
using ObjectModel.Common;

namespace KadOzenka.Dal.CommonFunctions.Repositories
{
	public class ImportDataLogRepository : GenericRepository<OMImportDataLog>, IImportDataLogRepository
	{
		protected override QSQuery<OMImportDataLog> GetBaseQuery(Expression<Func<OMImportDataLog, bool>> whereExpression)
		{
			return OMImportDataLog.Where(whereExpression);
		}

		protected override Expression<Func<OMImportDataLog, bool>> GetWhereByIdExpression(long id)
		{
			return x => x.Id == id;
		}
	}
}
