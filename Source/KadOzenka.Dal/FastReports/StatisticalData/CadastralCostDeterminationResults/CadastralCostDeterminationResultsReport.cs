using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using Core.Shared.Extensions;
using ObjectModel.Directory;
using ObjectModel.KO;

namespace KadOzenka.Dal.FastReports.StatisticalData.CadastralCostDeterminationResults
{
    public class CadastralCostDeterminationResultsReport : StatisticalDataReport, ICadastralCostDeterminationResultsReport
    {
        string ICadastralCostDeterminationResultsReport.GetTemplateName(NameValueCollection query)
        {
            return "CadastralCostDeterminationResultsReport";
        }

        DataSet ICadastralCostDeterminationResultsReport.GetData(NameValueCollection query, List<long> taskIdList)
        {
            var operations = GetOperations(taskIdList);

            var dataSet = new DataSet();
            var itemTable = GetItemDataTable(operations);
            dataSet.Tables.Add(itemTable);

            return dataSet;
        }

        
        #region Support Methods

        private List<ReportItem> GetOperations(List<long> taskIds)
        {
            if(taskIds.Count == 0)
                return new List<ReportItem>();

            var items = new List<ReportItem>();

            var units = OMUnit.Where(x => x.TaskId != null && taskIds.Contains((long) x.TaskId))
                .Select(x => x.CadastralBlock)
                .Select(x => x.CadastralNumber)
                .Select(x => x.PropertyType_Code)
                .Select(x => x.Square)
                .Select(x => x.Upks)
                .Select(x => x.CadastralCost)
                .OrderBy(x => x.CadastralBlock)
                .Execute();

            units.ForEach(unit =>
            {
                items.Add(new ReportItem
                {
                    CadastralDistrict = GetCadastralDistrict(unit.CadastralBlock),
                    CadastralNumber = unit.CadastralNumber,
                    Type = unit.PropertyType_Code,
                    Square = unit.Square,
                    Upks = unit.Upks,
                    Cost = unit.CadastralCost
                });
            });

            return items;
        }

        private DataTable GetItemDataTable(List<ReportItem> operations)
        {
            var dataTable = new DataTable("ITEM");

            dataTable.Columns.Add("Number");
            dataTable.Columns.Add("CadastralDistrict");
            dataTable.Columns.Add("CadastralNumber");
            dataTable.Columns.Add("Type");
            dataTable.Columns.Add("Square");
            dataTable.Columns.Add("Upks");
            dataTable.Columns.Add("Cost");

            for (var i = 0; i < operations.Count; i++)
            {
                dataTable.Rows.Add(i + 1,
                    operations[i].CadastralDistrict,
                    operations[i].CadastralNumber,
                    operations[i].Type.GetEnumDescription(),
                    operations[i].Square,
                    operations[i].Upks,
                    operations[i].Cost);
            }
            
            return dataTable;
        }

        private string GetCadastralDistrict(string cadastralQuartal)
        {
            return cadastralQuartal.Substring(0, 5);
        }

        #endregion

        #region Entities

        private class ReportItem
        {
            public string CadastralDistrict { get; set; }
            public string CadastralNumber { get; set; }
            public PropertyTypes Type { get; set; }
            public decimal? Square { get; set; }
            public decimal? Upks { get; set; }
            public decimal? Cost { get; set; }
        }

        #endregion
    }
}
