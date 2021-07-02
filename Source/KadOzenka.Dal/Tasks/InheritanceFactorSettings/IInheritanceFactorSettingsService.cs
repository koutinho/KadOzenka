using System.Collections.Generic;
using KadOzenka.Dal.Tasks.InheritanceFactorSettings.Dto;

namespace KadOzenka.Dal.Tasks.InheritanceFactorSettings
{
	public interface IInheritanceFactorSettingsService
	{
		List<InheritanceFactorSettingDto> Get(List<long> tourAttributes);
		int Add(InheritanceFactorSettingDto inheritanceFactor);
	}
}