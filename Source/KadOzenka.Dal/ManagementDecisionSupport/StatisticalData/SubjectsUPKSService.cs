using System;
using System.Collections.Generic;
using System.Linq;
using Core.Register.RegisterEntities;
using Core.Shared.Extensions;
using KadOzenka.Dal.GbuObject;
using KadOzenka.Dal.ManagementDecisionSupport.Dto.StatisticalData;
using KadOzenka.Dal.ManagementDecisionSupport.Enums;
using ObjectModel.Directory;
using ObjectModel.KO;

namespace KadOzenka.Dal.ManagementDecisionSupport.StatisticalData
{
	public class SubjectsUPKSService
	{
		private readonly StatisticalDataService _statisticalDataService;
		private readonly GbuObjectService _gbuObjectService;

		public SubjectsUPKSService(StatisticalDataService statisticalDataService, GbuObjectService gbuObjectService)
		{
			_statisticalDataService = statisticalDataService;
			_gbuObjectService = gbuObjectService;
		}

		public List<SubjectsUPKSByTypeDto> GetSubjectsUPKSByTypeData(long[] taskIdList)
		{
			var query = _statisticalDataService.GetQueryForUnitsByTasks(taskIdList);

			query.AddColumn(OMUnit.GetColumn(x => x.PropertyType, "PropertyType"));
			query.AddColumn(OMUnit.GetColumn(x => x.Upks, "ObjectUpks"));

			var table = query.ExecuteQuery();
			var data = new List<SubjectsUPKSByTypeObjectDto>();
			if (table.Rows.Count != 0)
			{
				for (var i = 0; i < table.Rows.Count; i++)
				{
					var dto = new SubjectsUPKSByTypeObjectDto
					{
						PropertyType = table.Rows[i]["PropertyType"].ParseToString(),
						ObjectValue = table.Rows[i]["ObjectUpks"].ParseToDecimalNullable(),
						//TODO: ObjectWeigth MUST BE CLARIFIED
						ObjectWeigth = 1
					};

					data.Add(dto);
				}
			}

			var dataGrouped =
				data.GroupBy(x => x.PropertyType);

			var result = new List<SubjectsUPKSByTypeDto>();
			foreach (var @group in dataGrouped)
			{
				var groupValues = @group.ToList();
				var upksCalcTypes = System.Enum.GetValues(typeof(UpksCalcType)).Cast<UpksCalcType>();
				foreach (var upksCalcType in upksCalcTypes)
				{
					var dto = new SubjectsUPKSByTypeDto
					{
						ObjectsCount = groupValues.Count,
						PropertyType = @group.Key,
						UpksCalcType = upksCalcType,
						UpksCalcValue = _statisticalDataService.GetCalcValue(upksCalcType, groupValues)
					};

					result.Add(dto);
				}
			}

			return result;
		}

		public List<SubjectsUPKSByTypeAndPurposeDto> GetSubjectsUPKSByTypeAndPurposeData(long[] taskIdList)
		{
			var buildingPurposeAttr = _statisticalDataService.GetRosreestrBuildingPurposeAttribute();
			var placementPurposeAttr = _statisticalDataService.GetRosreestrPlacementPurposeAttribute();

			var query = _statisticalDataService.GetQueryForUnitsByTasks(taskIdList);

			query.AddColumn(OMUnit.GetColumn(x => x.ObjectId, "ObjectId"));
			query.AddColumn(OMUnit.GetColumn(x => x.PropertyType_Code, "PropertyType"));
			query.AddColumn(OMUnit.GetColumn(x => x.Upks, "ObjectUpks"));

			var table = query.ExecuteQuery();
			var data = new List<SubjectsUPKSByTypeAndPurposeObjectDto>();
			if (table.Rows.Count != 0)
			{
				var objectIds = new List<long>();
				for (var i = 0; i < table.Rows.Count; i++)
				{
					objectIds.Add(table.Rows[i]["ObjectId"].ParseToLong());
				}

				var gbuAttributes = _gbuObjectService.GetAllAttributes(objectIds,
					new List<long> { buildingPurposeAttr.RegisterId, placementPurposeAttr.RegisterId },
					new List<long> { buildingPurposeAttr.Id, placementPurposeAttr.Id },
					DateTime.Now.GetEndOfTheDay());

				for (var i = 0; i < table.Rows.Count; i++)
				{
					var dto = new SubjectsUPKSByTypeAndPurposeObjectDto
					{
						PropertyTypeCode = (PropertyTypes)table.Rows[i]["PropertyType"].ParseToLong(),
						GbuObjectId = table.Rows[i]["ObjectId"].ParseToLongNullable(),
						ObjectValue = table.Rows[i]["ObjectUpks"].ParseToDecimalNullable(),
						//TODO: ObjectWeigth MUST BE CLARIFIED
						ObjectWeigth = 1
					};

					if (dto.PropertyTypeCode == PropertyTypes.Building)
					{
						dto.HasPurpose = true;
						FillPurposeData(dto, gbuAttributes, buildingPurposeAttr,  data);
					} 
					else if (dto.PropertyTypeCode == PropertyTypes.Pllacement)
					{
						dto.HasPurpose = true;
						FillPurposeData(dto, gbuAttributes, placementPurposeAttr, data);
					}
					else
					{
						dto.HasPurpose = false;
						data.Add(dto);
					}
				}
			}

			var dataGrouped =
				data.GroupBy(x => new {x.PropertyTypeCode, x.Purpose, x.HasPurpose });

			var result = new List<SubjectsUPKSByTypeAndPurposeDto>();
			foreach (var @group in dataGrouped)
			{
				var groupValues = @group.ToList();
				var upksCalcTypes = System.Enum.GetValues(typeof(UpksCalcType)).Cast<UpksCalcType>();
				foreach (var upksCalcType in upksCalcTypes)
				{
					var dto = new SubjectsUPKSByTypeAndPurposeDto
					{
						ObjectsCount = groupValues.Count,
						PropertyType = @group.Key.PropertyTypeCode.GetEnumDescription(),
						Purpose = @group.Key.Purpose,
						HasPurpose = @group.Key.HasPurpose,
						UpksCalcType = upksCalcType,
						UpksCalcValue = _statisticalDataService.GetCalcValue(upksCalcType, groupValues)
					};

					result.Add(dto);
				}
			}

			return result;
		}

		private void FillPurposeData(SubjectsUPKSByTypeAndPurposeObjectDto dto, List<GbuObjectAttribute> gbuAttributes, RegisterAttribute purposeAttr, List<SubjectsUPKSByTypeAndPurposeObjectDto> data)
		{
			var purpose = gbuAttributes
				.FirstOrDefault(x => x.ObjectId == dto.GbuObjectId && x.AttributeId == purposeAttr.Id);
			if (purpose != null)
			{
				dto.Purpose = purpose.GetValueInString();
				data.Add(dto);
			}
		}
	}
}
