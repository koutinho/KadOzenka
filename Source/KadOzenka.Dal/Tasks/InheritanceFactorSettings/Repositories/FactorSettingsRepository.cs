using System;
using System.Linq.Expressions;
using Core.Register.QuerySubsystem;
using KadOzenka.Dal.CommonFunctions;
using ObjectModel.KO;

namespace KadOzenka.Dal.Tasks.InheritanceFactorSettings.Repositories
{
	public class FactorSettingsRepository : GenericRepository<OMFactorSettings>, IFactorSettingsRepository
	{
		protected override QSQuery<OMFactorSettings> GetBaseQuery(Expression<Func<OMFactorSettings, bool>> whereExpression)
		{
			return OMFactorSettings.Where(whereExpression);
		}

		protected override Expression<Func<OMFactorSettings, bool>> GetWhereByIdExpression(long id)
		{
			return x => x.Id == id;
		}

		public bool IsFactorExists(long factorId)
		{
			return OMFactorSettings.Where(x => x.CorrectFactorId == factorId || x.FactorId == factorId).ExecuteExists();
		}
	}
}
