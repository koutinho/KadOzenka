using System.Collections.Generic;
using Core.Register;
using ObjectModel.Modeling;

namespace KadOzenka.Dal.Modeling.Objects.Import
{
	public interface IModelObjectsImporter
	{
		RegisterObject CreateObject(long? objectId);

		CoefficientForObject GetCoefficient(List<CoefficientForObject> coefficients, long attributeId);
	}
}