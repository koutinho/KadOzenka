using System;
using System.Collections.Generic;
using System.Linq;
using Core.Register;
using ObjectModel.Modeling;

namespace KadOzenka.Dal.Modeling.Objects.Import
{
	public class ModelObjectsImporterForCreation : IModelObjectsImporter
	{
		private readonly long _modelId;

		public ModelObjectsImporterForCreation(long? modelId)
		{
			if (modelId == null)
				throw new Exception("Не передан ИД модели для создания объектов моделирования");

			_modelId = modelId.Value;
		}

		public RegisterObject CreateRegisterObject(long? objectId)
		{
			var registerObject = new RegisterObject(OMModelToMarketObjects.GetRegisterId(), -1);
			registerObject.SetAttributeValue(OMModelToMarketObjects.GetColumnAttributeId(x => x.ModelId), _modelId);
			registerObject.SetAttributeValue(OMModelToMarketObjects.GetColumnAttributeId(x => x.Coefficients), string.Empty);

			return registerObject;
		}

		public CoefficientForObject GetCoefficient(List<CoefficientForObject> coefficientsFromDb, long attributeId)
		{
			var coefficientFromDb = coefficientsFromDb.FirstOrDefault(с => с.AttributeId == attributeId);
			if (coefficientFromDb == null)
			{
				coefficientFromDb = new CoefficientForObject(attributeId);
				coefficientsFromDb.Add(coefficientFromDb);
			}

			return coefficientFromDb;
		}

		public bool IsValidateObject(RegisterObject objectFromDb, bool isForControl, bool isForTraining)
		{
			return isForTraining && isForControl;
		}
	}
}