using System.Collections.Generic;
using Core.Register.QuerySubsystem;
using KadOzenka.Dal.Modeling.Dto;
using KadOzenka.Dal.Modeling.Dto.Factors;
using ObjectModel.Directory;
using ObjectModel.KO;

namespace KadOzenka.Dal.Modeling
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
		List<OMMarkCatalog> GetMarks(long? groupId, long? factorId);
		OMMarkCatalog GetMarkById(long id);
		int CreateMark(string value, decimal? metka, long? factorId, long? groupId);
		void UpdateMark(long id, string value, decimal? metka);
		void DeleteMark(long id);
		int DeleteMarks(long? groupId, long? factorId);
		List<OMMarkCatalog> GetMarks(long? groupId, List<long?> factorIds);
	}
}