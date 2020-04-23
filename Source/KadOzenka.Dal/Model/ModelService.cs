using System;
using ObjectModel.KO;

namespace KadOzenka.Dal.Model
{
    public class ModelService
    {
        public OMModel GetModelByGroupId(long? groupId)
        {
			if (groupId == null)
				throw new Exception("Не передан идентификатор Группы для поиска модели");

			var model = OMModel.Where(x => x.GroupId == groupId).SelectAll().ExecuteFirstOrDefault();
			if (model == null)
				throw new Exception($"Не найдена модель для Группы с id='{groupId}'");

			return model;
        }

        public OMModel GetModelById(long? modelId)
        {
	        if (modelId == null)
		        throw new Exception("Не передан идентификатор Модели для поиска");

	        var model = OMModel.Where(x => x.Id == modelId).SelectAll().ExecuteFirstOrDefault();
	        if (model == null)
		        throw new Exception($"Не найдена Модель с id='{modelId}'");

	        return model;
		}
    }
}
