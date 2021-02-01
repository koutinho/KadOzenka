using Core.Register.QuerySubsystem;
using ObjectModel.KO;

namespace KadOzenka.Dal.Modeling.Repositories
{
	public class ModelingRepository : IModelingRepository
	{
		public OMModel GetActiveModelEntityByGroupId(long? groupId)
		{
			return OMModel.Where(x => x.GroupId == groupId && x.IsActive.Coalesce(false) == true).SelectAll()
				.ExecuteFirstOrDefault();
		}

		public OMModel GetModelById(long modelId)
		{
			return OMModel.Where(x => x.Id == modelId).SelectAll().ExecuteFirstOrDefault();
		}
	}
}
