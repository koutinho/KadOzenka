﻿using KadOzenka.Dal.CommonFunctions;
using ObjectModel.KO;

namespace KadOzenka.Dal.Modeling.Repositories
{
	public interface IModelMarksRepository : IGenericRepository<OMModelingDictionariesValues>
	{
		bool IsTheSameMarkExists(long dictionaryId, long markId, string value);
	}
}
