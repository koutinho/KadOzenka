using System;
using System.Collections.Generic;
using System.Linq;
using Core.Register;
using Core.Register.QuerySubsystem;
using Core.Register.RegisterEntities;
using Core.Shared.Extensions;
using KadOzenka.Dal.GbuObject;
using KadOzenka.Dal.ManagementDecisionSupport.Dto.StatisticalData;
using ObjectModel.Directory;
using ObjectModel.Gbu;
using ObjectModel.KO;
using ObjectModel.Market;

namespace KadOzenka.Dal.ManagementDecisionSupport.StatisticalData
{
	public class AdditionalFormsService
	{
		private readonly StatisticalDataService _statisticalDataService;
		private readonly GbuObjectService _gbuObjectService;

		public string MoscowOktmo => "45000000";

		public AdditionalFormsService(StatisticalDataService statisticalDataService, GbuObjectService gbuObjectService)
		{
			_statisticalDataService = statisticalDataService;
			_gbuObjectService = gbuObjectService;
		}

		public List<ChangesUploadingDto> GetChangesUploadingData(long[] taskIdList)
		{
			var unitChangeJoin = new QSJoin
			{
				RegisterId = OMUnitChange.GetRegisterId(),
				JoinCondition = new QSConditionSimple
				{
					ConditionType = QSConditionType.Equal,
					LeftOperand = OMUnit.GetColumn(x => x.Id),
					RightOperand = OMUnitChange.GetColumn(x => x.UnitId)
				},
				JoinType = QSJoinType.Inner
			};
			var taskJoin = new QSJoin
			{
				RegisterId = OMUnitChange.GetRegisterId(),
				JoinCondition = new QSConditionSimple
				{
					ConditionType = QSConditionType.Equal,
					LeftOperand = OMUnit.GetColumn(x => x.TaskId),
					RightOperand = OMTask.GetColumn(x => x.Id)
				},
				JoinType = QSJoinType.Inner
			};

			var query = _statisticalDataService.GetQueryForUnitsByTasks(taskIdList, additionalJoins: new List<QSJoin> {unitChangeJoin, taskJoin});
			query.AddColumn(OMUnit.GetColumn(x => x.CadastralNumber, "CadastralNumber"));
			query.AddColumn(OMUnit.GetColumn(x => x.PropertyType, "PropertyType"));
			query.AddColumn(OMUnit.GetColumn(x => x.StatusRepeatCalc, "StatusRepeatCalc"));
			query.AddColumn(OMTask.GetColumn(x => x.CreationDate, "CreationDate"));
			query.AddColumn(OMUnitChange.GetColumn(x => x.OldValue, "OldValue"));
			query.AddColumn(OMUnitChange.GetColumn(x => x.NewValue, "NewValue"));
			query.AddColumn(OMUnitChange.GetColumn(x => x.ChangeStatus, "ChangeStatus"));

			var table = query.ExecuteQuery();
			var result = new List<ChangesUploadingDto>();
			if (table.Rows.Count != 0)
			{
				for (var i = 0; i < table.Rows.Count; i++)
				{
					var dto = new ChangesUploadingDto
					{
						CadastralNumber = table.Rows[i]["CadastralNumber"].ParseToString(),
						ChangedDate = table.Rows[i]["CreationDate"].ParseToDateTimeNullable(),
						PropertyType = table.Rows[i]["PropertyType"].ParseToString(),
						Status = table.Rows[i]["StatusRepeatCalc"].ParseToString(),
						OldValue = table.Rows[i]["OldValue"].ParseToString(),
						NewValue = table.Rows[i]["NewValue"].ParseToString(),
						Changing = table.Rows[i]["ChangeStatus"].ParseToString(),
					};

					result.Add(dto);
				}
			}

			return result;
		}

		public List<CalculationStatisticsDto> GetCalculationStatisticsData(long[] taskIdList)
		{
			var subgroupsId = OMUnit.Where(x => taskIdList.Contains((long)x.TaskId))
				.Select(x => x.GroupId)
				.Execute()
				.Select(x => x.GroupId)
				.Distinct()
				.ToList();

			if (subgroupsId.Count == 0)
			{
				return new List<CalculationStatisticsDto>();
			}

			var subgroups = OMGroup.Where(x => subgroupsId.Contains((long)x.Id))
				.SelectAll()
				.Execute();

			var result = new List<CalculationStatisticsDto>();
			foreach (var subgroup in subgroups)
			{
				var parentGroup = OMGroup.Where(x => x.Id == subgroup.ParentId)
					.Select(x => x.GroupName)
					.ExecuteFirstOrDefault();
				var subgroupName = $"{parentGroup?.GroupName} {subgroup.GroupName}";
				if (subgroup.GroupAlgoritm_Code == KoGroupAlgoritm.Model)
				{
					var model = OMModel.Where(x => x.GroupId == subgroup.Id).SelectAll().ExecuteFirstOrDefault();
					if (model != null)
					{
						var modelFactors = OMModelFactor.Where(x => x.ModelId == model.Id).SelectAll().Execute();
						foreach (var modelFactor in modelFactors)
						{
							var factorName =
								RegisterCache.RegisterAttributes.Values.FirstOrDefault(x => x.Id == modelFactor.FactorId)?.Name;
							var dto = new CalculationStatisticsDto
							{
								SubgroupId = subgroup.Id,
								SubgroupName = subgroupName,
								CalculationMethod = subgroup.GroupAlgoritm_Code.GetEnumDescription(),
								Formula = model.Formula,
								FactorsSubgroups = factorName,
								Coef = modelFactor.Weight,
								SighMarket = modelFactor.SignMarket ? "Да" : null
							};
							result.Add(dto);
						}
					}
				}
				else
				{
					var parentsCalcGroups = ObjectModel.KO.OMCalcGroup.Where(x => x.GroupId == subgroup.Id).SelectAll().Execute();
					foreach (var calcGroup in parentsCalcGroups)
					{
						var calcBaseGroup = OMGroup.Where(x => x.Id == calcGroup.ParentCalcGroupId)
							.SelectAll()
							.ExecuteFirstOrDefault();
						var calcParentGroup = OMGroup.Where(x => x.Id == calcBaseGroup.ParentId)
							.Select(x => x.GroupName)
							.ExecuteFirstOrDefault();
						if (calcBaseGroup != null)
						{
							var dto = new CalculationStatisticsDto
							{
								SubgroupId = subgroup.Id,
								SubgroupName = subgroupName,
								CalculationMethod = subgroup.GroupAlgoritm_Code.GetEnumDescription(),
								FactorsSubgroups = $"{calcParentGroup?.GroupName} {calcBaseGroup.GroupName}"
							};
							result.Add(dto);
						}
					}
				}
			}

			return result;
		}

		public List<MarketDataDto> GetMarketData(DateTime? dateFrom, DateTime? dateTo, long typeOfUseCodeAttributeId, long oksGroupAttributeId, long typeOfUseAttributeId, long typeOfRightAttributeId)
		{
			var gbuAttributesDataDictionary = new Dictionary<string, RegisterAttribute>();
			gbuAttributesDataDictionary.Add(nameof(MarketDataDto.TypeOfUseCode), RegisterCache.GetAttributeData(typeOfUseCodeAttributeId));
			gbuAttributesDataDictionary.Add(nameof(MarketDataDto.OksGroup), RegisterCache.GetAttributeData(oksGroupAttributeId));
			gbuAttributesDataDictionary.Add(nameof(MarketDataDto.TypeOfUse), RegisterCache.GetAttributeData(typeOfUseAttributeId));
			gbuAttributesDataDictionary.Add(nameof(MarketDataDto.TypeOfRight), RegisterCache.GetAttributeData(typeOfRightAttributeId));

			var conditions = new List<QSCondition>();
			if (dateFrom.HasValue)
			{
				var condition = new QSConditionGroup
				{
					Type = QSConditionGroupType.Or,
					Conditions = new List<QSCondition>
					{
						new QSConditionGroup
						{
							Type = QSConditionGroupType.And,
							Conditions = new List<QSCondition>
							{
								new QSConditionSimple(OMCoreObject.GetColumn(x => x.LastDateUpdate), QSConditionType.IsNotNull),
								new QSConditionSimple(OMCoreObject.GetColumn(x => x.LastDateUpdate), QSConditionType.GreaterOrEqual, dateFrom.Value.Date),
							}
						},
						new QSConditionGroup
						{
							Type = QSConditionGroupType.And,
							Conditions = new List<QSCondition>
							{
								new QSConditionSimple(OMCoreObject.GetColumn(x => x.LastDateUpdate), QSConditionType.IsNull),
								new QSConditionSimple(OMCoreObject.GetColumn(x => x.ParserTime), QSConditionType.GreaterOrEqual, dateFrom.Value.Date),
							}
						}
					}
				};
				conditions.Add(condition);
			}
			if (dateTo.HasValue)
			{
				var condition = new QSConditionGroup
				{
					Type = QSConditionGroupType.Or,
					Conditions = new List<QSCondition>
					{
						new QSConditionGroup
						{
							Type = QSConditionGroupType.And,
							Conditions = new List<QSCondition>
							{
								new QSConditionSimple(OMCoreObject.GetColumn(x => x.LastDateUpdate), QSConditionType.IsNotNull),
								new QSConditionSimple(OMCoreObject.GetColumn(x => x.LastDateUpdate), QSConditionType.LessOrEqual, dateTo.Value.GetEndOfTheDay()),
							}
						},
						new QSConditionGroup
						{
							Type = QSConditionGroupType.And,
							Conditions = new List<QSCondition>
							{
								new QSConditionSimple(OMCoreObject.GetColumn(x => x.LastDateUpdate), QSConditionType.IsNull),
								new QSConditionSimple(OMCoreObject.GetColumn(x => x.ParserTime), QSConditionType.LessOrEqual, dateTo.Value.GetEndOfTheDay()),
							}
						}
					}
				};
				conditions.Add(condition);
			}
			conditions.Add(new QSConditionSimple(OMCoreObject.GetColumn(x => x.ProcessType_Code), QSConditionType.Equal,
				(long)ProcessStep.Dealed));
			conditions.Add(new QSConditionSimple(OMMainObject.GetColumn(x => x.IsActive), QSConditionType.NotEqual, new QSColumnConstant(false)));

			var query = new QSQuery
			{
				MainRegisterID = OMCoreObject.GetRegisterId(),
				Condition = new QSConditionGroup
				{
					Type = QSConditionGroupType.And,
					Conditions = conditions
				},
				Joins = new List<QSJoin>
				{
					new QSJoin
					{
						RegisterId = OMMainObject.GetRegisterId(),
						JoinCondition = new QSConditionSimple
						{
							ConditionType = QSConditionType.Equal,
							LeftOperand = OMCoreObject.GetColumn(x => x.CadastralNumber),
							RightOperand = OMMainObject.GetColumn(x => x.CadastralNumber)
						},
						JoinType = QSJoinType.Left
					}
				}
			};
			query.AddColumn(OMCoreObject.GetColumn(x => x.Id, "Id"));
			query.AddColumn(OMCoreObject.GetColumn(x => x.MarketId, "MarketId"));
			query.AddColumn(OMMainObject.GetColumn(x => x.Id, "GbuObjectId"));
			query.AddColumn(OMCoreObject.GetColumn(x => x.CadastralNumber, "CadastralNumber"));
			query.AddColumn(OMCoreObject.GetColumn(x => x.CadastralQuartal, "CadastralQuartal"));
			query.AddColumn(OMCoreObject.GetColumn(x => x.Group, "SegmentGroup"));
			query.AddColumn(OMCoreObject.GetColumn(x => x.Address, "AddressReferencePoint"));
			query.AddColumn(OMCoreObject.GetColumn(x => x.Metro, "Metro"));
			query.AddColumn(OMCoreObject.GetColumn(x => x.Market_Code, "MarketCode"));
			query.AddColumn(OMCoreObject.GetColumn(x => x.Url, "Link"));
			query.AddColumn(OMCoreObject.GetColumn(x => x.PhoneNumber, "Phone"));
			query.AddColumn(OMCoreObject.GetColumn(x => x.LastDateUpdate, "LastDateUpdate"));
			query.AddColumn(OMCoreObject.GetColumn(x => x.ParserTime, "ParserTime"));
			query.AddColumn(OMCoreObject.GetColumn(x => x.Description, "AdText"));
			query.AddColumn(OMCoreObject.GetColumn(x => x.PropertyTypesCIPJS_Code, "PropertyTypesCIPJSCode"));
			query.AddColumn(OMCoreObject.GetColumn(x => x.RoomsCount, "RoomCount"));
			query.AddColumn(OMCoreObject.GetColumn(x => x.DealType_Code, "DealTypeCode"));
			query.AddColumn(OMCoreObject.GetColumn(x => x.Area, "Area"));
			query.AddColumn(OMCoreObject.GetColumn(x => x.AreaLand, "AreaLand"));
			query.AddColumn(OMCoreObject.GetColumn(x => x.Price, "Price"));


			var table = query.ExecuteQuery();
			var result = new List<MarketDataDto>();
			if (table.Rows.Count != 0)
			{
				var objectIds = new List<long>();
				for (var i = 0; i < table.Rows.Count; i++)
				{
					objectIds.Add(table.Rows[i]["GbuObjectId"].ParseToLong());
				}
				var gbuAttributes = _gbuObjectService.GetAllAttributes(objectIds,
					gbuAttributesDataDictionary.Values.Select(x => (long)x.RegisterId).Distinct().ToList(),
					gbuAttributesDataDictionary.Values.Select(x => x.Id).Distinct().ToList(),
					DateTime.Now.GetEndOfTheDay());

				var addedObjects = new List<long>();
				for (var i = 0; i < table.Rows.Count; i++)
				{
					var id = table.Rows[i]["Id"].ParseToLong();
					if (!addedObjects.Contains(id))
					{
						var lastDateUpdate = table.Rows[i]["LastDateUpdate"].ParseToDateTimeNullable();
						var parserTime = table.Rows[i]["ParserTime"].ParseToDateTimeNullable();
						var dealType = (DealType) table.Rows[i]["DealTypeCode"].ParseToLong();
						var propertyTypesCIPJS =
							(PropertyTypesCIPJS) table.Rows[i]["PropertyTypesCIPJSCode"].ParseToLong();
						var square = propertyTypesCIPJS == PropertyTypesCIPJS.LandArea
							? table.Rows[i]["AreaLand"].ParseToDecimalNullable() * 100
							: table.Rows[i]["Area"].ParseToDecimalNullable();
						var price = table.Rows[i]["Price"].ParseToDecimalNullable();
						var marketId = table.Rows[i]["MarketId"].ParseToLongNullable();

						var dto = new MarketDataDto
						{
							UniqueNumber = marketId.HasValue ? marketId.Value : id,
							Kn = table.Rows[i]["CadastralNumber"].ParseToStringNullable(),
							SegmentGroup = table.Rows[i]["SegmentGroup"].ParseToStringNullable(),
							SubjectCode =
								GetSubjectCodeByCadastralQuarter(table.Rows[i]["CadastralQuartal"]
									.ParseToStringNullable()),
							OKTMO = MoscowOktmo,
							AddressReferencePoint = table.Rows[i]["AddressReferencePoint"].ParseToStringNullable(),
							Metro = table.Rows[i]["Metro"].ParseToStringNullable(),
							Market = (MarketTypes) table.Rows[i]["MarketCode"].ParseToLong() != MarketTypes.None
								? ((MarketTypes) table.Rows[i]["MarketCode"].ParseToLong()).GetEnumDescription()
								: null,
							Link = table.Rows[i]["Link"].ParseToStringNullable(),
							Phone = table.Rows[i]["Phone"].ParseToStringNullable(),
							Date = lastDateUpdate ?? parserTime,
							AdText = table.Rows[i]["AdText"].ParseToStringNullable(),
							TypeOfProperty = propertyTypesCIPJS != PropertyTypesCIPJS.None
								? propertyTypesCIPJS.GetEnumDescription()
								: null,
							RoomCount = table.Rows[i]["RoomCount"].ParseToLongNullable(),
							DealSuggestion = dealType != DealType.None
								? dealType.GetEnumDescription()
								: null,
							Square = square,
							Price = price,
							Upks = (dealType == DealType.SaleDeal || dealType == DealType.SaleSuggestion)
								? GetUpks(price, square)
								: null,
							AnnualRateOfRent = dealType == DealType.RentDeal || dealType == DealType.RentSuggestion
								? GetAnnualRateOfRent(price, square)
								: null,
						};

						foreach (var attribute in gbuAttributes.Where(x =>
							x.ObjectId == table.Rows[i]["GbuObjectId"].ParseToLong()))
						{
							var attributeKeys = gbuAttributesDataDictionary.Where(x => x.Value.Id == attribute.AttributeId)
								.Select(x => x.Key);
							foreach (var key in attributeKeys)
							{
								typeof(MarketDataDto).GetProperty(key)
									.SetValue(dto, attribute.GetValueInString());
							}
						}

						result.Add(dto);
						addedObjects.Add(id);
					}
				}
			}

			return result;
		}

		public List<ResultsAnalysisDto> GetResultsAnalysisData(long[] taskIdList)
		{
			var conditions = new List<QSCondition>
			{
				new QSConditionSimple(OMUnit.GetColumn(x => x.TaskId), QSConditionType.In, taskIdList.Select(x => (double) x).ToList()),
			};
			var data = GetResultsAnalysisObjects(conditions);

			var objectsWithTheSameKn = new List<ResultsAnalysisObjectDto>();
			var cadastralNumbers = data.Select(x => x.CadastralNumber).ToList();
			if (cadastralNumbers.Count > 0)
			{
				var taskTourId = data.FirstOrDefault()?.TaskTourId;
				var conditionsForPreviousObjects = new List<QSCondition>
				{
					new QSConditionSimple(OMUnit.GetColumn(x => x.CadastralNumber), QSConditionType.In, cadastralNumbers),
					new QSConditionSimple(OMTask.GetColumn(x => x.TourId), QSConditionType.NotEqual, taskTourId.GetValueOrDefault()),
					new QSConditionSimple(OMTask.GetColumn(x => x.NoteType_Code), QSConditionType.Equal,
						(long) KoNoteType.Initial),
				};
				objectsWithTheSameKn = GetResultsAnalysisObjects(conditionsForPreviousObjects);
			}

			var objectsWithTheSameKnByCadastralNumber = objectsWithTheSameKn.GroupBy(x => x.CadastralNumber)
				.ToDictionary(g => g.Key, g => g.ToList());

			var result = new List<ResultsAnalysisDto>();
			foreach (var dto in data)
			{
				var resultDto = new ResultsAnalysisDto
				{
					CadastralNumber = dto.CadastralNumber,
					PropertyType = dto.PropertyType,
					Square = dto.Square,
					Upks = dto.Upks,
					CadastralCost = dto.CadastralCost,
					Status = dto.Status
				};

				if (objectsWithTheSameKnByCadastralNumber.ContainsKey(dto.CadastralNumber))
				{
					var prevObject = objectsWithTheSameKnByCadastralNumber[dto.CadastralNumber]
						.Where(x => x.TaskTourId != dto.TaskTourId && x.TaskId != dto.TaskId && x.TaskCreationDate <= dto.TaskCreationDate )
						.OrderByDescending(x => x.TaskCreationDate).FirstOrDefault();
					if (prevObject != null)
					{
						resultDto.PreviousUpks = prevObject.Upks;
						resultDto.PreviousCadastralCost = prevObject.CadastralCost;
					}
				}
				
				result.Add(resultDto);
			}

			return result;
		}

		private List<ResultsAnalysisObjectDto> GetResultsAnalysisObjects(List<QSCondition> qsConditions)
		{
			var query = new QSQuery
			{
				MainRegisterID = OMUnit.GetRegisterId(),
				Condition = new QSConditionGroup
				{
					Type = QSConditionGroupType.And,
					Conditions = qsConditions
				},
				Joins = new List<QSJoin>
				{
					new QSJoin
					{
						RegisterId = OMUnitChange.GetRegisterId(),
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

			query.AddColumn(OMUnit.GetColumn(x => x.CadastralNumber, "CadastralNumber"));
			query.AddColumn(OMTask.GetColumn(x => x.TourId, "TaskTourId"));
			query.AddColumn(OMUnit.GetColumn(x => x.TaskId, "TaskId"));
			query.AddColumn(OMTask.GetColumn(x => x.CreationDate, "TaskCreationDate"));
			query.AddColumn(OMUnit.GetColumn(x => x.PropertyType, "PropertyType"));
			query.AddColumn(OMUnit.GetColumn(x => x.Square, "Square"));
			query.AddColumn(OMUnit.GetColumn(x => x.StatusRepeatCalc, "StatusRepeatCalc"));
			query.AddColumn(OMUnit.GetColumn(x => x.CadastralCost, "CadastralCost"));
			query.AddColumn(OMUnit.GetColumn(x => x.Upks, "Upks"));

			var table = query.ExecuteQuery();
			var data = new List<ResultsAnalysisObjectDto>();
			if (table.Rows.Count != 0)
			{
				for (var i = 0; i < table.Rows.Count; i++)
				{
					var dto = new ResultsAnalysisObjectDto
					{
						CadastralNumber = table.Rows[i]["CadastralNumber"].ParseToString(),
						TaskTourId = table.Rows[i]["TaskTourId"].ParseToLongNullable(),
						TaskId = table.Rows[i]["TaskId"].ParseToLongNullable(),
						TaskCreationDate = table.Rows[i]["TaskCreationDate"].ParseToDateTimeNullable(),
						PropertyType = table.Rows[i]["PropertyType"].ParseToString(),
						Square = table.Rows[i]["Square"].ParseToDecimalNullable(),
						Status = table.Rows[i]["StatusRepeatCalc"].ParseToString(),
						CadastralCost = table.Rows[i]["CadastralCost"].ParseToDecimalNullable(),
						Upks = table.Rows[i]["Upks"].ParseToDecimalNullable(),
					};

					data.Add(dto);
				}
			}

			return data;
		}

		private decimal? GetAnnualRateOfRent(decimal? price, decimal? square)
		{
			return square.HasValue ? (price / square) * 12 : null;
		}

		private decimal? GetUpks(decimal? price, decimal? square)
		{
			return square.HasValue ? price / square : null;
		}

		public string GetSubjectCodeByCadastralQuarter(string cadastralQuartal)
		{
			if (string.IsNullOrEmpty(cadastralQuartal))
			{
				return null;
			}
			var delimeterIndex = cadastralQuartal.IndexOf(':');
			return cadastralQuartal.Substring(0, delimeterIndex);
		}
	}
}
