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
using ObjectModel.KO;

namespace KadOzenka.Dal.ManagementDecisionSupport.StatisticalData
{
	public class KRSummaryResultsService
	{
		private readonly GbuObjectService _gbuObjectService;
		private readonly StatisticalDataService _statisticalDataService;

		public KRSummaryResultsService(GbuObjectService gbuObjectService, StatisticalDataService statisticalDataService)
		{
			_gbuObjectService = gbuObjectService;
            _statisticalDataService = statisticalDataService;
        }

		public List<KRSummaryResultsOksDto> GetKRSummaryResultsOksData(long[] taskIdList, long klardAttributeId, long parentKnAttributeId)
		{
            var gbuAttributesDataDictionary = new Dictionary<string, RegisterAttribute>();
			gbuAttributesDataDictionary.Add(nameof(KRSummaryResultsOksDto.Kladr), RegisterCache.GetAttributeData(klardAttributeId));
			gbuAttributesDataDictionary.Add(nameof(KRSummaryResultsOksDto.ParentKn), RegisterCache.GetAttributeData(parentKnAttributeId));
			gbuAttributesDataDictionary.Add(nameof(KRSummaryResultsOksDto.Name), _statisticalDataService.GetRosreestrObjectNameAttribute());
			gbuAttributesDataDictionary.Add(nameof(KRSummaryResultsOksDto.Purpose), _statisticalDataService.GetRosreestrConstructionPurposeAttribute());
			gbuAttributesDataDictionary.Add(nameof(KRSummaryResultsOksDto.Address), _statisticalDataService.GetRosreestrAddressAttribute());
			gbuAttributesDataDictionary.Add(nameof(KRSummaryResultsOksDto.Location), _statisticalDataService.GetRosreestrLocationAttribute());
			gbuAttributesDataDictionary.Add(nameof(KRSummaryResultsOksDto.ZuCadastralNumber), _statisticalDataService.GetRosreestrParcelAttribute());
			gbuAttributesDataDictionary.Add(nameof(KRSummaryResultsOksDto.BuildingYear), _statisticalDataService.GetRosreestrBuildYearAttribute());
			gbuAttributesDataDictionary.Add(nameof(KRSummaryResultsOksDto.CommissioningYear), _statisticalDataService.GetRosreestrCommissioningYearAttribute());
			gbuAttributesDataDictionary.Add(nameof(KRSummaryResultsOksDto.FloorCount), _statisticalDataService.GetRosreestrFloorsNumberAttribute());
			gbuAttributesDataDictionary.Add(nameof(KRSummaryResultsOksDto.UndergroundFloorCount), _statisticalDataService.GetRosreestrUndergroundFloorsNumberAttribute());
			gbuAttributesDataDictionary.Add(nameof(KRSummaryResultsOksDto.FloorNumber), _statisticalDataService.GetRosreestrFloorAttribute());
			gbuAttributesDataDictionary.Add(nameof(KRSummaryResultsOksDto.WallMaterial), _statisticalDataService.GetRosreestrWallMaterialAttribute());

            var notSteadCondition = new QSConditionSimple(OMUnit.GetColumn(x => x.PropertyType_Code), QSConditionType.NotEqual, (long) PropertyTypes.Stead);
            var notNoneCondition = new QSConditionSimple(OMUnit.GetColumn(x => x.PropertyType_Code), QSConditionType.NotEqual, (long)PropertyTypes.None);
            var query = _statisticalDataService.GetQueryForUnitsByTasks(taskIdList, new List<QSCondition> { notSteadCondition, notNoneCondition});
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
            var gbuAttributesDataDictionary = new Dictionary<string, RegisterAttribute>();
			gbuAttributesDataDictionary.Add(nameof(KRSummaryResultsZuDto.Kladr), RegisterCache.GetAttributeData(klardAttributeId));
			gbuAttributesDataDictionary.Add(nameof(KRSummaryResultsZuDto.PermittedUsing), _statisticalDataService.GetRosreestrTypeOfUseByDocumentsAttribute());
			gbuAttributesDataDictionary.Add(nameof(KRSummaryResultsZuDto.Address), _statisticalDataService.GetRosreestrAddressAttribute());
			gbuAttributesDataDictionary.Add(nameof(KRSummaryResultsZuDto.Location), _statisticalDataService.GetRosreestrLocationAttribute());
			gbuAttributesDataDictionary.Add(nameof(KRSummaryResultsZuDto.LandCategory), _statisticalDataService.GetRosreestrParcelCategoryAttribute());

			var steadCondition = new QSConditionSimple(OMUnit.GetColumn(x => x.PropertyType_Code), QSConditionType.Equal, (long)PropertyTypes.Stead);
			var query = _statisticalDataService.GetQueryForUnitsByTasks(taskIdList, new List<QSCondition> {steadCondition});
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
