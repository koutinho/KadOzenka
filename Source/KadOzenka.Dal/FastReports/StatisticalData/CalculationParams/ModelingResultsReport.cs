using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using Core.Shared.Extensions;
using KadOzenka.Dal.FastReports.StatisticalData.Common;
using KadOzenka.Dal.ManagementDecisionSupport.Enums;
using KadOzenka.Dal.ManagementDecisionSupport.StatisticalData;
using Core.UI.Registers.Reports.Model;
using KadOzenka.Dal.CancellationQueryManager;
using Microsoft.Practices.EnterpriseLibrary.Data;
using ObjectModel.KO;
using Serilog;

namespace KadOzenka.Dal.FastReports.StatisticalData.CalculationParams
{
    public class ModelingResultsReport : StatisticalDataReport
    {
	    private readonly ILogger _logger;
	    protected override ILogger Logger => _logger;

	    private readonly QueryManager queryManager;
	    public ModelingResultsReport()
	    {
            queryManager = new QueryManager();
		    _logger = Log.ForContext<ModelingResultsReport>();
	    }


        protected override string TemplateName(NameValueCollection query)
        {
            return "CalculationParamsModelingResultsReport";
        }

        public override void InitializeFilterValues(long objId, string senderName, bool initialization, List<FilterValue> filterValues)
        {
            GroupFilter.InitializeFilterValues(StatisticalDataType.ModelingResults, initialization, filterValues);
        }

        protected override DataSet GetReportData(NameValueCollection query, HashSet<long> objectList = null)
        {
            queryManager.SetBaseToken(CancellationToken);
            var taskIdList = GetTaskIdList(query).ToList();
            var groupId = GetGroupIdFromFilter(query);

            var group = OMGroup.Where(x => x.Id == groupId).Select(x => new
            {
                x.GroupName,
                x.Number
            }).ExecuteFirstOrDefault();

            if (queryManager.IsRequestCancellationToken())
            {
                return new DataSet();
            }
            var model = ModelingRepository.GetActiveModelEntityByGroupId(groupId);
            Logger.Debug("ИД модели '{ModelId}' для группы '{GroupId}'", model?.Id, groupId);

            var operations = GetOperations(taskIdList, model?.Id, groupId);
            Logger.Debug("Найдено {Count} объектов", operations?.Count);

            Logger.Debug("Начато формирование таблиц");
            var dataSet = new DataSet();
            var itemTable = GetItemDataTable(operations);
            var commonTable = GetCommonDataTable(group);
            dataSet.Tables.Add(itemTable);
            dataSet.Tables.Add(commonTable);
            Logger.Debug("Закончено формирование таблиц");

            return dataSet;
        }


        #region Support Methods

        private string GetSql(List<long> taskIds, long? modelId, long groupId,
            List<FactorsService.PricingFactors> groupedFactors)
        {
	        var sqlForModelFactors = FactorsService.GetSqlForModelFactors(modelId, groupedFactors);

            var addressAttribute = RosreestrRegisterService.GetAddressAttribute();

            var baseSelect = $@"SELECT
                unit.PROPERTY_TYPE as {nameof(ReportItem.ObjectType)}, 
                SUBSTRING(COALESCE((select * from  gbu_get_allpri_attribute_value(unit.object_id, {GbuCodRegisterService.GetCadastralQuarterFinalAttribute().Id})), unit.CADASTRAL_BLOCK), 0, 6) as {nameof(ReportItem.CadastralDistrict)}, 
                unit.CADASTRAL_NUMBER as {nameof(ReportItem.CadastralNumber)}, 
                (select * from  gbu_get_allpri_attribute_value(unit.object_id, {addressAttribute.Id})) as {nameof(ReportItem.Address)},
                unit.SQUARE as {nameof(ReportItem.Square)}, 
                unit.UPKS as {nameof(ReportItem.Upks)}, 
                unit.CADASTRAL_COST as {nameof(ReportItem.CadastralCost)} ";

            var query = string.IsNullOrWhiteSpace(sqlForModelFactors.Columns) 
	            ? baseSelect 
	            : $"{baseSelect}, {sqlForModelFactors.Columns} ";

            //ИД, у которых внесены показатели вручную (для тестирования)
            //and (unit.id = 16615885 or unit.id = 16615913)
            //modelId = 100018, groupId = 100018
            //taskId = 15365643
            query = $@"{query} FROM ko_unit unit 
                        {sqlForModelFactors.Tables} 
                    WHERE unit.TASK_ID in ({string.Join(",", taskIds)}) and unit.GROUP_ID= {groupId} and unit.PROPERTY_TYPE_CODE<>2190
                    order by COALESCE((select * from  gbu_get_allpri_attribute_value(unit.object_id, 548)), unit.CADASTRAL_BLOCK)";

            return query;
        }

        private List<ReportItem> GetOperations(List<long> taskIds, long? modelId, long groupId)
        {
            var groupedFactors = modelId == null
                ? new List<FactorsService.PricingFactors>()
                : FactorsService.GetGroupedModelFactors(modelId.Value);
            var generalAttributes = groupedFactors.SelectMany(x => x.Attributes).ToList();

            var sql = GetSql(taskIds, modelId, groupId, groupedFactors);
            var dataTable = queryManager.ExecuteSqlStringToDataSet(sql).Tables[0];

            var items = new List<ReportItem>();
            foreach (DataRow row in dataTable.Rows)
            {
                var item = new ReportItem
                {
                    ObjectType = row[nameof(ReportItem.ObjectType)].ParseToStringNullable(),
                    CadastralDistrict = row[nameof(ReportItem.CadastralDistrict)].ParseToStringNullable(),
                    CadastralNumber = row[nameof(ReportItem.CadastralNumber)].ParseToStringNullable(),
                    Address = row[nameof(ReportItem.Address)].ParseToStringNullable(),
                    Square = row[nameof(ReportItem.Square)].ParseToDecimalNullable(),
                    Upks = row[nameof(ReportItem.Upks)].ParseToDecimalNullable(),
                    CadastralCost = row[nameof(ReportItem.CadastralCost)].ParseToDecimalNullable(),
                    Factors = FactorsService.ProcessModelFactors(row, generalAttributes)
                };

                items.Add(item);
            }

            return items;
        }

        private DataTable GetItemDataTable(List<ReportItem> operations)
        {
            var dataTable = new DataTable("Data");

            dataTable.Columns.Add("Number");
            dataTable.Columns.Add("ObjectType");
            dataTable.Columns.Add("CadastralDistrict");
            dataTable.Columns.Add("CadastralNumber");
            dataTable.Columns.Add("FactorName");
            dataTable.Columns.Add("FactorValue");
            dataTable.Columns.Add("Address");
            dataTable.Columns.Add("Square");
            dataTable.Columns.Add("Upks");
            dataTable.Columns.Add("CadastralCost");

            for (var i = 0; i < operations.Count; i++)
            {
                var counter = i + 1;
                if (operations[i].Factors.Count == 0)
                {
                    dataTable.Rows.Add(counter,
                        operations[i].ObjectType,
                        operations[i].CadastralDistrict,
                        operations[i].CadastralNumber,
                        string.Empty,
                        string.Empty,
                        operations[i].Address,
                        operations[i].Square?.ToString(DecimalFormat),
                        operations[i].Upks?.ToString(DecimalFormat),
                        operations[i].CadastralCost?.ToString(DecimalFormat));
                }
                else
                {
                    foreach (var keyValuePair in operations[i].Factors)
                    {
                        dataTable.Rows.Add(counter,
                            operations[i].ObjectType,
                            operations[i].CadastralDistrict,
                            operations[i].CadastralNumber,
                            keyValuePair.Name,
                            keyValuePair.Value,
                            operations[i].Address,
                            operations[i].Square?.ToString(DecimalFormat),
                            operations[i].Upks?.ToString(DecimalFormat),
                            operations[i].CadastralCost?.ToString(DecimalFormat));
                    }
                }
            }

            return dataTable;
        }

        private DataTable GetCommonDataTable(OMGroup group)
        {
            var dataTable = new DataTable("Common");

            dataTable.Columns.Add("GroupName");

            dataTable.Rows.Add($"{group?.Number}. {group?.GroupName}");

            return dataTable;
        }

        #endregion


        #region Entities

        private class ReportItem
        {
            public string ObjectType { get; set; }
            public string CadastralDistrict { get; set; }
            public string CadastralNumber { get; set; }
            public List<FactorsService.Attribute> Factors { get; set; }
            public string Address { get; set; }
            public decimal? Square { get; set; }
            public decimal? Upks { get; set; }
            public decimal? CadastralCost { get; set; }
        }

        #endregion
    }
}
