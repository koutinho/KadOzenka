using System.Collections.Generic;
using ObjectModel.Market;

namespace MarketPlaceBusiness.Interfaces.Utils
{
	public interface IMissingDataFromGbuService
	{
		List<OMCoreObject> GetInitialObjects();

		List<OMCoreObject> GetExistingObjects();

		bool FillBuildingYearData(OMCoreObject omCoreObject, string yearStr);

		bool FillWallMaterialData(OMCoreObject omCoreObject, string wallMaterial);
	}
}