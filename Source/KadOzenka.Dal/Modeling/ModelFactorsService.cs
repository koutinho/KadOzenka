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
using Core.Shared.Misc;
using KadOzenka.Dal.CommonFunctions;
using KadOzenka.Dal.LongProcess.Modeling.Entities;
using KadOzenka.Dal.Modeling.Exceptions.Factors;
using KadOzenka.Dal.Modeling.Repositories;
using Microsoft.Practices.EnterpriseLibrary.Data;
using ObjectModel.Core.Register;
using ObjectModel.Directory.Ko;
using ObjectModel.Directory.KO;

namespace KadOzenka.Dal.Modeling
{
	public class ModelFactorsService : IModelFactorsService
	{
		private IModelFactorsRepository ModelFactorsRepository { get; }
		private IModelDictionaryService ModelDictionaryService { get; }
		public IRegisterCacheWrapper RegisterCacheWrapper { get; }


		public ModelFactorsService(IModelFactorsRepository modelFactorsRepository = null,
			IModelDictionaryService modelDictionaryService = null,
			IRegisterCacheWrapper registerCacheWrapper = null)
		{
			ModelFactorsRepository = modelFactorsRepository ?? new ModelFactorsRepository();
			ModelDictionaryService = modelDictionaryService ?? new ModelDictionaryService();
			RegisterCacheWrapper = registerCacheWrapper ?? new RegisterCacheWrapper();
		}


		#region Факторы

		public OMModelFactor GetFactorById(long? id)
		{
			var factor = ModelFactorsRepository.GetById(id.GetValueOrDefault(), null);
			if (factor == null)
				throw new Exception($"Не найден фактор модели с ИД '{id}'");

			return factor;
		}

		public List<OMModelFactor> GetFactors(long? modelId, KoAlgoritmType type)
		{
			var types = GetPossibleTypes(type);

			return OMModelFactor.Where(x => x.ModelId == modelId && types.Contains(x.AlgorithmType_Code)).SelectAll().Execute();
		}

		public List<ModelAttributeRelationPure> GetGeneralModelAttributes(long modelId)
		{
			var query = GetModelFactorsQuery(modelId);

			query.AddColumn(OMAttribute.GetColumn(x => x.RegisterId, nameof(ModelAttributeRelationDto.RegisterId)));
			query.AddColumn(OMAttribute.GetColumn(x => x.Id, nameof(ModelAttributeRelationDto.AttributeId)));
			query.AddColumn(OMAttribute.GetColumn(x => x.Name, nameof(ModelAttributeRelationDto.AttributeName)));
			query.AddColumn(OMAttribute.GetColumn(x => x.Type, nameof(ModelAttributeRelationDto.AttributeType)));
			query.AddColumn(OMModelingDictionary.GetColumn(x => x.Id, nameof(ModelAttributeRelationDto.DictionaryId)));
			query.AddColumn(OMModelingDictionary.GetColumn(x => x.Name, nameof(ModelAttributeRelationDto.DictionaryName)));
			query.AddColumn(OMModelFactor.GetColumn(x => x.IsActive, nameof(ModelAttributeRelationDto.IsActive)));

			var attributes = new List<ModelAttributeRelationPure>();
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
				var dictionaryName = row[nameof(ModelAttributeRelationDto.DictionaryName)].ParseToStringNullable();
				
				var isActive = row[nameof(ModelAttributeRelationDto.IsActive)].ParseToBooleanNullable();

				attributes.Add(new ModelAttributeRelationPure
				{
					Id = id,
					RegisterId = registerId,
					AttributeId = attributeId,
					AttributeName = attributeName,
					AttributeType = attributeType,
					DictionaryId = dictionaryId,
					DictionaryName = dictionaryName,
					IsActive = isActive.GetValueOrDefault()
				});
			}

			//если модель автоматическая, её факторы дублируются для лин/экс/мульт типов, поэтому группируем по AttributeId

			return attributes.GroupBy(x => x.AttributeId).Select(x => x.FirstOrDefault()).ToList();
		}

		//TODO разделить на получение факторов для Ручной и Автоматический моделей
		public List<ModelAttributeRelationDto> GetModelAttributes(long modelId, KoAlgoritmType type)
		{
			//для совместимости с уже ранее созданными моделями (не через блок "Справочники моделей")
			QSConditionSimple typeCondition = null;
			var isFactorsWithSpecificTypeExist = OMModelFactor.Where(x => x.ModelId == modelId && x.AlgorithmType_Code == type).ExecuteExists();
			if (isFactorsWithSpecificTypeExist)
			{
				typeCondition = new QSConditionSimple(OMModelFactor.GetColumn(x => x.AlgorithmType_Code), QSConditionType.Equal, (int)type);
			}

			var query = GetModelFactorsQuery(modelId, typeCondition);

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
			query.AddColumn(OMModelFactor.GetColumn(x => x.IsActive, nameof(ModelAttributeRelationDto.IsActive)));
			query.AddColumn(OMModelFactor.GetColumn(x => x.MarkType, nameof(ModelAttributeRelationDto.MarkType)));
			query.AddColumn(OMModelFactor.GetColumn(x => x.CorrectingTerm, nameof(ModelAttributeRelationDto.CorrectingTerm)));
			query.AddColumn(OMModelFactor.GetColumn(x => x.K, nameof(ModelAttributeRelationDto.K)));

			//var sql = query.GetSql();

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
				var correctingTerm = row[nameof(ModelAttributeRelationDto.CorrectingTerm)].ParseToDecimalNullable();
				var k = row[nameof(ModelAttributeRelationDto.K)].ParseToDecimalNullable();
				var previousWeight = row[nameof(ModelAttributeRelationDto.PreviousWeight)].ParseToDecimalNullable();
				var isActive = row[nameof(ModelAttributeRelationDto.IsActive)].ParseToBooleanNullable();
				var markType = row[nameof(ModelAttributeRelationDto.MarkType)].ParseToString();

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
					CorrectingTerm = correctingTerm,
					K = k,
					PreviousWeight = previousWeight,
					IsActive = isActive.GetValueOrDefault(),
					MarkType = markType
				});
			}

			return attributes;
		}

		public QSQuery GetModelFactorsQuery(long modelId, QSCondition additionalCondition = null)
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
					},
					new QSJoin
					{
						RegisterId = OMModelingDictionary.GetRegisterId(),
						JoinCondition = new QSConditionSimple
						{
							ConditionType = QSConditionType.Equal,
							LeftOperand = OMModelFactor.GetColumn(x => x.DictionaryId),
							RightOperand = OMModelingDictionary.GetColumn(x => x.Id)
						},
						JoinType = QSJoinType.Left
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
					var newFactor = new OMModelFactor
					{
						ModelId = dto.ModelId,
						FactorId = dto.FactorId,
						DictionaryId = dto.DictionaryId,
						MarkerId = -1,
						AlgorithmType_Code = type,
						PreviousWeight = dto.PreviousWeight ?? 1,
						IsActive = dto.IsActive,
						MarkType_Code = dto.MarkType
					};

					ModelFactorsRepository.Save(newFactor);
				});

				ts.Complete();
			}
		}

		public bool UpdateAutomaticFactor(AutomaticModelFactorDto dto)
		{
			ValidateAutomaticFactor(dto);

			var factor = GetFactorById(dto.Id);

			var mustResetTrainingResult = false;
			using (var ts = new TransactionScope())
			{
				if (factor.IsActive.GetValueOrDefault() != dto.IsActive)
				{
					var factors = OMModelFactor.Where(x => x.ModelId == dto.ModelId && x.FactorId == dto.FactorId)
						.Select(x => new
						{
							x.IsActive
						}).Execute();
					
					factors.ForEach(x =>
					{
						x.IsActive = dto.IsActive;
						ModelFactorsRepository.Save(x);
					});
					mustResetTrainingResult = true;
				}

				factor.PreviousWeight = dto.PreviousWeight ?? 1;
				factor.MarkType_Code = dto.MarkType;
				ModelFactorsRepository.Save(factor);

				ts.Complete();
			}

			return mustResetTrainingResult;
		}

		public int AddManualFactor(ManualModelFactorDto dto)
		{
			ValidateManualFactorBeforeAddition(dto);

			var newFactor = new OMModelFactor
			{
				ModelId = dto.GeneralModelId,
				FactorId = dto.FactorId,
				DictionaryId = dto.DictionaryId,
				MarkerId = -1,
				Weight = dto.Weight,
				B0 = dto.B0,
				SignDiv = dto.SignDiv,
				SignAdd = dto.SignAdd,
				AlgorithmType_Code = dto.Type,
				MarkType_Code = dto.MarkType
			};

			if (IsSpecialMarkType(dto.MarkType))
			{
				newFactor.CorrectingTerm = dto.CorrectItem;
				newFactor.K = dto.K;
			}

			var id = ModelFactorsRepository.Save(newFactor);

			//RecalculateFormula(dto.GeneralModelId);

			return id;
		}

		public void UpdateManualFactor(ManualModelFactorDto dto)
		{
			ValidateManualFactor(dto);

			var factor = GetFactorById(dto.Id);

			factor.Weight = dto.Weight;
			factor.B0 = dto.B0;
			factor.SignDiv = dto.SignDiv;
			factor.SignAdd = dto.SignAdd;
			factor.MarkType_Code = dto.MarkType;

			if (IsSpecialMarkType(dto.MarkType))
			{
				factor.CorrectingTerm = dto.CorrectItem;
				factor.K = dto.K;
			}

			ModelFactorsRepository.Save(factor);

			//RecalculateFormula(dto.GeneralModelId);
		}

		public void DeleteManualModelFactor(long? id)
		{
			var factor = GetFactorById(id);

			using (var ts = TransactionScopeWrapper.OpenTransaction(TransactionScopeOption.RequiresNew))
			{
				factor.Destroy();

				ModelDictionaryService.DeleteDictionary(factor.DictionaryId);

				//RecalculateFormula(factor.ModelId);

				ts.Complete();
			}
		}

		public void DeleteAutomaticModelFactor(long? id)
		{
			//todo инжектить нельзя, вынести в отдельный сервис?
			var modelService = new ModelingService();
			var factor = GetFactorById(id);

			var allFactors = OMModelFactor.Where(x => x.ModelId == factor.ModelId && x.FactorId == factor.FactorId)
				.Execute();

			using (var ts = new TransactionScope())
			{
				allFactors.ForEach(x => x.Destroy());

				ModelDictionaryService.DeleteDictionary(factor.DictionaryId);

				ts.Complete();
			}

			modelService.ResetTrainingResults(factor.ModelId, KoAlgoritmType.None);

			var model = OMModel.Where(x => x.Id == factor.ModelId)
				.Select(x => new
				{
					x.GroupId,
					x.ObjectsStatistic
				})
				.ExecuteFirstOrDefault();

			var statistic = model?.ObjectsStatistic?.DeserializeFromXml<ModelingObjectsStatistic>();
			var deletedFactorStatistic = statistic?.ObjectsByAttributeStatistics.FirstOrDefault(x => x.AttributeId == factor.FactorId);
			if (deletedFactorStatistic != null)
			{
				statistic.ObjectsByAttributeStatistics.Remove(deletedFactorStatistic);
				model.ObjectsStatistic = statistic.SerializeToXml();
				model.Save();
			}
		}

		#region Support Methods

		private void ValidateManualFactorBeforeAddition(ManualModelFactorDto factorDto)
		{
			ValidateManualFactor(factorDto);

			if (factorDto.MarkType == MarkType.Default && factorDto.DictionaryId.GetValueOrDefault() == 0)
				throw new EmptyDictionaryForFactorWithDefaultMarkException();
		}

		private void ValidateManualFactor(ManualModelFactorDto factorDto)
		{
			ValidateBaseFactor(factorDto.Id, factorDto.GeneralModelId, factorDto.FactorId, factorDto.Type);

			if (factorDto.Type == KoAlgoritmType.None)
				throw new Exception("Не передан тип алгоритма модели для фактора");

			var factorAttributeType = RegisterCacheWrapper.GetAttributeData(factorDto.FactorId.GetValueOrDefault()).Type;
			if (factorAttributeType != RegisterAttributeType.DECIMAL &&
			    factorAttributeType != RegisterAttributeType.INTEGER && factorDto.MarkType != MarkType.Default)
				throw new WrongFactorTypeException();

			if (IsSpecialMarkType(factorDto.MarkType))
			{
				if (factorDto.CorrectItem == null)
					throw new EmptyCorrectTermForFactorException();
				
				if (factorDto.K == null)
					throw new EmptyKForFactorException();

				if (factorDto.MarkType == MarkType.Straight && factorDto.K.GetValueOrDefault() == 0)
					throw new EmptyKForFactorWithStraightMarkException();
			}
		}

		private bool IsSpecialMarkType(MarkType markType)
		{
			return markType == MarkType.Straight || markType == MarkType.Reverse;
		}

		private void ValidateAutomaticFactor(AutomaticModelFactorDto factor)
		{
			ValidateBaseFactor(factor.Id, factor.ModelId, factor.FactorId, factor.Type);

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
							if (dictionary.Type_Code != ModelDictionaryType.String)
								errors.Add(GenerateMessage(attribute.Name, ModelDictionaryType.String));
							break;
						}
					case RegisterAttributeType.DATE:
						{
							if (dictionary.Type_Code != ModelDictionaryType.Date)
								errors.Add(GenerateMessage(attribute.Name, ModelDictionaryType.Date));
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

			var isTheSameAttributeExists = ModelFactorsRepository.IsTheSameAttributeExists(id, factorId.Value, modelId.Value, type);
			if (isTheSameAttributeExists)
				throw new Exception($"Атрибут '{RegisterCache.GetAttributeData(factorId.GetValueOrDefault()).Name}' уже был добавлен");
		}

		private string GenerateMessage(string attributeName, ModelDictionaryType dictionaryType)
		{
			return $"Выберите словарь типа '{dictionaryType.GetEnumDescription()}' для атрибута '{attributeName}'";
		}

		private List<KoAlgoritmType> GetPossibleTypes(KoAlgoritmType type)
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

		//private void RecalculateFormula(long? generalModelId)
		//{
		//	var model = OMModel.Where(x => x.Id == generalModelId).SelectAll().ExecuteFirstOrDefault();
		//	if(model == null)
		//		throw new Exception($"Не найдена модель с ИД '{generalModelId}'");

		//	model.Formula = model.GetFormulaFull(true);
		//	model.Save();
		//}

		#endregion

		#endregion

		#region Метки


		public List<OMMarkCatalog> GetMarks(long? groupId, List<long?> factorIds)
		{
			var notNullFactorIds = factorIds.Where(x => x.HasValue).ToList();
			if (groupId == null || notNullFactorIds.Count == 0)
				return new List<OMMarkCatalog>();

			return OMMarkCatalog.Where(x => notNullFactorIds.Contains(x.FactorId) && x.GroupId == groupId).SelectAll().Execute();
		}

		#endregion
	}
}
