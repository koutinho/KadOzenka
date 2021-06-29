using System.Collections.Generic;
using KadOzenka.Dal.Groups.Dto;
using KadOzenka.Dal.Models.Filters;

namespace KadOzenka.Web.Models.Tour
{
	public class TourGroupGroupingSettingsModel
	{
		public long? GroupId { get; set; }

		public List<Filters> GroupFilters { get; set; }

		public List<long?> KoAttributes { get; set; }

		public TourGroupGroupingSettingsModel()
		{
			GroupFilters = new List<Filters>
			{
				new Filters(), new Filters()
			};
			KoAttributes = new List<long?>{0,0};
		}
		public TourGroupGroupingSettingsDto ToDto()
		{
			return new()
			{
				GroupId = GroupId,
				GroupFilterSetting = GroupFilters,
				KoAttributes = KoAttributes
			};
		}

		public static TourGroupGroupingSettingsModel FromDto(TourGroupGroupingSettingsDto dto)
		{
			return new()
			{
				GroupId = dto.GroupId,
				GroupFilters = dto.GroupFilterSetting,
				KoAttributes = dto.KoAttributes
			};
		}
	}
}
