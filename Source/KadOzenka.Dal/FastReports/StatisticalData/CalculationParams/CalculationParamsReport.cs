using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using Core.Register.QuerySubsystem;
using Core.Shared.Extensions;
using Core.Shared.Misc;
using ObjectModel.Directory;
using Core.UI.Registers.Reports.Model;
using KadOzenka.Dal.ManagementDecisionSupport;
using KadOzenka.Dal.ManagementDecisionSupport.Enums;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using ObjectModel.KO;

namespace KadOzenka.Dal.FastReports.StatisticalData.CalculationParams
{
    public class CalculationParamsReport : StatisticalDataReport
    {
        protected override string TemplateName(NameValueCollection query)
        {
            return "CalculationParamsReport";
        }

        public override void InitializeFilterValues(long objId, string senderName, bool initialisation, List<FilterValue> filterValues)
        {
            if (!initialisation)
                return;

            var groupsFilterValue = filterValues.FirstOrDefault(f => f.ParamName == "Groups");
            if (groupsFilterValue != null)
            {
                var taskIds = GetTaskIdsFromSession();
                var groups = GetGroups(taskIds);

                groupsFilterValue.ReportParameters = new List<ReportParameter>();
                groupsFilterValue.ReportParameters.AddRange(groups.Select(x => new ReportParameter { Value = $"{x.Name}", Key = $"key:{x.GroupId}" }));
            }
        }


        protected override DataSet GetData(NameValueCollection query, HashSet<long> objectList = null)
        {
            var groupId = GetQueryParam<long>("Groups", query);
            var operations = GetOperations(groupId);

            var dataSet = new DataSet();
            var itemTable = GetItemDataTable(operations);
            dataSet.Tables.Add(itemTable);

            return dataSet;
        }


        #region Support Methods

        private List<long> GetTaskIdsFromSession()
        {
            var reportCode = StatisticalDataType.CalculationParams.GetAttributeValue<StatisticalDataFmReportCodeAttribute>(
                nameof(StatisticalDataFmReportCodeAttribute.Code));

            var taskIdsStr = HttpContextHelper.HttpContext.Session.GetString($"Report{reportCode}TaskFilter");

            return JsonConvert.DeserializeObject<List<long>>(taskIdsStr);
        }

        private List<Group> GetGroups(List<long> taskIds)
        {
            var query = new QSQuery
            {
                MainRegisterID = OMUnit.GetRegisterId(),
                Condition = new QSConditionGroup
                {
                    Type = QSConditionGroupType.And,
                    Conditions = new List<QSCondition>
                    {
                        new QSConditionSimple(OMTask.GetColumn(x => x.Id), QSConditionType.In, taskIds.Select(x => (double)x).ToList())
                    }
                },
                Joins = new List<QSJoin>
                {
                    new QSJoin
                    {
                        RegisterId = OMGroup.GetRegisterId(),
                        JoinCondition = new QSConditionSimple
                        {
                            ConditionType = QSConditionType.Equal,
                            LeftOperand = OMUnit.GetColumn(x => x.GroupId),
                            RightOperand = OMGroup.GetColumn(x => x.Id)
                        },
                        JoinType = QSJoinType.Inner
                    }
                }
            };

            query.AddColumn(OMGroup.GetColumn(x => x.Id, nameof(Group.GroupId)));
            query.AddColumn(OMGroup.GetColumn(x => x.GroupName, nameof(Group.Name)));

            var result = query.ExecuteQuery<Group>();

            return result.DistinctBy(x => x.GroupId).ToList();
        }

        private List<ReportItem> GetOperations(long groupId)
        {
            return new List<ReportItem>();
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

        private class Group
        {
            public long Id { get; set; }
            public long GroupId { get; set; }
            public string Name { get; set; }
        }

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
