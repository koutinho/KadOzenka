using KadOzenka.Common.Tests;
using KadOzenka.Common.Tests.Builders.Modeling.Dictionaries;
using ObjectModel.KO;

namespace KadOzenka.Dal.UnitTests._Builders.Modeling.Dictionaries
{
	public class MarkBuilder : AMarkBuilder
	{
		public override OMModelingDictionariesValues Build()
		{
			_mark.Id = RandomGenerator.GenerateRandomId();
			return _mark;
		}
	}
}