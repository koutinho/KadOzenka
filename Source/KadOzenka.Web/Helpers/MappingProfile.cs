using AutoMapper;
using KadOzenka.Web.Models.MarketObject;
using MarketPlaceBusiness.Dto;

namespace KadOzenka.Web.Helpers
{
	public class MappingProfile : Profile
	{
		public MappingProfile()
		{
			CreateMap<MarketSaveObjectDto, MarketObjectDto>()
				.ForMember(m => m.PropertyTypesCIPJS_Code, x => x.MapFrom(scr => scr.PropertyTypeCode))
				.ForMember(m => m.PropertyMarketSegment_Code, x => x.MapFrom(y => y.MarketSegmentCode))
				.ForMember(m => m.QualityClass_Code, x => x.MapFrom(y => y.QualityClassCode));
		}
	}
}
