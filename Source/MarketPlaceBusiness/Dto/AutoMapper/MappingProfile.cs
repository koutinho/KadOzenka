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
				.ForMember(m => m.Url, SetPreCondition(p => p.Url))
				.ForMember(m => m.Market, SetPreCondition(p => p.Market))
				.ForMember(m => m.Market_Code, SetPreCondition(y => y.Market_Code))
				.ForMember(m => m.MarketId, SetPreCondition(y => y.MarketId))
				.ForMember(m => m.Price, SetPreCondition(y => y.Price))
				.ForMember(m => m.PricePerMeter, SetPreCondition(y => y.PricePerMeter))
				.ForMember(m => m.ParserTime, SetPreCondition(y => y.ParserTime))
				.ForMember(m => m.Region, SetPreCondition(y => y.Region))
				.ForMember(m => m.City, SetPreCondition(y => y.City))
				.ForMember(m => m.Address, SetPreCondition(y => y.Address))
				.ForMember(m => m.Metro, SetPreCondition(y => y.Metro))
				.ForMember(m => m.Images, SetPreCondition(y => y.Images))
				.ForMember(m => m.Description, SetPreCondition(y => y.Description))
				.ForMember(m => m.Lat, SetPreCondition(y => y.Lat))
				.ForMember(m => m.DealType, SetPreCondition(y => y.DealType))
				.ForMember(m => m.DealType_Code, SetPreCondition(y => y.DealType_Code))
				.ForMember(m => m.RoomsCount, SetPreCondition(y => y.RoomsCount))
				.ForMember(m => m.Lng, SetPreCondition(y => y.Lng))
				.ForMember(m => m.FloorNumber, SetPreCondition(y => y.FloorNumber))
				.ForMember(m => m.FloorsCount, SetPreCondition(y => y.FloorsCount))
				.ForMember(m => m.Area, SetPreCondition(y => y.Area))
				.ForMember(m => m.AreaKitchen, SetPreCondition(y => y.AreaKitchen))
				.ForMember(m => m.AreaLiving, SetPreCondition(y => y.AreaLiving))
				.ForMember(m => m.AreaLand, SetPreCondition(y => y.AreaLand))
				.ForMember(m => m.BuildingYear, SetPreCondition(y => y.BuildingYear))
				.ForMember(m => m.ZoneRegion, SetPreCondition(y => y.ZoneRegion))
				.ForMember(m => m.CustomZone, SetPreCondition(y => y.CustomZone))
				.ForMember(m => m.District, SetPreCondition(y => y.District))
				.ForMember(m => m.District_Code, SetPreCondition(y => y.District_Code))
				.ForMember(m => m.Neighborhood, SetPreCondition(y => y.Neighborhood))
				.ForMember(m => m.Neighborhood_Code, SetPreCondition(y => y.Neighborhood_Code))
				.ForMember(m => m.CadastralNumber, SetPreCondition(y => y.CadastralNumber))
				.ForMember(m => m.BuildingCadastralNumber, SetPreCondition(y => y.BuildingCadastralNumber))
				.ForMember(m => m.CadastralQuartal, SetPreCondition(y => y.CadastralQuartal))
				.ForMember(m => m.Group, SetPreCondition(y => y.Group))
				.ForMember(m => m.Group_Code, SetPreCondition(y => y.Group_Code))
				.ForMember(m => m.Subgroup, SetPreCondition(y => y.Subgroup))
				.ForMember(m => m.Subgroup_Code, SetPreCondition(y => y.Subgroup_Code))
				.ForMember(m => m.Zone, SetPreCondition(y => y.Zone))
				.ForMember(m => m.ProcessType, SetPreCondition(y => y.ProcessType))
				.ForMember(m => m.ProcessType_Code, SetPreCondition(y => y.ProcessType_Code))
				.ForMember(m => m.ExclusionStatus, SetPreCondition(y => y.ExclusionStatus))
				.ForMember(m => m.ExclusionStatus_Code, SetPreCondition(y => y.ExclusionStatus_Code))
				.ForMember(m => m.PhoneNumber, SetPreCondition(y => y.PhoneNumber))
				.ForMember(m => m.PropertyMarketSegment, SetPreCondition(y => y.PropertyMarketSegment))
				.ForMember(m => m.PropertyMarketSegment_Code, SetPreCondition(y => y.PropertyMarketSegment_Code))
				.ForMember(m => m.WallMaterial, SetPreCondition(y => y.WallMaterial))
				.ForMember(m => m.WallMaterial_Code, SetPreCondition(y => y.WallMaterial_Code))
				.ForMember(m => m.QualityClass, SetPreCondition(y => y.QualityClass))
				.ForMember(m => m.QualityClass_Code, SetPreCondition(y => y.QualityClass_Code))
				.ForMember(m => m.SubwaySpace, SetPreCondition(y => y.SubwaySpace))
				.ForMember(m => m.RegionId, SetPreCondition(y => y.RegionId))
				.ForMember(m => m.CityId, SetPreCondition(y => y.CityId))
				.ForMember(m => m.LastDateUpdate, SetPreCondition(y => y.LastDateUpdate))
				.ForMember(m => m.PropertyTypesCIPJS, SetPreCondition(y => y.PropertyTypesCIPJS))
				.ForMember(m => m.PropertyTypesCIPJS_Code, SetPreCondition(y => y.PropertyTypesCIPJS_Code))
				.ForMember(m => m.PropertyLawType, SetPreCondition(y => y.PropertyLawType))
				.ForMember(m => m.PropertyPartSize, SetPreCondition(y => y.PropertyPartSize))
				.ForMember(m => m.PriceAfterCorrectionByDate, SetPreCondition(y => y.PriceAfterCorrectionByDate))
				.ForMember(m => m.PriceAfterCorrectionByBargain, SetPreCondition(y => y.PriceAfterCorrectionByBargain))
				.ForMember(m => m.PriceAfterCorrectionByRooms, SetPreCondition(y => y.PriceAfterCorrectionByRooms))
				.ForMember(m => m.FormalizedAddressId, SetPreCondition(y => y.FormalizedAddressId))
				.ForMember(m => m.PriceAfterCorrectionForFirstFloor, SetPreCondition(y => y.PriceAfterCorrectionForFirstFloor))
				.ForMember(m => m.PriceAfterCorrectionByStage, SetPreCondition(y => y.PriceAfterCorrectionByStage))
				.ForMember(m => m.OwnershipType, SetPreCondition(y => y.OwnershipType))
				.ForMember(m => m.PlacementType, SetPreCondition(y => y.PlacementType))
				.ForMember(m => m.Quality, SetPreCondition(y => y.Quality))
				.ForMember(m => m.IsOperatingCostsIncluded, SetPreCondition(y => y.IsOperatingCostsIncluded))
				.ForMember(m => m.IsUtilitiesIncluded, SetPreCondition(y => y.IsUtilitiesIncluded))
				.ForMember(m => m.Vat, SetPreCondition(y => y.Vat))
				.ForMember(m => m.Vat_Code, SetPreCondition(y => y.Vat_Code))
				.ForMember(m => m.EntranceType, SetPreCondition(y => y.EntranceType))
				.ForMember(m => m.Renovation, SetPreCondition(y => y.Renovation))
				.ForMember(m => m.BuildingLine, SetPreCondition(y => y.BuildingLine))
				.ForMember(m => m.CCT, SetPreCondition(y => y.CCT));
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