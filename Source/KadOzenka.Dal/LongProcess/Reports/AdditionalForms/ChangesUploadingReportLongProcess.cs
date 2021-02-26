using System;
using System.Collections.Generic;
using System.Linq;
using Core.Register.QuerySubsystem;
using Core.Shared.Extensions;
using KadOzenka.Dal.CancellationQueryManager;
using KadOzenka.Dal.GbuObject;
using KadOzenka.Dal.LongProcess.Reports.Entities;
using KadOzenka.Dal.LongProcess.Reports.ResultsForApproval;
using KadOzenka.Dal.ManagementDecisionSupport.StatisticalData;
using ObjectModel.Directory;
using ObjectModel.KO;
using Serilog;

namespace KadOzenka.Dal.LongProcess.Reports.AdditionalForms
{
    public class ChangesUploadingReportLongProcess : ALinearReportsLongProcessTemplate<ChangesUploadingReportLongProcess.ReportItem, ReportLongProcessOnlyTasksInputParameters>
    {
        protected override string ReportName => "Выгрузка изменений";
        protected override string ProcessName => nameof(ChangesUploadingReportLongProcess);
        protected StatisticalDataService StatisticalDataService { get; set; }
        private string TaskIdsStr { get; set; }
        private string BaseUnitsCondition { get; set; }
        private string BaseSql { get; set; }


        public ChangesUploadingReportLongProcess() : base(Log.ForContext<ResultsForApprovalLongProcess>())
        {
            StatisticalDataService = new StatisticalDataService();
        }


        protected override bool AreInputParametersValid(ReportLongProcessOnlyTasksInputParameters inputParameters)
        {
            return inputParameters?.TaskIds != null && inputParameters.TaskIds.Count != 0;
        }

        protected override void PrepareVariables(ReportLongProcessOnlyTasksInputParameters inputParameters)
        {
            BaseSql = GetBaseSql(inputParameters);
            TaskIdsStr = string.Join(',', inputParameters.TaskIds);

            BaseUnitsCondition = $@" where unit.TASK_ID IN ({TaskIdsStr}) AND 
                                        unit.PROPERTY_TYPE_CODE<>2190 ";
        }

        protected override ReportsConfig GetProcessConfig()
        {
            var defaultPackageSize = 400000;
            var defaultThreadsCount = 4;

            return GetProcessConfigFromSettings("ResultsForApproval", defaultPackageSize, defaultThreadsCount);
        }

        protected override int GetMaxItemsCount(ReportLongProcessOnlyTasksInputParameters inputParameters)
        {
            return GetMaxUnitsCount(BaseUnitsCondition);
        }

        protected override string GetSql(int packageIndex, int packageSize)
        {
            var unitsCondition = $@"{BaseUnitsCondition}
                                        order by unit.id 
                                        limit {packageSize} offset {packageIndex * packageSize}";

            return string.Format(BaseSql, unitsCondition);
        }

        protected override Func<IEnumerable<ReportItem>, IEnumerable<ReportItem>> FuncForDownloadedItems()
        {
	        return x => x.OrderBy(y => y.CadastralNumber);
        }

        protected override string GenerateReportTitle()
        {
            return "Выгрузка изменений";
        }

        protected override List<Column> GenerateReportHeaders()
        {
            var columns = new List<Column>
            {
                new Column {Header = "КН", Width = ColumnWidthForCadastralNumber},
                new Column {Header = "Дата изменения сведений", Width = ColumnWidthForDates},
                new Column {Header = "Тип", Width = 5},
                new Column {Header = "Статус"},
                new Column {Header = "Старое значение", Width = 5},
                new Column {Header = "Новое значение", Width = 5},
                new Column {Header = "Изменение", Width = 5}
            };

            var counter = 0;
            columns.ForEach(x => x.Index = counter++);

            return columns;
        }

        protected override List<object> GenerateReportReportRow(int index, ReportItem item)
        {
            return new List<object>
            {
                item.CadastralNumber,
                ProcessDate(item.CreationDate),
                item.PropertyType,
                item.StatusRepeatCalc,
                item.OldValue,
                item.NewValue,
                item.ChangeStatus
            };
        }



        #region Support Methods

        private string GetBaseSql(ReportLongProcessOnlyTasksInputParameters parameters)
        {
            var unitChangeJoin = new QSJoin
            {
                RegisterId = OMUnitChange.GetRegisterId(),
                JoinCondition = new QSConditionSimple
                {
                    ConditionType = QSConditionType.Equal,
                    LeftOperand = OMUnit.GetColumn(x => x.Id),
                    RightOperand = OMUnitChange.GetColumn(x => x.UnitId)
                },
                JoinType = QSJoinType.Inner
            };
            var taskJoin = new QSJoin
            {
                RegisterId = OMUnitChange.GetRegisterId(),
                JoinCondition = new QSConditionSimple
                {
                    ConditionType = QSConditionType.Equal,
                    LeftOperand = OMUnit.GetColumn(x => x.TaskId),
                    RightOperand = OMTask.GetColumn(x => x.Id)
                },
                JoinType = QSJoinType.Inner
            };

            var notCadastralQuarterType = new QSConditionSimple(OMUnit.GetColumn(x => x.PropertyType_Code),
                QSConditionType.NotEqual, (long)PropertyTypes.CadastralQuartal);
            var query = StatisticalDataService.GetQueryForUnitsByTasks(parameters.TaskIds.ToArray(), new List<QSCondition>{ notCadastralQuarterType }, new List<QSJoin> {unitChangeJoin, taskJoin});
            query.AddColumn(OMUnit.GetColumn(x => x.CadastralNumber, "CadastralNumber"));
            query.AddColumn(OMUnit.GetColumn(x => x.PropertyType, "PropertyType"));
            query.AddColumn(OMUnit.GetColumn(x => x.StatusRepeatCalc, "StatusRepeatCalc"));
            query.AddColumn(OMTask.GetColumn(x => x.CreationDate, "CreationDate"));
            query.AddColumn(OMUnitChange.GetColumn(x => x.OldValue, "OldValue"));
            query.AddColumn(OMUnitChange.GetColumn(x => x.NewValue, "NewValue"));
            query.AddColumn(OMUnitChange.GetColumn(x => x.ChangeStatus, "ChangeStatus"));

            return query.GetSql();
        }

        #endregion


        #region Entities

        public class ReportItem
        {
            public string CadastralNumber { get; set; }
            public string CreationDate { get; set; }
            public string PropertyType { get; set; }
            public string StatusRepeatCalc { get; set; }
            public string OldValue { get; set; }
            public string NewValue { get; set; }
            public string ChangeStatus { get; set; }
        }

        #endregion
    }
}
