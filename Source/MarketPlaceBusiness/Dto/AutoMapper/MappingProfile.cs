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
				.ForMember(m => m.Layout, SetPreCondition(y => y.Layout))
				.ForMember(m => m.Layout_Code, SetPreCondition(y => y.Layout_Code))
				.ForMember(m => m.PermittedUseType, SetPreCondition(y => y.PermittedUseType))
				.ForMember(m => m.PermittedUseType_Code, SetPreCondition(y => y.PermittedUseType_Code))
				.ForMember(m => m.DrivewayType, SetPreCondition(y => y.DrivewayType))
				.ForMember(m => m.DrivewayType_Code, SetPreCondition(y => y.DrivewayType_Code))
				.ForMember(m => m.ParcelAreaUnitType, SetPreCondition(y => y.ParcelAreaUnitType))
				.ForMember(m => m.ParcelAreaUnitType_Code, SetPreCondition(y => y.ParcelAreaUnitType_Code))
				.ForMember(m => m.ParcelType, SetPreCondition(y => y.ParcelType))
				.ForMember(m => m.ParcelType_Code, SetPreCondition(y => y.ParcelType_Code))
				.ForMember(m => m.ParcelStatus, SetPreCondition(y => y.ParcelStatus))
				.ForMember(m => m.ParcelStatus_Code, SetPreCondition(y => y.ParcelStatus_Code))
				.ForMember(m => m.ElectricityLocationType, SetPreCondition(y => y.ElectricityLocationType))
				.ForMember(m => m.ElectricityLocationType_Code, SetPreCondition(y => y.ElectricityLocationType_Code))
				.ForMember(m => m.PossibilityToConnectElectricity, SetPreCondition(y => y.PossibilityToConnectElectricity))
				.ForMember(m => m.ElectricityPower, SetPreCondition(y => y.ElectricityPower))
				.ForMember(m => m.GasLocationType, SetPreCondition(y => y.GasLocationType))
				.ForMember(m => m.GasLocationType_Code, SetPreCondition(y => y.GasLocationType_Code))
				.ForMember(m => m.PossibilityToConnectGas, SetPreCondition(y => y.PossibilityToConnectGas))
				.ForMember(m => m.GasCapacity, SetPreCondition(y => y.GasCapacity))
				.ForMember(m => m.GasPressureType, SetPreCondition(y => y.GasPressureType))
				.ForMember(m => m.GasPressureType_Code, SetPreCondition(y => y.GasPressureType_Code))
				.ForMember(m => m.DrainageLocationType, SetPreCondition(y => y.DrainageLocationType))
				.ForMember(m => m.DrainageLocationType_Code, SetPreCondition(y => y.DrainageLocationType_Code))
				.ForMember(m => m.PossibilityToConnectDrainage, SetPreCondition(y => y.PossibilityToConnectDrainage))
				.ForMember(m => m.DrainageCapacity, SetPreCondition(y => y.DrainageCapacity))
				.ForMember(m => m.DrainageType, SetPreCondition(y => y.DrainageType))
				.ForMember(m => m.DrainageType_Code, SetPreCondition(y => y.DrainageType_Code))
				.ForMember(m => m.WaterLocationType, SetPreCondition(y => y.WaterLocationType))
				.ForMember(m => m.WaterLocationType_Code, SetPreCondition(y => y.WaterLocationType_Code))
				.ForMember(m => m.PossibilityToConnectWater, SetPreCondition(y => y.PossibilityToConnectWater))
				.ForMember(m => m.WaterCapacity, SetPreCondition(y => y.WaterCapacity))
				.ForMember(m => m.WaterType, SetPreCondition(y => y.WaterType))
				.ForMember(m => m.WaterType_Code, SetPreCondition(y => y.WaterType_Code));
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
