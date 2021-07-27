using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
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

		public List<OMModelFactor> GetFactorsEntities(long? modelId)
		{
			return OMModelFactor.Where(x => x.ModelId == modelId).SelectAll().Execute();
		}

		public List<ModelFactorRelation> GetFactors(long modelId)
		{
			var query = GetModelFactorsQuery(modelId);

			query.AddColumn(OMAttribute.GetColumn(x => x.RegisterId, nameof(ModelFactorRelation.RegisterId)));
			query.AddColumn(OMAttribute.GetColumn(x => x.Id, nameof(ModelFactorRelation.AttributeId)));
			query.AddColumn(OMAttribute.GetColumn(x => x.Name, nameof(ModelFactorRelation.AttributeName)));
			query.AddColumn(OMAttribute.GetColumn(x => x.Type, nameof(ModelFactorRelation.AttributeType)));
			query.AddColumn(OMModelingDictionary.GetColumn(x => x.Id, nameof(ModelFactorRelation.DictionaryId)));
			query.AddColumn(OMModelingDictionary.GetColumn(x => x.Name, nameof(ModelFactorRelation.DictionaryName)));
			query.AddColumn(OMModelFactor.GetColumn(x => x.CoefficientForLinear, nameof(ModelFactorRelation.CoefficientForLinear)));
			query.AddColumn(OMModelFactor.GetColumn(x => x.CoefficientForExponential, nameof(ModelFactorRelation.CoefficientForExponential)));
			query.AddColumn(OMModelFactor.GetColumn(x => x.CoefficientForMultiplicative, nameof(ModelFactorRelation.CoefficientForMultiplicative)));
			query.AddColumn(OMModelFactor.GetColumn(x => x.SignMarket, nameof(ModelFactorRelation.SignMarket)));
			query.AddColumn(OMModelFactor.GetColumn(x => x.Correction, nameof(ModelFactorRelation.Correction)));
			query.AddColumn(OMModelFactor.GetColumn(x => x.IsActive, nameof(ModelFactorRelation.IsActive)));
			query.AddColumn(OMModelFactor.GetColumn(x => x.MarkType_Code, nameof(ModelFactorRelation.MarkTypeCode)));
			query.AddColumn(OMModelFactor.GetColumn(x => x.CorrectingTerm, nameof(ModelFactorRelation.CorrectingTerm)));
			query.AddColumn(OMModelFactor.GetColumn(x => x.K, nameof(ModelFactorRelation.K)));

			//var sql = query.GetSql();

			var attributes = new List<ModelFactorRelation>();
			var table = query.ExecuteQuery();
			for (var i = 0; i < table.Rows.Count; i++)
			{
				var row = table.Rows[i];

				var id = row[nameof(ModelFactorRelation.Id)].ParseToLong();

				var registerId = row[nameof(ModelFactorRelation.RegisterId)].ParseToLong();

				var attributeId = row[nameof(ModelFactorRelation.AttributeId)].ParseToLong();
				var attributeName = row[nameof(ModelFactorRelation.AttributeName)].ParseToString();
				var attributeType = row[nameof(ModelFactorRelation.AttributeType)].ParseToInt();

				var dictionaryId = row[nameof(ModelFactorRelation.DictionaryId)].ParseToLongNullable();
				var dictionaryName = row[nameof(ModelFactorRelation.DictionaryName)].ParseToString();

				var coefficientForLinear = row[nameof(ModelFactorRelation.CoefficientForLinear)].ParseToDecimalNullable();
				var coefficientForExponential = row[nameof(ModelFactorRelation.CoefficientForExponential)].ParseToDecimalNullable();
				var coefficientForMultiplicative = row[nameof(ModelFactorRelation.CoefficientForMultiplicative)].ParseToDecimalNullable();
				var signMarket = row[nameof(ModelFactorRelation.SignMarket)].ParseToBooleanNullable();
				var correction = row[nameof(ModelFactorRelation.Correction)].ParseToDecimalNullable();
				var correctingTerm = row[nameof(ModelFactorRelation.CorrectingTerm)].ParseToDecimalNullable();
				var k = row[nameof(ModelFactorRelation.K)].ParseToDecimalNullable();
				var isActive = row[nameof(ModelFactorRelation.IsActive)].ParseToBooleanNullable();
				var markType = row[nameof(ModelFactorRelation.MarkTypeCode)].ParseToLong();

				attributes.Add(new ModelFactorRelation
				{
					Id = id,
					RegisterId = registerId,
					AttributeId = attributeId,
					AttributeName = attributeName,
					AttributeType = attributeType,
					DictionaryId = dictionaryId,
					DictionaryName = dictionaryName,
					CoefficientForLinear = coefficientForLinear,
					CoefficientForExponential = coefficientForExponential,
					CoefficientForMultiplicative = coefficientForMultiplicative,
					SignMarket = signMarket.GetValueOrDefault(),
					Correction = correction,
					CorrectingTerm = correctingTerm,
					K = k,
					IsActive = isActive.GetValueOrDefault(),
					MarkTypeCode = (MarkType) markType
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

			var newFactor = new OMModelFactor
			{
				ModelId = dto.ModelId,
				FactorId = dto.FactorId,
				DictionaryId = dto.DictionaryId,
				IsActive = dto.IsActive,
				MarkType_Code = dto.MarkType,
				Correction = dto.Correction
			};

			ModelFactorsRepository.Save(newFactor);
		}

		public bool UpdateAutomaticFactor(AutomaticModelFactorDto dto)
		{
			ValidateAutomaticFactor(dto);
			
			var factor = GetFactorById(dto.Id);
			var mustResetTrainingResult = factor.DictionaryId != dto.DictionaryId ||
			                              factor.IsActive.GetValueOrDefault() != dto.IsActive ||
			                              factor.MarkType_Code != dto.MarkType;

			factor.IsActive = dto.IsActive;
			factor.MarkType_Code = dto.MarkType;
			factor.Correction = dto.Correction;
			ProcessDictionary(factor, dto);

			ModelFactorsRepository.Save(factor);

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
				Correction = dto.Correction,
				MarkType_Code = dto.MarkType
			};
			newFactor.SetCoefficient(dto.Coefficient, dto.Type);

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
			factor.SetCoefficient(dto.Coefficient, dto.Type);
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

			using (var ts = new TransactionScope())
			{
				factor.Destroy();

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

			var isTheSameAttributeExists = ModelFactorsRepository.IsTheSameAttributeExists(factor.Id, factor.FactorId.Value, factor.ModelId.Value);
			if (isTheSameAttributeExists)
				throw new Exception($"Атрибут '{RegisterCache.GetAttributeData(factor.FactorId.GetValueOrDefault()).Name}' уже был добавлен");
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
