using Core.Register.QuerySubsystem;
using MarketPlaceBusiness.Dto;
using MarketPlaceBusiness.Interfaces;
using ObjectModel.Directory;
using ObjectModel.Market;

namespace MarketPlaceBusiness
{
	public class MarketObjectsForMapService : AMarketObjectBaseService, IMarketObjectsForMapService
	{
		public QSQuery<OMCoreObject> GetBaseQuery()
		{
			return OMCoreObject.Where(x =>
				(x.ProcessType_Code == ProcessStep.InProcess || x.ProcessType_Code == ProcessStep.Dealed) &&
				x.Lng != null && x.Lat != null && (x.LastDateUpdate != null || x.Market_Code == MarketTypes.Rosreestr));
		}

		public void UpdateInfoFromCard(MarketObjectDto dto)
		{
			var obj = GetById(dto.Id);

			obj.Lng = dto.Lng;
			obj.Lat = dto.Lat;
			obj.PropertyTypesCIPJS_Code = dto.PropertyTypesCIPJS_Code;
			obj.PropertyMarketSegment_Code = dto.PropertyMarketSegment_Code;
			obj.ProcessType_Code = dto.ProcessType_Code;
			obj.EntranceType = dto.EntranceType;
			obj.QualityClass_Code = dto.QualityClass_Code;
			obj.Renovation = dto.Renovation;
			obj.BuildingLine = dto.BuildingLine;
			obj.FloorNumber = dto.FloorNumber;

			obj.Save();
		}
	}
}
