using KadOzenka.Common.Tests.Builders.Modeling;
using ObjectModel.KO;

namespace KadOzenka.Dal.Integration._Builders.Model
{
	public class MarkBuilder : AMarkBuilder
	{
		public override OMModelingDictionariesValues Build()
		{
			_mark.Save();
			return _mark;
		}
	}
}