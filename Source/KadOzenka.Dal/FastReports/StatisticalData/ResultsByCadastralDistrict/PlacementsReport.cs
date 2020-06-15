﻿using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using Core.UI.Registers.Reports.Model;
using ObjectModel.Directory;
using Core.Register;
using Core.Register.RegisterEntities;
using Core.Shared.Extensions;
using KadOzenka.Dal.FastReports.StatisticalData.Common;

namespace KadOzenka.Dal.FastReports.StatisticalData.ResultsByCadastralDistrict
{
    public class PlacementsReport : BaseReport
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
            var tourId = GetTourId(query);
            var inputParameters = GetInputParameters(query);

            var operations = GetOperations(tourId, taskIds, inputParameters);

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

        private List<ReportItem> GetOperations(long tourId, List<long> taskIds, InputParameters inputParameters)
        {
            var attributesDictionary = GetAttributesForReport(tourId, inputParameters);

            var units = GetUnits(taskIds, PropertyTypes.Pllacement);
            //объект с parent-зданием (для тестирования)
            //var objectIdForTesting = 11188991;
            //units[0].ObjectId = objectIdForTesting;

            var gbuAttributes = GbuObjectService.GetAllAttributes(
                units.Select(x => x.ObjectId.GetValueOrDefault()).Distinct().ToList(),
                attributesDictionary.Values.Select(x => (long)x.RegisterId).Distinct().ToList(),
                attributesDictionary.Values.Select(x => x.Id).Distinct().ToList(),
                DateTime.Now.GetEndOfTheDay());

            var result = new List<ReportItem>();
            units.ToList().ForEach(unit =>
            //units.Where(x => x.ObjectId == objectIdForTesting).ToList().ForEach(unit =>  // для тестирования
            {
                var item = new ReportItem
                {
                    CadastralNumber = unit.CadastralNumber,
                    Square = unit.Square,
                    Upks = unit.Upks,
                    CadastralCost = unit.CadastralCost
                };

                SetAttributes(unit.ObjectId, gbuAttributes, attributesDictionary, item);
                SetParentObjectAttributes(tourId, item.ParentCadastralNumber, item);

                result.Add(item);
            });

            return result;
        }

        private Dictionary<string, RegisterAttribute> GetAttributesForReport(long tourId, InputParameters inputParameters)
        {
            var attributesDictionary = new Dictionary<string, RegisterAttribute>();
            attributesDictionary.Add(nameof(ReportItem.CommissioningYear), StatisticalDataService.GetRosreestrCommissioningYearAttribute());
            attributesDictionary.Add(nameof(ReportItem.BuildYear), StatisticalDataService.GetRosreestrBuildYearAttribute());
            attributesDictionary.Add(nameof(ReportItem.UndergroundFloorsNumber), StatisticalDataService.GetRosreestrUndergroundFloorsNumberAttribute());
            attributesDictionary.Add(nameof(ReportItem.FloorsNumber), StatisticalDataService.GetRosreestrFloorsNumberAttribute());
            attributesDictionary.Add(nameof(ReportItem.WallMaterial), StatisticalDataService.GetRosreestrWallMaterialAttribute());
            attributesDictionary.Add(nameof(ReportItem.Location), StatisticalDataService.GetRosreestrLocationAttribute());
            attributesDictionary.Add(nameof(ReportItem.Address), StatisticalDataService.GetRosreestrAddressAttribute());
            attributesDictionary.Add(nameof(ReportItem.ParentCadastralNumber), StatisticalDataService.GetRosreestrParentCadastralNumberAttribute());
            attributesDictionary.Add(nameof(ReportItem.PlacementPurpose), StatisticalDataService.GetRosreestrPlacementPurposeAttribute());
            attributesDictionary.Add(nameof(ReportItem.ObjectName), StatisticalDataService.GetRosreestrObjectNameAttribute());
            attributesDictionary.Add(nameof(ReportItem.Floor), StatisticalDataService.GetRosreestrFloorAttribute());

            attributesDictionary.Add(nameof(ReportItem.Segment), RegisterCache.GetAttributeData(inputParameters.SegmentAttributeId));
            attributesDictionary.Add(nameof(ReportItem.UsageTypeName), RegisterCache.GetAttributeData(inputParameters.UsageTypeNameAttributeId));
            attributesDictionary.Add(nameof(ReportItem.UsageTypeCode), RegisterCache.GetAttributeData(inputParameters.UsageTypeCodeAttributeId));
            attributesDictionary.Add(nameof(ReportItem.UsageTypeCodeSource), RegisterCache.GetAttributeData(inputParameters.UsageTypeCodeSourceAttributeId));
            attributesDictionary.Add(nameof(ReportItem.SubGroupUsageTypeCode), RegisterCache.GetAttributeData(inputParameters.SubGroupUsageTypeCodeAttributeId));
            attributesDictionary.Add(nameof(ReportItem.FunctionalSubGroupName), RegisterCache.GetAttributeData(inputParameters.FunctionalSubGroupNameAttributeId));

            var generalAttributes = GetAttributesFromTourSettingsForReport(tourId);
            var result = attributesDictionary.Concat(generalAttributes).ToDictionary(x => x.Key, x => x.Value);

            return result;
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
            dataTable.Columns.Add("ParentPurpose");
            dataTable.Columns.Add("ParentCadastralNumber");
            dataTable.Columns.Add("ParentGroup");
            dataTable.Columns.Add("PlacementPurpose");
            dataTable.Columns.Add("ObjectName");

            dataTable.Columns.Add("Square");
            dataTable.Columns.Add("ObjectType");

            dataTable.Columns.Add("Floor");

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
                    operations[i].CadastralNumber,
                    operations[i].CommissioningYear,
                    operations[i].BuildYear,
                    operations[i].UndergroundFloorsNumber,
                    operations[i].FloorsNumber,
                    operations[i].WallMaterial,
                    operations[i].Location,
                    operations[i].Address,
                    operations[i].ParentPurpose,
                    operations[i].ParentCadastralNumber,
                    operations[i].ParentGroup,
                    operations[i].PlacementPurpose,
                    operations[i].ObjectName,
                    operations[i].Square,
                    operations[i].ObjectType,
                    operations[i].Floor,
                    operations[i].CadastralQuartal,
                    operations[i].Segment,
                    operations[i].UsageTypeName,
                    operations[i].UsageTypeCode,
                    operations[i].UsageTypeCodeSource,
                    operations[i].SubGroupUsageTypeCode,
                    operations[i].FunctionalSubGroupName,
                    operations[i].SubGroupNumber,
                    operations[i].Upks,
                    operations[i].CadastralCost);
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

        private class ReportItem : InfoFromTourSettings, IParentInfo
        {
            //From Units
            public string CadastralNumber { get; set; }
            public decimal? Square { get; set; }
            public decimal? Upks { get; set; }
            public decimal? CadastralCost { get; set; }

            //From Rosreestr
            public string CommissioningYear { get; set; }
            public string BuildYear { get; set; }
            public string UndergroundFloorsNumber { get; set; }
            public string FloorsNumber { get; set; }
            public string WallMaterial { get; set; }
            public string Location { get; set; }
            public string Address { get; set; }
            public string ParentCadastralNumber { get; set; }
            public string PlacementPurpose { get; set; }
            public string ObjectName { get; set; }
            public string Floor { get; set; }

            //From Tour Settings


            //From UI
            public string Segment { get; set; }
            public string UsageTypeName { get; set; }
            public string UsageTypeCode { get; set; }
            public string UsageTypeCodeSource { get; set; }
            public string SubGroupUsageTypeCode { get; set; }
            public string FunctionalSubGroupName { get; set; }

            //From Parent
            public string ParentPurpose { get; set; }
            public string ParentGroup { get; set; }
        }

        #endregion
    }
}
