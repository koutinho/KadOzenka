using System;
using System.Linq.Expressions;
using Core.Register.QuerySubsystem;
using KadOzenka.Dal.CommonFunctions;
using ObjectModel.KO;

namespace KadOzenka.Dal.Modeling.Repositories
{
	public class ModelDictionaryRepository : GenericRepository<OMModelingDictionary>, IModelDictionaryRepository
	{
		protected override QSQuery<OMModelingDictionary> GetBaseQuery(Expression<Func<OMModelingDictionary, bool>> whereExpression)
		{
			return OMModelingDictionary.Where(whereExpression);
		}

		protected override Expression<Func<OMModelingDictionary, bool>> GetWhereByIdExpression(long id)
		{
			return x => x.Id == id;
		}
	}
}
