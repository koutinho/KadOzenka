using System;
using System.Collections.Generic;
using System.Linq;
using CommonSdks;
using Core.Register;
using Core.Register.QuerySubsystem;
using Core.Register.RegisterEntities;
using Core.Shared.Extensions;
using KadOzenka.Dal.GbuObject;
using KadOzenka.Dal.ManagementDecisionSupport.Dto.ReportingFormFormation;
using ObjectModel.Directory;
using ObjectModel.KO;

namespace KadOzenka.Dal.ManagementDecisionSupport
{
	public class ReportingFormFormationService
	{
		private readonly GbuObjectService _gbuObjectService;
		private readonly QueryManager _queryManager;

		public ReportingFormFormationService(GbuObjectService gbuObjectService, QueryManager queryManager)
		{
			_gbuObjectService = gbuObjectService;
			_queryManager = queryManager;

		}

		public List<UnitDto> GetNewlyRegisteredObjects(DateTime? taskCreationDateFrom, DateTime? taskCreationDateTo)
		{
			var query = MakeQsQuery(taskCreationDateFrom, taskCreationDateTo, KoUnitStatus.New);
			query
				.Select(x => x.CadastralNumber)
				.Select(x => x.ParentTask.CreationDate)
				.Select(x => x.StatusRepeatCalc_Code)
				.Select(x => x.Square)
				.Select(x => x.PropertyType_Code)
				.Select(x => x.Status_Code);
		

			var objects = _queryManager.ExecuteQuery(query);
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
				query.GroupBy(x => x.PropertyType_Code);

			var objectsByPropertyType = _queryManager.ExecuteSelect(query, x => new
			{
				x.PropertyType_Code,
				ObjectCount = QSExtensions.Count<OMUnit>(y => 1)
			})?.Select(x => new UnitCountByPropertyTypeDto { PropertyType = x.PropertyType_Code, ObjectsCount = x.ObjectCount }).ToList();

			return objectsByPropertyType ?? new List<UnitCountByPropertyTypeDto>();
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
			query
				.Select(x => x.CadastralNumber)
				.Select(x => x.ParentTask.CreationDate)
				.Select(x => x.StatusRepeatCalc_Code)
				.Select(x => x.Square)
				.Select(x => x.PropertyType_Code)
				.Select(x => x.Status_Code);

			var objects = _queryManager.ExecuteQuery(query);

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

			var objectsByPropertyType = _queryManager.ExecuteSelect(query.GroupBy(x => x.PropertyType_Code),
				x => new
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

		public List<UnitWithChangedFactorsDto> GetChangedObjects(DateTime? taskCreationDateFrom, DateTime? taskCreationDateTo)
		{
			var query = new QSQuery
			{
				MainRegisterID = OMUnit.GetRegisterId(),
				Joins = new List<QSJoin>
				{
					new QSJoin
					{
						RegisterId = OMTask.GetRegisterId(),
						JoinCondition = new QSConditionSimple
						{
							ConditionType = QSConditionType.Equal,
							LeftOperand = OMUnit.GetColumn(x => x.TaskId),
							RightOperand = OMTask.GetColumn(x => x.Id)
						},
						JoinType = QSJoinType.Inner
					},
					new QSJoin
					{
						RegisterId = OMUnitChange.GetRegisterId(),
						JoinCondition = new QSConditionSimple
						{
							ConditionType = QSConditionType.Equal,
							LeftOperand = OMUnit.GetColumn(x => x.Id),
							RightOperand = OMUnitChange.GetColumn(x => x.UnitId)
						},
						JoinType = QSJoinType.Inner
					}
				}
			};

			if (taskCreationDateFrom.HasValue || taskCreationDateTo.HasValue)
			{
				var condition = new QSConditionGroup(QSConditionGroupType.And);
				if (taskCreationDateFrom.HasValue)
				{
					condition.Conditions.Add(new QSConditionSimple(OMTask.GetColumn(x => x.CreationDate), QSConditionType.GreaterOrEqual,
						taskCreationDateFrom.Value.Date));
				}

				if (taskCreationDateTo.HasValue)
				{
					condition.Conditions.Add(new QSConditionSimple(OMTask.GetColumn(x => x.CreationDate), QSConditionType.LessOrEqual,
						taskCreationDateTo.Value.GetEndOfTheDay()));
				}

				query.Condition = condition;
			}

			query.AddColumn(OMUnit.GetColumn(x => x.Id, nameof(UnitWithChangedFactorsDto.Id)));
			query.AddColumn(OMUnit.GetColumn(x => x.CadastralNumber, nameof(UnitWithChangedFactorsDto.CadastralNumber)));
			query.AddColumn(OMTask.GetColumn(x => x.CreationDate, nameof(UnitWithChangedFactorsDto.CreationDate)));
			query.AddColumn(OMUnit.GetColumn(x => x.StatusRepeatCalc_Code, nameof(UnitWithChangedFactorsDto.StatusRepeatCalc)));
			query.AddColumn(OMUnit.GetColumn(x => x.Square, nameof(UnitWithChangedFactorsDto.Square)));
			query.AddColumn(OMUnit.GetColumn(x => x.PropertyType_Code, nameof(UnitWithChangedFactorsDto.PropertyType)));
			query.AddColumn(OMUnit.GetColumn(x => x.Status_Code, nameof(UnitWithChangedFactorsDto.Status)));
			query.AddColumn(OMUnitChange.GetColumn(x => x.ChangeStatus, nameof(UnitWithChangedFactorsDto.ChangedFactor)));

			//var table = query.ExecuteQuery();
			var table = _queryManager.ExecuteQueryToDataTable(query);
			var data = new List<UnitWithChangedFactorsDto>();
			if (table.Rows.Count != 0)
			{
				for (var i = 0; i < table.Rows.Count; i++)
				{
					var dto = new UnitWithChangedFactorsDto
					{
						Id = table.Rows[i][nameof(UnitWithChangedFactorsDto.Id)].ParseToLong(),
						CadastralNumber = table.Rows[i][nameof(UnitWithChangedFactorsDto.CadastralNumber)].ParseToString(),
						CreationDate = table.Rows[i][nameof(UnitWithChangedFactorsDto.CreationDate)].ParseToDateTimeNullable(),
						StatusRepeatCalc = (KoStatusRepeatCalc)table.Rows[i][nameof(UnitWithChangedFactorsDto.StatusRepeatCalc)].ParseToLong(),
						Square = table.Rows[i][nameof(UnitWithChangedFactorsDto.Square)].ParseToDecimalNullable(),
						PropertyType = (PropertyTypes)table.Rows[i][nameof(UnitWithChangedFactorsDto.PropertyType)].ParseToLong(),
						Status = (KoUnitStatus)table.Rows[i][nameof(UnitWithChangedFactorsDto.Status)].ParseToLong(),
						ChangedFactor = table.Rows[i][nameof(UnitWithChangedFactorsDto.ChangedFactor)].ParseToString()
					};
					data.Add(dto);
				}
			}

			var result = data.GroupBy(x => x.Id).Select(
				group => new UnitWithChangedFactorsDto
				{
					Id = group.Key,
					CadastralNumber = group.ToList().FirstOrDefault()?.CadastralNumber,
					CreationDate = group.ToList().FirstOrDefault()?.CreationDate,
					StatusRepeatCalc = group.ToList().Count > 0 ? group.ToList().First().StatusRepeatCalc : KoStatusRepeatCalc.None,
					Square = group.ToList().FirstOrDefault()?.Square,
					PropertyType = group.ToList().Count > 0 ?  group.ToList().First().PropertyType : PropertyTypes.None,
					Status = group.ToList().Count > 0 ? group.ToList().First().Status : KoUnitStatus.None,
					ChangedFactors =  group.ToList().Select(x => x.ChangedFactor).Distinct().ToList()
				}).ToList();

			return result;
		}

		public List<UnitCountByPropertyTypeDto> GetChangedObjectsByPropertyType(DateTime? taskCreationDateFrom, DateTime? taskCreationDateTo)
		{
			var query = MakeQsQuery(taskCreationDateFrom, taskCreationDateTo);

			var subQuery = new QSQuery(OMUnitChange.GetRegisterId())
			{
				Columns = new List<QSColumn>
					{
						new QSColumnConstant(1)
					},
				Condition = new QSConditionGroup(QSConditionGroupType.And)
				{
					Conditions = new List<QSCondition>
						{
							new QSConditionSimple(
								OMUnitChange.GetColumn(x => x.UnitId),
								QSConditionType.Equal,
								OMUnit.GetColumn(x => x.Id)){
								RightOperandLevel = 1
							}
						}
				}
			};

			var factorsCondition = new QSConditionSimple(new QSColumnQuery(subQuery, "factors"), QSConditionType.Exists);
			query = query.And(factorsCondition);
			query
				.Select(x => x.PropertyType)
				.Select(x => x.PropertyType_Code);

			var objects = _queryManager.ExecuteQuery(query);

			return objects
				.GroupBy(x => x.PropertyType_Code)
				.Select(g => new UnitCountByPropertyTypeDto { PropertyType = g.Key, ObjectsCount = g.ToList().Count }).ToList();
		}

		private QSQuery<OMUnit> MakeQsQuery(DateTime? taskCreationDateFrom, DateTime? taskCreationDateTo, KoUnitStatus? status = null)
		{
			var query = OMUnit.Where(x => true);
			if (status.HasValue)
			{
				query.And(x => x.Status_Code == status);
			}

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
			if (!_queryManager.IsRequestCancellationToken())
			{
				var values = _gbuObjectService.GetAttributeValueKoObjectsCount(attribute.Id, status, taskCreationDateFrom, taskCreationDateTo, _queryManager)
					.Select(x => new UnitCountByTypeOfUseDto
						{ TypeOfUse = GetAttribureValueString(x.AttributeValue, attribute), ObjectsCount = x.ObjectsCount }).ToList();

				var query = MakeQsQuery(taskCreationDateFrom, taskCreationDateTo, status);
				var objectsCount = _queryManager.ExecuteCount(query);

				if (values.Sum(x => x.ObjectsCount) < objectsCount)
				{
					values.Add(new UnitCountByTypeOfUseDto { TypeOfUse = "Значение отсутствует", ObjectsCount = objectsCount - values.Sum(x => x.ObjectsCount) });
				}

				return values;
			}
			
			return new List<UnitCountByTypeOfUseDto>();
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
