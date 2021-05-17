﻿using System;
using System.Linq.Expressions;
using AutoMapper;
using ObjectModel.Market;

namespace MarketPlaceBusiness.Dto.AutoMapper
{
	public class MappingProfile : Profile
	{
		public MappingProfile()
		{
			CreateMap<OMCoreObject, MarketObjectDto>()
				.ForMember(m => m.Market, SetPreCondition(p => p.Market))
				.ForMember(m => m.Market_Code, SetPreCondition(y => y.Market_Code))
				.ForMember(m => m.Price, SetPreCondition(y => y.Price))
				.ForMember(m => m.PricePerMeter, SetPreCondition(y => y.PricePerMeter))
				.ForMember(m => m.ParserTime, SetPreCondition(y => y.ParserTime))
				.ForMember(m => m.Address, SetPreCondition(y => y.Address))
				.ForMember(m => m.DealType, SetPreCondition(y => y.DealType))
				.ForMember(m => m.DealType_Code, SetPreCondition(y => y.DealType_Code))
				.ForMember(m => m.FloorNumber, SetPreCondition(y => y.FloorNumber))
				.ForMember(m => m.Area, SetPreCondition(y => y.Area))
				.ForMember(m => m.CadastralNumber, SetPreCondition(y => y.CadastralNumber))
				.ForMember(m => m.PropertyMarketSegment, SetPreCondition(y => y.PropertyMarketSegment))
				.ForMember(m => m.PropertyMarketSegment_Code, SetPreCondition(y => y.PropertyMarketSegment_Code))
				.ForMember(m => m.WallMaterial, SetPreCondition(y => y.WallMaterial))
				.ForMember(m => m.WallMaterial_Code, SetPreCondition(y => y.WallMaterial_Code))
				.ForMember(m => m.QualityClass, SetPreCondition(y => y.QualityClass))
				.ForMember(m => m.QualityClass_Code, SetPreCondition(y => y.QualityClass_Code))
				.ForMember(m => m.PropertyTypesCIPJS, SetPreCondition(y => y.PropertyTypesCIPJS))
				.ForMember(m => m.PropertyTypesCIPJS_Code, SetPreCondition(y => y.PropertyTypesCIPJS_Code));
				//.ForMember(m => m.Renovation, SetPreCondition(y => y.Renovation));
				//.ForMember(m => m.BuildingLine, SetPreCondition(y => y.BuildingLine))
		}


		#region Support Methods

		private Action<IMemberConfigurationExpression<OMCoreObject, MarketObjectDto, T>> SetPreCondition<T>(
			Expression<Func<OMCoreObject, T>> expression)
		{
			return x => x.PreCondition(coreObject => coreObject.PropertyInited(expression));
		}

		#endregion
	}
}
