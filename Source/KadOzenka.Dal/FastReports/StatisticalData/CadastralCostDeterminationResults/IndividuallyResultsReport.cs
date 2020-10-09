﻿using System.Collections.Generic;
using System.Collections.Specialized;
using ObjectModel.Directory;
using ObjectModel.KO;

namespace KadOzenka.Dal.FastReports.StatisticalData.CadastralCostDeterminationResults
{
    public class IndividuallyResultsReport : ICadastralCostDeterminationResultsReport
    {
        public string GetTemplateName(NameValueCollection query)
        {
            return "CadastralCostDeterminationIndividuallyResultsReport";
        }

        public List<OMUnit> GetUnitsForCadastralCostDetermination(List<long> taskIds)
        {
            if (taskIds.Count == 0)
                return new List<OMUnit>();

            return OMUnit.Where(x => x.TaskId != null && taskIds.Contains((long) x.TaskId) &&
                                     x.ParentGroup.GroupName.ToLower().Contains(
                                         CadastralCostDeterminationResultsMainReport
                                             .IndividuallyResultsGroupNamePhrase) && x.PropertyType_Code != PropertyTypes.CadastralQuartal)
                .Select(x => x.CadastralBlock)
                .Select(x => x.ObjectId)
                .Select(x => x.CadastralNumber)
                .Select(x => x.PropertyType_Code)
                .Select(x => x.Square)
                .Select(x => x.Upks)
                .Select(x => x.CadastralCost)
                .Execute();
        }
    }
}
