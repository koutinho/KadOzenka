using System;
using System.Collections.Generic;
using System.Text;

namespace DebugApplication.Report
{
    public class ReportType
    {
        public int Code;
        public string InternalName;
        public string ClassFullName;
        public string Title;
        public bool OpenEmpty;
        public string ReportEngine;
        public ReportEngineType ReportEngineType
        {
            get
            {
                return (!string.IsNullOrEmpty(ReportEngine) && ReportEngine == "FastReport")
                    ? ReportEngineType.FastReport : ReportEngineType.DevExpress;
            }
        }
        public List<FilterValue> FilterValues;

        /// <summary>
        /// Признак необходимости генерации номера для сохраненного отчета
        /// </summary>
        public bool GenerateNumber;
    }
}
