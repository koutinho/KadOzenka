using System;
using System.Collections.Generic;
using Core.Register.QuerySubsystem;
using KadOzenka.Dal.Model.Dto;
using ObjectModel.Core.Register;
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

        public List<ModelFactorDto> GetModelFactors(long modelId)
        {
            var query = new QSQuery
            {
                MainRegisterID = OMModelFactor.GetRegisterId(),
                Condition = new QSConditionGroup
                {
                    Type = QSConditionGroupType.And,
                    Conditions = new List<QSCondition>
                    {
                        new QSConditionSimple(OMModelFactor.GetColumn(x => x.ModelId), QSConditionType.Equal, modelId)
                    }
                },
                Joins = new List<QSJoin>
                {
                    new QSJoin
                    {
                        RegisterId = OMAttribute.GetRegisterId(),
                        JoinCondition = new QSConditionSimple
                        {
                            ConditionType = QSConditionType.Equal,
                            LeftOperand = OMModelFactor.GetColumn(x => x.FactorId),
                            RightOperand = OMAttribute.GetColumn(x => x.Id)
                        },
                        JoinType = QSJoinType.Inner
                    }
                }
            };

            query.AddColumn(OMModelFactor.GetColumn(x => x.ModelId, nameof(ModelFactorDto.ModelId)));
            query.AddColumn(OMModelFactor.GetColumn(x => x.FactorId, nameof(ModelFactorDto.FactorId)));
            query.AddColumn(OMAttribute.GetColumn(x => x.Name, nameof(ModelFactorDto.Factor)));
            query.AddColumn(OMAttribute.GetColumn(x => x.Type, nameof(ModelFactorDto.Type)));
            query.AddColumn(OMAttribute.GetColumn(x => x.RegisterId, nameof(ModelFactorDto.RegisterId)));
            query.AddColumn(OMModelFactor.GetColumn(x => x.MarkerId, nameof(ModelFactorDto.MarkerId)));
            query.AddColumn(OMModelFactor.GetColumn(x => x.Weight, nameof(ModelFactorDto.Weight)));
            query.AddColumn(OMModelFactor.GetColumn(x => x.B0, nameof(ModelFactorDto.B0)));
            query.AddColumn(OMModelFactor.GetColumn(x => x.SignDiv, nameof(ModelFactorDto.SignDiv)));
            query.AddColumn(OMModelFactor.GetColumn(x => x.SignAdd, nameof(ModelFactorDto.SignAdd)));
            query.AddColumn(OMModelFactor.GetColumn(x => x.SignMarket, nameof(ModelFactorDto.SignMarket)));

            return query.ExecuteQuery<ModelFactorDto>();
        }
    }
}
