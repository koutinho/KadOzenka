using KadOzenka.Common.Tests.Builders.Modeling.Dictionaries;
using ObjectModel.KO;

namespace KadOzenka.Dal.Integration._Builders.Model
{
	public class DictionaryBuilder : ADictionaryBuilder
	{
		public override OMModelingDictionary Build()
		{
			_dictionary.Save();
			return _dictionary;
		}
	}
}