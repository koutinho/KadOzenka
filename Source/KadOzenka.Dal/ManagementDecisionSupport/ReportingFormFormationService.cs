using System;
using System.Collections.Generic;
using System.Linq;
using Core.Register;
using Core.Register.QuerySubsystem;
using Core.Register.RegisterEntities;
using Core.Shared.Extensions;
using KadOzenka.Dal.GbuObject;
using KadOzenka.Dal.ManagementDecisionSupport.Dto.ReportingFormFormation;
using ObjectModel.Core.Register;
using ObjectModel.Directory;
using ObjectModel.KO;

namespace KadOzenka.Dal.ManagementDecisionSupport
{
	public class ReportingFormFormationService
	{
		private readonly GbuObjectService _gbuObjectService;

		public ReportingFormFormationService(GbuObjectService gbuObjectService)
		{
			_gbuObjectService = gbuObjectService;
		}

		public List<UnitDto> GetNewlyRegisteredObjects(DateTime? taskCreationDateFrom, DateTime? taskCreationDateTo)
		{
			var query = MakeQsQuery(taskCreationDateFrom, taskCreationDateTo, KoUnitStatus.New);
			var objects = query
				.Select(x => x.CadastralNumber)
				.Select(x => x.ParentTask.CreationDate)
				.Select(x => x.StatusRepeatCalc_Code)
				.Select(x => x.Square)
				.Select(x => x.PropertyType_Code)
				.Select(x => x.Status_Code)
				.Execute();

			return objects.Select(x => new UnitDto
			{
				CadastralNumber = x.CadastralNumber,
				CreationDate = x.ParentTask.CreationDate,
				StatusRepeatCalc = x.StatusRepeatCalc_Code,
				Square = x.Square,
				PropertyType = x.PropertyType_Code,
				Status = x.Status_Code
			}).ToList();
		}

		public List<UnitCountByPropertyTypeDto> GetNewlyRegisteredObjectsByPropertyType(DateTime? taskCreationDateFrom, DateTime? taskCreationDateTo)
		{
			var query = MakeQsQuery(taskCreationDateFrom, taskCreationDateTo, KoUnitStatus.New);
			var objectsByPropertyType = query
				.GroupBy(x => x.PropertyType_Code)
				.ExecuteSelect(x => new
				{
					x.PropertyType_Code,
					ObjectCount = QSExtensions.Count<OMUnit>(y => 1)
				}).Select(x => new UnitCountByPropertyTypeDto { PropertyType = x.PropertyType_Code, ObjectsCount = x.ObjectCount }).ToList();

			return objectsByPropertyType;
		}

		public List<UnitCountByTypeOfUseDto> GetNewlyRegisteredObjectsByTypeOfUse(DateTime? taskCreationDateFrom, DateTime? taskCreationDateTo, long typeOfUseAttributeId)
		{
			var attribute = RegisterCache.RegisterAttributes.Values.FirstOrDefault(y => y.Id == typeOfUseAttributeId && y.IsDeleted == false);
			if (attribute == null)
			{
				throw new Exception($"Атрибут с ИД {typeOfUseAttributeId} не существует");
			}
			
			return GetObjectsByTypeOfUseAttributeData(attribute, KoUnitStatus.New, taskCreationDateFrom, taskCreationDateTo);
		}

		public List<UnitDto> GetPreviouslyRegisteredObjects(DateTime? taskCreationDateFrom, DateTime? taskCreationDateTo)
		{
			var query = MakeQsQuery(taskCreationDateFrom, taskCreationDateTo, KoUnitStatus.Recalculated);
			var objects = query
				.Select(x => x.CadastralNumber)
				.Select(x => x.ParentTask.CreationDate)
				.Select(x => x.StatusRepeatCalc_Code)
				.Select(x => x.Square)
				.Select(x => x.PropertyType_Code)
				.Select(x => x.Status_Code)
				.Execute();

			return objects.Select(x => new UnitDto
			{
				CadastralNumber = x.CadastralNumber,
				CreationDate = x.ParentTask.CreationDate,
				StatusRepeatCalc = x.StatusRepeatCalc_Code,
				Square = x.Square,
				PropertyType = x.PropertyType_Code,
				Status = x.Status_Code
			}).ToList();
		}

		public List<UnitCountByPropertyTypeDto> GetPreviouslyRegisteredObjectsByPropertyType(DateTime? taskCreationDateFrom, DateTime? taskCreationDateTo)
		{
			var query = MakeQsQuery(taskCreationDateFrom, taskCreationDateTo, KoUnitStatus.Recalculated);
			var objectsByPropertyType = query
				.GroupBy(x => x.PropertyType_Code)
				.ExecuteSelect(x => new
				{
					x.PropertyType_Code,
					ObjectCount = QSExtensions.Count<OMUnit>(y => 1)
				}).Select(x => new UnitCountByPropertyTypeDto { PropertyType = x.PropertyType_Code, ObjectsCount = x.ObjectCount }).ToList();

			return objectsByPropertyType;
		}

		public List<UnitCountByTypeOfUseDto> GetPreviouslyRegisteredObjectsByTypeOfUse(DateTime? taskCreationDateFrom, DateTime? taskCreationDateTo, long typeOfUseAttributeId)
		{
			var attribute = RegisterCache.RegisterAttributes.Values.FirstOrDefault(y => y.Id == typeOfUseAttributeId && y.IsDeleted == false);
			if (attribute == null)
			{
				throw new Exception($"Атрибут с ИД {typeOfUseAttributeId} не существует");
			}

			return GetObjectsByTypeOfUseAttributeData(attribute, KoUnitStatus.Recalculated, taskCreationDateFrom, taskCreationDateTo);
		}

		private QSQuery<OMUnit> MakeQsQuery(DateTime? taskCreationDateFrom, DateTime? taskCreationDateTo, KoUnitStatus status)
		{
			var query = OMUnit.Where(x => x.Status_Code == status);
			if (taskCreationDateFrom.HasValue)
			{
				var dateFrom = taskCreationDateFrom.Value.Date;
				query.And(x => x.ParentTask.CreationDate >= dateFrom);
			}

			if (taskCreationDateTo.HasValue)
			{
				var dateTo = taskCreationDateTo.Value.GetEndOfTheDay();
				query.And(x => x.ParentTask.CreationDate <= dateTo);
			}

			return query;
		}

		private List<UnitCountByTypeOfUseDto> GetObjectsByTypeOfUseAttributeData(RegisterAttribute attribute, KoUnitStatus status, DateTime? taskCreationDateFrom, DateTime? taskCreationDateTo)
		{
			taskCreationDateFrom = taskCreationDateFrom?.Date;
			taskCreationDateTo = taskCreationDateTo?.GetEndOfTheDay();
			var values = _gbuObjectService.GetAttributeValueKoObjectsCount(attribute.Id, status, taskCreationDateFrom, taskCreationDateTo)
				.Select(x => new UnitCountByTypeOfUseDto
					{ TypeOfUse = GetAttribureValueString(x.AttributeValue, attribute), ObjectsCount = x.ObjectsCount }).ToList();

			var query = MakeQsQuery(taskCreationDateFrom, taskCreationDateTo, status);
			var objectsCount = query.ExecuteCount();

			if (values.Sum(x => x.ObjectsCount) < objectsCount)
			{
				values.Add(new UnitCountByTypeOfUseDto { TypeOfUse = "Значение отсутствует", ObjectsCount = objectsCount - values.Sum(x => x.ObjectsCount) });
			}

			return values;
		}

		private string GetAttribureValueString(object attributeValue, RegisterAttribute attribute)
		{
			switch (attribute.Type)
			{
				case RegisterAttributeType.INTEGER:
				case RegisterAttributeType.DECIMAL:
				case RegisterAttributeType.BOOLEAN:
					return attributeValue.ParseToDecimalNullable()?.ToString();
				case RegisterAttributeType.DATE:
					return attributeValue.ParseToDateTimeNullable()?.ToShortDateString();
				default:
					return attributeValue.ParseToString();
			}

		}
	}
}
