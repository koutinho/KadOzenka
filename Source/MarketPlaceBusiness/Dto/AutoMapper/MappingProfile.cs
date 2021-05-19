using System;
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
				.ForMember(m => m.Address, SetPreCondition(y => y.Address))
				.ForMember(m => m.FloorNumber, SetPreCondition(y => y.FloorNumber))
				.ForMember(m => m.Area, SetPreCondition(y => y.Area))
				.ForMember(m => m.CadastralNumber, SetPreCondition(y => y.CadastralNumber))
				.ForMember(m => m.PropertyMarketSegment, SetPreCondition(y => y.PropertyMarketSegment))
				.ForMember(m => m.PropertyMarketSegment_Code, SetPreCondition(y => y.PropertyMarketSegment_Code))
				.ForMember(m => m.QualityClass, SetPreCondition(y => y.QualityClass))
				.ForMember(m => m.QualityClass_Code, SetPreCondition(y => y.QualityClass_Code))
				.ForMember(m => m.PropertyTypesCIPJS, SetPreCondition(y => y.PropertyTypesCIPJS))
				.ForMember(m => m.PropertyTypesCIPJS_Code, SetPreCondition(y => y.PropertyTypesCIPJS_Code))
				.ForMember(m => m.DownloadDate, SetPreCondition(y => y.DownloadDate))
				.ForMember(m => m.ExternalAdvertisementId, SetPreCondition(y => y.ExternalAdvertisementId))
				.ForMember(m => m.AdvertisementDescription, SetPreCondition(y => y.AdvertisementDescription))
				.ForMember(m => m.AreaFrom, SetPreCondition(y => y.AreaFrom))
				.ForMember(m => m.Name, SetPreCondition(y => y.Name))
				.ForMember(m => m.FlatNumber, SetPreCondition(y => y.FlatNumber))
				.ForMember(m => m.SectionNumber, SetPreCondition(y => y.SectionNumber))
				.ForMember(m => m.FlatType, SetPreCondition(y => y.FlatType))
				.ForMember(m => m.DealType, SetPreCondition(y => y.DealType))
				.ForMember(m => m.DealType_Code, SetPreCondition(y => y.DealType_Code))
				.ForMember(m => m.HouseLine, SetPreCondition(y => y.HouseLine))
				.ForMember(m => m.HouseLine_Code, SetPreCondition(y => y.HouseLine_Code))
				.ForMember(m => m.Developer, SetPreCondition(y => y.Developer))
				.ForMember(m => m.FinishingCondition, SetPreCondition(y => y.FinishingCondition))
				.ForMember(m => m.FinishingCondition_Code, SetPreCondition(y => y.FinishingCondition_Code))
				.ForMember(m => m.HouseType, SetPreCondition(y => y.HouseType))
				.ForMember(m => m.HouseType_Code, SetPreCondition(y => y.HouseType_Code))
				;
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
