using System;
using System.Collections.Generic;
using System.Linq;
using Core.Register;
using Core.Shared.Extensions;
using ObjectModel.Modeling;

namespace KadOzenka.Dal.Modeling.Objects.Import
{
	public class ModelObjectsImporterForUpdating : IModelObjectsImporter
	{
		private readonly List<OMModelToMarketObjects> _objectsFromDb;
		
		public ModelObjectsImporterForUpdating(List<ModelObjectsImporter.ModelObjectsFromExcelData> objectsFromExcel)
		{
			var modelObjectsIds = objectsFromExcel.Select(x => x.Id).ToList();
			if (modelObjectsIds.Count == 0)
				throw new Exception("В файле не было найдено ИД объектов");

			_objectsFromDb = OMModelToMarketObjects.Where(x => modelObjectsIds.Contains(x.Id)).SelectAll().Execute();
			//_log.Debug("{LoggerBasePhrase} найдено {ModelObjectsCount} объектов в БД", LoggerBasePhrase, _objectsFromDb.Count);
		}

		public RegisterObject CreateRegisterObject(long? objectId)
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

		public CoefficientForObject GetCoefficient(List<CoefficientForObject> coefficientsFromDb, long attributeId)
		{
			var coefficientFromDb = coefficientsFromDb.FirstOrDefault(с => с.AttributeId == attributeId);
			if (coefficientFromDb == null)
				throw new Exception($"Не найден атрибут '{RegisterCache.GetAttributeData(attributeId).Name}'");

			return coefficientFromDb;
		}

		public bool IsValidateObject(RegisterObject objectFromDb, bool isForControl, bool isForTraining)
		{
			var isForTrainingInObject = objectFromDb.AttributesValues[OMModelToMarketObjects.GetColumnAttributeId(x => x.IsForTraining)].Value?.ParseToBooleanNullable();
			var isForControlInObject = objectFromDb.AttributesValues[OMModelToMarketObjects.GetColumnAttributeId(x => x.IsForControl)].Value?.ParseToBooleanNullable();
			
			return isForControl && isForTrainingInObject.GetValueOrDefault() ||
			       (isForTraining && isForControlInObject.GetValueOrDefault()) ||
			       (isForTraining && isForControl);
		}
	}
}