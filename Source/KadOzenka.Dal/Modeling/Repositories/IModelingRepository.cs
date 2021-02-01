using ObjectModel.KO;

namespace KadOzenka.Dal.Modeling.Repositories
{
	public interface IModelingRepository
	{
		OMModel GetModelById(long modelId);

		OMModel GetActiveModelEntityByGroupId(long? groupId);
	}
}
