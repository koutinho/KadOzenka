using System;
using Core.Register.QuerySubsystem;
using ObjectModel.KO;

namespace KadOzenka.Dal.Modeling
{
	public class ModelingRepository
	{
		public OMModel GetActiveModelEntityByGroupId(long? groupId)
		{
			if (groupId == null)
				throw new Exception("Не передан идентификатор Группы для поиска модели");

			return OMModel.Where(x => x.GroupId == groupId && x.IsActive.Coalesce(false) == true).SelectAll()
				.ExecuteFirstOrDefault();
		}
	}
}
