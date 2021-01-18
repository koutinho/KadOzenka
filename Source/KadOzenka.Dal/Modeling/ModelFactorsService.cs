using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using Core.Register;
using Core.Shared.Extensions;
using KadOzenka.Dal.Modeling.Dto;
using KadOzenka.Dal.Modeling.Dto.Factors;
using ObjectModel.Directory;
using ObjectModel.KO;
using Core.Register.QuerySubsystem;
using ObjectModel.Core.Register;
using ObjectModel.Directory.ES;

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
			var types = GetPossibleTypes(type);

			return OMModelFactor.Where(x => x.ModelId == modelId && types.Contains(x.AlgorithmType_Code)).SelectAll().Execute();
		}

		public List<KoAlgoritmType> GetPossibleTypes(KoAlgoritmType type)
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

			return types;
		}

		public List<ModelAttributeRelationDto> GetGeneralModelAttributes(long modelId)
		{
			var query = GetModelFactorsQuery(modelId);

			query.AddColumn(OMAttribute.GetColumn(x => x.RegisterId, nameof(ModelAttributeRelationDto.RegisterId)));
			query.AddColumn(OMAttribute.GetColumn(x => x.Id, nameof(ModelAttributeRelationDto.AttributeId)));
			query.AddColumn(OMAttribute.GetColumn(x => x.Name, nameof(ModelAttributeRelationDto.AttributeName)));
			query.AddColumn(OMAttribute.GetColumn(x => x.Type, nameof(ModelAttributeRelationDto.AttributeType)));
			query.AddColumn(OMModelFactor.GetColumn(x => x.DictionaryId, nameof(ModelAttributeRelationDto.DictionaryId)));

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

			//если модель автоматическая, её факторы дублируются для лин/экс/мульт типов, поэтому группируем по AttributeId

			return attributes.GroupBy(x => x.AttributeId).Select(x => x.FirstOrDefault()).ToList();
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

			//для совместимости с уже ранее созданными моделями (не через блок "Справочники моделей")
			QSConditionSimple typeCondition = null;
			var isFactorsWithSpecificTypeExist = OMModelFactor.Where(x => x.ModelId == modelId && x.AlgorithmType_Code == type).ExecuteExists();
			if (isFactorsWithSpecificTypeExist)
			{
				typeCondition = new QSConditionSimple(OMModelFactor.GetColumn(x => x.AlgorithmType_Code), QSConditionType.Equal, (int)type);
			}

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
			query.AddColumn(OMModelFactor.GetColumn(x => x.PreviousWeight, nameof(ModelAttributeRelationDto.PreviousWeight)));

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
				var previousWeight = row[nameof(ModelAttributeRelationDto.PreviousWeight)].ParseToDecimalNullable();

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
					Coefficient = weight,
					PreviousWeight = previousWeight
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

		public void AddAutomaticFactor(AutomaticModelFactorDto dto)
		{
			ValidateAutomaticFactor(dto);

			var types = GetPossibleTypes(KoAlgoritmType.None);

			using (var ts = new TransactionScope())
			{
				types.ForEach(type =>
				{
					new OMModelFactor
					{
						ModelId = dto.ModelId,
						FactorId = dto.FactorId,
						DictionaryId = dto.DictionaryId,
						MarkerId = -1,
						AlgorithmType_Code = type,
						PreviousWeight = dto.PreviousWeight ?? 1
					}.Save();
				});

				ts.Complete();
			}
		}

		public void UpdateAutomaticFactor(AutomaticModelFactorDto dto)
		{
			ValidateAutomaticFactor(dto);

			var factor = GetFactorById(dto.Id);

			using (var ts = new TransactionScope())
			{
				if (factor.DictionaryId != dto.DictionaryId)
				{
					var factors = OMModelFactor.Where(x => x.ModelId == dto.ModelId && x.FactorId == dto.FactorId)
						.Select(x => x.DictionaryId).Execute();
					factors.ForEach(x =>
					{
						x.DictionaryId = dto.DictionaryId;
						x.Save();
					});
				}

				factor.PreviousWeight = dto.PreviousWeight ?? 1;
				factor.Save();

				ts.Complete();
			}
		}

		public int AddManualFactor(ManualModelFactorDto dto)
		{
			ValidateManualFactor(dto);

			var id = new OMModelFactor
			{
				ModelId = dto.GeneralModelId,
				FactorId = dto.FactorId,
				MarkerId = -1,
				Weight = dto.Weight,
				B0 = dto.Weight,
				SignDiv = dto.SignDiv,
				SignAdd = dto.SignAdd,
				SignMarket = dto.SignMarket,
				AlgorithmType_Code = dto.Type
			}.Save();

			RecalculateFormula(dto.GeneralModelId);

			return id;
		}

		public void UpdateManualFactor(ManualModelFactorDto dto)
		{
			var factor = GetFactorById(dto.Id);

			factor.Weight = dto.Weight;
			factor.B0 = dto.B0;
			factor.SignDiv = dto.SignDiv;
			factor.SignAdd = dto.SignAdd;
			factor.SignMarket = dto.SignMarket;

			factor.Save();

			RecalculateFormula(dto.GeneralModelId);
		}

		public void DeleteManualModelFactor(long? id)
		{
			var factor = GetFactorById(id);

			factor.Destroy();

			RecalculateFormula(factor.ModelId);
		}

		public void DeleteAutomaticModelFactor(OMModelFactor factor)
		{
			if (factor == null)
				throw new Exception("Не передан фактор для удаления");

			var allFactors = OMModelFactor.Where(x => x.ModelId == factor.ModelId && x.FactorId == factor.FactorId)
				.Execute();

			using (var ts = new TransactionScope())
			{
				allFactors.ForEach(x => x.Destroy());

				ts.Complete();
			}
		}


		#region Support Methods

		private void ValidateManualFactor(ManualModelFactorDto factorDto)
		{
			ValidateBaseFactor(factorDto.Id, factorDto.GeneralModelId, factorDto.FactorId, factorDto.Type);

			if (factorDto.Type == KoAlgoritmType.None)
				throw new Exception("Не передан тип алгоритма для фактора");
		}

		private void ValidateAutomaticFactor(AutomaticModelFactorDto factor)
		{
			//тип алгоритма не важен, т.к. для автоматической модели существует три фактора для каждого типа алгоритма
			ValidateBaseFactor(factor.Id, factor.ModelId, factor.FactorId, KoAlgoritmType.Line);

			var model = OMModel.Where(x => x.Id == factor.ModelId).Select(x => x.GroupId).ExecuteFirstOrDefault();
			if (model == null)
				throw new Exception($"Не найдена модель с ИД '{factor.ModelId}'");
			var tourToGroupRelation = OMTourGroup.Where(x => x.GroupId == model.GroupId).Select(x => new
			{
				x.ParentTour.Year
			}).ExecuteFirstOrDefault();
			//в 2016 почти все атрибуты забиты как строки, поэтому его не валидируем
			if (tourToGroupRelation?.ParentTour?.Year == 2016)
				return;

			var errors = new List<string>();

			var attribute = RegisterCache.RegisterAttributes.Values.FirstOrDefault(x => x.Id == factor.FactorId);

			if ((attribute?.Type == RegisterAttributeType.STRING || attribute?.Type == RegisterAttributeType.DATE) &&
				factor.DictionaryId == null)
			{
				errors.Add($"Для атрибута '{attribute.Name}' нужно выбрать словарь");
			}
			if (factor.DictionaryId != null)
			{
				var dictionary = OMModelingDictionary.Where(x => x.Id == factor.DictionaryId).Select(x => x.Type_Code)
					.ExecuteFirstOrDefault();
				if (dictionary == null)
					throw new Exception($"Не найден словарь для моделирования с ИД '{factor.DictionaryId}'");

				switch (attribute?.Type)
				{
					case RegisterAttributeType.STRING:
						{
							if (dictionary.Type_Code != ReferenceItemCodeType.String)
								errors.Add(GenerateMessage(attribute.Name, ReferenceItemCodeType.String));
							break;
						}
					case RegisterAttributeType.DATE:
						{
							if (dictionary.Type_Code != ReferenceItemCodeType.Date)
								errors.Add(GenerateMessage(attribute.Name, ReferenceItemCodeType.Date));
							break;
						}
				}
			}

			if (errors.Count > 0)
				throw new Exception(string.Join("<br>", errors));
		}

		private void ValidateBaseFactor(long id, long? modelId, long? factorId, KoAlgoritmType type)
		{
			if (modelId == null)
				throw new Exception("Не передан ИД основной модели");

			if (factorId == null)
				throw new Exception("Не передан ИД фактора");

			var isTheSameAttributeExists = OMModelFactor.Where(x =>
					x.Id != id && x.FactorId == factorId && x.ModelId == modelId && x.AlgorithmType_Code == type)
				.ExecuteExists();
			if (isTheSameAttributeExists)
				throw new Exception($"Атрибут '{RegisterCache.GetAttributeData(factorId.GetValueOrDefault()).Name}' уже был добавлен");
		}

		private string GenerateMessage(string attributeName, ReferenceItemCodeType dictionaryType)
		{
			return $"Выберите словарь типа '{dictionaryType.GetEnumDescription()}' для атрибута '{attributeName}'";
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

		public List<OMMarkCatalog> GetMarks(long? groupId, long? factorId)
		{
			return OMMarkCatalog.Where(x => x.FactorId == factorId && x.GroupId == groupId).SelectAll().Execute();
		}

		public OMMarkCatalog GetMarkById(long id)
		{
			var mark = OMMarkCatalog.Where(x => x.Id == id).SelectAll().ExecuteFirstOrDefault();
			if (mark == null)
				throw new Exception($"Не найдена метка с ИД {id}");

			return mark;
		}

		public int CreateMark(string value, decimal? metka, long? factorId, long? groupId)
		{
			ValidateMark(groupId, value, metka);

			return new OMMarkCatalog
			{
				GroupId = groupId,
				FactorId = factorId,
				MetkaFactor = metka,
				ValueFactor = value
			}.Save();
		}

		public void UpdateMark(long id, string value, decimal? metka)
		{
			var mark = GetMarkById(id);

			ValidateMark(mark.GroupId, value, metka);

			mark.ValueFactor = value;
			mark.MetkaFactor = metka;

			mark.Save();
		}

		public void DeleteMark(long id)
		{
			var mark = GetMarkById(id);

			mark.Destroy();
		}

		public int DeleteMarks(long? groupId, long? factorId)
		{
			var marks = GetMarks(groupId, factorId);

			marks.ForEach(x => x.Destroy());

			return marks.Count;
		}


		#region Support Methods

		private void ValidateMark(long? groupId, string value, decimal? metka)
		{
			if (string.IsNullOrWhiteSpace(value))
				throw new Exception("Нельзя сохранить пустое значение");
			if (metka == null)
				throw new Exception("Нельзя сохранить пустую метку");
			if (metka.GetValueOrDefault() == 0)
				throw new Exception("Нельзя сохранить нулевую метку");
			if (groupId == null)
				throw new Exception("Не переден ИД группы");
		}

		#endregion

		#endregion
	}
}
