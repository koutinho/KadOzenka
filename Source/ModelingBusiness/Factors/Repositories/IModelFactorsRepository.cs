using CommonSdks.Repositories;
using ObjectModel.KO;

namespace ModelingBusiness.Factors.Repositories
{
	public interface IModelFactorsRepository : IGenericRepository<OMModelFactor>
	{
		bool IsTheSameAttributeExists(long id, long factorId, long modelId);
		OMModelFactor GetFactorByDictionary(long dictionaryId);
	}
}
