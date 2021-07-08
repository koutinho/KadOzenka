using KadOzenka.Common.Tests;
using KadOzenka.Common.Tests.Builders.Modeling;
using ObjectModel.KO;

namespace KadOzenka.Dal.UnitTests._Builders.Modeling
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