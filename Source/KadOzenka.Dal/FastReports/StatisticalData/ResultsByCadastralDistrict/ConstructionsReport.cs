using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using Core.UI.Registers.Reports.Model;
using Core.Register;
using Core.Register.QuerySubsystem;
using KadOzenka.Dal.ManagementDecisionSupport.StatisticalData.Entities;

namespace KadOzenka.Dal.FastReports.StatisticalData.ResultsByCadastralDistrict
{
    public class ConstructionsReport : ResultsByCadastralDistrictBaseReport
    {
        private readonly string _segment = "Segment";
        private readonly string _usageTypeName = "UsageTypeName";
        private readonly string _usageTypeCode = "UsageTypeCode";
        private readonly string _usageTypeCodeSource = "UsageTypeCodeSource";
        private readonly string _subGroupUsageTypeCode = "SubGroupUsageTypeCode";
        private readonly string _functionalSubGroupName = "FunctionalSubGroupName";


        protected override string TemplateName(NameValueCollection query)
        {
            return "ResultsByCadastralDistrictForConstructionsReport";
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
            var sql = GetSqlFileContent("Constructions");

            var commissioningYear = RosreestrRegisterService.GetRosreestrCommissioningYearAttribute();
            var buildYear = RosreestrRegisterService.GetBuildYearAttribute();
            var formationDate = RosreestrRegisterService.GetFormationDateAttribute();
            var undergroundFloorsNumber = RosreestrRegisterService.GetUndergroundFloorsNumberAttribute();
            var floorsNumber = RosreestrRegisterService.GetFloorsNumberAttribute();
            var wallMaterial = RosreestrRegisterService.GetWallMaterialAttribute();
            var location = RosreestrRegisterService.GetLocationAttribute();
            var address = RosreestrRegisterService.GetAddressAttribute();
            var constructionPurpose = RosreestrRegisterService.GetConstructionPurposeAttribute();
            var objectName = RosreestrRegisterService.GetObjectNameAttribute();

            var segment = RegisterCache.GetAttributeData(inputParameters.SegmentAttributeId);
            var usageTypeName = RegisterCache.GetAttributeData(inputParameters.UsageTypeNameAttributeId);
            var usageTypeCode = RegisterCache.GetAttributeData(inputParameters.UsageTypeCodeAttributeId);
            var usageTypeCodeSource = RegisterCache.GetAttributeData(inputParameters.UsageTypeCodeSourceAttributeId);
            var subGroupUsageTypeCode = RegisterCache.GetAttributeData(inputParameters.SubGroupUsageTypeCodeAttributeId);
            var functionalSubGroupName = RegisterCache.GetAttributeData(inputParameters.FunctionalSubGroupNameAttributeId);

            var objectType = StatisticalDataService.GetObjectTypeAttributeFromTourSettings(tourId);
            var cadastralQuartal = StatisticalDataService.GetCadastralQuartalAttributeFromTourSettings(tourId);
            var subGroupNumber = StatisticalDataService.GetGroupAttributeFromTourSettings(tourId);

            var sqlWithParameters = string.Format(sql, string.Join(", ", taskIds), commissioningYear.Id, buildYear.Id,
                formationDate.Id, undergroundFloorsNumber.Id, floorsNumber.Id, wallMaterial.Id, location.Id,
                address.Id, constructionPurpose.Id, objectName.Id, segment.Id, usageTypeName.Id,
                usageTypeCode.Id, usageTypeCodeSource.Id, subGroupUsageTypeCode.Id, functionalSubGroupName.Id,
                objectType.Id, cadastralQuartal.Id, subGroupNumber.Id);

            var result = QSQuery.ExecuteSql<ReportItem>(sqlWithParameters);

             return result;
        }

        private DataTable GetItemDataTable(List<ReportItem> operations)
        {
            var dataTable = new DataTable("Item");

            dataTable.Columns.Add("Number");

            dataTable.Columns.Add("CadastralNumber");

            dataTable.Columns.Add("CommissioningYear");
            dataTable.Columns.Add("BuildYear");
            dataTable.Columns.Add("FormationDate");
            dataTable.Columns.Add("UndergroundFloorsNumber");
            dataTable.Columns.Add("FloorsNumber");
            dataTable.Columns.Add("WallMaterial");
            dataTable.Columns.Add("Location");
            dataTable.Columns.Add("Address");
            dataTable.Columns.Add("ConstructionPurpose");
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
                var formationDateStr = operations[i].FormationDate;
                if (!string.IsNullOrWhiteSpace(formationDateStr) && DateTime.TryParse(formationDateStr, out var date))
                {
                    formationDateStr = date.ToString(DateFormat);
                }

                dataTable.Rows.Add(i + 1,
                    operations[i].CadastralNumber,
                    operations[i].CommissioningYear,
                    operations[i].BuildYear,
                    formationDateStr,
                    operations[i].UndergroundFloorsNumber,
                    operations[i].FloorsNumber,
                    operations[i].WallMaterial,
                    operations[i].Location,
                    operations[i].Address,
                    operations[i].ConstructionPurpose,
                    operations[i].ObjectName,
                    operations[i].Square,
                    operations[i].ObjectType,
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

        private class ReportItem : InfoFromTourSettings
        {
            //From Units
            public string CadastralNumber { get; set; }
            public decimal? Square { get; set; }
            public decimal? Upks { get; set; }
            public decimal? CadastralCost { get; set; }

            //From Rosreestr
            public string CommissioningYear { get; set; }
            public string BuildYear { get; set; }
            public string FormationDate { get; set; }
            public string UndergroundFloorsNumber { get; set; }
            public string FloorsNumber { get; set; }
            public string WallMaterial { get; set; }
            public string Location { get; set; }
            public string Address { get; set; }
            public string ConstructionPurpose { get; set; }
            public string ObjectName { get; set; }

            //From Tour Settings


            //From UI
            public string Segment { get; set; }
            public string UsageTypeName { get; set; }
            public string UsageTypeCode { get; set; }
            public string UsageTypeCodeSource { get; set; }
            public string SubGroupUsageTypeCode { get; set; }
            public string FunctionalSubGroupName { get; set; }
        }

        #endregion
    }
}
