using System.Collections.Generic;
using KadOzenka.Dal.Groups.Dto;

namespace KadOzenka.Dal.Groups
{
	public interface IGroupFactorService
	{
		List<GroupFactorDto> GetGroupFactors(long? groupId);
		GroupFactorDto GetGroupFactor(long id);
		long CreateGroupFactor(GroupFactorDto dto);
		void UpdateGroupFactor(GroupFactorDto dto);
		void DeleteGroupFactor(long id);
	}
}