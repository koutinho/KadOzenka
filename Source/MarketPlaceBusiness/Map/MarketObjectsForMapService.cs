using Core.Register.QuerySubsystem;
using MarketPlaceBusiness.Common;
using MarketPlaceBusiness.Common.Dto;
using ObjectModel.Market;

namespace MarketPlaceBusiness.Map
{
	public class MarketObjectsForMapService : AMarketObjectBaseService, IMarketObjectsForMapService
	{
		public QSQuery<OMCoreObject> GetBaseQuery()
		{
			//return OMCoreObject.Where(x => x.Lng != null && x.Lat != null && x.Market_Code == MarketTypes.Rosreestr);
			return OMCoreObject.Where(x => true);
		}

		public void UpdateInfoFromCard(MarketObjectDto dto)
		{
			var obj = GetById(dto.Id);

			obj.PropertyTypesCIPJS_Code = dto.PropertyTypesCIPJS_Code;
			obj.PropertyMarketSegment_Code = dto.PropertyMarketSegment_Code;
			obj.QualityClass_Code = dto.QualityClass_Code;
			//obj.Renovation = dto.Renovation;
			//obj.BuildingLine = dto.BuildingLine;
			obj.FloorNumber = dto.FloorNumber;

			obj.Save();
		}
	}
}
