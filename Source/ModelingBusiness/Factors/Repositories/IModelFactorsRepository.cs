using CommonSdks;
using CommonSdks.Repositories;
using ObjectModel.Directory;
using ObjectModel.KO;

namespace ModelingBusiness.Factors.Repositories
{
	public interface IModelFactorsRepository : IGenericRepository<OMModelFactor>
	{
		bool IsTheSameAttributeExists(long id, long factorId, long modelId, KoAlgoritmType type);
		OMModelFactor GetFactorByDictionary(long dictionaryId);
	}
}
