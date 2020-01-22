using System;
using KadOzenka.Dal.Model.Dto;
using ObjectModel.KO;

namespace KadOzenka.Dal.Model
{
    public class ModelService
    {
        public ModelDto GetModelByGroupId(long? groupId)
        {
            if (groupId == null)
                throw new Exception("Не передан идентификатор Группы для поиска модели");

            var model = OMModel.Where(x => x.GroupId == groupId).SelectAll().ExecuteFirstOrDefault();
            if (model == null)
                throw new Exception($"Не найдена модель для Группы с id='{groupId}'");

            return new ModelDto
            {
                Id = model.Id,
                GroupId = model.GroupId,
                Name = model.Name,
                Description = model.Description,
                Formula = model.Formula,
                AlgorithmType = model.AlgoritmType,
                AlgorithmTypeCode = model.AlgoritmType_Code,
                A0 = model.A0
            };
        }
    }
}
