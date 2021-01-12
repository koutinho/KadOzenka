using System.Collections.Generic;
using System.IO;
using System.Linq;
using KadOzenka.Dal.CancellationQueryManager;
using KadOzenka.Dal.ManagementDecisionSupport.Dto.StatisticalData;

namespace KadOzenka.Dal.ManagementDecisionSupport.StatisticalData
{
	public class NumberOfObjectsByGroupsService
	{

		public class InitialData
        {
            public string PropertyType { get; set; }
            public string Group { get; set; }
            public string ParentGroup { get; set; }
            public long objectsCount { get; set; }
        }

        public CancellationManager CancellationManager = new CancellationManager();
        public List<NumberOfObjectsByGroupsDto> GetNumberOfObjectsByGroups(long[] taskList, bool isOksReportType)
        {
            string contents = string.Empty, fileName = string.Empty;

            if (isOksReportType) fileName = "NumberOfObjectsByGroupsDto_isOksReportType";
            else fileName = "NumberOfObjectsByGroupsDto_isNotOksReportType";
            using (var sr = new StreamReader(Core.ConfigParam.Configuration.GetFileStream(fileName, "sql", "SqlQueries"))) contents = sr.ReadToEnd();

            var table = CancellationManager.ExecuteSql<InitialData>(string.Format(contents, string.Join(", ", taskList)));
            var result = new List<NumberOfObjectsByGroupsDto>();
            if (table.Count != 0)
            {
                foreach (InitialData initial in table)
                {
                    var group = initial.Group;
                    var parentGroup = initial.ParentGroup;
                    var dto = new NumberOfObjectsByGroupsDto
                    {
                        PropertyType = initial.PropertyType,
                        Group = string.IsNullOrEmpty(group) ? "Без группы" : group,
                        HasGroup = !string.IsNullOrEmpty(group),
                        ParentGroup = string.IsNullOrEmpty(parentGroup) ? "Без группы" : parentGroup,
                        HasParentGroup = !string.IsNullOrEmpty(parentGroup),
                        Count = initial.objectsCount
                    };
                    result.Add(dto);
                }
                result = result.GroupBy(x => new { x.PropertyType, x.Group, x.HasGroup, x.ParentGroup, x.HasParentGroup }).Select(
                group => new NumberOfObjectsByGroupsDto
                {
                    PropertyType = group.Key.PropertyType,
                    Group = group.Key.Group,
                    HasGroup = group.Key.HasGroup,
                    ParentGroup = group.Key.ParentGroup,
                    HasParentGroup = group.Key.HasParentGroup,
                    Count = group.ToList().FirstOrDefault().Count
                }).OrderBy(x => x.HasParentGroup).ThenBy(x => x.HasGroup).ToList();
            }

            return result;
        }
    }
}
