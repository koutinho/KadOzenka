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
	public class QualityPricingFactorsEncodingResultsService : StatisticalDataService
	{
		private readonly GbuObjectService _gbuObjectService;
		private readonly StatisticalDataService _statisticalDataService;

		public QualityPricingFactorsEncodingResultsService(GbuObjectService gbuObjectService, StatisticalDataService statisticalDataService)
		{
			_gbuObjectService = gbuObjectService;
			_statisticalDataService = statisticalDataService;
		}

		public List<QualityPricingFactorsEncodingResultsOksDto> GetDataForOksObjects(long[] taskIdList, long parentKnAttributeId, long typeOfUsingNameAttributeId, long typeOfUsingCodeAttributeId,
			long typeOfUsingCodeSourceAttributeId, long typeOfUsingGroupCodeAttributeId, long functionalGroupNameAttributeId, long segmentAttributeId)
		{
			var gbuAttributesDataDictionary = new Dictionary<string, RegisterAttribute>();
			gbuAttributesDataDictionary.Add(nameof(QualityPricingFactorsEncodingResultsOksDto.ParentKn), RegisterCache.GetAttributeData(parentKnAttributeId));
			gbuAttributesDataDictionary.Add(nameof(QualityPricingFactorsEncodingResultsOksDto.TypeOfUsingName), RegisterCache.GetAttributeData(typeOfUsingNameAttributeId));
			gbuAttributesDataDictionary.Add(nameof(QualityPricingFactorsEncodingResultsOksDto.TypeOfUsingCode), RegisterCache.GetAttributeData(typeOfUsingCodeAttributeId));
			gbuAttributesDataDictionary.Add(nameof(QualityPricingFactorsEncodingResultsOksDto.TypeOfUsingCodeSource), RegisterCache.GetAttributeData(typeOfUsingCodeSourceAttributeId));
			gbuAttributesDataDictionary.Add(nameof(QualityPricingFactorsEncodingResultsOksDto.TypeOfUsingGroupCode), RegisterCache.GetAttributeData(typeOfUsingGroupCodeAttributeId));
			gbuAttributesDataDictionary.Add(nameof(QualityPricingFactorsEncodingResultsOksDto.Segment), RegisterCache.GetAttributeData(segmentAttributeId));
			gbuAttributesDataDictionary.Add(nameof(QualityPricingFactorsEncodingResultsOksDto.FunctionalGroupName), RegisterCache.GetAttributeData(functionalGroupNameAttributeId));
			gbuAttributesDataDictionary.Add(nameof(QualityPricingFactorsEncodingResultsOksDto.Name), _statisticalDataService.GetRosreestrObjectNameAttribute());
			gbuAttributesDataDictionary.Add(nameof(QualityPricingFactorsEncodingResultsOksDto.Purpose), _statisticalDataService.GetRosreestrConstructionPurposeAttribute());
			gbuAttributesDataDictionary.Add(nameof(QualityPricingFactorsEncodingResultsOksDto.Address), _statisticalDataService.GetRosreestrAddressAttribute());
			gbuAttributesDataDictionary.Add(nameof(QualityPricingFactorsEncodingResultsOksDto.Location), _statisticalDataService.GetRosreestrLocationAttribute());
			gbuAttributesDataDictionary.Add(nameof(QualityPricingFactorsEncodingResultsOksDto.ZuCadastralNumber), _statisticalDataService.GetRosreestrParcelAttribute());
			gbuAttributesDataDictionary.Add(nameof(QualityPricingFactorsEncodingResultsOksDto.BuildingYear), _statisticalDataService.GetRosreestrBuildYearAttribute());
			gbuAttributesDataDictionary.Add(nameof(QualityPricingFactorsEncodingResultsOksDto.CommissioningYear), _statisticalDataService.GetRosreestrCommissioningYearAttribute());
			gbuAttributesDataDictionary.Add(nameof(QualityPricingFactorsEncodingResultsOksDto.FloorCount), _statisticalDataService.GetRosreestrFloorsNumberAttribute());
			gbuAttributesDataDictionary.Add(nameof(QualityPricingFactorsEncodingResultsOksDto.UndergroundFloorCount), _statisticalDataService.GetRosreestrUndergroundFloorsNumberAttribute());
			gbuAttributesDataDictionary.Add(nameof(QualityPricingFactorsEncodingResultsOksDto.FloorNumber), _statisticalDataService.GetRosreestrFloorAttribute());
			gbuAttributesDataDictionary.Add(nameof(QualityPricingFactorsEncodingResultsOksDto.WallMaterial), _statisticalDataService.GetRosreestrWallMaterialAttribute());

			var notSteadCondition = new QSConditionSimple(OMUnit.GetColumn(x => x.PropertyType_Code), QSConditionType.NotEqual, (long)PropertyTypes.Stead);
			var notNoneCondition = new QSConditionSimple(OMUnit.GetColumn(x => x.PropertyType_Code), QSConditionType.NotEqual, (long)PropertyTypes.None);
			var query = _statisticalDataService.GetQueryForUnitsByTasks(taskIdList, notSteadCondition, notNoneCondition);

			query.AddColumn(OMUnit.GetColumn(x => x.ObjectId, "ObjectId"));
			query.AddColumn(OMUnit.GetColumn(x => x.CadastralNumber, "CadastralNumber"));
			query.AddColumn(OMUnit.GetColumn(x => x.CadastralBlock, "CadastralQuartal"));
			query.AddColumn(OMUnit.GetColumn(x => x.PropertyType, "PropertyType"));
			query.AddColumn(OMUnit.GetColumn(x => x.Square, "Square"));

			var table = query.ExecuteQuery();

			var result = new List<QualityPricingFactorsEncodingResultsOksDto>();
			if (table.Rows.Count != 0)
			{
				var objectIds = new List<long>();
				for (var i = 0; i < table.Rows.Count; i++)
				{
					objectIds.Add(table.Rows[i]["ObjectId"].ParseToLong());
				}

				var gbuAttributes = _gbuObjectService.GetAllAttributes(objectIds,
					gbuAttributesDataDictionary.Values.Select(x => (long) x.RegisterId).Distinct().ToList(),
					gbuAttributesDataDictionary.Values.Select(x => x.Id).Distinct().ToList(),
					DateTime.Now.GetEndOfTheDay());

				for (var i = 0; i < table.Rows.Count; i++)
				{
					var dto = new QualityPricingFactorsEncodingResultsOksDto
					{
						PropertyType = table.Rows[i]["PropertyType"].ParseToString(),
						CadastralNumber = table.Rows[i]["CadastralNumber"].ParseToString(),
						Square = table.Rows[i]["Square"].ParseToDecimalNullable(),
						CadastralQuarter = table.Rows[i]["CadastralQuartal"].ParseToString(),
					};

					foreach (var attribute in gbuAttributes.Where(x =>
						x.ObjectId == table.Rows[i]["ObjectId"].ParseToLong()))
					{
						var attributeKeys = gbuAttributesDataDictionary.Where(x => x.Value.Id == attribute.AttributeId)
							.Select(x => x.Key);
						foreach (var key in attributeKeys)
						{
							typeof(QualityPricingFactorsEncodingResultsOksDto).GetProperty(key)
								.SetValue(dto, attribute.GetValueInString());
						}
					}

					result.Add(dto);
				}
			}

			return result;
		}

		public List<QualityPricingFactorsEncodingResultsZuDto> GetDataForZuObjects(long[] taskIdList, long linkedObjectsInfoAttributeId, long linkedObjectsInfoSourceAttributeId,
			long segmentAttributeId, long typeOfUsingNameAttributeId, long typeOfUsingCodeAttributeId, long typeOfUsingCodeSourceAttributeId)
		{
			var gbuAttributesDataDictionary = new Dictionary<string, RegisterAttribute>();
			gbuAttributesDataDictionary.Add(nameof(QualityPricingFactorsEncodingResultsZuDto.LinkedObjectsInfo), RegisterCache.GetAttributeData(linkedObjectsInfoAttributeId));
			gbuAttributesDataDictionary.Add(nameof(QualityPricingFactorsEncodingResultsZuDto.LinkedObjectsInfoSource), RegisterCache.GetAttributeData(linkedObjectsInfoSourceAttributeId));
			gbuAttributesDataDictionary.Add(nameof(QualityPricingFactorsEncodingResultsZuDto.Segment), RegisterCache.GetAttributeData(segmentAttributeId));
			gbuAttributesDataDictionary.Add(nameof(QualityPricingFactorsEncodingResultsZuDto.TypeOfUsingName), RegisterCache.GetAttributeData(typeOfUsingNameAttributeId));
			gbuAttributesDataDictionary.Add(nameof(QualityPricingFactorsEncodingResultsZuDto.TypeOfUsingCode), RegisterCache.GetAttributeData(typeOfUsingCodeAttributeId));
			gbuAttributesDataDictionary.Add(nameof(QualityPricingFactorsEncodingResultsZuDto.TypeOfUsingCodeSource), RegisterCache.GetAttributeData(typeOfUsingCodeSourceAttributeId));
			gbuAttributesDataDictionary.Add(nameof(QualityPricingFactorsEncodingResultsZuDto.Name), _statisticalDataService.GetRosreestrParcelNameAttribute());
			gbuAttributesDataDictionary.Add(nameof(QualityPricingFactorsEncodingResultsZuDto.PermittedUsing), _statisticalDataService.GetRosreestrTypeOfUseByDocumentsAttribute());
			gbuAttributesDataDictionary.Add(nameof(QualityPricingFactorsEncodingResultsZuDto.Address), _statisticalDataService.GetRosreestrAddressAttribute());
			gbuAttributesDataDictionary.Add(nameof(QualityPricingFactorsEncodingResultsZuDto.Location), _statisticalDataService.GetRosreestrLocationAttribute());

			var steadCondition = new QSConditionSimple(OMUnit.GetColumn(x => x.PropertyType_Code), QSConditionType.Equal, (long)PropertyTypes.Stead);
			var query = _statisticalDataService.GetQueryForUnitsByTasks(taskIdList, steadCondition);

			query.AddColumn(OMUnit.GetColumn(x => x.ObjectId, "ObjectId"));
			query.AddColumn(OMUnit.GetColumn(x => x.CadastralNumber, "CadastralNumber"));
			query.AddColumn(OMUnit.GetColumn(x => x.CadastralBlock, "CadastralQuartal"));
			query.AddColumn(OMUnit.GetColumn(x => x.PropertyType, "PropertyType"));
			query.AddColumn(OMUnit.GetColumn(x => x.Square, "Square"));

			var table = query.ExecuteQuery();

			var result = new List<QualityPricingFactorsEncodingResultsZuDto>();
			if (table.Rows.Count != 0)
			{
				var objectIds = new List<long>();
				for (var i = 0; i < table.Rows.Count; i++)
				{
					objectIds.Add(table.Rows[i]["ObjectId"].ParseToLong());
				}

				var gbuAttributes = _gbuObjectService.GetAllAttributes(objectIds,
					gbuAttributesDataDictionary.Values.Select(x => (long)x.RegisterId).Distinct().ToList(),
					gbuAttributesDataDictionary.Values.Select(x => (long)x.Id).Distinct().ToList(), 
					DateTime.Now.GetEndOfTheDay());

				for (var i = 0; i < table.Rows.Count; i++)
				{
					var dto = new QualityPricingFactorsEncodingResultsZuDto
					{
						PropertyType = table.Rows[i]["PropertyType"].ParseToString(),
						CadastralNumber = table.Rows[i]["CadastralNumber"].ParseToString(),
						Square = table.Rows[i]["Square"].ParseToDecimalNullable(),
						CadastralQuarter = table.Rows[i]["CadastralQuartal"].ParseToString(),

					};

					foreach (var attribute in gbuAttributes.Where(x => x.ObjectId == table.Rows[i]["ObjectId"].ParseToLong()))
					{
						var attributeKeys = gbuAttributesDataDictionary.Where(x => x.Value.Id == attribute.AttributeId)
							.Select(x => x.Key);
						foreach (var key in attributeKeys)
						{
							typeof(QualityPricingFactorsEncodingResultsZuDto).GetProperty(key)
								.SetValue(dto, attribute.GetValueInString());
						}
					}

					result.Add(dto);
				}
			}

			return result;
		}


		public List<QualityPricingFactorsEncodingResultsGroupingDto> GetGroupingData(long[] taskIdList)
		{
			var query = new QSQuery
			{
				MainRegisterID = OMUnit.GetRegisterId(),
				Condition = new QSConditionGroup
				{
					Type = QSConditionGroupType.And,
					Conditions = new List<QSCondition>
					{
						new QSConditionSimple(OMTask.GetColumn(x => x.Id), QSConditionType.In, taskIdList.Select(x => (double)x).ToList())
					}
				},
				Joins = new List<QSJoin>
				{
					new QSJoin
					{
						RegisterId = OMGroup.GetRegisterId(),
						JoinCondition = new QSConditionSimple
						{
							ConditionType = QSConditionType.Equal,
							LeftOperand = OMUnit.GetColumn(x => x.GroupId),
							RightOperand = OMGroup.GetColumn(x => x.Id)
						},
						JoinType = QSJoinType.Left
					},
					new QSJoin
					{
						RegisterId = OMModel.GetRegisterId(),
						JoinCondition = new QSConditionSimple
						{
							ConditionType = QSConditionType.Equal,
							LeftOperand = OMUnit.GetColumn(x => x.GroupId),
							RightOperand = OMModel.GetColumn(x => x.GroupId)
						},
						JoinType = QSJoinType.Left
					}
				}
			};

			query.AddColumn(OMUnit.GetColumn(x => x.PropertyType, "PropertyType"));
			query.AddColumn(OMUnit.GetColumn(x => x.CadastralNumber, "CadastralNumber"));
			query.AddColumn(OMGroup.GetColumn(x => x.Number, "GroupNumber"));
			query.AddColumn(OMModel.GetColumn(x => x.CalculationMethod, "ModelCalculationMethod"));

			var table = query.ExecuteQuery();

			var result = new List<QualityPricingFactorsEncodingResultsGroupingDto>();
			if (table.Rows.Count != 0)
			{
				for (var i = 0; i < table.Rows.Count; i++)
				{
					var modelCalculationMethod = table.Rows[i]["ModelCalculationMethod"].ParseToLongNullable();
					var dto = new QualityPricingFactorsEncodingResultsGroupingDto
					{
						PropertyType = table.Rows[i]["PropertyType"].ParseToString(),
						CadastralNumber = table.Rows[i]["CadastralNumber"].ParseToString(),
						GroupNumber = table.Rows[i]["GroupNumber"].ParseToStringNullable(),
						ModelCalculationMethod =
							((KoCalculationMethod) modelCalculationMethod.GetValueOrDefault(0)) != KoCalculationMethod.None
								? ((KoCalculationMethod) modelCalculationMethod.GetValueOrDefault(0)).GetEnumDescription()
								: null,
					};
					result.Add(dto);
				}
			}

			return result;
		}
	}
}
