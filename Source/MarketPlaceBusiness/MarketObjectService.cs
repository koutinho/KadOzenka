using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using MarketPlaceBusiness.Common;
using MarketPlaceBusiness.Interfaces;
using ObjectModel.Directory;
using ObjectModel.Market;

namespace MarketPlaceBusiness
{
	public class MarketObjectService : IMarketObjectService, IMissingDataFromGbuService, IMarketObjectForCadastralInfoFiller
	{
		//"закладываемся" на то, что будем возвращать не OMCoreObject, а OMCoreObjectDto,
		//поэтому делаем отдельный метод в сервисе с вызовом репозитория
		private IMarketObjectsRepository MarketObjectsRepository { get; }

		
		//TODO inject via IoC
		public MarketObjectService(IMarketObjectsRepository marketObjectsRepository = null)
		{
			MarketObjectsRepository = marketObjectsRepository ?? new MarketObjectsRepository();
		}

		public List<OMCoreObject> GetObjectsByCondition(Expression<Func<OMCoreObject, bool>> whereExpression,
			Expression<Func<OMCoreObject, object>> selectExpression)
		{
			return MarketObjectsRepository.GetEntitiesByCondition(whereExpression, selectExpression);
		}


		#region Для процедуры получения доп. данных из ГБУ части

		public List<OMCoreObject> GetInitialObjects()
		{
			Expression<Func<OMCoreObject, bool>> whereExpression = x =>
				x.LastDateUpdate == null && (x.BuildingYear == null || x.WallMaterial == null);

			Expression<Func<OMCoreObject, object>> selectExpression = x => new
			{
				x.BuildingYear,
				x.WallMaterial,
				x.CadastralNumber
			};

			return GetObjectsByCondition(whereExpression, selectExpression);
		}

		public List<OMCoreObject> GetExistingObjects()
		{
			Expression<Func<OMCoreObject, bool>> whereExpression = x =>
				(x.ProcessType_Code == ProcessStep.InProcess || x.ProcessType_Code == ProcessStep.Dealed) &&
				(x.BuildingYear == null || x.WallMaterial == null);

			Expression<Func<OMCoreObject, object>> selectExpression = x => new
			{
				x.BuildingYear,
				x.WallMaterial,
				x.CadastralNumber
			};

			return GetObjectsByCondition(whereExpression, selectExpression);
		}

		#endregion

		#region Для привязки к аналогам кадастровых кварталов

		public List<OMCoreObject> GetObjectsWithCadastralNumber()
		{
			return OMCoreObject.Where(x => x.CadastralNumber != null && x.CadastralNumber != string.Empty)
				.Select(x => new
				{
					x.CadastralNumber,
					x.CadastralQuartal
				})
				.Execute();
		}

		public List<OMCoreObject> GetObjectsWithCadastralQuartal()
		{
			return OMCoreObject.Where(x => x.CadastralQuartal != null && x.CadastralQuartal != string.Empty)
				.Select(x => new
				{
					x.CadastralNumber,
					x.CadastralQuartal,
					x.District,
					x.District_Code,
					x.Neighborhood,
					x.Neighborhood_Code,
					x.Zone,
					x.ZoneRegion
				})
				.Execute();
		}

		#endregion
	}
}
