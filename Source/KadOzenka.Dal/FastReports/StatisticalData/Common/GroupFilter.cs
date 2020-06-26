using System.Collections.Generic;
using System.Linq;
using Core.Shared.Extensions;
using Core.Shared.Misc;
using Core.UI.Registers.Reports.Model;
using KadOzenka.Dal.ManagementDecisionSupport;
using KadOzenka.Dal.ManagementDecisionSupport.Enums;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using ObjectModel.KO;
using KadOzenka.Dal.FastReports.StatisticalData.Common.Entities;

namespace KadOzenka.Dal.FastReports.StatisticalData.Common
{
    public static class GroupFilter
    {
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
            var groupIds = OMUnit.Where(x => taskIds.Contains((long)x.TaskId) && x.GroupId != null)
                .Select(x => x.GroupId)
                .Execute()
                .Select(x => x.GroupId)
                .Distinct();

            return OMGroup.Where(x => groupIds.Contains(x.Id)).Select(x => x.Id).Select(x => x.GroupName).Execute()
                .Select(x => new Group { Id = x.Id, Name = x.GroupName }).ToList();
        }

        #endregion
    }
}
