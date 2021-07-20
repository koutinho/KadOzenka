using CommonSdks;
using CommonSdks.Repositories;
using ObjectModel.KO;

namespace ModelingBusiness.Dictionaries.Repositories
{
	public interface IModelMarksRepository : IGenericRepository<OMModelingDictionariesValues>
	{
		bool IsTheSameMarkExists(long dictionaryId, long markId, string value);
	}
}
