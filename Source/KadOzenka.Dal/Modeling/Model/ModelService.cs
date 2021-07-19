using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Transactions;
using Core.Register.QuerySubsystem;
using Core.Shared.Extensions;
using KadOzenka.Dal.CommonFunctions;
using KadOzenka.Dal.Modeling.Factors;
using KadOzenka.Dal.Modeling.Model.Entities;
using KadOzenka.Dal.Modeling.Model.Exceptions;
using KadOzenka.Dal.Modeling.Model.Formulas;
using KadOzenka.Dal.Modeling.Model.Repositories;
using KadOzenka.Dal.Modeling.Resources;
using KadOzenka.Dal.RecycleBin;
using ModelingBusiness.Objects.Entities;
using ModelingBusiness.Objects.Repositories;
using ObjectModel.Directory;
using ObjectModel.Directory.Ko;
using ObjectModel.KO;
using ObjectModel.Modeling;
using Serilog;

namespace KadOzenka.Dal.Modeling.Model
{
	public class ModelService : IModelService
	{
		private readonly ILogger _log = Log.ForContext<ModelService>();
        private IModelingRepository ModelingRepository { get; set; }
        private IModelObjectsRepository ModelObjectsRepository { get; set; }
        private IModelFactorsService ModelFactorsService { get; set; }
        private RecycleBinService RecycleBinService { get; }
        public IRegisterCacheWrapper RegisterCacheWrapper { get; }
        

        public ModelService(IModelingRepository modelingRepository = null,
			IModelObjectsRepository modelObjectsRepository = null,
			IModelFactorsService modelFactorsService = null,
			IRegisterCacheWrapper registerCacheWrapper = null)
		{
			ModelFactorsService = modelFactorsService ?? new ModelFactorsService();
			ModelingRepository = modelingRepository ?? new ModelingRepository();
			RecycleBinService = new RecycleBinService();
			ModelObjectsRepository = modelObjectsRepository ?? new ModelObjectsRepository();
			RegisterCacheWrapper = registerCacheWrapper ?? new RegisterCacheWrapper();
		}


		#region CRUD General Model

		public OMModel GetActiveModelEntityByGroupId(long? groupId)
        {
	        if (groupId == null)
		        throw new Exception("Не передан идентификатор Группы для поиска модели");

			var model = ModelingRepository.GetEntityByCondition(x => x.GroupId == groupId && x.IsActive.Coalesce(false) == true, null);

			return model;
        }

		public List<OMModel> GetGroupModels(long? groupId)
		{
			if (groupId == null)
				throw new Exception("Не передан идентификатор Группы для поиска модели");

			return OMModel.Where(x => x.GroupId == groupId)
				.OrderByDescending(x => x.IsActive.Coalesce(false)).OrderBy(x => x.Name)
				.Select(x => new
				{
					x.Id,
					x.Name
				})
				.Execute();
		}

		public OMModel GetModelEntityById(long? modelId)
        {
	        if (modelId.GetValueOrDefault() == 0)
		        throw new Exception(Messages.EmptyModelId);

	        var model = ModelingRepository.GetById(modelId.Value, null);
	        if (model == null)
		        throw new ModelNotFoundByIdException($"Не найдена Модель с id='{modelId}'");

	        return model;
        }

        public ModelDto GetModelById(long modelId)
        {
	        Expression<Func<OMModel, object>> selectExpression = x => new
	        {
		        x.Description,
		        x.Name,
		        x.LinearTrainingResult,
		        x.ExponentialTrainingResult,
		        x.MultiplicativeTrainingResult,
		        x.GroupId,
		        x.ParentGroup.Id,
		        x.ParentGroup.GroupName,
		        x.IsOksObjectType,
		        x.Type_Code,
		        x.AlgoritmType_Code,
		        x.CalculationType_Code,
		        x.A0,
		        x.A0ForExponential,
		        x.A0ForMultiplicative,
		        x.Formula,
		        x.CalculationMethod_Code,
		        x.IsActive
	        };
	        
	        var model = ModelingRepository.GetById(modelId, selectExpression);
	        if (model == null)
		        throw new Exception($"Не найдена модель с ИД '{modelId}'");

			var tour = GetModelTour(model.GroupId);

            return new ModelDto
	        {
		        ModelId = model.Id,
		        Name = model.Name,
		        Description = model.Description,
		        LinearTrainingResult = model.LinearTrainingResult,
		        ExponentialTrainingResult = model.ExponentialTrainingResult,
		        MultiplicativeTrainingResult = model.MultiplicativeTrainingResult,
				TourId = tour.Id,
				TourYear = tour.Year.GetValueOrDefault(),
				GroupId = model.ParentGroup.Id,
		        GroupName = model.ParentGroup.GroupName,
		        IsOksObjectType = model.IsOksObjectType.GetValueOrDefault(),
                Type = model.Type_Code,
                AlgorithmTypeForCadastralPriceCalculation = model.AlgoritmType_Code,
                CalculationType = model.CalculationType_Code,
                A0 = model.GetA0(),
                Formula = model.Formula,
                CalculationMethod = model.CalculationMethod_Code,
                IsActive = model.IsActive.GetValueOrDefault(),
				IsModelWasTrained = model.IsModelWasTrained,
				HasLinearTrainingResult = model.HasLinearTrainingResult,
				HasExponentialTrainingResult = model.HasExponentialTrainingResult,
				HasMultiplicativeTrainingResult = model.HasMultiplicativeTrainingResult,
			};
        }

        public bool IsModelGroupExist(long modelId)
        {
	        var model = ModelingRepository.GetById(modelId, x => new {x.GroupId});
	        if (model == null)
		        return false;

	        return OMGroup.Where(x => x.Id == model.GroupId).ExecuteExists();
        }

        public OMTour GetModelTour(long? groupId)
        {
	        var tourToGroupRelation = OMTourGroup.Where(x => x.GroupId == groupId).Select(x => new
	        {
		        x.ParentTour.Id,
		        x.ParentTour.Year
	        }).ExecuteFirstOrDefault();
	        if (tourToGroupRelation?.ParentTour == null)
		        throw new Exception($"Для группы {groupId} не найдено Тура");

	        return tourToGroupRelation.ParentTour;
        }

        public void AddAutomaticModel(ModelDto modelDto)
        {
	        ValidateModelDuringAddition(modelDto);

	        var model = new OMModel
	        {
		        Name = modelDto.Name,
		        Description = modelDto.Description,
		        GroupId = modelDto.GroupId,
                IsOksObjectType = modelDto.IsOksObjectType,
		        CalculationType_Code = KoCalculationType.Comparative,
		        AlgoritmType_Code = modelDto.AlgorithmTypeForCadastralPriceCalculation,
		        Type_Code = KoModelType.Automatic,
                Formula = "-"
	        };

	        ModelingRepository.Save(model);
        }

        public void AddManualModel(ModelDto modelDto)
        {
	        ValidateModelDuringAddition(modelDto);

	        var model = new OMModel
	        {
		        Name = modelDto.Name,
		        Description = modelDto.Description,
		        GroupId = modelDto.GroupId,
		        IsOksObjectType = modelDto.IsOksObjectType,
                AlgoritmType_Code = modelDto.AlgorithmTypeForCadastralPriceCalculation,
		        Type_Code = KoModelType.Manual,
		        Formula = "-"
			};

	        ModelingRepository.Save(model);
		}

        public void UpdateAutomaticModel(ModelDto modelDto)
		{
			ValidateBaseModel(modelDto);

            var existedModel = GetModelEntityById(modelDto.ModelId);

            existedModel.Name = modelDto.Name;
            existedModel.Description = modelDto.Description;
            existedModel.AlgoritmType_Code = modelDto.AlgorithmTypeForCadastralPriceCalculation;

            existedModel.Save();
        }

        public void UpdateManualModel(ModelDto modelDto)
        {
	        ValidateBaseModel(modelDto);

            var existedModel = GetModelEntityById(modelDto.ModelId);

            using (var ts = new TransactionScope())
            {
	            if (existedModel.AlgoritmType_Code != modelDto.AlgorithmTypeForCadastralPriceCalculation)
	            {
		            var factors = ModelFactorsService.GetFactors(modelDto.ModelId, existedModel.AlgoritmType_Code);
		            factors.ForEach(x =>
		            {
			            x.AlgorithmType_Code = modelDto.AlgorithmTypeForCadastralPriceCalculation;
			            x.Save();
		            });
	            }

	            existedModel.Name = modelDto.Name;
	            existedModel.Description = modelDto.Description;
	            existedModel.AlgoritmType_Code = modelDto.AlgorithmTypeForCadastralPriceCalculation;
				existedModel.SetA0(modelDto.A0, modelDto.AlgorithmTypeForCadastralPriceCalculation);

				existedModel.CalculationMethod_Code = modelDto.CalculationType == KoCalculationType.Comparative
		            ? modelDto.CalculationMethod
		            : KoCalculationMethod.None;

	            existedModel.CalculationType_Code = modelDto.CalculationType;
	            //existedModel.Formula = GetFormula(existedModel);

	            existedModel.Save();

                ts.Complete();
            }
        }

        public void MakeModelActive(long modelId)
        {
	        var model = ModelingRepository.GetById(modelId, x => new
	        {
		        x.GroupId,
		        x.Type_Code,
		        x.LinearTrainingResult,
		        x.ExponentialTrainingResult,
		        x.MultiplicativeTrainingResult,
		        x.IsActive
	        });

	        if (model.Type_Code == KoModelType.Automatic)
	        {
		        var hasFormedObjectArray = ModelObjectsRepository.AreIncludedModelObjectsExist(modelId, IncludedObjectsMode.Training);
		        var hasTrainingResult = !string.IsNullOrWhiteSpace(model.LinearTrainingResult) ||
		                                !string.IsNullOrWhiteSpace(model.ExponentialTrainingResult) ||
		                                !string.IsNullOrWhiteSpace(model.MultiplicativeTrainingResult);
		        if (!hasFormedObjectArray || !hasTrainingResult)
			        throw new Exception(Messages.CanNotActivateNotPreparedAutomaticModel);
			}
	        
			using (var ts = new TransactionScope())
			{
				var otherModelsForGroup = ModelingRepository.GetEntitiesByCondition(
					x => x.GroupId == model.GroupId && x.IsActive.Coalesce(false) == true, x => new {x.IsActive});
				otherModelsForGroup.ForEach(x =>
				{
					x.IsActive = false;
					ModelingRepository.Save(x);
				});

		        if (!model.IsActive.GetValueOrDefault())
		        {
			        model.IsActive = true;
			        ModelingRepository.Save(model);
		        }

		        ts.Complete();
	        }
        }

        public void DeleteModel(long modelId)
        {
			var model = GetModelEntityById(modelId);

			var factors = ModelFactorsService.GetFactors(modelId, KoAlgoritmType.None);
			factors.ForEach(factor => factor.Destroy());

			if (model.Type_Code == KoModelType.Automatic)
			{
				var modelToObjectsRelation = OMModelToMarketObjects.Where(x => x.ModelId == modelId).Execute();
				modelToObjectsRelation.ForEach(x => x.Destroy());
			}

			model.Destroy();
		}

        public void DeleteModelLogically(long modelId, long eventId)
        {
	        var model = GetModelEntityById(modelId);

	        var factors = ModelFactorsService.GetFactors(modelId, KoAlgoritmType.None);
	        RecycleBinService.MoveObjectsToRecycleBin(factors.Select(x => x.Id).ToList(), OMModelFactor.GetRegisterId(), eventId);

	        if (model.Type_Code == KoModelType.Automatic)
	        {
		        var modelToObjectsRelation = OMModelToMarketObjects.Where(x => x.ModelId == modelId).Execute();
		        RecycleBinService.MoveObjectsToRecycleBin(modelToObjectsRelation.Select(x => x.Id).ToList(), OMModelToMarketObjects.GetRegisterId(), eventId);
	        }

			RecycleBinService.MoveObjectToRecycleBin(model.Id, OMModel.GetRegisterId(), eventId);
		}

		#region Support Methods

		private void ValidateBaseModel(ModelDto modelDto)
        {
	        var message = new StringBuilder();

	        if (string.IsNullOrWhiteSpace(modelDto.Name))
		        message.AppendLine(Messages.EmptyName);
	        if (string.IsNullOrWhiteSpace(modelDto.Description))
		        message.AppendLine("У модели не заполнено Описание");

	        if (modelDto.Type == KoModelType.Manual && modelDto.AlgorithmTypeForCadastralPriceCalculation == KoAlgoritmType.None)
		        message.AppendLine($"Для модели типа '{KoModelType.Manual.GetEnumDescription()}' нужно указать Тип алгоритма");

	        if (message.Length != 0)
		        throw new ModelCrudException(message.ToString());
        }

        private void ValidateModelDuringAddition(ModelDto modelDto)
        {
	        ValidateBaseModel(modelDto);

	        var message = new StringBuilder();

	        if (modelDto.GroupId == 0)
		        message.AppendLine("Для модели не выбрана группа");

	        var isGroupBelongToTour = OMTourGroup.Where(x => x.TourId == modelDto.TourId && x.GroupId == modelDto.GroupId).ExecuteExists();
	        if (!isGroupBelongToTour)
		        message.AppendLine($"Группа c Id='{modelDto.GroupId}'не принадлежит туру с Id='{modelDto.TourId}'");

	        if (message.Length != 0)
				throw new ModelCrudException(message.ToString());
		}

        #endregion

        #endregion

        #region Formulas

		public string GetFormula(OMModel model, KoAlgoritmType algorithmType)
		{
			//для ручной модели существует один набор факторов под алгоритм самой модели
			//для автоматической модели набор факторов разный под тип алгоритма
			var typeForFactors = model.IsAutomatic ? algorithmType : model.AlgoritmType_Code;
			var factors = ModelFactorsService.GetFactors(model.Id, typeForFactors);
			if (factors.Count == 0 || (model.IsAutomatic && !model.IsModelWasTrained))
				return "Y = 0";

			var formulaCreator = GetFormulaCreator(algorithmType);

			var factorsInFormula = new StringBuilder();

			factors.ForEach(x =>
			{
				var attributeName = $"\"{RegisterCacheWrapper.GetAttributeData(x.FactorId.GetValueOrDefault()).Name}\"";
				var weightInFormula = formulaCreator.ProcessNumber(x.WeightInFormula);
				var b0InFormula = formulaCreator.ProcessNumber(x.B0InFormula);
				var correctingTermInFormula = formulaCreator.ProcessNumber(x.CorrectingTermInFormula);
				var kInFormula = formulaCreator.ProcessNumber(x.KInFormula);
				var modelInfo = new ModelInfoForFormula(attributeName, weightInFormula, b0InFormula,
					correctingTermInFormula, kInFormula);

				switch (x.MarkType_Code)
				{
					case MarkType.None:
						factorsInFormula.Append(formulaCreator.GetPartForNoneMarkType(modelInfo));
						break;
					case MarkType.Default:
						factorsInFormula.Append(formulaCreator.GetPartForDefaultMarkType(modelInfo));
						break;
					case MarkType.Straight:
						factorsInFormula.Append(formulaCreator.GetPartForStraightMarkType(modelInfo));
						break;
					case MarkType.Reverse:
						factorsInFormula.Append(formulaCreator.GetPartForReverseMarkType(modelInfo));
						break;
					default:
						throw new FormulaCreationException($"Передан неизвестный тип метки: '{x.MarkType_Code.GetEnumDescription()}'");
				}

				factorsInFormula.Append(formulaCreator.FactorsSeparator);
			});

			var processedFactors = factorsInFormula.ToString().TrimEnd(formulaCreator.FactorsSeparator);
			return formulaCreator.GetBaseFormulaPart(model, processedFactors);
		}


		#region Support Methods

		private BaseFormula GetFormulaCreator(KoAlgoritmType algorithmType)
		{
			switch (algorithmType)
			{
				case KoAlgoritmType.None:
					throw new FormulaCreationException("Не передан тип алгоритма расчета модели");
				case KoAlgoritmType.Exp:
					return new ExponentialFormula();
				case KoAlgoritmType.Line:
					return new LinearFormula();
				case KoAlgoritmType.Multi:
					return new MultiplicativeFormula();
				default:
					throw new FormulaCreationException($"Передан неизвестный тип алгоритма расчета модели: '{algorithmType.GetEnumDescription()}'");
			}
		}

		#endregion

		#endregion
	}
}
