using Core.Register.QuerySubsystem;
using Core.Register.RegisterEntities;
using ObjectModel.Market;

namespace MarketPlaceBusiness.Common
{
	public static class Consts
	{
		public static readonly int RegisterId = OMCoreObject.GetRegisterId();

		//TODO KOMO-33: если будет много колонок, подумать над отдельным методом
		public static readonly QSColumn PrimaryKeyColumn = OMCoreObject.GetColumn(x => x.Id);
		public static readonly QSColumn ProcessTypeCodeColumn = OMCoreObject.GetColumn(x => x.ProcessType_Code);
		public static readonly QSColumn MarketSegmentCodeColumn = OMCoreObject.GetColumn(x => x.PropertyMarketSegment_Code);
		public static readonly QSColumn AreaColumn = OMCoreObject.GetColumn(x => x.Area);
		public static readonly QSColumn DealTypeCodeColumn = OMCoreObject.GetColumn(x => x.DealType_Code);
		public static readonly QSColumn LngColumn = OMCoreObject.GetColumn(x => x.Lng);
		public static readonly QSColumn LatColumn = OMCoreObject.GetColumn(x => x.Lat);
		public static readonly QSColumn ParserTimeColumn = OMCoreObject.GetColumn(x => x.ParserTime);
		public static readonly QSColumn CadastralNumberColumn = OMCoreObject.GetColumn(x => x.CadastralNumber);


		public static readonly long PriceAttributeId = OMCoreObject.GetColumnAttributeId(x => x.Price);
		public static readonly long CadastralNumberAttributeId = OMCoreObject.GetColumnAttributeId(x => x.CadastralNumber);
		public static readonly long AddressAttributeId = OMCoreObject.GetColumnAttributeId(x => x.Address);
		
		
		public static readonly RegisterAttribute NeighborhoodAttribute = OMCoreObject.GetAttributeData(x => x.Neighborhood);
		public static readonly RegisterAttribute ZoneRegionAttribute = OMCoreObject.GetAttributeData(x => x.ZoneRegion);
		public static readonly RegisterAttribute PropertyTypesCIPJSAttribute = OMCoreObject.GetAttributeData(x => x.PropertyTypesCIPJS);
		public static readonly RegisterAttribute DealTypeAttribute = OMCoreObject.GetAttributeData(x => x.DealType);
		public static readonly RegisterAttribute PropertyMarketSegmentAttribute = OMCoreObject.GetAttributeData(x => x.PropertyMarketSegment);
		public static readonly RegisterAttribute MarketAttribute = OMCoreObject.GetAttributeData(x => x.Market);
		public static readonly RegisterAttribute ProcessTypeCodeAttribute = OMCoreObject.GetAttributeData(x => x.ProcessType_Code);
		public static readonly RegisterAttribute QualityClassCodeAttribute = OMCoreObject.GetAttributeData(x => x.QualityClass_Code);
		public static readonly RegisterAttribute PriceAttribute = OMCoreObject.GetAttributeData(x => x.Price);
		public static readonly RegisterAttribute Metrottribute = OMCoreObject.GetAttributeData(x => x.Metro);
	}
}
