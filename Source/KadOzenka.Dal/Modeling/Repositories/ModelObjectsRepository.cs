using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Core.Register.QuerySubsystem;
using KadOzenka.Dal.CommonFunctions;
using ObjectModel.Modeling;

namespace KadOzenka.Dal.Modeling.Repositories
{
	public class ModelObjectsRepository : GenericRepository<OMModelToMarketObjects>, IModelObjectsRepository
	{
		protected override QSQuery<OMModelToMarketObjects> GetBaseQuery(Expression<Func<OMModelToMarketObjects, bool>> whereExpression)
		{
			return OMModelToMarketObjects.Where(whereExpression);
		}

		protected override Expression<Func<OMModelToMarketObjects, bool>> GetWhereByIdExpression(long id)
		{
			return x => x.Id == id;
		}

		public bool AreIncludedModelObjectsExist(long? modelId, bool isForTraining)
		{
			return GetIncludedModelObjectsQuery(modelId, isForTraining).ExecuteExists();
		}

		public List<OMModelToMarketObjects> GetIncludedModelObjects(long modelId, bool isForTraining)
		{
			return GetIncludedModelObjectsQuery(modelId, isForTraining).SelectAll().Execute();
		}

		#region Support Methods

		private QSQuery<OMModelToMarketObjects> GetIncludedModelObjectsQuery(long? modelId, bool isForTraining)
		{
			if (isForTraining)
			{
				return OMModelToMarketObjects
					.Where(x => x.ModelId == modelId && x.IsExcluded.Coalesce(false) == false &&
					            (x.IsForTraining.Coalesce(false) == true || x.IsForControl.Coalesce(false) == true));
			}

			return OMModelToMarketObjects
				.Where(x => x.ModelId == modelId && x.IsExcluded.Coalesce(false) == false &&
				            x.IsForTraining.Coalesce(false) == false && x.IsForControl.Coalesce(false) == false);
		}

		#endregion
	}
}
