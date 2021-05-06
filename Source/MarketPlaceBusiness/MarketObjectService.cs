using System.Collections.Generic;
using System.Linq;
using MarketPlaceBusiness.Common;
using MarketPlaceBusiness.Interfaces;
using ObjectModel.Directory;
using ObjectModel.Market;

namespace MarketPlaceBusiness
{
	public class MarketObjectService : AMarketObjectBaseService, IMarketObjectService
	{
		//TODO inject via IoC
		public MarketObjectService(IMarketObjectsRepository marketObjectsRepository = null)
			: base(marketObjectsRepository)
		{
		}

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

		public List<OMCoreObject> GetObjectsToAssignDistrictsRegionsAndZones()
		{
			return OMCoreObject.Where(x =>
					x.ZoneRegion == null && (x.ProcessType_Code == ProcessStep.InProcess ||
					                         x.ProcessType_Code == ProcessStep.Dealed))
				.Select(x => new
				{
					x.District_Code, x.Neighborhood_Code, x.Neighborhood, x.ZoneRegion, x.Market_Code, x.Market,
					x.CadastralQuartal, x.Zone
				})
				.Execute()
				.ToList();
		}

		public List<OMCoreObject> GetObjectsToAssignCoordinates()
		{
			return OMCoreObject
				.Where(x => x.Lng == null && x.Lat == null)
				.Select(x => new {x.ProcessType_Code, x.Address, x.Lng, x.Lat, x.ExclusionStatus_Code})
				.Execute()
				.ToList()
				.Take(1000)
				.ToList();
		}

		public decimal? GetPricePerSquareMeter(OMCoreObject entity)
		{
			decimal? result;
			if (entity.PropertyTypesCIPJS_Code == PropertyTypesCIPJS.LandArea && entity.Price.HasValue &&
			    entity.AreaLand.HasValue && entity.AreaLand != 0)
				result = entity.Price / (entity.AreaLand * 100);

			else if (entity.Price.HasValue && entity.Area.HasValue && entity.Area != 0)
				result = entity.Price / entity.Area;

			else 
				result = null;

			return result;
		}
	}
}
