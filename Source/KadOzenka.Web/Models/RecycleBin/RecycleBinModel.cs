using System;
using KadOzenka.Dal.CommonFunctions.Dto;

namespace KadOzenka.Web.Models.RecycleBin
{
	public class RecycleBinModel
	{
		public long Id { get; set; }
		public string ObjectType { get; set; }
		public string ObjectName { get; set; }
		public DateTime DeletedTime { get; set; }

		public static RecycleBinModel FromDto(RecycleBinDto dto)
		{
			return new RecycleBinModel
			{
				Id = dto.Id,
				ObjectType = dto.ObjectType,
				ObjectName = dto.ObjectName,
				DeletedTime = dto.DeletedTime
			};
		}
	}
}
