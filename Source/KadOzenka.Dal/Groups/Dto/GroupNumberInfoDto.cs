using KadOzenka.Dal.Groups.Dto.Consts;
using ObjectModel.KO;

namespace KadOzenka.Dal.Groups.Dto
{
	public class GroupNumberInfoDto
	{
		public long? Id { get; set; }
		public string CombinedName { get; set; }
		public int? Number { get; set; }
		public int? ParentNumber { get; set; }

		public static GroupNumberInfoDto FromOMGroup(OMGroup omGroup, GroupService service)
		{
			var dto = new GroupNumberInfoDto
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

		public static GroupNumberInfoDto FromGroupInfoDto(GroupAlgorithmInfoDto omGroup, GroupService service)
		{
			var dto = new GroupNumberInfoDto
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
