using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using Core.Shared.Extensions;
using Core.Shared.Misc;
using Core.UI.Registers.Reports.Model;
using KadOzenka.Dal.FastReports.StatisticalData.Common;
using KadOzenka.Dal.ManagementDecisionSupport;
using KadOzenka.Dal.ManagementDecisionSupport.Enums;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using ObjectModel.KO;

namespace KadOzenka.Dal.FastReports.StatisticalData.CalculationParams
{
    public abstract class BaseCalculationParamsReport : StatisticalDataReport
    {
        protected class Group
        {
            public long? Id { get; set; }
            public string Name { get; set; }
        }

        protected abstract StatisticalDataType GetReportType();

        protected List<Group> GetGroups(List<long> taskIds)
        {
            return OMUnit.Where(x => taskIds.Contains((long)x.TaskId) && x.ObjectId != null)
                .Select(x => x.ParentGroup.Id)
                .Select(x => x.ParentGroup.GroupName)
                .Execute()
                .Select(x => new Group
                {
                    Id = x.ParentGroup?.Id,
                    Name = x.ParentGroup?.GroupName
                })
                .Where(x => x.Id != null)
                .DistinctBy(x => x.Id).ToList();
        }

        public override void InitializeFilterValues(long objId, string senderName, bool initialisation, List<FilterValue> filterValues)
        {
            if (!initialisation)
                return;

            var groupsFilterValue = filterValues.FirstOrDefault(f => f.ParamName == "Groups");
            if (groupsFilterValue != null)
            {
                var reportType = GetReportType();
                var taskIds = GetTaskIdsFromSession(reportType);
                var groups = GetGroups(taskIds);

                groupsFilterValue.ReportParameters = new List<ReportParameter>();
                groupsFilterValue.ReportParameters.AddRange(groups.Select(x => new ReportParameter { Value = $"{x.Name}", Key = $"key:{x.Id}" }));
            }
        }

        protected List<long> GetTaskIdsFromSession(StatisticalDataType reportType)
        {
            var reportCode = reportType.GetAttributeValue<StatisticalDataFmReportCodeAttribute>(
                nameof(StatisticalDataFmReportCodeAttribute.Code));

            var taskIdsStr = HttpContextHelper.HttpContext.Session.GetString($"Report{reportCode}TaskFilter");

            return JsonConvert.DeserializeObject<List<long>>(taskIdsStr);
        }

        protected long GetGroupIdFromFilter(NameValueCollection query)
        {
            var groupId = GetQueryParam<long>("Groups", query);
            if (groupId == 0)
                throw new Exception("Не выбрана группа");

            return groupId;
        }
    }
}
