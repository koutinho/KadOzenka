using System;
using System.Collections.Generic;
using System.Linq;
using Core.Register;
using Core.Shared.Extensions;
using KadOzenka.Dal.Modeling.Dto;
using KadOzenka.Dal.Modeling.Dto.Factors;
using ObjectModel.Directory;
using ObjectModel.Directory.ES;
using ObjectModel.KO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Transactions;
using Core.Register;
using Core.Register.QuerySubsystem;
using Core.Shared.Extensions;
using KadOzenka.Dal.Modeling.Dto;
using ObjectModel.Core.Register;
using ObjectModel.Directory;
using ObjectModel.KO;
using ObjectModel.Market;
using ObjectModel.Modeling;
using GemBox.Spreadsheet;
using KadOzenka.Dal.Modeling.Dto.Factors;
using Kendo.Mvc.Extensions;
using ObjectModel.Directory.ES;
using ObjectModel.Ko;

namespace KadOzenka.Dal.Modeling
{
	public class ModelFactorsService
	{
		#region Факторы

		public OMModelFactor GetFactorById(long? id)
		{
			var factor = OMModelFactor.Where(x => x.Id == id).SelectAll().ExecuteFirstOrDefault();
			if (factor == null)
				throw new Exception($"Не найден фактор модели с ИД '{id}'");

			return factor;
		}

		public List<OMModelFactor> GetFactors(long? modelId, KoAlgoritmType type)
		{
			var types = new List<KoAlgoritmType>();
			if (type == KoAlgoritmType.None)
			{
				types.Add(KoAlgoritmType.Line);
				types.Add(KoAlgoritmType.Exp);
				types.Add(KoAlgoritmType.Multi);
			}
			else
			{
				types.Add(type);
			}

			return OMModelFactor.Where(x => x.ModelId == modelId && types.Contains(x.AlgorithmType_Code)).SelectAll().Execute();
		}

		public List<ModelAttributeRelationDto> GetGeneralModelAttributes(long modelId)
		{
			var query = GetModelFactorsQuery(modelId);

			query.AddColumn(OMAttribute.GetColumn(x => x.RegisterId, nameof(ModelAttributeRelationDto.RegisterId)));
			query.AddColumn(OMAttribute.GetColumn(x => x.Id, nameof(ModelAttributeRelationDto.AttributeId)));
			query.AddColumn(OMAttribute.GetColumn(x => x.Name, nameof(ModelAttributeRelationDto.AttributeName)));
			query.AddColumn(OMAttribute.GetColumn(x => x.Type, nameof(ModelAttributeRelationDto.AttributeType)));
			query.AddColumn(OMModelFactor.GetColumn(x => x.DictionaryId, nameof(ModelAttributeRelationDto.DictionaryId)));
			query.AddColumn(OMModelFactor.GetColumn(x => x.AlgorithmType_Code, nameof(ModelAttributeRelationDto.Type)));

			var attributes = new List<ModelAttributeRelationDto>();
			var table = query.ExecuteQuery();
			for (var i = 0; i < table.Rows.Count; i++)
			{
				var row = table.Rows[i];

				var id = row[nameof(ModelAttributeRelationDto.Id)].ParseToLong();

				var registerId = row[nameof(ModelAttributeRelationDto.RegisterId)].ParseToLong();

				var attributeId = row[nameof(ModelAttributeRelationDto.AttributeId)].ParseToLong();
				var attributeName = row[nameof(ModelAttributeRelationDto.AttributeName)].ParseToString();
				var attributeType = row[nameof(ModelAttributeRelationDto.AttributeType)].ParseToInt();

				var dictionaryId = row[nameof(ModelAttributeRelationDto.DictionaryId)].ParseToLongNullable();

				attributes.Add(new ModelAttributeRelationDto
				{
					Id = id,
					RegisterId = registerId,
					AttributeId = attributeId,
					AttributeName = attributeName,
					AttributeType = attributeType,
					DictionaryId = dictionaryId
				});
			}

			var a = attributes.GroupBy(x => x.Type).ToList();

			return attributes.GroupBy(x => x.Type).Select(x => x.FirstOrDefault()).ToList();
		}

		public List<ModelAttributeRelationDto> GetModelAttributes(long modelId, KoAlgoritmType type)
		{
			var dictionaryJoin = new QSJoin
			{
				RegisterId = OMModelingDictionary.GetRegisterId(),
				JoinCondition = new QSConditionSimple
				{
					ConditionType = QSConditionType.Equal,
					LeftOperand = OMModelFactor.GetColumn(x => x.DictionaryId),
					RightOperand = OMModelingDictionary.GetColumn(x => x.Id)
				},
				JoinType = QSJoinType.Left
			};

			var typeCondition = new QSConditionSimple(OMModelFactor.GetColumn(x => x.AlgorithmType_Code), QSConditionType.Equal, (int)type);

			var query = GetModelFactorsQuery(modelId, dictionaryJoin, typeCondition);

			query.AddColumn(OMAttribute.GetColumn(x => x.RegisterId, nameof(ModelAttributeRelationDto.RegisterId)));
			query.AddColumn(OMAttribute.GetColumn(x => x.Id, nameof(ModelAttributeRelationDto.AttributeId)));
			query.AddColumn(OMAttribute.GetColumn(x => x.Name, nameof(ModelAttributeRelationDto.AttributeName)));
			query.AddColumn(OMAttribute.GetColumn(x => x.Type, nameof(ModelAttributeRelationDto.AttributeType)));
			query.AddColumn(OMModelingDictionary.GetColumn(x => x.Id, nameof(ModelAttributeRelationDto.DictionaryId)));
			query.AddColumn(OMModelingDictionary.GetColumn(x => x.Name, nameof(ModelAttributeRelationDto.DictionaryName)));
			query.AddColumn(OMModelFactor.GetColumn(x => x.B0, nameof(ModelAttributeRelationDto.B0)));
			query.AddColumn(OMModelFactor.GetColumn(x => x.SignAdd, nameof(ModelAttributeRelationDto.SignAdd)));
			query.AddColumn(OMModelFactor.GetColumn(x => x.SignDiv, nameof(ModelAttributeRelationDto.SignDiv)));
			query.AddColumn(OMModelFactor.GetColumn(x => x.SignMarket, nameof(ModelAttributeRelationDto.SignMarket)));
			query.AddColumn(OMModelFactor.GetColumn(x => x.Weight, nameof(ModelAttributeRelationDto.Coefficient)));

			var sql = query.GetSql();

			var attributes = new List<ModelAttributeRelationDto>();
			var table = query.ExecuteQuery();
			for (var i = 0; i < table.Rows.Count; i++)
			{
				var row = table.Rows[i];

				var id = row[nameof(ModelAttributeRelationDto.Id)].ParseToLong();

				var registerId = row[nameof(ModelAttributeRelationDto.RegisterId)].ParseToLong();

				var attributeId = row[nameof(ModelAttributeRelationDto.AttributeId)].ParseToLong();
				var attributeName = row[nameof(ModelAttributeRelationDto.AttributeName)].ParseToString();
				var attributeType = row[nameof(ModelAttributeRelationDto.AttributeType)].ParseToInt();

				var dictionaryId = row[nameof(ModelAttributeRelationDto.DictionaryId)].ParseToLongNullable();
				var dictionaryName = row[nameof(ModelAttributeRelationDto.DictionaryName)].ParseToString();

				var b0 = row[nameof(ModelAttributeRelationDto.B0)].ParseToDecimalNullable();
				var signAdd = row[nameof(ModelAttributeRelationDto.SignAdd)].ParseToBooleanNullable();
				var signDiv = row[nameof(ModelAttributeRelationDto.SignDiv)].ParseToBooleanNullable();
				var signMarket = row[nameof(ModelAttributeRelationDto.SignMarket)].ParseToBooleanNullable();
				var weight = row[nameof(ModelAttributeRelationDto.Coefficient)].ParseToDecimalNullable();

				attributes.Add(new ModelAttributeRelationDto
				{
					Id = id,
					RegisterId = registerId,
					AttributeId = attributeId,
					AttributeName = attributeName,
					AttributeType = attributeType,
					DictionaryId = dictionaryId,
					DictionaryName = dictionaryName,
					B0 = b0.GetValueOrDefault(),
					SignAdd = signAdd.GetValueOrDefault(),
					SignDiv = signDiv.GetValueOrDefault(),
					SignMarket = signMarket.GetValueOrDefault(),
					Coefficient = weight
				});
			}

			return attributes;
		}

		public QSQuery GetModelFactorsQuery(long modelId, QSJoin additionalJoin = null, QSCondition additionalCondition = null)
		{
			var conditions = new List<QSCondition>
			{
				new QSConditionSimple(OMModelFactor.GetColumn(x => x.ModelId), QSConditionType.Equal, modelId)
			};
			if (additionalCondition != null)
				conditions.Add(additionalCondition);

			var query = new QSQuery
			{
				MainRegisterID = OMModelFactor.GetRegisterId(),
				Condition = new QSConditionGroup
				{
					Type = QSConditionGroupType.And,
					Conditions = conditions
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
				},
				OrderBy = new List<QSOrder>
				{
					new QSOrder
					{
						Column = OMAttribute.GetColumn(x => x.Name),
						Order = QSOrderType.ASC
					}
				}
			};

			if (additionalJoin != null)
				query.Joins.Add(additionalJoin);

			return query;
		}

		public int AddFactor(ModelFactorDto dto)
		{
			ValidateFactor(dto);

			var id = new OMModelFactor
			{
				ModelId = dto.GeneralModelId,
				FactorId = dto.FactorId,
				MarkerId = -1,
				Weight = dto.Weight,
				B0 = dto.Weight,
				SignDiv = dto.SignDiv,
				SignAdd = dto.SignAdd,
				SignMarket = dto.SignMarket
			}.Save();

			RecalculateFormula(dto.GeneralModelId);

			return id;
		}

		public void UpdateFactor(ModelFactorDto dto)
		{
			var factor = GetFactorById(dto.Id);

			ValidateFactor(dto);

			factor.Weight = dto.Weight;
			factor.B0 = dto.B0;
			factor.SignDiv = dto.SignDiv;
			factor.SignAdd = dto.SignAdd;
			factor.SignMarket = dto.SignMarket;

			factor.Save();

			RecalculateFormula(dto.GeneralModelId);
		}

		#region Support Methods

		private void ValidateFactor(ModelFactorDto factorDto)
		{
			if (factorDto.GeneralModelId == null)
				throw new Exception("Не передан ИД основной модели");

			if (factorDto.FactorId == null)
				throw new Exception("Не передан ИД фактора");

			if (factorDto.Type == KoAlgoritmType.None)
				throw new Exception("Не передан тип алгоритма для фактора");

			var isTheSameFactorExist = OMModelFactor.Where(x =>
				x.ModelId == factorDto.GeneralModelId && x.FactorId == factorDto.FactorId &&
				x.AlgorithmType_Code == factorDto.Type).ExecuteExists();
			if(isTheSameFactorExist)
				throw new Exception($"Фактор {factorDto.FactorId} уже был добавлен для модели {factorDto.GeneralModelId} типа '{factorDto.Type.GetEnumDescription()}'");
		}

		private void RecalculateFormula(long? generalModelId)
		{
			var model = OMModel.Where(x => x.Id == generalModelId).SelectAll().ExecuteFirstOrDefault();
			if(model == null)
				throw new Exception($"Не найдена модель с ИД '{generalModelId}'");

			model.Formula = model.GetFormulaFull(true);
			model.Save();
		}

		#endregion

		#endregion

		#region Метки

		public List<OMMarkCatalog> GetMarks(long generalModelId, long factorId)
		{
			return OMMarkCatalog.Where(x => x.FactorId == factorId && x.GeneralModelId == generalModelId).SelectAll().Execute();
		}

		public OMMarkCatalog GetMarkById(long id)
		{
			var mark = OMMarkCatalog.Where(x => x.Id == id).SelectAll().ExecuteFirstOrDefault();
			if (mark == null)
				throw new Exception($"Не найдена метка с ИД {id}");

			return mark;
		}

		public int CreateMark(string value, decimal? metka, long? factorId, long? generalModelId)
		{
			if (generalModelId == null)
				throw new Exception("Не переден ИД основной модели");

			var id = new OMMarkCatalog
			{
				GroupId = -1,
				FactorId = factorId,
				MetkaFactor = metka,
				ValueFactor = value,
				GeneralModelId = generalModelId,
			}.Save();

			return id;
		}

		public void UpdateMark(long id, string value, decimal? metka)
		{
			var mark = GetMarkById(id);

			mark.ValueFactor = value;
			mark.MetkaFactor = metka;

			mark.Save();
		}

		public void DeleteMark(long id)
		{
			var mark = GetMarkById(id);

			mark.Destroy();
		}

		#endregion
	}
}
