using System;
using System.Linq.Expressions;
using CommonSdks.Repositories;
using Core.Register.QuerySubsystem;
using ObjectModel.KO;

namespace ModelingBusiness.Factors.Repositories
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

		public bool IsTheSameAttributeExists(long id, long factorId, long modelId)
		{
			return OMModelFactor.Where(x => x.Id != id && x.FactorId == factorId && x.ModelId == modelId)
				.ExecuteExists();
		}

		public OMModelFactor GetFactorByDictionary(long dictionaryId)
		{
			return OMModelFactor.Where(x => x.DictionaryId == dictionaryId).SelectAll().ExecuteFirstOrDefault();
		}
	}
}
