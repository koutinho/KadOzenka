using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using MarketPlaceBusiness.Common;
using MarketPlaceBusiness.Dto;
using MarketPlaceBusiness.Interfaces;
using ObjectModel.Directory;
using ObjectModel.Market;

namespace MarketPlaceBusiness
{
	public class MarketObjectService : AMarketObjectBaseService, IMarketObjectService
	{
		//TODO inject via IoC
		public MarketObjectService(IMarketObjectsRepository marketObjectsRepository = null, IMapper mapper = null)
			: base(marketObjectsRepository, mapper)
		{
		}

		#region Проверк на дублирование

		//public List<OMCoreObject> GetObjectsForDuplicatesChecking()
		//{
		//	return OMCoreObject
		//		.Where(x => x.ProcessType_Code == ProcessStep.CadastralNumberStep ||
		//		            x.ProcessType_Code == ProcessStep.InProcess ||
		//		            x.ExclusionStatus_Code == ExclusionStatus.Duplicate)
		//		.Select(x => new { x.CadastralNumber, x.DealType_Code, x.PropertyTypesCIPJS_Code, x.PropertyMarketSegment_Code, x.Market_Code, x.Market, x.ExclusionStatus_Code, x.Price, x.Area, x.ParserTime, x.DealType })
		//		.Execute()
		//		.ToList();
		//}

		//public List<List<OMCoreObject>> SplitListByPersentForDuplicates(List<OMCoreObject> list, double areaDelta, double priceDelta,
		//	int selectedMarket)
		//{
		//	List<List<OMCoreObject>> buffer = new(), result = new();
		//	var FEL = list.ElementAt(0);
		//	int counter = 0;
		//	buffer.Add(new List<OMCoreObject>());
		//	list.ForEach(x =>
		//	{
		//		if (FEL.Area.GetValueOrDefault() >= x.Area.GetValueOrDefault() * Convert.ToDecimal(1 - areaDelta) &&
		//		    FEL.Price.GetValueOrDefault() >= x.Price.GetValueOrDefault() * Convert.ToDecimal(1 - priceDelta) &&
		//		    FEL.Price.GetValueOrDefault() <= x.Price.GetValueOrDefault() * Convert.ToDecimal(1 + priceDelta))
		//			buffer.ElementAt(counter).Add(x);
		//		else
		//		{
		//			FEL = x;
		//			counter++;
		//			buffer.Add(new List<OMCoreObject>());
		//			buffer.ElementAt(counter).Add(FEL);
		//		}
		//	});

		//	buffer.ForEach(x => result.Add(x.OrderBy(y => y.Market_Code != (MarketTypes) selectedMarket)
		//		.ThenByDescending(y => y.ParserTime.GetValueOrDefault()).ToList()));

		//	return result;
		//}

		#endregion

		//public List<OMCoreObject> GetObjectsToAssignDistrictsRegionsAndZones()
		//{
		//	//раньше были устовия на District, Region, Zone, но по факту процедура их не использует, 
		//	//поэтому они были удалены из Where
		//	return OMCoreObject.Where(x =>
		//			x.ZoneRegion == null && (x.ProcessType_Code == ProcessStep.InProcess ||
		//			                         x.ProcessType_Code == ProcessStep.Dealed))
		//		.Select(x => new
		//		{
		//			x.District_Code,
		//			x.Neighborhood_Code,
		//			x.Neighborhood,
		//			x.ZoneRegion,
		//			x.Market_Code,
		//			x.Market,
		//			x.CadastralQuartal,
		//			x.Zone
		//		})
		//		.Execute()
		//		.ToList();
		//}

		public List<OMCoreObject> GetObjectsToAssignCoordinates()
		{
			return OMCoreObject
				.Where(x => x.Lng == null && x.Lat == null)
				//.Select(x => new {x.ProcessType_Code, x.Address, x.Lng, x.Lat, x.ExclusionStatus_Code})
				.Select(x => new {x.Address, x.Lng, x.Lat})
				.Execute()
				.ToList()
				.Take(1000)
				.ToList();
		}
	}
}
