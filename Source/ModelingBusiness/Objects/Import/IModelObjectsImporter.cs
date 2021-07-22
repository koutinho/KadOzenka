using System.Collections.Generic;
using Core.Register;
using ModelingBusiness.Objects.Entities;
using ObjectModel.Modeling;

namespace ModelingBusiness.Objects.Import
{
	public interface IModelObjectsImporter
	{
		RegisterObject CreateObject(long? objectId);

		CoefficientForObject GetCoefficient(List<CoefficientForObject> coefficients, long attributeId);
	}
}