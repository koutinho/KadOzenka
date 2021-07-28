using System.Collections.Generic;
using Core.Register.QuerySubsystem;
using ModelingBusiness.Factors.Entities;
using ObjectModel.Directory;
using ObjectModel.KO;

namespace ModelingBusiness.Factors
{
	public interface IModelFactorsService
	{
		OMModelFactor GetFactorById(long? id);
		List<OMModelFactor> GetFactorsEntities(long? modelId);
		List<ModelFactorRelation> GetFactors(long modelId);
		QSQuery GetModelFactorsQuery(long modelId, QSCondition additionalCondition = null);
		void AddAutomaticFactor(AutomaticModelFactorDto dto);
		bool UpdateAutomaticFactor(AutomaticModelFactorDto dto);
		int AddManualFactor(ManualModelFactorDto dto);
		void UpdateManualFactor(ManualModelFactorDto dto);
		void DeleteManualModelFactor(long? id);
		void DeleteAutomaticModelFactor(long? id);
		List<long> GetAttributesWhichMustBeUnActive();
		List<ModelFactorRelation> GetCodedFactors(long modelId);
	}
}