//using System;
//using System.Collections.Generic;
//using System.Collections.Specialized;
//using System.Data;
//using System.Linq;
//using Core.UI.Registers.Reports.Model;
//using ObjectModel.Directory;
//using Core.Register;
//using Core.Register.QuerySubsystem;
//using Core.Register.RegisterEntities;
//using Core.Shared.Extensions;
//using KadOzenka.Dal.ManagementDecisionSupport.StatisticalData.Entities;
//using Serilog;

//namespace KadOzenka.Dal.FastReports.StatisticalData.ResultsByCadastralDistrict
//{
//    public class UncompletedBuildingReport : ResultsByCadastralDistrictBaseReport
//    {
//        private readonly string _segment = "Segment";
//        private readonly string _usageTypeName = "UsageTypeName";
//        private readonly string _usageTypeCode = "UsageTypeCode";
//        private readonly string _usageTypeCodeSource = "UsageTypeCodeSource";
//        private readonly string _subGroupUsageTypeCode = "SubGroupUsageTypeCode";
//        private readonly string _functionalSubGroupName = "FunctionalSubGroupName";
//        private readonly ILogger _logger;
//        protected override ILogger Logger => _logger;

//        public UncompletedBuildingReport()
//        {
//	        _logger = Log.ForContext<UncompletedBuildingReport>();
//        }


//        protected override string TemplateName(NameValueCollection query)
//        {
//            return "ResultsByCadastralDistrictForUncompletedBuildingReport";
//        }

//        protected override DataSet GetReportData(NameValueCollection query, HashSet<long> objectList = null)
//        {
//            var taskIds = GetTaskIdList(query)?.ToList();
//            var tourId = GetTourId(query);
//            var inputParameters = GetInputParameters(query);

//            var operations = GetOperations(tourId, taskIds, inputParameters).ToList();
//            Logger.Debug("Найдено {Count} объектов", operations?.Count);

//            Logger.Debug("Начато формирование таблиц");
//            var dataSet = new DataSet();
//            var itemTable = GetItemDataTable(operations);
//            dataSet.Tables.Add(itemTable);
//            Logger.Debug("Закончено формирование таблиц");

//            return dataSet;
//        }

//        public override void InitializeFilterValues(long objId, string senderName, bool initialisation, List<FilterValue> filterValues)
//        {
//            if (!initialisation)
//                return;

//            var segmentFilter = filterValues.FirstOrDefault(f => f.ParamName == _segment);
//            var usageTypeNameFilter = filterValues.FirstOrDefault(f => f.ParamName == _usageTypeName);
//            var usageTypeCodeFilter = filterValues.FirstOrDefault(f => f.ParamName == _usageTypeCode);
//            var usageTypeCodeSourceFilter = filterValues.FirstOrDefault(f => f.ParamName == _usageTypeCodeSource);
//            var subGroupUsageTypeCodeFilter = filterValues.FirstOrDefault(f => f.ParamName == _subGroupUsageTypeCode);
//            var functionalSubGroupNameFilter = filterValues.FirstOrDefault(f => f.ParamName == _functionalSubGroupName);

//            InitialiseGbuAttributesFilterValue(segmentFilter, usageTypeNameFilter, usageTypeCodeFilter,
//                usageTypeCodeSourceFilter, subGroupUsageTypeCodeFilter, functionalSubGroupNameFilter);
//        }

//        #region Support Methods

//        private InputParameters GetInputParameters(NameValueCollection query)
//        {
//            var segmentAttributeId = GetFilterParameterValue(query, _segment, "Сегмент");
//            var usageTypeNameAttributeId = GetFilterParameterValue(query, _usageTypeName, "Наименование вида использования");
//            var usageTypeCodeAttributeId = GetFilterParameterValue(query, _usageTypeCode, "Код вида использования");
//            var usageTypeCodeSourceAttributeId = GetFilterParameterValue(query, _usageTypeCodeSource, "Источник информации кода вида использования");
//            var subGroupUsageTypeCodeAttributeId = GetFilterParameterValue(query, _subGroupUsageTypeCode, "Код подгруппы вида использования");
//            var functionalSubGroupNameAttributeId = GetFilterParameterValue(query, _functionalSubGroupName, "Наименование функциональной подгруппы");

//            return new InputParameters
//            {
//                SegmentAttributeId = segmentAttributeId,
//                UsageTypeNameAttributeId = usageTypeNameAttributeId,
//                UsageTypeCodeAttributeId = usageTypeCodeAttributeId,
//                UsageTypeCodeSourceAttributeId = usageTypeCodeSourceAttributeId,
//                SubGroupUsageTypeCodeAttributeId = subGroupUsageTypeCodeAttributeId,
//                FunctionalSubGroupNameAttributeId = functionalSubGroupNameAttributeId
//            };
//        }

//        private List<ReportItem> GetOperationsViaSql(long tourId, List<long> taskIds, InputParameters inputParameters)
//        {
//            var sql = GetSqlFileContent("UncompletedBuildings");

//            var buildYear = RosreestrRegisterService.GetBuildYearAttribute();
//            var formationDate = RosreestrRegisterService.GetFormationDateAttribute();
//            var undergroundFloorsNumber = RosreestrRegisterService.GetUndergroundFloorsNumberAttribute();
//            var floorsNumber = RosreestrRegisterService.GetFloorsNumberAttribute();
//            var wallMaterial = RosreestrRegisterService.GetWallMaterialAttribute();
//            var location = RosreestrRegisterService.GetLocationAttribute();
//            var address = RosreestrRegisterService.GetAddressAttribute();
//            var buildingPurpose = RosreestrRegisterService.GetBuildingPurposeAttribute();
//            var objectName = RosreestrRegisterService.GetObjectNameAttribute();
//            var readinessPercentage = RosreestrRegisterService.GetReadinessPercentageAttribute();

//            var segment = RegisterCache.GetAttributeData(inputParameters.SegmentAttributeId);
//            var usageTypeName = RegisterCache.GetAttributeData(inputParameters.UsageTypeNameAttributeId);
//            var usageTypeCode = RegisterCache.GetAttributeData(inputParameters.UsageTypeCodeAttributeId);
//            var usageTypeCodeSource = RegisterCache.GetAttributeData(inputParameters.UsageTypeCodeSourceAttributeId);
//            var subGroupUsageTypeCode = RegisterCache.GetAttributeData(inputParameters.SubGroupUsageTypeCodeAttributeId);
//            var functionalSubGroupName = RegisterCache.GetAttributeData(inputParameters.FunctionalSubGroupNameAttributeId);

//            var objectType = StatisticalDataService.GetObjectTypeAttributeFromTourSettings(tourId);
//            var cadastralQuartal = StatisticalDataService.GetCadastralQuartalAttributeFromTourSettings(tourId);
//            var subGroupNumber = StatisticalDataService.GetGroupAttributeFromTourSettings(tourId);

//            var sqlWithParameters = string.Format(sql, string.Join(", ", taskIds), buildYear.Id, formationDate.Id,
//                undergroundFloorsNumber.Id, floorsNumber.Id, wallMaterial.Id, location.Id, address.Id,
//                buildingPurpose.Id, objectName.Id, readinessPercentage.Id, segment.Id, usageTypeName.Id,
//                usageTypeCode.Id, usageTypeCodeSource.Id, subGroupUsageTypeCode.Id, functionalSubGroupName.Id,
//                objectType.Id, cadastralQuartal.Id, subGroupNumber.Id);

//            var result = QSQuery.ExecuteSql<ReportItem>(sqlWithParameters);

//            return result;
//        }

//        private List<ReportItem> GetOperations(long tourId, List<long> taskIds, InputParameters inputParameters)
//        {
//            var attributesDictionary = GetAttributesForReport(tourId, inputParameters);

//            var units = GetUnits(taskIds, PropertyTypes.UncompletedBuilding);

//            var gbuAttributes = GbuObjectService.GetAllAttributes(
//                units.Select(x => x.ObjectId.GetValueOrDefault()).Distinct().ToList(),
//                attributesDictionary.Values.Select(x => (long)x.RegisterId).Distinct().ToList(),
//                attributesDictionary.Values.Select(x => x.Id).Distinct().ToList(),
//                DateTime.Now.GetEndOfTheDay(), isLight: true);

//            var result = new List<ReportItem>();
//            units.ToList().ForEach(unit =>
//            {
//                var item = new ReportItem
//                {
//                    CadastralNumber = unit.CadastralNumber,
//                    Square = unit.Square,
//                    Upks = unit.Upks,
//                    CadastralCost = unit.CadastralCost
//                };

//                SetAttributes(unit.ObjectId, gbuAttributes, attributesDictionary, item);

//                result.Add(item);
//            });

//            return result;
//        }

//        private Dictionary<string, RegisterAttribute> GetAttributesForReport(long tourId, InputParameters inputParameters)
//        {
//            var attributesDictionary = new Dictionary<string, RegisterAttribute>();
//            attributesDictionary.Add(nameof(ReportItem.BuildYear), RosreestrRegisterService.GetBuildYearAttribute());
//            attributesDictionary.Add(nameof(ReportItem.FormationDate), RosreestrRegisterService.GetFormationDateAttribute());
//            attributesDictionary.Add(nameof(ReportItem.UndergroundFloorsNumber), RosreestrRegisterService.GetUndergroundFloorsNumberAttribute());
//            attributesDictionary.Add(nameof(ReportItem.FloorsNumber), RosreestrRegisterService.GetFloorsNumberAttribute());
//            attributesDictionary.Add(nameof(ReportItem.WallMaterial), RosreestrRegisterService.GetWallMaterialAttribute());
//            attributesDictionary.Add(nameof(ReportItem.Location), RosreestrRegisterService.GetLocationAttribute());
//            attributesDictionary.Add(nameof(ReportItem.Address), RosreestrRegisterService.GetAddressAttribute());
//            attributesDictionary.Add(nameof(ReportItem.BuildingPurpose), RosreestrRegisterService.GetBuildingPurposeAttribute());
//            attributesDictionary.Add(nameof(ReportItem.ObjectName), RosreestrRegisterService.GetObjectNameAttribute());
//            attributesDictionary.Add(nameof(ReportItem.ReadinessPercentage), RosreestrRegisterService.GetReadinessPercentageAttribute());

//            attributesDictionary.Add(nameof(ReportItem.Segment), RegisterCache.GetAttributeData(inputParameters.SegmentAttributeId));
//            attributesDictionary.Add(nameof(ReportItem.UsageTypeName), RegisterCache.GetAttributeData(inputParameters.UsageTypeNameAttributeId));
//            attributesDictionary.Add(nameof(ReportItem.UsageTypeCode), RegisterCache.GetAttributeData(inputParameters.UsageTypeCodeAttributeId));
//            attributesDictionary.Add(nameof(ReportItem.UsageTypeCodeSource), RegisterCache.GetAttributeData(inputParameters.UsageTypeCodeSourceAttributeId));
//            attributesDictionary.Add(nameof(ReportItem.SubGroupUsageTypeCode), RegisterCache.GetAttributeData(inputParameters.SubGroupUsageTypeCodeAttributeId));
//            attributesDictionary.Add(nameof(ReportItem.FunctionalSubGroupName), RegisterCache.GetAttributeData(inputParameters.FunctionalSubGroupNameAttributeId));

//            var generalAttributes = GetAttributesFromTourSettingsForReport(tourId);
//            var result = attributesDictionary.Concat(generalAttributes).ToDictionary(x => x.Key, x => x.Value);

//            return result;
//        }

//        private DataTable GetItemDataTable(List<ReportItem> operations)
//        {
//            var dataTable = new DataTable("Item");

//            dataTable.Columns.Add("Number");

//            dataTable.Columns.Add("CadastralNumber");

//            dataTable.Columns.Add("BuildYear");
//            dataTable.Columns.Add("FormationDate");
//            dataTable.Columns.Add("UndergroundFloorsNumber");
//            dataTable.Columns.Add("FloorsNumber");
//            dataTable.Columns.Add("WallMaterial");
//            dataTable.Columns.Add("Location");
//            dataTable.Columns.Add("Address");
//            dataTable.Columns.Add("BuildingPurpose");
//            dataTable.Columns.Add("ObjectName");

//            dataTable.Columns.Add("Square");

//            dataTable.Columns.Add("ReadinessPercentage");

//            dataTable.Columns.Add("ObjectType");
//            dataTable.Columns.Add("CadastralQuartal");

//            dataTable.Columns.Add("Segment");
//            dataTable.Columns.Add("UsageTypeName");
//            dataTable.Columns.Add("UsageTypeCode");
//            dataTable.Columns.Add("UsageTypeCodeSource");
//            dataTable.Columns.Add("SubGroupUsageTypeCode");
//            dataTable.Columns.Add("FunctionalSubGroupName");

//            dataTable.Columns.Add("Group");
//            dataTable.Columns.Add("Upks");
//            dataTable.Columns.Add("CadastralCost");

//            for (var i = 0; i < operations.Count; i++)
//            {
//                var formationDateStr = ProcessDate(operations[i].FormationDate);

//                dataTable.Rows.Add(i + 1,
//                    operations[i].CadastralNumber,
//                    operations[i].BuildYear,
//                    formationDateStr,
//                    operations[i].UndergroundFloorsNumber,
//                    operations[i].FloorsNumber,
//                    operations[i].WallMaterial,
//                    operations[i].Location,
//                    operations[i].Address,
//                    operations[i].BuildingPurpose,
//                    operations[i].ObjectName,
//                    operations[i].Square,
//                    operations[i].ReadinessPercentage,
//                    operations[i].ObjectType,
//                    operations[i].CadastralQuartal,
//                    operations[i].Segment,
//                    operations[i].UsageTypeName,
//                    operations[i].UsageTypeCode,
//                    operations[i].UsageTypeCodeSource,
//                    operations[i].SubGroupUsageTypeCode,
//                    operations[i].FunctionalSubGroupName,
//                    operations[i].SubGroupNumber,
//                    operations[i].Upks,
//                    operations[i].CadastralCost);
//            }

//            return dataTable;
//        }

//        #endregion

//        #region Entities

//        private class InputParameters
//        {
//            public long SegmentAttributeId { get; set; }
//            public long UsageTypeNameAttributeId { get; set; }
//            public long UsageTypeCodeAttributeId { get; set; }
//            public long UsageTypeCodeSourceAttributeId { get; set; }
//            public long SubGroupUsageTypeCodeAttributeId { get; set; }
//            public long FunctionalSubGroupNameAttributeId { get; set; }
//        }

//        private class ReportItem : InfoFromTourSettings
//        {
//            //From Units
//            public string CadastralNumber { get; set; }
//            public decimal? Square { get; set; }
//            public decimal? Upks { get; set; }
//            public decimal? CadastralCost { get; set; }

//            //From Rosreestr
//            public string BuildYear { get; set; }
//            public string FormationDate { get; set; }
//            public string UndergroundFloorsNumber { get; set; }
//            public string FloorsNumber { get; set; }
//            public string WallMaterial { get; set; }
//            public string Location { get; set; }
//            public string Address { get; set; }
//            public string BuildingPurpose { get; set; }
//            public string ObjectName { get; set; }
//            public string ReadinessPercentage { get; set; }

//            //From Tour Settings


//            //From UI
//            public string Segment { get; set; }
//            public string UsageTypeName { get; set; }
//            public string UsageTypeCode { get; set; }
//            public string UsageTypeCodeSource { get; set; }
//            public string SubGroupUsageTypeCode { get; set; }
//            public string FunctionalSubGroupName { get; set; }
//        }

//        #endregion
//    }
//}
