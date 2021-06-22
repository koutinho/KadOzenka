using System.Collections.Generic;
using KadOzenka.Dal.Tours;
using ObjectModel.KO;

namespace KadOzenka.Dal.Units
{
	public interface IUnitService
	{
		List<UnitFactor> GetUnitModelFactors(OMUnit unit);
		List<UnitFactor> GetUnitGroupFactors(OMUnit unit);
		List<UnitFactor> GetUnitFactors(OMUnit unit, List<long> attributes = null);
	}
}
