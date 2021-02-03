using System.Collections.Generic;
using System.IO;
using GemBox.Spreadsheet;
using KadOzenka.Dal.CommonFunctions;
using KadOzenka.Dal.Modeling.Dto;
using ObjectModel.Directory;
using ObjectModel.KO;
using ObjectModel.Modeling;

namespace KadOzenka.Dal.Modeling
{
	public interface IModelingService
	{
		ModelFactorsService ModelFactorsService { get; set; }
		RecycleBinService RecycleBinService { get; }
		OMModel GetActiveModelEntityByGroupId(long? groupId);
		OMModel GetModelEntityById(long? modelId);
		ModelingModelDto GetModelById(long modelId);
		bool IsModelGroupExist(long modelId);
		OMTour GetModelTour(long? groupId);
		void AddAutomaticModel(ModelingModelDto modelDto);
		void AddManualModel(ModelingModelDto modelDto);
		void UpdateAutomaticModel(ModelingModelDto modelDto);
		void UpdateManualModel(ModelingModelDto modelDto);
		void MakeModelActive(long modelId);
		void DeleteModel(long modelId);
		void DeleteModelLogically(long modelId, long eventId);
		List<OMModelToMarketObjects> GetModelObjects(long modelId);
		List<OMModelToMarketObjects> GetIncludedModelObjects(long modelId, bool isForTraining);
		int DestroyModelMarketObjects(OMModel model);
		void ChangeObjectsStatusInCalculation(List<ModelMarketObjectRelationDto> objects);
		Stream ExportMarketObjectsToExcel(long modelId);
		ModelingService.UpdateModelObjectsResult UpdateModelObjects(long modelId, ExcelFile file);

		ModelingService.ModelObjectsCalculationParameters GetModelCalculationParameters(decimal? a0, decimal? objectPrice,
			List<OMModelFactor> factors, List<CoefficientForObject> objectCoefficients, string cadastralNumber);

		void ResetTrainingResults(long? modelId, KoAlgoritmType type);
		void ResetTrainingResults(OMModel generalModel, KoAlgoritmType type);
	}
}