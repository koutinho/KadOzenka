﻿using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Reflection;
using Core.UI.Registers.Reports.Model;
using ObjectModel.Directory;
using Core.Register;
using Core.Register.RegisterEntities;
using Core.Shared.Extensions;
using KadOzenka.Dal.GbuObject;
using ObjectModel.KO;

namespace KadOzenka.Dal.FastReports.StatisticalData.ResultsByCadastralDistrict
{
    //TODO отчет не доделан, т.к. пришлось переключится на другую задачу
    public class PlacementsReport : StatisticalDataReport
    {
        private readonly string _segment = "Segment";
        private readonly string _usageTypeName = "UsageTypeName";
        private readonly string _usageTypeCode = "UsageTypeCode";
        private readonly string _usageTypeCodeSource = "UsageTypeCodeSource";
        private readonly string _subGroupUsageTypeCode = "SubGroupUsageTypeCode";
        private readonly string _functionalSubGroupName = "FunctionalSubGroupName";


        protected override string TemplateName(NameValueCollection query)
        {
            return "ResultsByCadastralDistrictForPlacementsReport";
        }

        protected override DataSet GetData(NameValueCollection query, HashSet<long> objectList = null)
        {
            var taskIds = GetTaskIdList(query)?.ToList();
            var inputParameters = GetInputParameters(query);

            var operations = GetOperations(taskIds, inputParameters);

            var dataSet = new DataSet();
            var itemTable = GetItemDataTable(operations);
            dataSet.Tables.Add(itemTable);

            return dataSet;
        }

        public override void InitializeFilterValues(long objId, string senderName, bool initialisation, List<FilterValue> filterValues)
        {
            if (!initialisation)
                return;

            var segmentFilter = filterValues.FirstOrDefault(f => f.ParamName == _segment);
            var usageTypeNameFilter = filterValues.FirstOrDefault(f => f.ParamName == _usageTypeName);
            var usageTypeCodeFilter = filterValues.FirstOrDefault(f => f.ParamName == _usageTypeCode);
            var usageTypeCodeSourceFilter = filterValues.FirstOrDefault(f => f.ParamName == _usageTypeCodeSource);
            var subGroupUsageTypeCodeFilter = filterValues.FirstOrDefault(f => f.ParamName == _subGroupUsageTypeCode);
            var functionalSubGroupNameFilter = filterValues.FirstOrDefault(f => f.ParamName == _functionalSubGroupName);

            InitialiseGbuAttributesFilterValue(segmentFilter, usageTypeNameFilter, usageTypeCodeFilter,
                usageTypeCodeSourceFilter, subGroupUsageTypeCodeFilter, functionalSubGroupNameFilter);
        }

        #region Support Methods

        private InputParameters GetInputParameters(NameValueCollection query)
        {
            var segmentAttributeId = GetFilterParameterValue(query, _segment, "Сегмент");
            var usageTypeNameAttributeId = GetFilterParameterValue(query, _usageTypeName, "Наименование вида использования");
            var usageTypeCodeAttributeId = GetFilterParameterValue(query, _usageTypeCode, "Код вида использования");
            var usageTypeCodeSourceAttributeId = GetFilterParameterValue(query, _usageTypeCodeSource, "Источник информации кода вида использования");
            var subGroupUsageTypeCodeAttributeId = GetFilterParameterValue(query, _subGroupUsageTypeCode, "Код подгруппы вида использования");
            var functionalSubGroupNameAttributeId = GetFilterParameterValue(query, _functionalSubGroupName, "Наименование функциональной подгруппы");

            return new InputParameters
            {
                SegmentAttributeId = segmentAttributeId,
                UsageTypeNameAttributeId = usageTypeNameAttributeId,
                UsageTypeCodeAttributeId = usageTypeCodeAttributeId,
                UsageTypeCodeSourceAttributeId = usageTypeCodeSourceAttributeId,
                SubGroupUsageTypeCodeAttributeId = subGroupUsageTypeCodeAttributeId,
                FunctionalSubGroupNameAttributeId = functionalSubGroupNameAttributeId
            };
        }

        //TODO
        private List<ReportItem> GetOperations(List<long> taskIds, InputParameters inputParameters)
        {
            return new List<ReportItem>();
            //var attributesDictionary = GetAttributesForReport(inputParameters);

            ////var units = GetUnits(taskIds, PropertyTypes.Construction);
            ////объект с parent-зданием (для тестирования)
            //var units = OMUnit.Where(x => x.ObjectId == 16671634).SelectAll().Select(x => x.ParentGroup.GroupName).Execute();
            ////units[0].ObjectId = 11188991; 

            //var gbuAttributes = GbuObjectService.GetAllAttributes(
            //    units.Select(x => x.ObjectId.GetValueOrDefault()).Distinct().ToList(),
            //    attributesDictionary.Values.Select(x => (long)x.RegisterId).Distinct().ToList(),
            //    attributesDictionary.Values.Select(x => x.Id).Distinct().ToList(),
            //    DateTime.Now.GetEndOfTheDay());

            //var properties = typeof(ReportItem).GetProperties();
            //var rosreestrAttributesProperty = properties.FirstOrDefault(x => x.Name == nameof(ReportItem.RosreestrAttributes));
            //var customAttributesProperty = properties.FirstOrDefault(x => x.Name == nameof(ReportItem.CustomAttributes));

            //var result = new List<ReportItem>();
            //units.ToList().ForEach(unit =>
            ////units.Where(x => x.ObjectId == 11188991).ToList().ForEach(unit =>  // для тестирования
            //{
            //    var item = new ReportItem
            //    {
            //        UnitInfo = new UnitInfo
            //        {
            //            CadastralNumber = unit.CadastralNumber,
            //            CadastralQuartal = unit.CadastralBlock,
            //            ObjectType = unit.PropertyType_Code,
            //            Square = unit.Square,
            //            Group = unit.ParentGroup?.GroupName,
            //            Upks = unit.Upks,
            //            CadastralCost = unit.CadastralCost
            //        }
            //    };

            //    SetAttributes(unit.ObjectId, gbuAttributes, attributesDictionary, rosreestrAttributesProperty,
            //        customAttributesProperty, item);

            //    result.Add(item);
            //});

            //return result;
        }

        private static void SetAttributes(long? objectId, List<GbuObjectAttribute> gbuAttributes, Dictionary<string, RegisterAttribute> attributesDictionary,
            PropertyInfo rosreestrAttributesProperty, PropertyInfo customAttributesProperty, ReportItem item)
        {
            var objectAttributes = gbuAttributes.Where(x => x.ObjectId == objectId).ToList();
            foreach (var objectAttribute in objectAttributes)
            {
                var attributeKeys = attributesDictionary.Where(x => x.Value.Id == objectAttribute.AttributeId).Select(x => x.Key);
                foreach (var key in attributeKeys)
                {
                    var property = rosreestrAttributesProperty?.PropertyType.GetProperty(key);
                    if (property == null)
                    {
                        property = customAttributesProperty?.PropertyType.GetProperty(key);
                        property?.SetValue(item.CustomAttributes, objectAttribute.GetValueInString());
                    }
                    else
                    {
                        property.SetValue(item.RosreestrAttributes, objectAttribute.GetValueInString());
                    }
                }
            }
        }

        private Dictionary<string, RegisterAttribute> GetAttributesForReport(InputParameters inputParameters)
        {
            var attributesDictionary = new Dictionary<string, RegisterAttribute>();
            attributesDictionary.Add(nameof(RosreestrAttributes.CommissioningYear), StatisticalDataService.GetRosreestrCommissioningYearAttribute());
            attributesDictionary.Add(nameof(RosreestrAttributes.BuildYear), StatisticalDataService.GetRosreestrBuildYearAttribute());
            attributesDictionary.Add(nameof(RosreestrAttributes.UndergroundFloorsNumber), StatisticalDataService.GetRosreestrUndergroundFloorsNumberAttribute());
            attributesDictionary.Add(nameof(RosreestrAttributes.FloorsNumber), StatisticalDataService.GetRosreestrFloorsNumberAttribute());
            attributesDictionary.Add(nameof(RosreestrAttributes.WallMaterial), StatisticalDataService.GetRosreestrWallMaterialAttribute());
            attributesDictionary.Add(nameof(RosreestrAttributes.Location), StatisticalDataService.GetRosreestrLocationAttribute());
            attributesDictionary.Add(nameof(RosreestrAttributes.Address), StatisticalDataService.GetRosreestrAddressAttribute());
            attributesDictionary.Add(nameof(RosreestrAttributes.ParentCadastralNumber), StatisticalDataService.GetRosreestrParentCadastralNumberAttribute());
            attributesDictionary.Add(nameof(RosreestrAttributes.PlacementPurpose), StatisticalDataService.GetRosreestrPlacementPurposeAttribute());
            attributesDictionary.Add(nameof(RosreestrAttributes.ObjectName), StatisticalDataService.GetRosreestrObjectNameAttribute());

            attributesDictionary.Add(nameof(CustomAttributes.Segment), RegisterCache.GetAttributeData(inputParameters.SegmentAttributeId));
            attributesDictionary.Add(nameof(CustomAttributes.UsageTypeName), RegisterCache.GetAttributeData(inputParameters.UsageTypeNameAttributeId));
            attributesDictionary.Add(nameof(CustomAttributes.UsageTypeCode), RegisterCache.GetAttributeData(inputParameters.UsageTypeCodeAttributeId));
            attributesDictionary.Add(nameof(CustomAttributes.UsageTypeCodeSource), RegisterCache.GetAttributeData(inputParameters.UsageTypeCodeSourceAttributeId));
            attributesDictionary.Add(nameof(CustomAttributes.SubGroupUsageTypeCode), RegisterCache.GetAttributeData(inputParameters.SubGroupUsageTypeCodeAttributeId));
            attributesDictionary.Add(nameof(CustomAttributes.FunctionalSubGroupName), RegisterCache.GetAttributeData(inputParameters.FunctionalSubGroupNameAttributeId));

            return attributesDictionary;
        }

        private DataTable GetItemDataTable(List<ReportItem> operations)
        {
            var dataTable = new DataTable("Item");

            dataTable.Columns.Add("Number");

            dataTable.Columns.Add("CadastralNumber");

            dataTable.Columns.Add("CommissioningYear");
            dataTable.Columns.Add("BuildYear");
            dataTable.Columns.Add("UndergroundFloorsNumber");
            dataTable.Columns.Add("FloorsNumber");
            dataTable.Columns.Add("WallMaterial");
            dataTable.Columns.Add("Location");
            dataTable.Columns.Add("Address");
            dataTable.Columns.Add("PlacementPurpose");
            dataTable.Columns.Add("ObjectName");

            dataTable.Columns.Add("Square");
            dataTable.Columns.Add("ObjectType");
            dataTable.Columns.Add("CadastralQuartal");

            dataTable.Columns.Add("Segment");
            dataTable.Columns.Add("UsageTypeName");
            dataTable.Columns.Add("UsageTypeCode");
            dataTable.Columns.Add("UsageTypeCodeSource");
            dataTable.Columns.Add("SubGroupUsageTypeCode");
            dataTable.Columns.Add("FunctionalSubGroupName");

            dataTable.Columns.Add("Group");
            dataTable.Columns.Add("Upks");
            dataTable.Columns.Add("CadastralCost");

            for (var i = 0; i < operations.Count; i++)
            {
                dataTable.Rows.Add(i + 1,
                    operations[i].UnitInfo.CadastralNumber,

                    operations[i].RosreestrAttributes.CommissioningYear,
                    operations[i].RosreestrAttributes.BuildYear,
                    operations[i].RosreestrAttributes.UndergroundFloorsNumber,
                    operations[i].RosreestrAttributes.FloorsNumber,
                    operations[i].RosreestrAttributes.WallMaterial,
                    operations[i].RosreestrAttributes.Location,
                    operations[i].RosreestrAttributes.Address,
                    operations[i].RosreestrAttributes.PlacementPurpose,
                    operations[i].RosreestrAttributes.ObjectName,

                    operations[i].UnitInfo.Square,
                    operations[i].UnitInfo.ObjectType == PropertyTypes.None
                        ? null
                        : operations[i].UnitInfo.ObjectType.GetEnumDescription(),
                    operations[i].UnitInfo.CadastralQuartal,

                    operations[i].CustomAttributes.Segment,
                    operations[i].CustomAttributes.UsageTypeName,
                    operations[i].CustomAttributes.UsageTypeCode,
                    operations[i].CustomAttributes.UsageTypeCodeSource,
                    operations[i].CustomAttributes.SubGroupUsageTypeCode,
                    operations[i].CustomAttributes.FunctionalSubGroupName,

                    operations[i].UnitInfo.Group,
                    operations[i].UnitInfo.Upks,
                    operations[i].UnitInfo.CadastralCost);
            }

            return dataTable;
        }

        #endregion

        #region Entities

        private class InputParameters
        {
            public long SegmentAttributeId { get; set; }
            public long UsageTypeNameAttributeId { get; set; }
            public long UsageTypeCodeAttributeId { get; set; }
            public long UsageTypeCodeSourceAttributeId { get; set; }
            public long SubGroupUsageTypeCodeAttributeId { get; set; }
            public long FunctionalSubGroupNameAttributeId { get; set; }
        }

        private class UnitInfo
        {
            public string CadastralNumber { get; set; }
            public decimal? Square { get; set; }
            public PropertyTypes ObjectType { get; set; }
            public string CadastralQuartal { get; set; }
            public string Group { get; set; }
            public decimal? Upks { get; set; }
            public decimal? CadastralCost { get; set; }
        }

        private class RosreestrAttributes
        {
            public string CommissioningYear { get; set; }
            public string BuildYear { get; set; }
            //public string FormationDate { get; set; }
            public string UndergroundFloorsNumber { get; set; }
            public string FloorsNumber { get; set; }
            public string WallMaterial { get; set; }
            public string Location { get; set; }
            public string Address { get; set; }
            public string ParentPurpose { get; set; }
            public string ParentCadastralNumber { get; set; }
            
            public string PlacementPurpose { get; set; }
            public string ObjectName { get; set; }
        }

        private class CustomAttributes
        {
            public string Segment { get; set; }
            public string UsageTypeName { get; set; }
            public string UsageTypeCode { get; set; }
            public string UsageTypeCodeSource { get; set; }
            public string SubGroupUsageTypeCode { get; set; }
            public string FunctionalSubGroupName { get; set; }
        }

        private class ReportItem
        {
            public UnitInfo UnitInfo { get; set; }
            public RosreestrAttributes RosreestrAttributes { get; set; }
            public CustomAttributes CustomAttributes { get; set; }

            public ReportItem()
            {
                UnitInfo = new UnitInfo();
                RosreestrAttributes = new RosreestrAttributes();
                CustomAttributes = new CustomAttributes();
            }
        }

        #endregion
    }
}