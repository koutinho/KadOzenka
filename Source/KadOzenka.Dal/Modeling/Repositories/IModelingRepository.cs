using KadOzenka.Dal.CommonFunctions;
using ObjectModel.KO;

namespace KadOzenka.Dal.Modeling.Repositories
{
	public interface IModelingRepository : IGenericRepository<OMModel>
	{
		OMModel GetActiveModelEntityByGroupId(long? groupId);
	}
}
