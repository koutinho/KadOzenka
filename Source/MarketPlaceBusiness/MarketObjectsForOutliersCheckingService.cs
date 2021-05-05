using System.Collections.Generic;
using System.Linq;
using MarketPlaceBusiness.Interfaces;
using ObjectModel.Directory;
using ObjectModel.Market;

namespace MarketPlaceBusiness
{
	public class MarketObjectsForOutliersCheckingService : IMarketObjectsForOutliersCheckingService
	{
		public Dictionary<MarketSegment, List<OMCoreObject>> GetObjectsBySegments(List<PropertyTypesCIPJS> types,
			MarketSegment? segment)
		{
			var query = OMCoreObject.Where(x =>
				(x.ProcessType_Code == ProcessStep.InProcess || x.ProcessType_Code == ProcessStep.Dealed)
				&& x.PropertyMarketSegment != null
				&& x.PricePerMeter != null
				&& x.Zone != null
				&& x.District != null
				&& x.Neighborhood != null
				&& types.Contains(x.PropertyTypesCIPJS_Code));

			if (segment.HasValue)
				query.And(x => x.PropertyMarketSegment_Code == segment.Value);

			return query
				.Select(x => new
				{
					x.Id,
					x.CadastralNumber,
					x.Zone,
					x.District,
					x.District_Code,
					x.DealType,
					x.DealType_Code,
					x.Neighborhood,
					x.Neighborhood_Code,
					x.PricePerMeter,
					x.PropertyMarketSegment,
					x.PropertyMarketSegment_Code,
					x.ProcessType_Code,
					x.ExclusionStatus_Code,
					x.PropertyTypesCIPJS,
					x.PropertyTypesCIPJS_Code
				})
				.Execute()
				.GroupBy(x => x.PropertyMarketSegment_Code)
				.ToDictionary(x => x.Key, x => x.ToList());
		}
	}
}
