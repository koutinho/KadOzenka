//using System.Collections.Generic;
//using Core.Shared.Extensions;
//using KadOzenka.Dal.OutliersChecking.Dto;
//using ObjectModel.Directory;

//namespace KadOzenka.Web.Models.MarketObject
//{
//	public class OutliersSettingsModel
//	{
//		public long Id { get; set; }
//		public long Zone { get; set; }
//		public string ZoneName { get; set; }
//		public Hunteds District { get; set; }
//		public string DistrictName { get; set; }
//		public Districts Region { get; set; }
//		public string RegionName { get; set; }
//		public decimal? MinDeltaCoef { get; set; }
//		public decimal? MaxDeltaCoef { get; set; }

//		public static List<OutliersSettingsModel> FromDto(List<OutliersCheckingSettingDto> dtos)
//		{
//			var models = new List<OutliersSettingsModel>();
//			dtos.ForEach(x => models.Add(new OutliersSettingsModel
//			{
//				Id = x.Id, 
//				Zone = x.Zone,
//				ZoneName = $"Зона {x.Zone}", 
//				District = x.District,
//				DistrictName = x.District.GetShortTitle(), 
//				Region = x.Region,
//				RegionName = x.Region.GetEnumDescription(),
//				MinDeltaCoef = x.MinDeltaCoef, 
//				MaxDeltaCoef = x.MaxDeltaCoef
//			}));

//			return models;
//		}

//		public OutliersCheckingSettingDto ToDto()
//		{
//			return new OutliersCheckingSettingDto
//			{
//				Id = Id,
//				Zone = Zone,
//				District = District,
//				Region = Region,
//				MinDeltaCoef = MinDeltaCoef,
//				MaxDeltaCoef = MaxDeltaCoef
//			};
//		}
//	}
//}
