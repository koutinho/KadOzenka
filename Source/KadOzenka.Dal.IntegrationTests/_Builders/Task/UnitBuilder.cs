using KadOzenka.Common.Tests.Builders.Task;
using ObjectModel.KO;

namespace KadOzenka.Dal.Integration._Builders.Task
{
	public class UnitBuilder : AUnitBuilder
	{
		public UnitBuilder()
		{
		}

		private UnitBuilder(OMUnit unit) : base(unit)
		{
		}


		public override AUnitBuilder ShallowCopy()
		{
			return new UnitBuilder(_unit);
		}

		public override OMUnit Build()
		{
			_unit.Save();
			return _unit;
		}
	}
}
