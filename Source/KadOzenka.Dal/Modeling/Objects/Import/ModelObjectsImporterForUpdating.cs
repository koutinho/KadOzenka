using System;
using System.Collections.Generic;
using System.Linq;
using Core.Register;
using KadOzenka.Dal.Modeling.Objects.Import.Entities;
using ObjectModel.Modeling;
using Serilog;

namespace KadOzenka.Dal.Modeling.Objects.Import
{
	public class ModelObjectsImporterForUpdating : IModelObjectsImporter
	{
		private readonly List<OMModelToMarketObjects> _objectsFromDb;

		
		public ModelObjectsImporterForUpdating(List<ModelObjectsFromExcelData> objectsFromExcel,
			ILogger logger)
		{
			var modelObjectsIds = objectsFromExcel.Select(x => x.Id).ToList();
			if (modelObjectsIds.Count == 0)
				throw new Exception("В файле нет ИД объектов");

			_objectsFromDb = OMModelToMarketObjects.Where(x => modelObjectsIds.Contains(x.Id)).SelectAll().Execute();
			logger.Debug("Найдено {ModelObjectsCount} объектов моделирования в БД", _objectsFromDb.Count);
		}


		public RegisterObject CreateObject(long? objectId)
		{
			if (objectId == null)
				throw new Exception("Не передан ИД объекта моделирования");

			var objectFromDb = _objectsFromDb.FirstOrDefault(x => x.Id == objectId);
			if (objectFromDb == null)
				throw new Exception($"Объект с ИД '{objectId}' не найден в БД");

			var registerObject = new RegisterObject(OMModelToMarketObjects.GetRegisterId(), (int)objectId);
			
			registerObject.SetAttributeValue(OMModelToMarketObjects.GetColumnAttributeId(x => x.IsForControl), objectFromDb.IsForControl);
			registerObject.SetAttributeValue(OMModelToMarketObjects.GetColumnAttributeId(x => x.IsForTraining), objectFromDb.IsForTraining);
			registerObject.SetAttributeValue(OMModelToMarketObjects.GetColumnAttributeId(x => x.Coefficients), objectFromDb.Coefficients);

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