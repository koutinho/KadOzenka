using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using Core.Register.QuerySubsystem;
using KadOzenka.Dal.CancellationQueryManager;
using KadOzenka.Dal.FastReports.StatisticalData.Common;
using KadOzenka.Dal.ManagementDecisionSupport.StatisticalData.Entities;
using Serilog;

namespace KadOzenka.Dal.FastReports.StatisticalData.PricingFactorsComposition
{
    public class OksReport : StatisticalDataReport
    {
	    protected readonly string BaseFolderWithSql = "PricingFactorsComposition";
	    private readonly ILogger _logger;
	    protected override ILogger Logger => _logger;
	    private readonly QueryManager _queryManager;
	    public OksReport()
	    {
            _queryManager = new QueryManager();
		    _logger = Log.ForContext<OksReport>();
	    }

        protected override string TemplateName(NameValueCollection query)
        {
            return "PricingFactorsCompositionForOksReport";
        }

        protected override DataSet GetReportData(NameValueCollection query, HashSet<long> objectList = null)
        {
            _queryManager.SetBaseToken(CancellationToken);
            var taskIds = GetTaskIdList(query)?.ToList();
            var tourId = GetTourId(query);

            var operations = GetOperations(tourId, taskIds);
            Logger.Debug("Найдено {Count} объектов", operations?.Count);

            Logger.Debug("Начато формирование таблиц");
            var dataSet = new DataSet();
            var itemTable = GetItemDataTable(operations);
            dataSet.Tables.Add(itemTable);
            Logger.Debug("Закончено формирование таблиц");

            return dataSet;
        }


        #region Support Methods

        private List<ReportItem> GetOperations(long tourId, List<long> taskIds)
        {
            var sql = StatisticalDataService.GetSqlFileContent(BaseFolderWithSql, "Oks");

            if (_queryManager.IsRequestCancellationToken())
            {
                return new List<ReportItem>();
            }
            var commissioningYear = RosreestrRegisterService.GetCommissioningYearAttribute();
            var buildYear = RosreestrRegisterService.GetBuildYearAttribute();
            var formationDate = RosreestrRegisterService.GetFormationDateAttribute();
            var undergroundFloorsNumber = RosreestrRegisterService.GetUndergroundFloorsNumberAttribute();
            var floorsNumber = RosreestrRegisterService.GetFloorsNumberAttribute();
            var wallMaterial = RosreestrRegisterService.GetWallMaterialAttribute();
            var location = RosreestrRegisterService.GetLocationAttribute();
            var address = RosreestrRegisterService.GetAddressAttribute();
            var buildingPurpose = RosreestrRegisterService.GetBuildingPurposeAttribute();
            var placementPurpose = RosreestrRegisterService.GetPlacementPurposeAttribute();
            var constructionPurpose = RosreestrRegisterService.GetConstructionPurposeAttribute();
            var objectName = RosreestrRegisterService.GetObjectNameAttribute();

            var objectType = StatisticalDataService.GetObjectTypeAttributeFromTourSettings(tourId);
            var cadastralQuartal = StatisticalDataService.GetCadastralQuartalAttributeFromTourSettings(tourId);
            var subGroupNumber = StatisticalDataService.GetGroupAttributeFromTourSettings(tourId);

            var sqlWithParameters = string.Format(sql, string.Join(", ", taskIds), commissioningYear.Id,
                buildYear.Id, formationDate.Id, undergroundFloorsNumber.Id, floorsNumber.Id, wallMaterial.Id, location.Id,
                address.Id, buildingPurpose.Id, placementPurpose.Id, constructionPurpose.Id, objectName.Id,
                objectType.Id, cadastralQuartal.Id, subGroupNumber.Id);

            var result = _queryManager.ExecuteSql<ReportItem>(sqlWithParameters);

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
            dataTable.Columns.Add("Purpose");
            dataTable.Columns.Add("ObjectName");

            dataTable.Columns.Add("Square");

            dataTable.Columns.Add("ObjectType");
            dataTable.Columns.Add("CadastralQuartal");

            dataTable.Columns.Add("CostValue");
            dataTable.Columns.Add("DateValuation");
            dataTable.Columns.Add("DateEntering");
            dataTable.Columns.Add("DateApproval");
            dataTable.Columns.Add("DocNumber");
            dataTable.Columns.Add("DocDate");
            dataTable.Columns.Add("DocName");
            dataTable.Columns.Add("ApplicationDate");
            dataTable.Columns.Add("RevisalStatementDate");

            for (var i = 0; i < operations.Count; i++)
            {
                var formationDateStr = ProcessDate(operations[i].FormationDate);

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
                    operations[i].Purpose,
                    operations[i].ObjectName,
                    operations[i].Square,
                    operations[i].ObjectType,
                    operations[i].CadastralQuartal,
                    operations[i].CostValue?.ToString(DecimalFormat),
                    operations[i].DateValuation?.ToString(DateFormat),
                    operations[i].DateEntering?.ToString(DateFormat),
                    operations[i].DateApproval?.ToString(DateFormat),
                    operations[i].DocNumber,
                    operations[i].DocDate?.ToString(DateFormat),
                    operations[i].DocName,
                    operations[i].ApplicationDate?.ToString(DateFormat),
                    operations[i].RevisalStatementDate?.ToString(DateFormat));
            }

            return dataTable;
        }

        #endregion

        #region Entities

        private class ReportItem : InfoFromTourSettings
        {
            //From Unit
            public string CadastralNumber { get; set; }
            public decimal? Square { get; set; }

            //From Rosreestr
            public string CommissioningYear { get; set; }
            public string BuildYear { get; set; }
            public string FormationDate { get; set; }
            public string UndergroundFloorsNumber { get; set; }
            public string FloorsNumber { get; set; }
            public string WallMaterial { get; set; }
            public string Location { get; set; }
            public string Address { get; set; }
            public string Purpose { get; set; }
            public string ObjectName { get; set; }

            //From Tour Settings


            //From KO.CostRosreestr (KO_COST_ROSREESTR)
            /// <summary>
            /// Значение кадастровой стоимости
            /// </summary>
            public decimal? CostValue { get; set; }
            /// <summary>
            /// Дата определения кадастровой стоимости
            /// </summary>
            public DateTime? DateValuation { get; set; }
            /// <summary>
            /// Дата внесения сведений о кадастровой стоимости в ЕГРН
            /// </summary>
            public DateTime? DateEntering { get; set; }
            /// <summary>
            /// Дата утверждения кадастровой стоимости
            /// </summary>
            public DateTime? DateApproval { get; set; }
            /// <summary>
            /// Номер акта об утверждении кадастровой стоимости
            /// </summary>
            public string DocNumber { get; set; }
            /// <summary>
            /// Дата акта об утверждении кадастровой стоимости
            /// </summary>
            public DateTime? DocDate { get; set; }
            /// <summary>
            /// Наименование документа об утверждении кадастровой стоимости
            /// </summary>
            public string DocName { get; set; }
            /// <summary>
            /// Дата начала применения кадастровой стоимости
            /// </summary>
            public DateTime? ApplicationDate { get; set; }
            /// <summary>
            /// Дата подачи заявления о пересмотре кадастровой стоимости
            /// </summary>
            public DateTime? RevisalStatementDate { get; set; }
        }

        #endregion
    }
}
