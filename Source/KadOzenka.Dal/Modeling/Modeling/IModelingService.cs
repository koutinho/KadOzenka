using System.IO;
using KadOzenka.Dal.Modeling.Modeling.Entities;
using KadOzenka.Dal.Modeling.Modeling.Responses;
using ObjectModel.Directory;
using ObjectModel.KO;

namespace KadOzenka.Dal.Modeling.Modeling
{
	public interface IModelingService
	{
		void ResetTrainingResults(long? modelId, KoAlgoritmType type);

		void ResetTrainingResults(OMModel generalModel, KoAlgoritmType type);

		void UpdateTrainingQualityInfo(long modelId, KoAlgoritmType type, QualityControlInfo newQualityControlInfo);

		TrainingDetailsDto GetTrainingResult(long modelId, KoAlgoritmType type);

		Stream ExportQualityInfoToExcel(long modelId, KoAlgoritmType type);
		OMModelTrainingResultImages GetModelImages(long modelId, KoAlgoritmType type);
		
		long? GetDictionaryId(long? groupId, long? factorId);
	}
}