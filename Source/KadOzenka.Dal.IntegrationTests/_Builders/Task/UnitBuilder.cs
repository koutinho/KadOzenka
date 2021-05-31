using KadOzenka.Common.Tests.Builders.Task;
using ObjectModel.KO;

namespace KadOzenka.Dal.Integration._Builders.Task
{
	public class UnitBuilder : AUnitBuilder
	{
		public override OMUnit Build()
		{
			_unit.Save();
			return _unit;
		}
	}
}
