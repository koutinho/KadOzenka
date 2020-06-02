using System;
using System.Collections.Generic;
using System.Linq;
using Core.Register;
using Core.Register.QuerySubsystem;
using Core.Shared.Extensions;
using KadOzenka.Dal.GbuObject;
using KadOzenka.Dal.ManagementDecisionSupport.Dto.StatisticalData;
using ObjectModel.Core.Register;
using ObjectModel.Directory;
using ObjectModel.Gbu;
using ObjectModel.KO;

namespace KadOzenka.Dal.ManagementDecisionSupport.StatisticalData
{
	public class KRSummaryResultsService
	{
		private readonly GbuObjectService _gbuObjectService;

		public KRSummaryResultsService(GbuObjectService gbuObjectService)
		{
			_gbuObjectService = gbuObjectService;
		}

		public List<KRSummaryResultsOksDto> GetKRSummaryResultsOksData(long[] taskIdList, long klardAttributeId, long parentKnAttributeId)
		{
			var klardAttribute = RegisterCache.GetAttributeData(klardAttributeId);
			var parentKnAttribute = RegisterCache.GetAttributeData(parentKnAttributeId);

			var omMainObjectRegisterId = OMMainObject.GetRegisterId();
			var rosreestrRegister = OMRegister
				.Where(x => x.MainRegister == omMainObjectRegisterId &&
							x.RegisterDescription == "Источник: Росреестр").ExecuteFirstOrDefault();
			var nameAttr = RegisterCache.RegisterAttributes.Values
				.First(x => x.RegisterId == rosreestrRegister.RegisterId
							&& x.Name.Equals(
								"Наименование объекта"));
			var purposeAttr = RegisterCache.RegisterAttributes.Values
				.First(x => x.RegisterId == rosreestrRegister.RegisterId
							&& x.Name.Equals(
								"Назначение сооружения"));
			var addressAttr = RegisterCache.RegisterAttributes.Values
				.First(x => x.RegisterId == rosreestrRegister.RegisterId
							&& x.Name.Equals(
								"Адрес"));
			var locationAttr = RegisterCache.RegisterAttributes.Values
				.First(x => x.RegisterId == rosreestrRegister.RegisterId
							&& x.Name.Equals(
								"Местоположение"));
			var zuCadastralNumberAttr = RegisterCache.RegisterAttributes.Values
				.First(x => x.RegisterId == rosreestrRegister.RegisterId
							&& x.Name.Equals(
								"Земельный участок"));
			var buildingYearAttr = RegisterCache.RegisterAttributes.Values
				.First(x => x.RegisterId == rosreestrRegister.RegisterId
				            && x.Name.Equals(
					            "Год постройки"));
			var commissioningYearAttr = RegisterCache.RegisterAttributes.Values
				.First(x => x.RegisterId == rosreestrRegister.RegisterId
				            && x.Name.Equals(
								"Год ввода в эксплуатацию"));
			var floorCountAttr = RegisterCache.RegisterAttributes.Values
				.First(x => x.RegisterId == rosreestrRegister.RegisterId
				            && x.Name.Equals(
								"Количество этажей"));
			var undergroundFloorCountAttr = RegisterCache.RegisterAttributes.Values
				.First(x => x.RegisterId == rosreestrRegister.RegisterId
				            && x.Name.Equals(
								"Количество подземных этажей"));
			var floorNumberAttr = RegisterCache.RegisterAttributes.Values
				.First(x => x.RegisterId == rosreestrRegister.RegisterId
				            && x.Name.Equals(
								"Этаж"));
			var wallMaterialAttr = RegisterCache.RegisterAttributes.Values
				.First(x => x.RegisterId == rosreestrRegister.RegisterId
				            && x.Name.Equals(
								"Материал стен"));

			var query = new QSQuery
			{
				MainRegisterID = OMUnit.GetRegisterId(),
				Condition = new QSConditionGroup
				{
					Type = QSConditionGroupType.And,
					Conditions = new List<QSCondition>
					{
						new QSConditionSimple(OMTask.GetColumn(x => x.Id), QSConditionType.In, taskIdList.Select(x => (double)x).ToList()),
						new QSConditionSimple(OMUnit.GetColumn(x => x.PropertyType_Code), QSConditionType.NotEqual, (long)PropertyTypes.Stead),
						new QSConditionSimple(OMUnit.GetColumn(x => x.PropertyType_Code), QSConditionType.NotEqual, (long)PropertyTypes.None)
					}
				},
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
					}
				}
			};

			query.AddColumn(OMUnit.GetColumn(x => x.ObjectId, "ObjectId"));
			query.AddColumn(OMUnit.GetColumn(x => x.CadastralNumber, "CadastralNumber"));
			query.AddColumn(OMUnit.GetColumn(x => x.CadastralBlock, "CadastralQuartal"));
			query.AddColumn(OMUnit.GetColumn(x => x.PropertyType, "PropertyType"));
			query.AddColumn(OMUnit.GetColumn(x => x.Square, "Square"));
			query.AddColumn(OMUnit.GetColumn(x => x.DegreeReadiness, "DegreeReadiness"));
			query.AddColumn(OMUnit.GetColumn(x => x.Upks, "Upks"));
			query.AddColumn(OMUnit.GetColumn(x => x.CadastralCost, "CadastralCost"));

			var table = query.ExecuteQuery();

			var result = new List<KRSummaryResultsOksDto>();
			if (table.Rows.Count != 0)
			{
				var objectIds = new List<long>();
				for (var i = 0; i < table.Rows.Count; i++)
				{
					objectIds.Add(table.Rows[i]["ObjectId"].ParseToLong());
				}
				var attributes = _gbuObjectService.GetAllAttributes(objectIds,
					new List<long> { rosreestrRegister.RegisterId, klardAttribute.RegisterId, parentKnAttribute.RegisterId },
					new List<long>
					{
						klardAttributeId,
						parentKnAttributeId,
						nameAttr.Id,
						purposeAttr.Id,
						addressAttr.Id,
						locationAttr.Id,
						zuCadastralNumberAttr.Id,
						buildingYearAttr.Id,
						commissioningYearAttr.Id,
						floorCountAttr.Id,
						undergroundFloorCountAttr.Id,
						floorNumberAttr.Id,
						wallMaterialAttr.Id

					}, DateTime.Now.GetEndOfTheDay());

				for (var i = 0; i < table.Rows.Count; i++)
				{
					var dto = new KRSummaryResultsOksDto
					{
						CadastralNumber = table.Rows[i]["CadastralNumber"].ParseToString(),
						CadastralQuarter = table.Rows[i]["CadastralQuartal"].ParseToString(),
						PropertyType = table.Rows[i]["PropertyType"].ParseToString(),
						Square = table.Rows[i]["Square"].ParseToDecimalNullable(),
						AvailabilityPercentage = table.Rows[i]["DegreeReadiness"].ParseToLongNullable(),
						Upks = table.Rows[i]["Upks"].ParseToDecimalNullable(),
						CadastralCost = table.Rows[i]["CadastralCost"].ParseToDecimalNullable(),
					};

					foreach (var attribute in attributes.Where(x => x.ObjectId == table.Rows[i]["ObjectId"].ParseToLong()))
					{
						if (attribute.AttributeId == klardAttributeId)
						{
							dto.Kladr = attribute.GetValueInString();
						}
						else if (attribute.AttributeId == parentKnAttributeId)
						{
							dto.ParentKn = attribute.GetValueInString();
						}
						else
						{
							switch (attribute.AttributeData.Name)
							{
								case "Наименование объекта":
									dto.Name = attribute.GetValueInString();
									break;
								case "Назначение сооружения":
									dto.Purpose = attribute.GetValueInString();
									break;
								case "Адрес":
									dto.Address = attribute.GetValueInString();
									break;
								case "Местоположение":
									dto.Location = attribute.GetValueInString();
									break;
								case "Земельный участок":
									dto.ZuCadastralNumber = attribute.GetValueInString();
									break;
								case "Год постройки":
									dto.BuildingYear = attribute.GetValueInString();
									break;
								case "Год ввода в эксплуатацию":
									dto.CommissioningYear = attribute.GetValueInString();
									break;
								case "Количество этажей":
									dto.FloorCount = attribute.GetValueInString();
									break;
								case "Количество подземных этажей":
									dto.UndergroundFloorCount = attribute.GetValueInString();
									break;
								case "Этаж":
									dto.FloorNumber = attribute.GetValueInString();
									break;
								case "Материал стен":
									dto.WallMaterial = attribute.GetValueInString();
									break;
							}
						}
					}

					result.Add(dto);
				}
			}

			return result;
		}

		public List<KRSummaryResultsZuDto> GetKRSummaryResultsZuData(long[] taskIdList, long klardAttributeId)
		{
			var klardAttribute = RegisterCache.GetAttributeData(klardAttributeId);

			var omMainObjectRegisterId = OMMainObject.GetRegisterId();
			var rosreestrRegister = OMRegister
				.Where(x => x.MainRegister == omMainObjectRegisterId &&
				            x.RegisterDescription == "Источник: Росреестр").ExecuteFirstOrDefault();
			var nameAttr = RegisterCache.RegisterAttributes.Values
				.First(x => x.RegisterId == rosreestrRegister.RegisterId
				            && x.Name.Equals(
					            "Наименование земельного участка"));
			var permittedUsingAttr = RegisterCache.RegisterAttributes.Values
				.First(x => x.RegisterId == rosreestrRegister.RegisterId
				            && x.Name.Equals(
								"Вид использования по документам"));
			var addressAttr = RegisterCache.RegisterAttributes.Values
				.First(x => x.RegisterId == rosreestrRegister.RegisterId
				            && x.Name.Equals(
								"Адрес"));
			var locationAttr = RegisterCache.RegisterAttributes.Values
				.First(x => x.RegisterId == rosreestrRegister.RegisterId
				            && x.Name.Equals(
								"Местоположение"));
			var landCategoryAttr = RegisterCache.RegisterAttributes.Values
				.First(x => x.RegisterId == rosreestrRegister.RegisterId
				            && x.Name.Equals(
								"Категория земель"));

			var query = new QSQuery
			{
				MainRegisterID = OMUnit.GetRegisterId(),
				Condition = new QSConditionGroup
				{
					Type = QSConditionGroupType.And,
					Conditions = new List<QSCondition>
					{
						new QSConditionSimple(OMTask.GetColumn(x => x.Id), QSConditionType.In, taskIdList.Select(x => (double)x).ToList()),
						new QSConditionSimple(OMUnit.GetColumn(x => x.PropertyType_Code), QSConditionType.Equal, (long)PropertyTypes.Stead)
					}
				},
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
					}
				}
			};

			query.AddColumn(OMUnit.GetColumn(x => x.ObjectId, "ObjectId"));
			query.AddColumn(OMUnit.GetColumn(x => x.CadastralNumber, "CadastralNumber"));
			query.AddColumn(OMUnit.GetColumn(x => x.CadastralBlock, "CadastralQuartal"));
			query.AddColumn(OMUnit.GetColumn(x => x.PropertyType, "PropertyType"));
			query.AddColumn(OMUnit.GetColumn(x => x.Square, "Square"));
			query.AddColumn(OMUnit.GetColumn(x => x.Upks, "Upks"));
			query.AddColumn(OMUnit.GetColumn(x => x.CadastralCost, "CadastralCost"));

			var table = query.ExecuteQuery();

			var result = new List<KRSummaryResultsZuDto>();
			if (table.Rows.Count != 0)
			{
				var objectIds = new List<long>();
				for (var i = 0; i < table.Rows.Count; i++)
				{
					objectIds.Add(table.Rows[i]["ObjectId"].ParseToLong());
				}
				var attributes = _gbuObjectService.GetAllAttributes(objectIds,
					new List<long> { rosreestrRegister.RegisterId, klardAttribute.RegisterId },
					new List<long>
					{
						klardAttribute.Id,
						nameAttr.Id,
						permittedUsingAttr.Id,
						addressAttr.Id,
						locationAttr.Id,
						landCategoryAttr.Id

					}, DateTime.Now.GetEndOfTheDay());

				for (var i = 0; i < table.Rows.Count; i++)
				{
					var dto = new KRSummaryResultsZuDto
					{
						CadastralNumber = table.Rows[i]["CadastralNumber"].ParseToString(),
						CadastralQuarter = table.Rows[i]["CadastralQuartal"].ParseToString(),
						PropertyType = table.Rows[i]["PropertyType"].ParseToString(),
						Square = table.Rows[i]["Square"].ParseToDecimalNullable(),
						Upks = table.Rows[i]["Upks"].ParseToDecimalNullable(),
						CadastralCost = table.Rows[i]["CadastralCost"].ParseToDecimalNullable(),
					};

					foreach (var attribute in attributes.Where(x => x.ObjectId == table.Rows[i]["ObjectId"].ParseToLong()))
					{
						if (attribute.AttributeId == klardAttribute.Id)
						{
							dto.Kladr = attribute.GetValueInString();
						}
						else
						{
							switch (attribute.AttributeData.Name)
							{
								case "Наименование земельного участка":
									dto.Name = attribute.GetValueInString();
									break;
								case "Вид использования по документам":
									dto.PermittedUsing = attribute.GetValueInString();
									break;
								case "Адрес":
									dto.Address = attribute.GetValueInString();
									break;
								case "Местоположение":
									dto.Location = attribute.GetValueInString();
									break;
								case "Категория земель":
									dto.LandCategory = attribute.GetValueInString();
									break;
							}
						}
					}

					result.Add(dto);
				}
			}

			return result;
		}
	}
}
