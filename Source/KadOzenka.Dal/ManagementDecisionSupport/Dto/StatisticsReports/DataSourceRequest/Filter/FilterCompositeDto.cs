using System.Collections.Generic;
using System.Xml.Serialization;

namespace KadOzenka.Dal.ManagementDecisionSupport.Dto.StatisticsReports.DataSourceRequest.Filter
{

	public class FilterCompositeDto : FilterDto
	{
		[XmlArray("Filters")]
		[XmlArrayItem("FilterCompositeDto", typeof(FilterCompositeDto))]
		[XmlArrayItem("FilterSimpleDto", typeof(FilterSimpleDto))]
		public List<FilterDto> Filters { get; set; }
	}
}
