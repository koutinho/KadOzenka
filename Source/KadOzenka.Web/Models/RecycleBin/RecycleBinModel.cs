using System;
using KadOzenka.Dal.LongProcess.RecycleBin;
using KadOzenka.Dal.RecycleBin.Dto;

namespace KadOzenka.Web.Models.RecycleBin
{
	public class RecycleBinModel
	{
		public long Id { get; set; }
		public string ObjectType { get; set; }
		public long ObjectRegisterId { get; set; }
		public string ObjectName { get; set; }
		public DateTime DeletedTime { get; set; }
		public bool IsDuplicateProcessExists { get; set; }

		public static RecycleBinModel FromDto(RecycleBinDto dto, bool isDuplicateProcessExists)
		{
			return new RecycleBinModel
			{
				Id = dto.Id,
				ObjectType = dto.ObjectType,
				ObjectName = dto.ObjectName,
				DeletedTime = dto.DeletedTime,
				ObjectRegisterId = dto.ObjectRegisterId,
				IsDuplicateProcessExists = isDuplicateProcessExists
			};
		}

		public RestoreObjectFromRecycleBinLongProcessParams ToSettings()
		{
			return new RestoreObjectFromRecycleBinLongProcessParams {EventId = Id, ObjectName = ObjectName };
		}
	}
}
