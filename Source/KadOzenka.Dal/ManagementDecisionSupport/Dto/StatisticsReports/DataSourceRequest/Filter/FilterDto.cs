using System.Xml.Serialization;

namespace KadOzenka.Dal.ManagementDecisionSupport.Dto.StatisticsReports.DataSourceRequest.Filter
{
	[XmlInclude(typeof(FilterCompositeDto))]
	[XmlInclude(typeof(FilterSimpleDto))]
	public abstract class FilterDto
	{
	}
}
