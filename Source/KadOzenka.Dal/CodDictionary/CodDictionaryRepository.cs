using System;
using System.Linq.Expressions;
using CommonSdks;
using Core.Register.QuerySubsystem;
using KadOzenka.Dal.CommonFunctions;
using ObjectModel.KO;

namespace KadOzenka.Dal.CodDictionary
{
	public class CodDictionaryRepository : GenericRepository<OMCodJob>, ICodDictionaryRepository
	{
		protected override QSQuery<OMCodJob> GetBaseQuery(Expression<Func<OMCodJob, bool>> whereExpression)
		{
			return OMCodJob.Where(whereExpression);
		}

		protected override Expression<Func<OMCodJob, bool>> GetWhereByIdExpression(long id)
		{
			return x => x.Id == id;
		}
	}
}
