using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using KadOzenka.Dal.FastReports.StatisticalData.Common;
using Core.UI.Registers.Reports.Model;
using KadOzenka.Dal.ManagementDecisionSupport.Enums;
using KadOzenka.Dal.ManagementDecisionSupport.StatisticalData;
using KadOzenka.Dal.ManagementDecisionSupport.StatisticalData.Entities;

namespace KadOzenka.Dal.FastReports.StatisticalData.PricingFactorsComposition
{
    //TODO отчет работает очень медленно (большой объем данных + сложная матрица FastReport),
    //TODO поэтому для него дополнительно создан длительный процесс PreviousToursReportProcess
    public class PreviousToursReport : StatisticalDataReport
    {
        private PreviousToursService PreviousToursService { get; set; }

        public PreviousToursReport()
        {
            PreviousToursService = new PreviousToursService();
        }

        protected override string TemplateName(NameValueCollection query)
        {
            return "PricingFactorsCompositionForPreviousToursReport";
        }

        public override void InitializeFilterValues(long objId, string senderName, bool initialization, List<FilterValue> filterValues)
        {
            GroupFilter.InitializeFilterValues(StatisticalDataType.PricingFactorsCompositionForPreviousTours, initialization, filterValues);
        }

        protected override DataSet GetData(NameValueCollection query, HashSet<long> objectList = null)
        {
            var taskIds = GetTaskIdList(query)?.ToList();

            var groupId = GetGroupIdFromFilter(query);

            var reportInfo = PreviousToursService.GetReportInfo(taskIds, groupId);

            var dataSet = new DataSet();
            var itemTable = GetItemDataTable(reportInfo.Items, reportInfo.ColumnTitles);
            var commonTable = GetCommonDataTable(reportInfo.Title);
            dataSet.Tables.Add(itemTable);
            dataSet.Tables.Add(commonTable);

            return dataSet;
        }


        #region Support Methods

        private DataTable GetCommonDataTable(string reportTitle)
        {
            var dataTable = new DataTable("Common");

            dataTable.Columns.Add("Title");

            dataTable.Rows.Add(reportTitle);

            return dataTable;
        }

        private DataTable GetItemDataTable(List<PreviousTourReportItem> operations, List<string> columnTitles)
        {
            var dataTable = new DataTable("Data");

            dataTable.Columns.Add("Number");
            dataTable.Columns.Add("CadastralNumber");

            dataTable.Columns.Add("ColumnTitle");
            dataTable.Columns.Add("TourYear");
            dataTable.Columns.Add("ValueForTour");

            ////для тестирования
            //operations = new List<ReportItem>
            //{
            //    new ReportItem{CadastralNumber = "KN_1", Tour = new OMTour{Id = 1, Year = 2016}, Square = 16, ObjectType = "type_16"},
            //    new ReportItem{CadastralNumber = "KN_1", Tour = new OMTour{Id = 2, Year = 2018}, Square = 18, ObjectType = "type_18"},
            //    new ReportItem{CadastralNumber = "KN_3", Tour = new OMTour{Id = 3, Year = 2020}, Square = 20, ObjectType = "type_16"}
            //};

            var counter = 0;
            foreach (var item in operations)
            {
                counter++;
                columnTitles.ForEach(title =>
                {
                    var value = PreviousToursService.GetValueForReportItem(title, item);
                    dataTable.Rows.Add(counter,
                        item.CadastralNumber,
                        title,
                        item.Tour?.Year,
                        value);
                });

                foreach (var keyValuePair in item.Factors)
                {
                    dataTable.Rows.Add(counter,
                        item.CadastralNumber,
                        keyValuePair.Name,
                        item.Tour?.Year,
                        keyValuePair.Value);
                }
            }
            
            return dataTable;
        }

        #endregion
    }
}
