using CommonSdks;
using KadOzenka.Dal.CommonFunctions;
using ObjectModel.KO;

namespace KadOzenka.Dal.Tasks.InheritanceFactorSettings.Repositories
{
	public interface IFactorSettingsRepository : IGenericRepository<OMFactorSettings>
	{
		bool IsFactorExists(long settingId, long factorId);
	}
}
