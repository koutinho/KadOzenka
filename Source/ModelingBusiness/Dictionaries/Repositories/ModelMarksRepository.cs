using System;
using System.Linq.Expressions;
using CommonSdks;
using Core.Register.QuerySubsystem;
using ObjectModel.KO;

namespace ModelingBusiness.Dictionaries.Repositories
{
	public class ModelMarksRepository : GenericRepository<OMModelingDictionariesValues>, IModelMarksRepository
	{
		protected override QSQuery<OMModelingDictionariesValues> GetBaseQuery(Expression<Func<OMModelingDictionariesValues, bool>> whereExpression)
		{
			return OMModelingDictionariesValues.Where(whereExpression);
		}

		protected override Expression<Func<OMModelingDictionariesValues, bool>> GetWhereByIdExpression(long id)
		{
			return x => x.Id == id;
		}

		public bool IsTheSameMarkExists(long dictionaryId, long markId, string value)
		{
			return OMModelingDictionariesValues
				.Where(x => x.Id != markId && x.DictionaryId == dictionaryId && x.Value == value)
				.ExecuteExists();
		}
	}
}
