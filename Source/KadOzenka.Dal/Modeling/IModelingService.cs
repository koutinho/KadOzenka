using System.IO;
using KadOzenka.Dal.Modeling.Dto;
using KadOzenka.Dal.Modeling.Entities;
using ObjectModel.Directory;
using ObjectModel.KO;

namespace KadOzenka.Dal.Modeling
{
	public interface IModelingService
	{
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

		void ResetTrainingResults(long? modelId, KoAlgoritmType type);

		void ResetTrainingResults(OMModel generalModel, KoAlgoritmType type);

		void UpdateTrainingQualityInfo(long modelId, KoAlgoritmType type, QualityControlInfo newQualityControlInfo);

		TrainingDetailsDto GetTrainingResult(long modelId, KoAlgoritmType type);

		Stream ExportQualityInfoToExcel(long modelId, KoAlgoritmType type);
		string GetFormula(long modelId);
	}
}