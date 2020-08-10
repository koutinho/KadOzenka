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
		private readonly List<PropertyTypes> _propertyTypes;
		private readonly List<UpksCalcType> _upksCalcTypes;

		public SubjectsUPKSService(StatisticalDataService statisticalDataService, GbuObjectService gbuObjectService)
		{
			_statisticalDataService = statisticalDataService;
			_gbuObjectService = gbuObjectService;
			_propertyTypes = System.Enum.GetValues(typeof(PropertyTypes)).Cast<PropertyTypes>().ToList();
			_propertyTypes.Remove(PropertyTypes.None);
			_upksCalcTypes = System.Enum.GetValues(typeof(UpksCalcType)).Cast<UpksCalcType>().ToList();
		}

		public List<SubjectsUPKSByTypeDto> GetSubjectsUPKSByTypeData(long[] taskIdList)
		{
			var query = _statisticalDataService.GetQueryForUnitsByTasks(taskIdList);

			query.AddColumn(OMUnit.GetColumn(x => x.PropertyType, "PropertyType"));
			query.AddColumn(OMUnit.GetColumn(x => x.Upks, "ObjectUpks"));
            query.AddColumn(OMUnit.GetColumn(x => x.CadastralCost, "ObjectCost"));
            query.AddColumn(OMUnit.GetColumn(x => x.Square, "ObjectSquare"));

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
                        ObjectCost = table.Rows[i]["ObjectCost"].ParseToDecimalNullable(),
                        ObjectSquare = table.Rows[i]["ObjectSquare"].ParseToDecimalNullable(),
					};

					data.Add(dto);
				}
			}

			var dataGrouped = data.GroupBy(x => x.PropertyType)
				.ToDictionary(g => g.Key, g => g.ToList());
			var result = new List<SubjectsUPKSByTypeDto>();
			foreach (var propertyType in _propertyTypes.OrderBy(x => x).ToList())
			{
				if (dataGrouped.ContainsKey(propertyType.GetEnumDescription()))
				{
					var groupValues = dataGrouped[propertyType.GetEnumDescription()];
					foreach (var upksCalcType in _upksCalcTypes)
					{
						var dto = new SubjectsUPKSByTypeDto
						{
							ObjectsCount = groupValues.Count,
							PropertyType = propertyType.GetEnumDescription(),
							UpksCalcType = upksCalcType,
							UpksCalcValue = _statisticalDataService.GetCalcValue(upksCalcType, groupValues)
						};

						result.Add(dto);
					}
				}
				else
				{
					foreach (var upksCalcType in _upksCalcTypes)
					{
						var dto = new SubjectsUPKSByTypeDto
						{
							ObjectsCount = 0,
							PropertyType = propertyType.GetEnumDescription(),
							UpksCalcType = upksCalcType,
							UpksCalcValue = null
						};

						result.Add(dto);
					}
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
            query.AddColumn(OMUnit.GetColumn(x => x.CadastralCost, "ObjectCost"));
            query.AddColumn(OMUnit.GetColumn(x => x.Square, "ObjectSquare"));

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
                        ObjectCost = table.Rows[i]["ObjectCost"].ParseToDecimalNullable(),
                        ObjectSquare = table.Rows[i]["ObjectSquare"].ParseToDecimalNullable(),
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
				foreach (var upksCalcType in _upksCalcTypes)
				{
					var dto = new SubjectsUPKSByTypeAndPurposeDto
					{
						ObjectsCount = groupValues.Count,
						PropertyType = @group.Key.PropertyTypeCode.GetEnumDescription(),
						PropertyTypeCode = @group.Key.PropertyTypeCode,
						Purpose = @group.Key.Purpose,
						HasPurpose = @group.Key.HasPurpose,
						UpksCalcType = upksCalcType,
						UpksCalcValue = _statisticalDataService.GetCalcValue(upksCalcType, groupValues)
					};

					result.Add(dto);
				}
			}

			foreach (var propertyType in _propertyTypes)
			{
				if (result.All(x => x.PropertyTypeCode != propertyType))
				{
					foreach (var upksCalcType in _upksCalcTypes)
					{
						var dto = new SubjectsUPKSByTypeAndPurposeDto
						{
							ObjectsCount = 0,
							PropertyType = propertyType.GetEnumDescription(),
							PropertyTypeCode = propertyType,
							UpksCalcType = upksCalcType,
							UpksCalcValue = null
						};

						result.Add(dto);
					}
				}
			}

			return result.OrderBy(x => x.PropertyTypeCode).ToList();
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
