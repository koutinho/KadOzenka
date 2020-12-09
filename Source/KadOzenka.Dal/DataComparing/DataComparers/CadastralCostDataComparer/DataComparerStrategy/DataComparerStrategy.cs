using System.Collections.Generic;
using KadOzenka.Dal.DataComparing.Files;

namespace KadOzenka.Dal.DataComparing.DataComparers.CadastralCostDataComparer.DataComparerStrategy
{
	public abstract class DataComparerStrategy : IDataComparerStrategy
	{
		public abstract void Compare();

		protected Dictionary<string, decimal?> GetUnitCadastralCostDictionary(List<CadastralCostKsFile> costFiles)
		{
			var dictionary = new Dictionary<string, decimal?>();
			foreach (var costFile in costFiles)
			{
				costFile.LoadFileFromStorage();
				costFile.FillUnitCadastralCostDictionary(dictionary);
			}

			return dictionary;
		}

		protected string GetDecimalString(decimal? value) => value?.ToString("0.00").Replace(',', '.');
	}
}
