using System;
using System.Collections.Generic;
using System.Text;
using ObjectModel.Directory;

namespace KadOzenka.Dal.YandexParser
{
	public class FormMarketObjectsRequest
	{
		public string ObjectsListUrl { get; set; }
		public DealType DealType { get; set; }
		public MarketSegment MarketSegment { get; set; }
		public PropertyTypesCIPJS PropertyTypeCIPJS { get; set; }
		public PropertyTypes PropertyType { get; set; }
		public string Subcategory { get; set; }
	}
}
