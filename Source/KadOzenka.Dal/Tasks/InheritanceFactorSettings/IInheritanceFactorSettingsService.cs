using System.Collections.Generic;
using KadOzenka.Dal.Tasks.InheritanceFactorSettings.Dto;
using ObjectModel.KO;

namespace KadOzenka.Dal.Tasks.InheritanceFactorSettings
{
	public interface IInheritanceFactorSettingsService
	{
		List<InheritanceFactorSettingDto> Get(List<long> tourAttributes);
		OMFactorSettings GetById(long? settingId);
		int Add(InheritanceFactorSettingDto settingDto);
		void Update(InheritanceFactorSettingDto settingDto);
		void Delete(long? settingId);
	}
}