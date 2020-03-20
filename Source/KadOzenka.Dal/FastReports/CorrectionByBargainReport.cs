using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using Core.UI.Registers.Reports.Model;
using ObjectModel.Market;
using Platform.Reports;

namespace KadOzenka.Dal.FastReports
{
    public class CorrectionByBargainReport : FastReportBase
    {
        protected override string TemplateName(NameValueCollection query)
        {
            return nameof(CorrectionByBargainReport);
        }

        public override string GetTitle(long? objectId)
        {
            return ReportType.Title;
        }

        public override void InitializeFilterValues(long objId, string senderName, bool initialisation, List<FilterValue> filterValues)
        {
            if (initialisation)
            {
                var cadastralQuarterFilterValue = filterValues.FirstOrDefault(f => f.ParamName == "CadastralQuarter");
                if (cadastralQuarterFilterValue != null)
                {
                    cadastralQuarterFilterValue.ReportParameters = new List<ReportParameter>();
                    var omQuarters = OMQuartalDictionary.Where(x => true)
                        .Select(x => x.CadastralQuartal)
                        .Execute().Select(x => x.CadastralQuartal);

                    cadastralQuarterFilterValue.ReportParameters.Add(new ReportParameter { Value = string.Empty, Key = string.Empty });
                    cadastralQuarterFilterValue.ReportParameters.AddRange(omQuarters.Select(x => new ReportParameter { Value = x, Key = x }));
                }
            }
        }

        protected override DataSet GetData(NameValueCollection query, HashSet<long> objectList = null)
        {
            var dataSet = new DataSet();
            return dataSet;
        }
    }
}
