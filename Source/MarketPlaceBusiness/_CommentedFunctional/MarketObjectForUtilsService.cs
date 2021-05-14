//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Linq.Expressions;
//using Core.Shared.Extensions;
//using MarketPlaceBusiness.Common;
//using MarketPlaceBusiness.Interfaces.Utils;
//using ObjectModel.Directory;
//using ObjectModel.Market;

//namespace MarketPlaceBusiness
//{
//	/// <summary>
//	/// Сервис с методами, которые используются в утилитах BlFrontend
//	/// </summary>
//	public class MarketObjectForUtilsService : AMarketObjectBaseService, IMarketObjectsServiceForUtils
//	{
//		//TODO inject via IoC
//		public MarketObjectForUtilsService(IMarketObjectsRepository marketObjectsRepository = null)
//			: base(marketObjectsRepository)
//		{
//		}


//		#region Для процедуры получения доп. данных из ГБУ части

//		public List<OMCoreObject> GetInitialObjects()
//		{
//			Expression<Func<OMCoreObject, bool>> whereExpression = x =>
//				x.LastDateUpdate == null && (x.BuildingYear == null || x.WallMaterial == null);

//			Expression<Func<OMCoreObject, object>> selectExpression = x => new
//			{
//				x.BuildingYear,
//				x.WallMaterial,
//				x.CadastralNumber
//			};

//			return GetObjectsByCondition(whereExpression, selectExpression);
//		}

//		public List<OMCoreObject> GetExistingObjects()
//		{
//			Expression<Func<OMCoreObject, bool>> whereExpression = x =>
//				(x.ProcessType_Code == ProcessStep.InProcess || x.ProcessType_Code == ProcessStep.Dealed) &&
//				(x.BuildingYear == null || x.WallMaterial == null);

//			Expression<Func<OMCoreObject, object>> selectExpression = x => new
//			{
//				x.BuildingYear,
//				x.WallMaterial,
//				x.CadastralNumber
//			};

//			return GetObjectsByCondition(whereExpression, selectExpression);
//		}

//		public bool FillBuildingYearData(OMCoreObject omCoreObject, string yearStr)
//		{
//			if (!omCoreObject.BuildingYear.HasValue)
//			{
//				if (long.TryParse(yearStr, out var year))
//				{
//					omCoreObject.BuildingYear = year;
//					return true;
//				}
//			}

//			return false;
//		}

//		public bool FillWallMaterialData(OMCoreObject omCoreObject, string wallMaterial)
//		{
//			if (string.IsNullOrEmpty(omCoreObject.WallMaterial))
//			{
//				var attrValue = wallMaterial?.Replace(";", ",");
//				var enumValue = EnumExtensions.GetEnumByDescription<WallMaterial>(attrValue);
//				if (enumValue != 0)
//				{
//					omCoreObject.WallMaterial_Code = (WallMaterial)enumValue;
//					return true;
//				}
//			}

//			return false;
//		}

//		#endregion


//		#region Для привязки к аналогам кадастровых кварталов

//		//public List<OMCoreObject> GetObjectsWithCadastralNumber()
//		//{
//		//	return OMCoreObject.Where(x => x.CadastralNumber != null && x.CadastralNumber != string.Empty)
//		//		.Select(x => new
//		//		{
//		//			x.CadastralNumber,
//		//			x.CadastralQuartal
//		//		})
//		//		.Execute();
//		//}

//		//public List<OMCoreObject> GetObjectsWithCadastralQuartal()
//		//{
//		//	return OMCoreObject.Where(x => x.CadastralQuartal != null && x.CadastralQuartal != string.Empty)
//		//		.Select(x => new
//		//		{
//		//			x.CadastralNumber,
//		//			x.CadastralQuartal,
//		//			x.District,
//		//			x.District_Code,
//		//			x.Neighborhood,
//		//			x.Neighborhood_Code,
//		//			x.Zone,
//		//			x.ZoneRegion
//		//		})
//		//		.Execute();
//		//}

//		//public void FillQuarterByCadastralNumber(OMCoreObject marketObject)
//		//{
//		//	var ellipsisLastIndex = marketObject.CadastralNumber.LastIndexOf(":");
//		//	marketObject.CadastralQuartal = marketObject.CadastralNumber.Substring(0, ellipsisLastIndex);
//		//}

//		#endregion


//		#region Для Присвоения КН объектам сторонних маркетов

//		//public List<OMCoreObject> GetNotRosreestrObjectsWithAddressProceed()
//		//{
//		//	return OMCoreObject.Where(x => x.Market_Code != MarketTypes.Rosreestr && x.ProcessType_Code == ProcessStep.AddressStep)
//		//		.Select(x => new
//		//		{
//		//			x.Address, x.CadastralNumber, x.Lng, x.Lat, x.ExclusionStatus_Code, x.ProcessType_Code
//		//		})
//		//		.Execute()
//		//		.ToList();
//		//}

//		#endregion


//		#region Присвоение адресов необработанным объектам сторонних маркетов

//		public List<OMCoreObject> GetObjectsToSetAddress(int objectsCount)
//		{
//			Expression<Func<OMCoreObject, bool>> whereExpression = x => x.ProcessType_Code == ObjectModel.Directory.ProcessStep.DoNotProcessed;
//			Expression<Func<OMCoreObject, object>> selectExpression = x => new
//			{
//				x.Market_Code,
//				x.ProcessType_Code,
//				x.Address,
//				x.Lng,
//				x.Lat,
//				x.ExclusionStatus_Code
//			};

//			return GetObjectsByCondition(whereExpression, selectExpression)
//				.Take(objectsCount)
//				.ToList();
//		}

//		#endregion
//	}
//}
