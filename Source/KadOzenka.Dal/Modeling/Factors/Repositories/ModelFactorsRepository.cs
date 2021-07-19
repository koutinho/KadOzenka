using System;
using System.Linq.Expressions;
using CommonSdks;
using Core.Register.QuerySubsystem;
using KadOzenka.Dal.CommonFunctions;
using ObjectModel.Directory;
using ObjectModel.KO;

namespace KadOzenka.Dal.Modeling.Factors.Repositories
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

		public bool IsTheSameAttributeExists(long id, long factorId, long modelId, KoAlgoritmType type)
		{
			//todo вынести базовую часть запроса в QsQuery
			if (type == KoAlgoritmType.None)
			{
				return OMModelFactor.Where(x => x.Id != id && x.FactorId == factorId && x.ModelId == modelId)
					.ExecuteExists();
			}

			return OMModelFactor.Where(x =>
					x.Id != id && x.FactorId == factorId && x.ModelId == modelId && x.AlgorithmType_Code == type)
				.ExecuteExists();
		}

		public OMModelFactor GetFactorByDictionary(long dictionaryId)
		{
			return OMModelFactor.Where(x => x.DictionaryId == dictionaryId).SelectAll().ExecuteFirstOrDefault();
		}
	}
}
