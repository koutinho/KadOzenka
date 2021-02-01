using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.IO;
using System.Linq;
using Core.Register;
using Core.Register.RegisterEntities;
using KadOzenka.Dal.FastReports.StatisticalData.Common;
using Core.Shared.Extensions;
using KadOzenka.Dal.CancellationQueryManager;
using ObjectModel.Directory;
using ObjectModel.KO;
using Serilog;

namespace KadOzenka.Dal.FastReports.StatisticalData.CadastralCostDeterminationResults
{
    public class CadastralCostDeterminationResultsMainReport : StatisticalDataReport
    {
	    private readonly QueryManager _queryManager;
	    public static string IndividuallyResultsGroupNamePhrase => "индивидуального расчета";
        private Dictionary<Type, ICadastralCostDeterminationResultsReport> _reportsDictionary;
        private readonly ILogger _logger;
        protected override ILogger Logger => _logger;


        public CadastralCostDeterminationResultsMainReport()
        {
	        _queryManager = new QueryManager();
            _reportsDictionary = new Dictionary<Type, ICadastralCostDeterminationResultsReport>();
            _logger = Log.ForContext<CadastralCostDeterminationResultsMainReport>();
        }

        protected override string TemplateName(NameValueCollection query)
        {
            var report = GetReport(query);
            return report.GetTemplateName(query);
        }

        protected override DataSet GetReportData(NameValueCollection query, HashSet<long> objectList = null)
        { 
	        _queryManager.SetBaseToken(CancellationToken);
            var taskIdList = GetTaskIdList(query).ToList();

            var report = GetReport(query);
            Logger.Debug("Тип отчета {ReportType}", report.GetType().ToString());

            var groupIds = report.GetAvailableGroupIds();
            Logger.Debug("Найдено {GroupsCount} Групп", groupIds.Count);

            var units = GetUnits(taskIdList, groupIds);
            Logger.Debug("Найдено {UnitsCount} Единиц оценки", units?.Count);

            var operations = GetOperations(units);
            Logger.Debug("Найдено {Count} объектов", operations?.Count);

            Logger.Debug("Начато формирование таблиц");
            var dataSet = new DataSet();
            var itemTable = GetItemDataTable(operations);
            dataSet.Tables.Add(itemTable);
            Logger.Debug("Закончено формирование таблиц");

            return dataSet;
        }

        #region Support Methods

        private ICadastralCostDeterminationResultsReport GetReport(NameValueCollection query)
        {
            Type type;
            var reportType = GetQueryParam<string>("ReportType", query);
            switch (reportType)
            {
                case "Результаты определения кадастровой стоимости":
                    type = typeof(StateResultsReport);
                    break;
                case "Сведения о результатах определения КС ОН, КС которых определен индивидуально":
                    type = typeof(IndividuallyResultsReport);
                    break;
                default:
                    throw new InvalidDataException($"Неизвестный тип формирования данных: {reportType}");
            }

            if (!_reportsDictionary.TryGetValue(type, out var concreteReport))
            {
                concreteReport = (ICadastralCostDeterminationResultsReport)Activator.CreateInstance(type);
                _reportsDictionary[type] = concreteReport;
            }

            return concreteReport;
        }

        private List<OMUnit> GetUnits(List<long> taskIds, List<long?> groupIds)
        {
	        var query = OMUnit.Where(x => x.TaskId != null && taskIds.Contains((long) x.TaskId) &&
	                                      x.GroupId != null && groupIds.Contains(x.GroupId) &&
	                                      x.PropertyType_Code != PropertyTypes.CadastralQuartal)
		        .Select(x => new
		        {
			        x.CadastralBlock,
			        x.ObjectId,
			        x.CadastralNumber,
			        x.PropertyType_Code,
			        x.Square,
			        x.Upks,
			        x.CadastralCost
		        });

	        return _queryManager.ExecuteQuery(query);
        }

        private List<ReportItem> GetOperations(List<OMUnit> units)
        {
            if (units.Count == 0)
                return new List<ReportItem>();

            var cadastralQuartalGbuAttributes = GbuObjectService.GetAllAttributes(
	            units.Select(x => x.ObjectId.GetValueOrDefault()).Distinct().ToList(),
	            new List<long>{ GbuCodRegisterService.GetCadastralQuarterFinalAttribute().RegisterId},
	            new List<long> { GbuCodRegisterService.GetCadastralQuarterFinalAttribute().Id },
	            DateTime.Now.GetEndOfTheDay(), isLight: true);
            Logger.Debug("Найдено {Count} атрибутов Кадастрового квартала", cadastralQuartalGbuAttributes?.Count);

            var i = 0;
            return units.Select(unit =>
            {
	            var cadastralQuartalGbu = cadastralQuartalGbuAttributes.FirstOrDefault(x => x.ObjectId == unit.ObjectId)?.GetValueInString();
	            var cadastralQuartal = !string.IsNullOrEmpty(cadastralQuartalGbu)
		            ? cadastralQuartalGbu
		            : unit.CadastralBlock;

	            i++;
                if(i % 500 == 0)
                    Logger.Debug($"Обработано {i} объектов");

                return new ReportItem
	            {
		            CadastralQuartal = cadastralQuartal,
		            CadastralDistrict = GetCadastralDistrict(cadastralQuartal),
		            CadastralNumber = unit.CadastralNumber,
		            Type = unit.PropertyType_Code,
		            Square = unit.Square,
		            Upks = unit.Upks,
		            Cost = unit.CadastralCost
	            };
	            
            }).OrderBy(x => x.CadastralQuartal).ToList();
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

        #endregion

        #region Entities

        private class ReportItem
        {
	        public string CadastralQuartal{ get; set; }
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
