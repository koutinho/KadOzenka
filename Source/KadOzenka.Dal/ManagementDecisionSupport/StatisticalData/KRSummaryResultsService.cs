using System;
using System.Collections.Generic;
using System.Linq;
using Core.Register;
using Core.Register.QuerySubsystem;
using Core.Register.RegisterEntities;
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
			var omMainObjectRegisterId = OMMainObject.GetRegisterId();
			var rosreestrRegister = OMRegister
				.Where(x => x.MainRegister == omMainObjectRegisterId &&
				            x.RegisterDescription == "Источник: Росреестр").ExecuteFirstOrDefault();

			var gbuAttributesDataDictionary = new Dictionary<string, RegisterAttribute>();
			gbuAttributesDataDictionary.Add(nameof(KRSummaryResultsOksDto.Kladr), RegisterCache.GetAttributeData(klardAttributeId));
			gbuAttributesDataDictionary.Add(nameof(KRSummaryResultsOksDto.ParentKn), RegisterCache.GetAttributeData(parentKnAttributeId));
			gbuAttributesDataDictionary.Add(nameof(KRSummaryResultsOksDto.Name),
				RegisterCache.RegisterAttributes.Values.First(x => x.RegisterId == rosreestrRegister.RegisterId && x.Name.Equals("Наименование объекта")));
			gbuAttributesDataDictionary.Add(nameof(KRSummaryResultsOksDto.Purpose),
				RegisterCache.RegisterAttributes.Values.First(x => x.RegisterId == rosreestrRegister.RegisterId && x.Name.Equals("Назначение сооружения")));
			gbuAttributesDataDictionary.Add(nameof(KRSummaryResultsOksDto.Address),
				RegisterCache.RegisterAttributes.Values.First(x => x.RegisterId == rosreestrRegister.RegisterId && x.Name.Equals("Адрес")));
			gbuAttributesDataDictionary.Add(nameof(KRSummaryResultsOksDto.Location),
				RegisterCache.RegisterAttributes.Values.First(x => x.RegisterId == rosreestrRegister.RegisterId && x.Name.Equals("Местоположение")));
			gbuAttributesDataDictionary.Add(nameof(KRSummaryResultsOksDto.ZuCadastralNumber),
				RegisterCache.RegisterAttributes.Values.First(x => x.RegisterId == rosreestrRegister.RegisterId && x.Name.Equals("Земельный участок")));
			gbuAttributesDataDictionary.Add(nameof(KRSummaryResultsOksDto.BuildingYear),
				RegisterCache.RegisterAttributes.Values.First(x => x.RegisterId == rosreestrRegister.RegisterId && x.Name.Equals("Год постройки")));
			gbuAttributesDataDictionary.Add(nameof(KRSummaryResultsOksDto.CommissioningYear),
				RegisterCache.RegisterAttributes.Values.First(x => x.RegisterId == rosreestrRegister.RegisterId && x.Name.Equals("Год ввода в эксплуатацию")));
			gbuAttributesDataDictionary.Add(nameof(KRSummaryResultsOksDto.FloorCount),
				RegisterCache.RegisterAttributes.Values.First(x => x.RegisterId == rosreestrRegister.RegisterId && x.Name.Equals("Количество этажей")));
			gbuAttributesDataDictionary.Add(nameof(KRSummaryResultsOksDto.UndergroundFloorCount),
				RegisterCache.RegisterAttributes.Values.First(x => x.RegisterId == rosreestrRegister.RegisterId && x.Name.Equals("Количество подземных этажей")));
			gbuAttributesDataDictionary.Add(nameof(KRSummaryResultsOksDto.FloorNumber),
				RegisterCache.RegisterAttributes.Values.First(x => x.RegisterId == rosreestrRegister.RegisterId && x.Name.Equals("Этаж")));
			gbuAttributesDataDictionary.Add(nameof(KRSummaryResultsOksDto.WallMaterial),
				RegisterCache.RegisterAttributes.Values.First(x => x.RegisterId == rosreestrRegister.RegisterId && x.Name.Equals("Материал стен")));

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
				var gbuAttributes = _gbuObjectService.GetAllAttributes(objectIds,
					gbuAttributesDataDictionary.Values.Select(x => (long)x.RegisterId).Distinct().ToList(),
					gbuAttributesDataDictionary.Values.Select(x => x.Id).Distinct().ToList(),
					DateTime.Now.GetEndOfTheDay());

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

					foreach (var attribute in gbuAttributes.Where(x => x.ObjectId == table.Rows[i]["ObjectId"].ParseToLong()))
					{
						var attributeKeys = gbuAttributesDataDictionary.Where(x => x.Value.Id == attribute.AttributeId)
							.Select(x => x.Key);
						foreach (var key in attributeKeys)
						{
							typeof(KRSummaryResultsOksDto).GetProperty(key)
								.SetValue(dto, attribute.GetValueInString());
						}
					}

					result.Add(dto);
				}
			}

			return result;
		}

		public List<KRSummaryResultsZuDto> GetKRSummaryResultsZuData(long[] taskIdList, long klardAttributeId)
		{
			var omMainObjectRegisterId = OMMainObject.GetRegisterId();
			var rosreestrRegister = OMRegister
				.Where(x => x.MainRegister == omMainObjectRegisterId &&
				            x.RegisterDescription == "Источник: Росреестр").ExecuteFirstOrDefault();

			var gbuAttributesDataDictionary = new Dictionary<string, RegisterAttribute>();
			gbuAttributesDataDictionary.Add(nameof(KRSummaryResultsZuDto.Kladr), RegisterCache.GetAttributeData(klardAttributeId));
			gbuAttributesDataDictionary.Add(nameof(KRSummaryResultsZuDto.PermittedUsing),
				RegisterCache.RegisterAttributes.Values.First(x => x.RegisterId == rosreestrRegister.RegisterId && x.Name.Equals("Вид использования по документам")));
			gbuAttributesDataDictionary.Add(nameof(KRSummaryResultsZuDto.Address),
				RegisterCache.RegisterAttributes.Values.First(x => x.RegisterId == rosreestrRegister.RegisterId && x.Name.Equals("Адрес")));
			gbuAttributesDataDictionary.Add(nameof(KRSummaryResultsZuDto.Location),
				RegisterCache.RegisterAttributes.Values.First(x => x.RegisterId == rosreestrRegister.RegisterId && x.Name.Equals("Местоположение")));
			gbuAttributesDataDictionary.Add(nameof(KRSummaryResultsZuDto.LandCategory),
				RegisterCache.RegisterAttributes.Values.First(x => x.RegisterId == rosreestrRegister.RegisterId && x.Name.Equals("Категория земель")));

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
				var gbuAttributes = _gbuObjectService.GetAllAttributes(objectIds,
					gbuAttributesDataDictionary.Values.Select(x => (long)x.RegisterId).Distinct().ToList(),
					gbuAttributesDataDictionary.Values.Select(x => x.Id).Distinct().ToList(),
					DateTime.Now.GetEndOfTheDay());

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

					foreach (var attribute in gbuAttributes.Where(x => x.ObjectId == table.Rows[i]["ObjectId"].ParseToLong()))
					{
						var attributeKeys = gbuAttributesDataDictionary.Where(x => x.Value.Id == attribute.AttributeId)
							.Select(x => x.Key);
						foreach (var key in attributeKeys)
						{
							typeof(KRSummaryResultsZuDto).GetProperty(key)
								.SetValue(dto, attribute.GetValueInString());
						}
					}

					result.Add(dto);
				}
			}

			return result;
		}
	}
}
