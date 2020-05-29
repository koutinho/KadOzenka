using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.IO;
using System.Linq;

namespace KadOzenka.Dal.FastReports.StatisticalData.CadastralCostDeterminationResults
{
    public class CadastralCostDeterminationResultsMainReport : StatisticalDataReport
    {
        public Dictionary<Type, ICadastralCostDeterminationResultsReport> _reportsDictionary;

        public CadastralCostDeterminationResultsMainReport()
        {
            _reportsDictionary = new Dictionary<Type, ICadastralCostDeterminationResultsReport>();
        }

        protected override string TemplateName(NameValueCollection query)
        {
            var report = GetReport(query);
            return report.GetTemplateName(query);
        }

        protected override DataSet GetData(NameValueCollection query, HashSet<long> objectList = null)
        {
            var taskIdList = GetTaskIdList(query).ToList();

            var report = GetReport(query);
            return report.GetData(query, taskIdList);
        }


        #region Support Methods

        private ICadastralCostDeterminationResultsReport GetReport(NameValueCollection query)
        {
            Type type;
            var reportType = GetQueryParam<string>("ReportType", query);
            switch (reportType)
            {
                case "Результаты определения кадастровой стоимости":
                    type = typeof(CadastralCostDeterminationResultsReport);
                    break;
                case "Сведения о результатах определения КС ОН, КС которых определен индивидуально":
                    type = typeof(CadastralCostDeterminationIndividuallyResultsReport);
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

        #endregion
    }
}
