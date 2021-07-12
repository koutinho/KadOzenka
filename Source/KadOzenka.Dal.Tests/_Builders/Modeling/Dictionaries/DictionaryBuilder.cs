using KadOzenka.Common.Tests;
using KadOzenka.Common.Tests.Builders.Modeling.Dictionaries;
using ObjectModel.KO;

namespace KadOzenka.Dal.UnitTests._Builders.Modeling.Dictionaries
{
	public class DictionaryBuilder : ADictionaryBuilder
	{
		public override OMModelingDictionary Build()
		{
			_dictionary.Id = RandomGenerator.GenerateRandomId();
			return _dictionary;
		}
	}
}