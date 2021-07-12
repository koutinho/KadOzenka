using System.Collections.Generic;
using System.IO;
using KadOzenka.Dal.Modeling.Dto;
using KadOzenka.Dal.Modeling.Entities;
using ObjectModel.Directory;
using ObjectModel.KO;

namespace KadOzenka.Dal.Modeling
{
	public interface IModelService
	{
		OMModel GetActiveModelEntityByGroupId(long? groupId);
		List<OMModel> GetGroupModels(long? groupId);
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
		string GetFormula(OMModel model, KoAlgoritmType algorithmType);
	}
}