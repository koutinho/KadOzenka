//using System.Collections.Generic;
//using System.Linq;
//using ObjectModel.Market;

//namespace KadOzenka.Dal.OutliersChecking.Dto
//{
//	public class ObjectsByLocationSliceDto : LocationSliceDto
//	{
//		public List<OMCoreObject> MarketObjects { get; set; }

//		public static List<ObjectsByLocationSliceDto> FromEntityList(List<OMCoreObject> marketObjects)
//		{
//			var result = new List<ObjectsByLocationSliceDto>();
//			marketObjects
//				.OrderBy(x => x.Zone).ThenBy(x => x.District).ThenBy(x => x.Neighborhood)
//				.GroupBy(x => new{x.Zone, x.District_Code, x.Neighborhood_Code})
//				.ToList()
//				.ForEach(x =>
//				{
//					result.Add(new ObjectsByLocationSliceDto
//					{
//						Zone = x.Key.Zone.Value,
//						District = x.Key.District_Code,
//						Region = x.Key.Neighborhood_Code,
//						MarketObjects = x.ToList()
//					});
//				});

//			return result;
//		}
//	}
//}
