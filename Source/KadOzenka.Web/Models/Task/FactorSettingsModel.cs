using Core.Shared.Extensions;
using KadOzenka.Dal.Tasks.Dto;

namespace KadOzenka.Web.Models.Task
{
	public class FactorSettingsModel
	{
		public long Id { get; set; }
		public string FactorName { get; set; }
		public string FactorInheritanceType { get; set; }
		public string Source { get; set; }
		public string CorrectFactorName { get; set; }

		public static FactorSettingsModel FromDto(FactorSettingsDto dto)
		{
			return new FactorSettingsModel
			{
				Id = dto.Id,
				FactorName = dto.FactorName,
				FactorInheritanceType = dto.FactorInheritance.GetEnumDescription(),
				Source = dto.Source,
				CorrectFactorName = dto.CorrectFactorName
			};
		}
	}
}
