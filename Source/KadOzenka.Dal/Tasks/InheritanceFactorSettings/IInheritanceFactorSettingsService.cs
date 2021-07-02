using System.Collections.Generic;
using KadOzenka.Dal.Tasks.InheritanceFactorSettings.Dto;
using ObjectModel.KO;

namespace KadOzenka.Dal.Tasks.InheritanceFactorSettings
{
	public interface IInheritanceFactorSettingsService
	{
		List<InheritanceFactorSettingDto> Get(List<long> tourAttributes);
		int Add(InheritanceFactorSettingDto factor);
		void UpdateFactor(InheritanceFactorSettingDto settingDto);
		OMFactorSettings GetById(long? id);
		void DeleteSetting(long? settingId);
	}
}