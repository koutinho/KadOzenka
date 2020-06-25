using System;
using System.Collections.Generic;
using System.Collections.Specialized;
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
    public abstract class ReportWithGroup : StatisticalDataReport
    {
        protected abstract StatisticalDataType GetReportType();

        protected long GetGroupIdFromFilter(NameValueCollection query)
        {
            var groupId = GetQueryParam<long>("Groups", query);
            if (groupId == 0)
                throw new Exception("Не выбрана группа");

            return groupId;
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


        #region Support

        private List<long> GetTaskIdsFromSession(StatisticalDataType reportType)
        {
            var reportCode = reportType.GetAttributeValue<StatisticalDataFmReportCodeAttribute>(
                nameof(StatisticalDataFmReportCodeAttribute.Code));

            var taskIdsStr = HttpContextHelper.HttpContext.Session.GetString($"Report{reportCode}TaskFilter");

            return JsonConvert.DeserializeObject<List<long>>(taskIdsStr);
        }

        private List<Group> GetGroups(List<long> taskIds)
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

        #endregion
    }
}
