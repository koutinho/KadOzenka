using KadOzenka.Dal.Groups.Dto.Consts;
using ObjectModel.KO;

namespace KadOzenka.Dal.Groups.Dto
{
	public class GroupNumberDto
	{
		public long? Id { get; set; }
		public string CombinedName { get; set; }
		public int? Number { get; set; }
		public int? ParentNumber { get; set; }

		public static GroupNumberDto FromOMGroup(OMGroup omGroup, GroupService service)
		{
			var dto = new GroupNumberDto
			{
				Id = omGroup.Id,
				CombinedName = $"{omGroup.Number}. {omGroup.GroupName}",
				Number = service.ParseGroupNumber(omGroup.ParentId, omGroup.Number),
			};
			if (service.GetGroupType(omGroup.ParentId) == GroupType.SubGroup)
			{
				dto.ParentNumber = service.GetParentGroupNumber(omGroup.Number);
			}

			return dto;
		}

		public static GroupNumberDto FromGroupInfoDto(GroupInfoDto omGroup, GroupService service)
		{
			var dto = new GroupNumberDto
			{
				Id = omGroup.Id,
				CombinedName = $"{omGroup.Number}. {omGroup.Name}",
				Number = service.ParseGroupNumber(omGroup.ParentId, omGroup.Number),
			};
			if (service.GetGroupType(omGroup.ParentId) == GroupType.SubGroup)
			{
				dto.ParentNumber = service.GetParentGroupNumber(omGroup.Number);
			}

			return dto;
		}
	}
}
