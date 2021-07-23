using System;
using System.Linq.Expressions;
using CommonSdks;
using CommonSdks.Repositories;
using Core.Register.QuerySubsystem;
using ObjectModel.KO;

namespace ModelingBusiness.Dictionaries.Repositories
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
