using System.Collections.Generic;
using ModelingBusiness.Model.Entities;
using ObjectModel.Directory;
using ObjectModel.KO;

namespace ModelingBusiness.Model
{
	public interface IModelService
	{
		OMModel GetActiveModelEntityByGroupId(long? groupId);
		List<OMModel> GetGroupModels(long? groupId);
		OMModel GetModelEntityById(long? modelId);
		ModelDto GetModelById(long modelId);
		bool IsModelGroupExist(long modelId);
		OMTour GetModelTour(long? groupId);
		void AddAutomaticModel(ModelDto modelDto);
		void AddManualModel(ModelDto modelDto);
		void UpdateAutomaticModel(ModelDto modelDto);
		void UpdateManualModel(ModelDto modelDto);
		void MakeModelActive(long modelId);
		void DeleteModel(long modelId);
		void DeleteModelLogically(long modelId, long eventId);
		string GetFormula(OMModel model, KoAlgoritmType algorithmType);
	}
}