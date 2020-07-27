using KadOzenka.Dal.GbuObject.Dto;
using ObjectModel.Directory.Core.LongProcess;

namespace KadOzenka.Web.Models.GbuProgressBar
{
	public class LongProcessViewModel
	{
		public long Id { get; set; }
		public long ProcessTypeId { get; set; }
		public string Name { get; set; }
		public Status? StatusCode { get; set; }
		public string StatusName { get; set; }
		public long? Progress { get; set; }

		public static LongProcessViewModel ToModel(LongProcessDto dto)
		{
			return new LongProcessViewModel
			{
				Id = dto.Id,
				ProcessTypeId = dto.ProcessTypeId,
				Name = dto.Name,
				StatusCode = dto.StatusCode,
				StatusName = dto.StatusName,
				Progress = dto.Progress
			};
		}
	}
}
