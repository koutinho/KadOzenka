using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using MarketPlaceBusiness.Dto;
using ObjectModel.Directory;
using ObjectModel.Market;

namespace KadOzenka.Web.Models.MarketObject
{
	public class CoreObjectDto
	{
		public class PriceHistoryDto
		{
			public long Id { get; set; }
			public DateTime ChangingDate { get; set; }
			public long PriceValueTo { get; set; }
			public long? PriceValueFrom { get; set; }
		}

		public long Id { get; set; }
		[DisplayName("Источник")]
		public string Market { get; set; }
		[DisplayName("Адрес")]
		public string Address { get; set; }
		public string AddressShort { get; set; }
		[DisplayName("Площадь")]
		public decimal? Area { get; set; }
		[DisplayName("Площадь")]
		public string AreaStr { get; set; }
		[DisplayName("Цена")]
		public decimal? Price { get; set; }
		[DisplayName("Кадастровый номер")]
		public string CadastralNumber { get; set; }
		public long? FloorNumber { get; set; }
		public List<PriceHistoryDto> PriceHistories { get; set; }
		public bool IsRangePriceHistory { get; set; }
		public MarketTypes MarketType { get; set; }
        public string CIPJSType { get; set; }
        public PropertyTypesCIPJS CIPJSTypeCode { get; set; }
        [DisplayName("Сегмент")]
		public string MarketSegment { get; set; }
        public MarketSegment MarketSegmentCode { get; set; }
        public decimal? PricePerSquareMeter { get; set; }
		public string ImageUrl { get; set; }
		public string MarketLogoUrl { get; set; }
		public QualityClass? QualityClassCode { get; set; }

		[DisplayName("Дата загрузки")]
		public DateTime? DownloadDate { get; set; }

		[DisplayName("Внешний Id объявления")]
		public string ExternalAdvertisementId { get; set; }

		[DisplayName("Текст объявления")]
		public string AdvertisementDescription { get; set; }

		[DisplayName("Площадь от")]
		public decimal? AreaFrom { get; set; }

		[DisplayName("Название")]
		public string Name { get; set; }

		[DisplayName("Номер на площадке")]
		public long? FlatNumber { get; set; }

		[DisplayName("Номер секции")]
		public string SectionNumber { get; set; }

		[DisplayName("Тип квартиры")]
		public string FlatType { get; set; }

		[DisplayName("Тип сделки")]
		public string DealType { get; set; }

		[DisplayName("Линия застройки")]
		public string HouseLine { get; set; }

		[DisplayName("Застройщик")]
		public string Developer { get; set; }

		[DisplayName("Состояние отделки")]
		public string FinishingCondition { get; set; }

		[DisplayName("Тип дома")]
		public string HouseType { get; set; }

		[DisplayName("Планировка")]
		public string Layout { get; set; }

		[DisplayName("Вид разрешённого использования")]
		public string PermittedUseType { get; set; }

		[DisplayName("Подъездные пути")]
		public string DrivewayType { get; set; }

		[DisplayName("Статус земли")]
		public string ParcelStatus { get; set; }

		[DisplayName("Тип участка")]
		public string ParcelType { get; set; }

		[DisplayName("Единица измерения площади участка")]
		public string ParcelAreaUnitType { get; set; }

		[DisplayName("Мощность электроснабжения, кВТ")]
		public long? ElectricityPower { get; set; }

		[DisplayName("Возможность подключения электроснабжения")]
		public bool? PossibilityToConnectElectricity { get; set; }

		[DisplayName("Локация электроснабжения")]
		public string ElectricityLocationType { get; set; }

		[DisplayName("Давление газа")]
		public string GasPressureType { get; set; }

		[DisplayName("Емкость газоснабжения, м³/час")]
		public long? GasCapacity { get; set; }

		[DisplayName("Возможность подключения газа")]
		public bool? PossibilityToConnectGas { get; set; }

		[DisplayName("Локация газоснабжения")]
		public string GasLocationType { get; set; }

		[DisplayName("Тип канализации")]
		public string DrainageType { get; set; }

		[DisplayName("Объём канализации, м³/сутки")]
		public long? DrainageCapacity { get; set; }

		[DisplayName("Возможность подключения канализации")]
		public bool? PossibilityToConnectDrainage { get; set; }

		[DisplayName("Локация канализации")]
		public string DrainageLocationType { get; set; }

		[DisplayName("Тип водоснабжения")]
		public string WaterType { get; set; }

		[DisplayName("Объём водоснабжения, м³/сутки")]
		public long? WaterCapacity { get; set; }

		[DisplayName("Возможно подключить водоснабжение")]
		public bool? PossibilityToConnectWater { get; set; }

		[DisplayName("Локация водоснабжения")]
		public string WaterLocationType { get; set; }


		public static CoreObjectDto OMMap(MarketObjectDto entity, List<OMPriceHistory> priceHistory)
		{
            var dto = new CoreObjectDto
            {
                Id = entity.Id,
                Market = entity.Market,
                Address = entity.Address,
                Area = entity.Area,
				Price = entity.Price,
				CadastralNumber = entity.CadastralNumber,
				FloorNumber = entity.FloorNumber,
				MarketType = entity.Market_Code,
                CIPJSType = entity.PropertyTypesCIPJS,
                CIPJSTypeCode = entity.PropertyTypesCIPJS_Code,
                MarketSegment = entity.PropertyMarketSegment,
                MarketSegmentCode = entity.PropertyMarketSegment_Code,
                PricePerSquareMeter = GetPricePerSquareMeter(entity),
                QualityClassCode = entity.QualityClass_Code,
                DownloadDate = entity.DownloadDate,
                ExternalAdvertisementId = entity.ExternalAdvertisementId,
                AdvertisementDescription = entity.AdvertisementDescription,
                AreaFrom = entity.AreaFrom,
                Name = entity.Name,
                FlatNumber = entity.FlatNumber,
				SectionNumber = entity.SectionNumber,
				FlatType = entity.FlatType,
				DealType = entity.DealType,
				HouseLine = entity.HouseLine,
				Developer = entity.Developer,
				FinishingCondition = entity.FinishingCondition,
				HouseType = entity.HouseType,
				Layout = entity.Layout,
				PermittedUseType = entity.PermittedUseType,
				DrivewayType = entity.DrivewayType,
				ParcelAreaUnitType = entity.ParcelAreaUnitType,
				ParcelType = entity.ParcelType,
				ParcelStatus = entity.ParcelStatus,
				ElectricityLocationType = entity.ElectricityLocationType,
				PossibilityToConnectElectricity = entity.PossibilityToConnectElectricity,
				ElectricityPower = entity.ElectricityPower,
				GasLocationType = entity.GasLocationType,
				PossibilityToConnectGas = entity.PossibilityToConnectGas,
				GasCapacity = entity.GasCapacity,
				GasPressureType = entity.GasPressureType,
				DrainageLocationType = entity.DrainageLocationType,
				PossibilityToConnectDrainage = entity.PossibilityToConnectDrainage,
				DrainageCapacity = entity.DrainageCapacity,
				DrainageType = entity.DrainageType,
				WaterLocationType = entity.WaterLocationType,
				PossibilityToConnectWater = entity.PossibilityToConnectWater,
				WaterCapacity = entity.WaterCapacity,
				WaterType = entity.WaterType
            };
			if (priceHistory?.Count > 0)
			{
				dto.PriceHistories = priceHistory
					.OrderByDescending(x => x.ChangingDate)
					.Select(x =>
						new PriceHistoryDto
						{
							Id = x.Id,
							ChangingDate = x.ChangingDate,
							PriceValueTo = x.PriceValueTo,
							PriceValueFrom = x.PriceValueFrom
						})
					.ToList();
				dto.IsRangePriceHistory = dto.PriceHistories.Any(x => x.PriceValueFrom.HasValue);

				//TODO: calculation of changes between history records will be implemented later
				//dto.PriceHistories = new List<PriceHistoryDto>();
				//var orderedHistories = entity.PriceHistory.OrderByDescending(x => x.ChangingDate).ToList();
				//for(var i = 0; i < orderedHistories.Count(); i++)
				//{
				//	var historyDto = new PriceHistoryDto
				//	{
				//		Id = orderedHistories[i].Id,
				//		ChangingDate = orderedHistories[i].ChangingDate,
				//		PriceValue = orderedHistories[i].PriceValue,
				//		PriceValueDiff = i == orderedHistories.Count() - 1
				//			? (long?) null
				//			: orderedHistories[i].PriceValue - orderedHistories[i + 1].PriceValue
				//	};
				//	dto.PriceHistories.Add(historyDto);
				//}
			}
			return dto;
		}


		private static decimal? GetPricePerSquareMeter(MarketObjectDto entity)
		{
			decimal? result;
			
			if (entity.Area != 0)
				result = entity.Price / entity.Area;

			else
				result = null;

			return result;
		}
	}
}
