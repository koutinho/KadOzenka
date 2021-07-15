using System.Collections.Generic;
using Core.Register;
using ObjectModel.Modeling;

namespace KadOzenka.Dal.Modeling.Objects.Import
{
	public interface IModelObjectsImporter
	{
		bool IsValidateObject(RegisterObject omModelToMarketObject, bool isForControl,
			bool isForTraining);

		CoefficientForObject GetCoefficient(List<CoefficientForObject> coefficientsFromDb,
			long attributeId);

		RegisterObject CreateRegisterObject(long? objectId);
	}
}