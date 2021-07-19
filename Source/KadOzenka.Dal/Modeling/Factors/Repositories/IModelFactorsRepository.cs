using KadOzenka.Dal.CommonFunctions;
using ObjectModel.Directory;
using ObjectModel.KO;

namespace KadOzenka.Dal.Modeling.Factors.Repositories
{
	public interface IModelFactorsRepository : IGenericRepository<OMModelFactor>
	{
		bool IsTheSameAttributeExists(long id, long factorId, long modelId, KoAlgoritmType type);
		OMModelFactor GetFactorByDictionary(long dictionaryId);
	}
}
