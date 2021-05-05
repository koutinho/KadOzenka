using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using MarketPlaceBusiness.Common;
using MarketPlaceBusiness.Interfaces;
using MarketPlaceBusiness.Interfaces.ForBlFrontendApp;
using ObjectModel.Directory;
using ObjectModel.Market;

namespace MarketPlaceBusiness
{
	public class MarketObjectService : MarketObjectBaseService,
		IMarketObjectService, IMissingDataFromGbuService, IMarketObjectForCadastralInfoFiller,
		IMarketObjectsServiceForBlFrontendApp
	{
		//TODO inject via IoC
		public MarketObjectService(IMarketObjectsRepository marketObjectsRepository = null)
			: base(marketObjectsRepository)
		{
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

		
		#region Для удаления дубликатов

		public List<OMCoreObject> GetObjectsForDuplicatesChecking()
		{
			return OMCoreObject
				.Where(x => x.ProcessType_Code == ProcessStep.CadastralNumberStep ||
				            x.ProcessType_Code == ProcessStep.InProcess ||
				            x.ExclusionStatus_Code == ExclusionStatus.Duplicate)
				.Select(x => new { x.CadastralNumber, x.DealType_Code, x.PropertyTypesCIPJS_Code, x.PropertyMarketSegment_Code, x.Market_Code, x.Market, x.ExclusionStatus_Code, x.Price, x.Area, x.ParserTime, x.DealType })
				.Execute()
				.ToList();
		}

		#endregion


		#region Для Присвоения КН объектам сторонних маркетов

		public List<OMCoreObject> GetNotRosreestrObjectsWithAddressProceed()
		{
			return OMCoreObject.Where(x => x.Market_Code != MarketTypes.Rosreestr && x.ProcessType_Code == ProcessStep.AddressStep)
				.Select(x => new
				{
					x.Address, x.CadastralNumber, x.Lng, x.Lat, x.ExclusionStatus_Code, x.ProcessType_Code
				})
				.Execute()
				.ToList();
		}

		#endregion
	}
}
