using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.IO;

namespace KadOzenka.Dal.FastReports.StatisticalData.ResultsByCadastralDistrict
{
    public class ResultsByCadastralDistrictReport : StatisticalDataReport
    {
        public Dictionary<Type, IResultsByCadastralDistrictReport> _reportsDictionary;

        public ResultsByCadastralDistrictReport()
        {
            _reportsDictionary = new Dictionary<Type, IResultsByCadastralDistrictReport>();
        }

        protected override string TemplateName(NameValueCollection query)
        {
            var report = GetReport(query);
            return report.GetTemplateName(query);
        }

        protected override DataSet GetData(NameValueCollection query, HashSet<long> objectList = null)
        {
            var report = GetReport(query);
            return report.GetData(query);
        }

        
        #region Support Methods

        private IResultsByCadastralDistrictReport GetReport(NameValueCollection query)
        {
            Type type;
            var reportType = GetQueryParam<string>("ReportType", query);
            switch (reportType)
            {
                case "Земельные участки":
                    type = typeof(ParcelsReport);
                    break;
                case "Здания":
                    type = typeof(BuildingsReport);
                    break;
                default:
                    throw new InvalidDataException($"Неизвестный тип формирования данных: {reportType}");
            }

            if (!_reportsDictionary.TryGetValue(type, out var concreteReport))
            {
                concreteReport = (IResultsByCadastralDistrictReport) Activator.CreateInstance(type);
                _reportsDictionary[type] = concreteReport;
            }

            return concreteReport;
        }

        #endregion
    }
}
