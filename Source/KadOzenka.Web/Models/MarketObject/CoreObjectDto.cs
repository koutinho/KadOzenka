using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Core.Shared.Extensions;
using MarketPlaceBusiness;
using MarketPlaceBusiness.Dto;
using MarketPlaceBusiness.Interfaces;
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


		//public string Renovation { get; set; }
		//public string BuildingLine { get; set; }

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
                //Renovation = entity.Renovation,
                //BuildingLine = entity.BuildingLine
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
			
			if (entity.Price.HasValue && entity.Area.HasValue && entity.Area != 0)
				result = entity.Price / entity.Area;

			else
				result = null;

			return result;
		}
	}
}
