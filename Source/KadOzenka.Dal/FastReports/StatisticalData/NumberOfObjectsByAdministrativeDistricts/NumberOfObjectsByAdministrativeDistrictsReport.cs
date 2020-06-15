using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.IO;
using KadOzenka.Dal.FastReports.StatisticalData.Common;


namespace KadOzenka.Dal.FastReports.StatisticalData.NumberOfObjectsByAdministrativeDistricts
{
    public class NumberOfObjectsByAdministrativeDistrictsReport : StatisticalDataReport, IGetQueryPAramFunc
    {
	    protected override string TemplateName(NameValueCollection query)
        {
	        var reportHandler = GetReportHandler(query);
            return reportHandler.GetTemplateName(query, this);
        }

	    protected override DataSet GetData(NameValueCollection query, HashSet<long> objectList = null)
        {
	        var taskIdList = GetTaskIdList(query);

	        var reportHandler = GetReportHandler(query);
	        var dataSet = reportHandler.GetData(taskIdList, query, this);

	        return HadleData(dataSet);
        }

	    public T Call<T>(string paramName, NameValueCollection query)
        {
	        return GetQueryParam<T>(paramName, query);
        }

	    private INumberOfObjectsByAdministrativeDistrictsReportHandler GetReportHandler(NameValueCollection query)
	    {
		    var reportType = GetQueryParam<string>("ReportType", query);
		    switch (reportType)
		    {
			    case "Количество объектов недвижимости (по административным округам), в разрезе групп и видов объектов недвижимости":
				    return new NumberOfObjectsByAdministrativeDistrictsByGroupsAndTypesReportHandler();
			    case "Количество объектов по субъекту в разрезе групп и видов объектов недвижимости":
				    return new NumberOfObjectsByAdministrativeDistrictsBySubjectReportHandler();
			    case "Количество объектов по группам":
				    return new NumberOfObjectsByAdministrativeDistrictsByGroupsReportHandler();
			    default:
				    throw new InvalidDataException($"Неизвестный тип формирования данных: {reportType}");
		    }
	    }
    }
}
