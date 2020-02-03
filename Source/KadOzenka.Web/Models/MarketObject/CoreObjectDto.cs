using System;
using System.Collections.Generic;
using System.Linq;
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
		public string Market { get; set; }
		public string DealType { get; set; }
        public DealType DealTypeCode { get; set; }
		public DateTime? ParserTime { get; set; }
        public DateTime? LastDateUpdate { get; set; }
        public string Address { get; set; }
		public string Metro { get; set; }
		public decimal? Area { get; set; }
		public string Description { get; set; }
		public long? Price { get; set; }
		public string CadastralNumber { get; set; }
		public string CadastralQuartal { get; set; }
		public string District { get; set; }
		public string BuildingCadastralNumber { get; set; }
		public long? BuildingYear { get; set; }
		public long? FloorsCount { get; set; }
		public long? FloorNumber { get; set; }
		public decimal? AreaKitchen { get; set; }
		public decimal? AreaLiving { get; set; }
		public long? Zone { get; set; }
		public string Group { get; set; }
		public string Subgroup { get; set; }
		public List<PriceHistoryDto> PriceHistories { get; set; }
		public bool IsRangePriceHistory { get; set; }
		public string Url { get; set; }
		public string Status { get; set; }
		public decimal? Latitude { get; set; }
		public decimal? Longitude { get; set; }
		public ProcessStep ProcessType { get; set; }
		public MarketTypes MarketType { get; set; }
        public string CIPJSType { get; set; }
        public PropertyTypesCIPJS CIPJSTypeCode { get; set; }
        public string MarketSegment { get; set; }
        public MarketSegment MarketSegmentCode { get; set; }
        public decimal? PricePerSquareMeter { get; set; }

		public static CoreObjectDto OMMap(OMCoreObject entity)
		{
            var dto = new CoreObjectDto
            {
                Id = entity.Id,
                Market = entity.Market,
                DealType = entity.DealType,
                DealTypeCode = entity.DealType_Code,
                ParserTime = entity.ParserTime,
                LastDateUpdate = entity.LastDateUpdate, // != null ? entity.LastDateUpdate : new DateTime(1970, 1, 1, 0, 0, 0, 1),
                Address = entity.Address,
				Metro = entity.Metro,
				Area = entity.PropertyTypesCIPJS_Code == PropertyTypesCIPJS.LandArea ? entity.AreaLand * 100 : entity.Area,
				Description = entity.Description,
				Price = entity.Price,
				CadastralNumber = entity.CadastralNumber,
				CadastralQuartal = entity.CadastralQuartal,
				District = entity.District,
				BuildingCadastralNumber = entity.BuildingCadastralNumber,
				BuildingYear = entity.BuildingYear,
				FloorsCount = entity.FloorsCount,
				FloorNumber = entity.FloorNumber,
				AreaKitchen = entity.AreaKitchen,
				AreaLiving = entity.AreaLiving,
				Zone = entity.Zone,
				Group = entity.Group,
				Subgroup = entity.Subgroup,
				Url = entity.Url,
				Status = entity.ProcessType,
				Latitude = entity.Lat,
				Longitude = entity.Lng,
				ProcessType = entity.ProcessType_Code,
				MarketType = entity.Market_Code,
                CIPJSType = entity.PropertyTypesCIPJS,
                CIPJSTypeCode = entity.PropertyTypesCIPJS_Code,
                MarketSegment = entity.PropertyMarketSegment,
                MarketSegmentCode = entity.PropertyMarketSegment_Code,
                PricePerSquareMeter =
					entity.DealType_Code != ObjectModel.Directory.DealType.RentDeal &&
					entity.DealType_Code != ObjectModel.Directory.DealType.RentSuggestion
						? GetPricePerSquareMeter(entity) : (decimal?) null
			};
			if (entity.PriceHistory?.Count > 0)
			{
				dto.PriceHistories = entity.PriceHistory
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

		private static decimal? GetPricePerSquareMeter(OMCoreObject entity)
		{
			decimal? result;
			if (entity.PropertyTypesCIPJS_Code == PropertyTypesCIPJS.LandArea && entity.Price.HasValue && entity.AreaLand.HasValue && entity.AreaLand != 0) result = entity.Price / (entity.AreaLand * 100);
			else if (entity.Price.HasValue && entity.Area.HasValue && entity.Area != 0) result = entity.Price / entity.Area;
			else result = (decimal?) null;
			return result;
		}
	}
}
