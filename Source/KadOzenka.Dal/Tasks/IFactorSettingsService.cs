using System.Collections.Generic;
using KadOzenka.Dal.Tasks.Dto;

namespace KadOzenka.Dal.Tasks
{
	public interface IFactorSettingsService
	{
		List<FactorSettingsDto> Get(List<long> tourAttributes);
		int Add(FactorSettingsDto factor);
	}
}