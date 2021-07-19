using System.Collections.Generic;
using Core.Register.QuerySubsystem;
using ModelingBusiness.Factors.Entities;
using ObjectModel.Directory;
using ObjectModel.KO;

namespace KadOzenka.Dal.Modeling.Factors
{
	public interface IModelFactorsService
	{
		OMModelFactor GetFactorById(long? id);
		List<OMModelFactor> GetFactors(long? modelId, KoAlgoritmType type);
		List<ModelAttributeRelationPure> GetGeneralModelAttributes(long modelId);
		List<ModelAttributeRelationDto> GetModelAttributes(long modelId, KoAlgoritmType type);
		QSQuery GetModelFactorsQuery(long modelId, QSCondition additionalCondition = null);
		void AddAutomaticFactor(AutomaticModelFactorDto dto);
		bool UpdateAutomaticFactor(AutomaticModelFactorDto dto);
		int AddManualFactor(ManualModelFactorDto dto);
		void UpdateManualFactor(ManualModelFactorDto dto);
		void DeleteManualModelFactor(long? id);
		void DeleteAutomaticModelFactor(long? id);
		List<long> GetAttributesWhichMustBeUnActive();
	}
}