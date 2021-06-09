using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Core.Register.QuerySubsystem;
using KadOzenka.Dal.CommonFunctions;
using KadOzenka.Dal.Modeling.Entities;
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

		public bool AreIncludedModelObjectsExist(long? modelId, IncludedObjectsMode mode)
		{
			if (modelId.GetValueOrDefault() == 0)
				return false;

			return GetIncludedModelObjectsQuery(modelId, mode).ExecuteExists();
		}

		public List<OMModelToMarketObjects> GetIncludedModelObjects(long modelId, IncludedObjectsMode mode, Expression<Func<OMModelToMarketObjects, object>> selectExpression = null)
		{
			var query = GetIncludedModelObjectsQuery(modelId, mode);

			return RunQuery(query, selectExpression);

		}

		public List<OMModelToMarketObjects> GetIncludedObjectsForTraining(long modelId, TrainingSampleType mode, Expression<Func<OMModelToMarketObjects, object>> selectExpression = null)
		{
			var query = GetIncludedObjectsForTrainingQuery(modelId, mode).OrderBy(x => x.Price);

			return RunQuery(query, selectExpression);
		}


		#region Support Methods

		private QSQuery<OMModelToMarketObjects> GetIncludedModelObjectsQuery(long? modelId, IncludedObjectsMode mode)
		{
			var baseQuery = OMModelToMarketObjects.Where(x => x.ModelId == modelId && x.IsExcluded.Coalesce(false) == false);

			if (mode == IncludedObjectsMode.All)
			{
				return baseQuery;
			}

			if (mode == IncludedObjectsMode.Training)
			{
				return baseQuery.And(x => x.IsForTraining.Coalesce(false) == true || x.IsForControl.Coalesce(false) == true);
			}

			if (mode == IncludedObjectsMode.Prediction)
			{
				return baseQuery.And(x => x.IsForTraining.Coalesce(false) == false && x.IsForControl.Coalesce(false) == false);
			}

			throw new InvalidOperationException($"Указан неизвестный тип объектов моделирования '{mode}'");
		}

		private QSQuery<OMModelToMarketObjects> GetIncludedObjectsForTrainingQuery(long? modelId, TrainingSampleType mode)
		{
			var baseQuery = OMModelToMarketObjects.Where(x => x.ModelId == modelId && x.IsExcluded.Coalesce(false) == false);

			bool isForTraining;
			bool isForControl;
			switch (mode)
			{
				case TrainingSampleType.Control:
					isForTraining = false;
					isForControl = true;
					break;
				case TrainingSampleType.Training:
					isForTraining = true;
					isForControl = false;
					break;
				default:
					throw new InvalidOperationException($"Указан неизвестный тип выборки объектов моделирования '{mode}'");
			}

			return baseQuery.And(x => x.IsForTraining.Coalesce(false) == isForTraining && x.IsForControl.Coalesce(false) == isForControl);
		}

		private List<OMModelToMarketObjects> RunQuery(QSQuery<OMModelToMarketObjects> query,
			Expression<Func<OMModelToMarketObjects, object>> selectExpression = null)
		{
			query = selectExpression == null
				? query.SelectAll()
				: query.Select(selectExpression);

			return query.Execute();
		}

		#endregion
	}
}
