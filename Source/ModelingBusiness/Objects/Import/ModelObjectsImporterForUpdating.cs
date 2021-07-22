using System;
using System.Collections.Generic;
using System.Linq;
using Core.Register;
using ModelingBusiness.Objects.Entities;
using ObjectModel.Modeling;
using Serilog;

namespace ModelingBusiness.Objects.Import
{
	public class ModelObjectsImporterForUpdating : IModelObjectsImporter
	{
		private readonly long _isForTrainingAttributeId;
		private readonly long _isForControlAttributeId;
		private readonly long _coefficientsAttributeId;
		private readonly List<OMModelToMarketObjects> _objectsFromDb;

		
		public ModelObjectsImporterForUpdating(List<ModelObjectsFromExcelData> objectsFromExcel,
			long isForTrainingAttributeId, long isForControlAttributeId, long coefficientsAttributeId,
			ILogger logger)
		{
			var modelObjectsIds = objectsFromExcel.Select(x => x.Id).ToList();
			if (modelObjectsIds.Count == 0)
				throw new Exception("В файле нет ИД объектов");

			_objectsFromDb = OMModelToMarketObjects.Where(x => modelObjectsIds.Contains(x.Id))
				.Select(x => new
				{
					x.IsForTraining,
					x.IsForControl,
					x.Coefficients
				}).Execute();
			logger.Debug("Найдено {ModelObjectsCount} объектов моделирования в БД", _objectsFromDb.Count);

			_isForTrainingAttributeId = isForTrainingAttributeId;
			_isForControlAttributeId = isForControlAttributeId;
			_coefficientsAttributeId = coefficientsAttributeId;
		}


		public RegisterObject CreateObject(long? objectId)
		{
			if (objectId == null)
				throw new Exception("Не передан ИД объекта моделирования");

			var objectFromDb = _objectsFromDb.FirstOrDefault(x => x.Id == objectId);
			if (objectFromDb == null)
				throw new Exception($"Объект с ИД '{objectId}' не найден в БД");

			var registerObject = new RegisterObject(OMModelToMarketObjects.GetRegisterId(), (int)objectId);

			registerObject.SetAttributeValue(_isForTrainingAttributeId, objectFromDb.IsForTraining);
			registerObject.SetAttributeValue(_isForControlAttributeId, objectFromDb.IsForControl);
			registerObject.SetAttributeValue(_coefficientsAttributeId, objectFromDb.Coefficients);

			return registerObject;
		}

		public CoefficientForObject GetCoefficient(List<CoefficientForObject> coefficients, long attributeId)
		{
			var coefficientFromDb = coefficients.FirstOrDefault(с => с.AttributeId == attributeId);
			if (coefficientFromDb == null)
				throw new Exception($"Не найден атрибут '{RegisterCache.GetAttributeData(attributeId).Name}'");

			return coefficientFromDb;
		}
	}
}