using System.Collections.Generic;
using KadOzenka.Dal.Models.Filters;

namespace KadOzenka.Dal.Groups.Dto
{
	public class TourGroupGroupingSettingsDto
	{
		public long? GroupId { get; set; }

		public List<long?> DictionaryId { get; set; }

		public List<string> DictionaryValue { get; set; }
		public List<Filters> GroupFilterSetting { get; set; }

		public List<long?> KoAttributes { get; set; }
	}
}
