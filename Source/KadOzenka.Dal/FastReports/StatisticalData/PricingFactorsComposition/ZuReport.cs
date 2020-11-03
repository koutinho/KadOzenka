using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using Core.Register.QuerySubsystem;
using KadOzenka.Dal.FastReports.StatisticalData.Common;
using KadOzenka.Dal.ManagementDecisionSupport.StatisticalData.Entities;
using Serilog;

namespace KadOzenka.Dal.FastReports.StatisticalData.PricingFactorsComposition
{
    public class ZuReport : StatisticalDataReport
    {
	    protected readonly string BaseFolderWithSql = "PricingFactorsComposition";
	    private readonly ILogger _logger;
	    protected override ILogger Logger => _logger;

	    public ZuReport()
	    {
		    _logger = Log.ForContext<ZuReport>();
	    }


        protected override string TemplateName(NameValueCollection query)
        {
            return "PricingFactorsCompositionForZuReport";
        }

        protected override DataSet GetReportData(NameValueCollection query, HashSet<long> objectList = null)
        {
            var taskIds = GetTaskIdList(query)?.ToList();
            var tourId = GetTourId(query);

            var operations = GetOperations(tourId, taskIds);

            var dataSet = new DataSet();
            var itemTable = GetItemDataTable(operations);
            dataSet.Tables.Add(itemTable);

            return dataSet;
        }


        #region Support Methods

        private List<ReportItem> GetOperations(long tourId, List<long> taskIds)
        {
            var sql = StatisticalDataService.GetSqlFileContent(BaseFolderWithSql, "Zu");

            var typeOfUseByDocuments = RosreestrRegisterService.GetTypeOfUseByDocumentsAttribute();
            var typeOfUseByClassifier = RosreestrRegisterService.GetTypeOfUseByClassifierAttribute();
            var formationDate = RosreestrRegisterService.GetFormationDateAttribute();
            var parcelCategory = RosreestrRegisterService.GetParcelCategoryAttribute();
            var location = RosreestrRegisterService.GetLocationAttribute();
            var address = RosreestrRegisterService.GetAddressAttribute();
            var parcelName = RosreestrRegisterService.GetParcelNameAttribute();

            var objectType = StatisticalDataService.GetObjectTypeAttributeFromTourSettings(tourId);
            var cadastralQuartal = StatisticalDataService.GetCadastralQuartalAttributeFromTourSettings(tourId);
            var subGroupNumber = StatisticalDataService.GetGroupAttributeFromTourSettings(tourId);

            var sqlWithParameters = string.Format(sql, string.Join(", ", taskIds), typeOfUseByDocuments.Id,
                typeOfUseByClassifier.Id, formationDate.Id, parcelCategory.Id, location.Id, address.Id, parcelName.Id,
                objectType.Id, cadastralQuartal.Id, subGroupNumber.Id);

            var result = QSQuery.ExecuteSql<ReportItem>(sqlWithParameters);

            return result;
        }

        private DataTable GetItemDataTable(List<ReportItem> operations)
        {
            var dataTable = new DataTable("Item");

            dataTable.Columns.Add("Number");

            dataTable.Columns.Add("CadastralNumber");

            dataTable.Columns.Add("TypeOfUseByDocuments");
            dataTable.Columns.Add("TypeOfUseByClassifier");
            dataTable.Columns.Add("FormationDate");
            dataTable.Columns.Add("ParcelCategory");
            dataTable.Columns.Add("Location");
            dataTable.Columns.Add("Address");
            dataTable.Columns.Add("ParcelName");

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
                dataTable.Rows.Add(i + 1,
                    operations[i].CadastralNumber,
                    operations[i].TypeOfUseByDocuments,
                    operations[i].TypeOfUseByClassifier,
                    operations[i].FormationDate,
                    operations[i].ParcelCategory,
                    operations[i].Location,
                    operations[i].Address,
                    operations[i].ParcelName,
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

            //From Rosreestr
            public string TypeOfUseByDocuments { get; set; }
            public string TypeOfUseByClassifier { get; set; }
            public string FormationDate { get; set; }
            public string ParcelCategory { get; set; }
            public string Location { get; set; }
            public string Address { get; set; }
            public string ParcelName { get; set; }

            //From Tour Settings
        }

        #endregion
    }
}
