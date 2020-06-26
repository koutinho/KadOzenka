using System.Collections.Generic;
using System.Linq;
using Core.Shared.Extensions;
using Core.Shared.Misc;
using Core.UI.Registers.Reports.Model;
using KadOzenka.Dal.ManagementDecisionSupport;
using KadOzenka.Dal.ManagementDecisionSupport.Enums;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using KadOzenka.Dal.FastReports.StatisticalData.Common.Entities;
using KadOzenka.Dal.Groups;

namespace KadOzenka.Dal.FastReports.StatisticalData.Common
{
    public static class GroupFilter
    {
        public static GroupService GroupService { get; set; }

        static GroupFilter()
        {
            GroupService = new GroupService();
        }

        public static void InitializeFilterValues(StatisticalDataType reportType, bool initialization, List<FilterValue> filterValues)
        {
            if (!initialization)
                return;

            var groupsFilterValue = filterValues.FirstOrDefault(f => f.ParamName == "Groups");
            if (groupsFilterValue != null)
            {
                var taskIds = GetTaskIdsFromSession(reportType);
                var groups = GetGroups(taskIds);

                groupsFilterValue.ReportParameters = new List<ReportParameter>();
                groupsFilterValue.ReportParameters.AddRange(groups.Select(x => new ReportParameter { Value = $"{x.Name}", Key = $"key:{x.Id}" }));
            }
        }


        #region Support Methods

        private static List<long> GetTaskIdsFromSession(StatisticalDataType reportType)
        {
            var reportCode = reportType.GetAttributeValue<StatisticalDataFmReportCodeAttribute>(
                nameof(StatisticalDataFmReportCodeAttribute.Code));

            var taskIdsStr = HttpContextHelper.HttpContext.Session.GetString($"Report{reportCode}TaskFilter");

            return JsonConvert.DeserializeObject<List<long>>(taskIdsStr);
        }

        private static List<Group> GetGroups(List<long> taskIds)
        {
            return GroupService.GetGroupsByTasks(taskIds).Select(x => new Group { Id = x.Id, Name = x.GroupName }).ToList();
        }

        #endregion
    }
}
