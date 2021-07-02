using KadOzenka.Dal.CommonFunctions;
using ObjectModel.KO;

namespace KadOzenka.Dal.Tasks.Repositories
{
	public interface IFactorSettingsRepository : IGenericRepository<OMFactorSettings>
	{
		bool IsFactorExists(long factorId);
	}
}
