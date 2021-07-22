using System.Collections.Generic;
using System.Linq;
using Core.Register;
using ModelingBusiness.Objects.Entities;
using ObjectModel.Modeling;

namespace ModelingBusiness.Objects.Import
{
	public class ModelObjectsImporterForCreation : IModelObjectsImporter
	{
		private readonly long _modelId;
		private readonly long _modelIdAttributeId;
		private readonly long _coefficientAttributeId;

		public ModelObjectsImporterForCreation(long modelId, long modelIdAttributeId, long coefficientAttributeId)
		{
			_modelId = modelId;
			_modelIdAttributeId = modelIdAttributeId;
			_coefficientAttributeId = coefficientAttributeId;
		}

		public RegisterObject CreateObject(long? objectId)
		{
			var registerObject = new RegisterObject(OMModelToMarketObjects.GetRegisterId(), -1);
			
			registerObject.SetAttributeValue(_modelIdAttributeId, _modelId);
			registerObject.SetAttributeValue(_coefficientAttributeId, string.Empty);

			return registerObject;
		}

		public CoefficientForObject GetCoefficient(List<CoefficientForObject> coefficients, long attributeId)
		{
			var coefficientFromDb = coefficients.FirstOrDefault(с => с.AttributeId == attributeId);
			if (coefficientFromDb == null)
			{
				coefficientFromDb = new CoefficientForObject(attributeId);
				coefficients.Add(coefficientFromDb);
			}

			return coefficientFromDb;
		}
	}
}