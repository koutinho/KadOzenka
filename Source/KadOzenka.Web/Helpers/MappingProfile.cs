﻿using AutoMapper;
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
				.ForMember(m => m.ProcessType_Code, x => x.MapFrom(y => y.StatusCode))
				.ForMember(m => m.QualityClass_Code, x => x.MapFrom(y => y.QualityClassCode))
				.ForMember(m => m.Lat, x => x.MapFrom(y => y.Lat))
				.ForMember(m => m.Lng, x => x.MapFrom(y => y.Lng));
		}
	}
}