﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using CommonSdks;
using CommonSdks.PlatformWrappers;
using Core.Register;
using Core.Register.QuerySubsystem;
using Core.Shared.Extensions;
using Core.Shared.Misc;
using ModelingBusiness.Dictionaries;
using ModelingBusiness.Factors.Entities;
using ModelingBusiness.Factors.Exceptions;
using ModelingBusiness.Factors.Repositories;
using ModelingBusiness.Modeling;
using ModelingBusiness.Objects;
using ObjectModel.Core.Register;
using ObjectModel.Directory;
using ObjectModel.Directory.Ko;
using ObjectModel.Directory.KO;
using ObjectModel.KO;

namespace ModelingBusiness.Factors
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

		public List<ModelFactorRelationPure> GetGeneralModelFactors(long modelId)
		{
			var query = GetModelFactorsQuery(modelId);

			query.AddColumn(OMAttribute.GetColumn(x => x.RegisterId, nameof(ModelFactorRelationPure.RegisterId)));
			query.AddColumn(OMAttribute.GetColumn(x => x.Id, nameof(ModelFactorRelationPure.AttributeId)));
			query.AddColumn(OMAttribute.GetColumn(x => x.Name, nameof(ModelFactorRelationPure.AttributeName)));
			query.AddColumn(OMAttribute.GetColumn(x => x.Type, nameof(ModelFactorRelationPure.AttributeType)));
			query.AddColumn(OMModelingDictionary.GetColumn(x => x.Id, nameof(ModelFactorRelationPure.DictionaryId)));
			query.AddColumn(OMModelingDictionary.GetColumn(x => x.Name, nameof(ModelFactorRelationPure.DictionaryName)));
			query.AddColumn(OMModelFactor.GetColumn(x => x.IsActive, nameof(ModelFactorRelationPure.IsActive)));
			query.AddColumn(OMModelFactor.GetColumn(x => x.MarkType_Code, nameof(ModelFactorRelationPure.MarkType)));

			var attributes = new List<ModelFactorRelationPure>();
			var table = query.ExecuteQuery();
			for (var i = 0; i < table.Rows.Count; i++)
			{
				var row = table.Rows[i];

				var id = row[nameof(ModelFactorRelationPure.Id)].ParseToLong();

				var registerId = row[nameof(ModelFactorRelationPure.RegisterId)].ParseToLong();

				var attributeId = row[nameof(ModelFactorRelationPure.AttributeId)].ParseToLong();
				var attributeName = row[nameof(ModelFactorRelationPure.AttributeName)].ParseToString();
				var attributeType = row[nameof(ModelFactorRelationPure.AttributeType)].ParseToInt();

				var markType = row[nameof(ModelFactorRelationPure.MarkType)].ParseToLong();
				var dictionaryId = row[nameof(ModelFactorRelationPure.DictionaryId)].ParseToLongNullable();
				var dictionaryName = row[nameof(ModelFactorRelationPure.DictionaryName)].ParseToStringNullable();
				
				var isActive = row[nameof(ModelFactorRelationPure.IsActive)].ParseToBooleanNullable();

				attributes.Add(new ModelFactorRelationPure
				{
					Id = id,
					RegisterId = registerId,
					AttributeId = attributeId,
					AttributeName = attributeName,
					AttributeType = attributeType,
					MarkType = (MarkType) markType,
					DictionaryId = dictionaryId,
					DictionaryName = dictionaryName,
					IsActive = isActive.GetValueOrDefault()
				});
			}

			//если модель автоматическая, её факторы дублируются для лин/экс/мульт типов, поэтому группируем по AttributeId

			return attributes.GroupBy(x => x.AttributeId).Select(x => x.FirstOrDefault()).ToList();
		}

		public List<ModelFactorRelationDto> GetModelAttributes(long modelId, KoAlgoritmType type)
		{
			//для совместимости с уже ранее созданными моделями (не через блок "Справочники моделей")
			QSConditionSimple typeCondition = null;
			var isFactorsWithSpecificTypeExist = OMModelFactor.Where(x => x.ModelId == modelId && x.AlgorithmType_Code == type).ExecuteExists();
			if (isFactorsWithSpecificTypeExist)
			{
				typeCondition = new QSConditionSimple(OMModelFactor.GetColumn(x => x.AlgorithmType_Code), QSConditionType.Equal, (int)type);
			}

			var query = GetModelFactorsQuery(modelId, typeCondition);

			query.AddColumn(OMAttribute.GetColumn(x => x.RegisterId, nameof(ModelFactorRelationDto.RegisterId)));
			query.AddColumn(OMAttribute.GetColumn(x => x.Id, nameof(ModelFactorRelationDto.AttributeId)));
			query.AddColumn(OMAttribute.GetColumn(x => x.Name, nameof(ModelFactorRelationDto.AttributeName)));
			query.AddColumn(OMAttribute.GetColumn(x => x.Type, nameof(ModelFactorRelationDto.AttributeType)));
			query.AddColumn(OMModelingDictionary.GetColumn(x => x.Id, nameof(ModelFactorRelationDto.DictionaryId)));
			query.AddColumn(OMModelingDictionary.GetColumn(x => x.Name, nameof(ModelFactorRelationDto.DictionaryName)));
			query.AddColumn(OMModelFactor.GetColumn(x => x.CoefficientForLinear, nameof(ModelFactorRelationDto.Coefficient)));
			query.AddColumn(OMModelFactor.GetColumn(x => x.SignMarket, nameof(ModelFactorRelationDto.SignMarket)));
			query.AddColumn(OMModelFactor.GetColumn(x => x.Correction, nameof(ModelFactorRelationDto.Correction)));
			query.AddColumn(OMModelFactor.GetColumn(x => x.IsActive, nameof(ModelFactorRelationDto.IsActive)));
			query.AddColumn(OMModelFactor.GetColumn(x => x.MarkType, nameof(ModelFactorRelationDto.MarkType)));
			query.AddColumn(OMModelFactor.GetColumn(x => x.CorrectingTerm, nameof(ModelFactorRelationDto.CorrectingTerm)));
			query.AddColumn(OMModelFactor.GetColumn(x => x.K, nameof(ModelFactorRelationDto.K)));

			//var sql = query.GetSql();

			var attributes = new List<ModelFactorRelationDto>();
			var table = query.ExecuteQuery();
			for (var i = 0; i < table.Rows.Count; i++)
			{
				var row = table.Rows[i];

				var id = row[nameof(ModelFactorRelationDto.Id)].ParseToLong();

				var registerId = row[nameof(ModelFactorRelationDto.RegisterId)].ParseToLong();

				var attributeId = row[nameof(ModelFactorRelationDto.AttributeId)].ParseToLong();
				var attributeName = row[nameof(ModelFactorRelationDto.AttributeName)].ParseToString();
				var attributeType = row[nameof(ModelFactorRelationDto.AttributeType)].ParseToInt();

				var dictionaryId = row[nameof(ModelFactorRelationDto.DictionaryId)].ParseToLongNullable();
				var dictionaryName = row[nameof(ModelFactorRelationDto.DictionaryName)].ParseToString();

				var coefficient = row[nameof(ModelFactorRelationDto.Coefficient)].ParseToDecimalNullable();
				var signMarket = row[nameof(ModelFactorRelationDto.SignMarket)].ParseToBooleanNullable();
				var correction = row[nameof(ModelFactorRelationDto.Correction)].ParseToDecimalNullable();
				var correctingTerm = row[nameof(ModelFactorRelationDto.CorrectingTerm)].ParseToDecimalNullable();
				var k = row[nameof(ModelFactorRelationDto.K)].ParseToDecimalNullable();
				var isActive = row[nameof(ModelFactorRelationDto.IsActive)].ParseToBooleanNullable();
				var markType = row[nameof(ModelFactorRelationDto.MarkType)].ParseToString();

				attributes.Add(new ModelFactorRelationDto
				{
					Id = id,
					RegisterId = registerId,
					AttributeId = attributeId,
					AttributeName = attributeName,
					AttributeType = attributeType,
					DictionaryId = dictionaryId,
					DictionaryName = dictionaryName,
					Coefficient = coefficient.GetValueOrDefault(),
					SignMarket = signMarket.GetValueOrDefault(),
					Correction = correction,
					CorrectingTerm = correctingTerm,
					K = k,
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

		public List<long> GetAttributesWhichMustBeUnActive()
		{
			return RegisterCacheWrapper.GetAttributeDataList(MarketPlaceBusiness.Common.Consts.RegisterId)
				.Select(x => x.Id).ToList();
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
				if (factor.DictionaryId != dto.DictionaryId || 
				    factor.IsActive.GetValueOrDefault() != dto.IsActive ||
				    factor.MarkType_Code != dto.MarkType)
				{
					var factors = OMModelFactor.Where(x => x.ModelId == dto.ModelId && x.FactorId == dto.FactorId)
						.Select(x => new
						{
							x.IsActive,
							x.DictionaryId,
							x.MarkType_Code
						}).Execute();

					factors.ForEach(x =>
					{
						x.IsActive = dto.IsActive;
						x.MarkType_Code = dto.MarkType;
						ProcessDictionary(x, dto);

						ModelFactorsRepository.Save(x);
					});
					mustResetTrainingResult = true;
				}

				ts.Complete();
			}

			return mustResetTrainingResult;
		}

		public int AddManualFactor(ManualModelFactorDto dto)
		{
			ValidateManualFactor(dto);

			var newFactor = new OMModelFactor
			{
				ModelId = dto.ModelId,
				FactorId = dto.FactorId,
				DictionaryId = dto.DictionaryId,
				MarkerId = -1,
				Correction = dto.Correction,
				CoefficientForLinear = dto.Coefficient,
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
			ProcessDictionary(factor, dto);

			factor.Correction = dto.Correction;
			factor.CoefficientForLinear = dto.Coefficient;
			factor.MarkType_Code = dto.MarkType;

			if (IsSpecialMarkType(dto.MarkType))
			{
				factor.CorrectingTerm = dto.CorrectItem;
				factor.K = dto.K;
			}
			else
			{
				factor.CorrectingTerm = null;
				factor.K = null;
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

		private void ValidateManualFactor(ManualModelFactorDto factorDto)
		{
			ValidateBaseFactor(factorDto);

			if (factorDto.Type == KoAlgoritmType.None)
				throw new Exception("Не передан тип алгоритма модели для фактора");

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
			ValidateBaseFactor(factor);

			var activeForbiddenAttributes = GetAttributesWhichMustBeUnActive();
			if (activeForbiddenAttributes.Contains(factor.FactorId.GetValueOrDefault()) && factor.IsActive)
				throw new Exception("Атрибут недоступен для активации");

			var model = OMModel.Where(x => x.Id == factor.ModelId).Select(x => x.GroupId).ExecuteFirstOrDefault();
			if (model == null)
				throw new Exception($"Не найдена модель с ИД '{factor.ModelId}'");
		}

		private void ValidateBaseFactor(AModelFactorDto factor)
		{
			if (factor.ModelId == null)
				throw new Exception("Не передан ИД основной модели");

			if (factor.FactorId == null)
				throw new Exception("Не передан ИД фактора");

			if (factor.MarkType == MarkType.Default && factor.DictionaryId.GetValueOrDefault() == 0)
				throw new EmptyDictionaryForFactorWithDefaultMarkException();

			var factorAttributeType = RegisterCacheWrapper.GetAttributeData(factor.FactorId.GetValueOrDefault()).Type;
			if (factorAttributeType != RegisterAttributeType.DECIMAL &&
			    factorAttributeType != RegisterAttributeType.INTEGER && factor.MarkType != MarkType.Default)
				throw new WrongFactorTypeException();

			var isTheSameAttributeExists = ModelFactorsRepository.IsTheSameAttributeExists(factor.Id, factor.FactorId.Value, factor.ModelId.Value, factor.Type);
			if (isTheSameAttributeExists)
				throw new Exception($"Атрибут '{RegisterCache.GetAttributeData(factor.FactorId.GetValueOrDefault()).Name}' уже был добавлен");
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

		private void ProcessDictionary(OMModelFactor factor, AModelFactorDto dto)
		{
			//если раньше был тип метки по умолчанию, а потом его изменили, то удаляем словарь
			if (factor.DictionaryId != null && dto.MarkType != MarkType.Default)
			{
				ModelDictionaryService.DeleteDictionary(factor.DictionaryId);
				factor.DictionaryId = null;
			}
			else
			{
				factor.DictionaryId = dto.DictionaryId;
			}
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
	}
}
