using System;
using System.Collections.Generic;
using System.Text;
using Core.Register.QuerySubsystem;
using Core.Shared.Extensions;
using KadOzenka.Dal.Modeling.Dto;
using ObjectModel.Core.Register;
using ObjectModel.Directory;
using ObjectModel.ES;
using ObjectModel.KO;
using ObjectModel.Modeling;

namespace KadOzenka.Dal.Modeling
{
	public class ModelingService
	{
		public ModelingModelDto GetModelById(long modelId)
		{
			var model = OMModelingModel.Where(x => x.Id == modelId).SelectAll().ExecuteFirstOrDefault();
			if (model == null)
				throw new Exception($"Не найдена модель с Id='{modelId}'");

			return ToDto(model);
		}


		public List<Attributes> GetModelAttributes(long modelId)
		{
			var query = new QSQuery
			{
				MainRegisterID = OMModelAttributesRelation.GetRegisterId(),
				Condition = new QSConditionSimple(OMModelAttributesRelation.GetColumn(x => x.ModelId), QSConditionType.Equal, modelId),
				Joins = new List<QSJoin>
				{
					new QSJoin
					{
						RegisterId = OMAttribute.GetRegisterId(),
						JoinCondition = new QSConditionSimple
						{
							ConditionType = QSConditionType.Equal,
							LeftOperand = OMModelAttributesRelation.GetColumn(x => x.AttributeId),
							RightOperand = OMAttribute.GetColumn(x => x.Id)
						},
						JoinType = QSJoinType.Inner
					},
					new QSJoin
					{
						RegisterId = OMEsReference.GetRegisterId(),
						JoinCondition = new QSConditionSimple
						{
							ConditionType = QSConditionType.Equal,
							LeftOperand = OMModelAttributesRelation.GetColumn(x => x.DictionaryId),
							RightOperand = OMEsReference.GetColumn(x => x.Id)
						},
						JoinType = QSJoinType.Left
					}
				}
			};
			query.AddColumn(OMAttribute.GetColumn(x => x.Id, nameof(Attributes.AttributeId)));
			query.AddColumn(OMAttribute.GetColumn(x => x.Name, nameof(Attributes.AttributeName)));
			query.AddColumn(OMEsReference.GetColumn(x => x.Id, nameof(Attributes.DictionaryId)));
			query.AddColumn(OMEsReference.GetColumn(x => x.Name, nameof(Attributes.DictionaryName)));

			var attributes = new List<Attributes>();
			var table = query.ExecuteQuery();
			for (var i = 0; i < table.Rows.Count; i++)
			{
				var row = table.Rows[i];
				
				var attributeId = row[nameof(Attributes.AttributeId)].ParseToLongNullable();
				var attributeName = row[nameof(Attributes.AttributeName)].ParseToString();

				var dictionaryId = row[nameof(Attributes.DictionaryId)].ParseToLongNullable();
				var dictionaryName = row[nameof(Attributes.DictionaryName)].ParseToString();

				if (attributeId != null || dictionaryId != null)
					attributes.Add(new Attributes
					{
						AttributeId = attributeId,
						AttributeName = attributeName,
						DictionaryId = dictionaryId,
						DictionaryName = dictionaryName
					});
			}

			return attributes;
		}

		public int AddModel(ModelingModelDto modelDto)
		{
			ValidateModel(modelDto);

			return new OMModelingModel
			{
				Name = modelDto.Name,
				TourId = modelDto.TourId,
				MarketSegment_Code = modelDto.MarketSegment
			}.Save();
		}

		public void UpdateModel(ModelingModelDto modelDto)
		{
			ValidateModel(modelDto);
			var existedModel = OMModelingModel.Where(x => x.Id == modelDto.ModelId).SelectAll().ExecuteFirstOrDefault();
			if (existedModel == null)
				throw new Exception($"Не найдена модель с Id='{modelDto.ModelId}'");

			//todo add attributes
			existedModel.Name = modelDto.Name;
			existedModel.TourId = modelDto.TourId;
			existedModel.MarketSegment_Code = modelDto.MarketSegment;
			existedModel.Save();
		}


		#region Support Methods

		public ModelingModelDto ToDto(OMModelingModel entity)
		{
			return new ModelingModelDto
			{
				ModelId = entity.Id,
				Name = entity.Name,
				TourId = entity.TourId,
				MarketSegment = entity.MarketSegment_Code
			};
		}

		private void ValidateModel(ModelingModelDto modelDto)
		{
			var message = new StringBuilder();

			if (string.IsNullOrWhiteSpace(modelDto.Name))
				message.AppendLine("У модели не заполнено имя");

			var isTourExists = OMTour.Where(x => x.Id == modelDto.TourId).ExecuteExists();
			if(!isTourExists)
				message.AppendLine($"Не найден Тур с Id='{modelDto.TourId}'");

			if(modelDto.MarketSegment == MarketSegment.None)
				message.AppendLine($"Для модели не выбран сегмент");

			if (message.Length != 0)
				throw new Exception(message.ToString());
		}

		#endregion
	}
}
