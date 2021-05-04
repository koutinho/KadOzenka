using MarketPlaceBusiness;
using MarketPlaceBusiness.Common;
using ObjectModel.Gbu;
using ObjectModel.Market;

namespace KadOzenka.Dal.DataImport.DataImporterByTemplate
{
	public class DataImporterByTemplateFactory
	{
		public static DataImporterByTemplate CreateDataImporterByTemplate(int mainRegisterId)
		{
			if (mainRegisterId == OMMainObject.GetRegisterId())
				return new DataImporterByTemplateGbu();

			if (mainRegisterId == Consts.RegisterId)
				return new DataImporterByTemplateMarket();

			return new DataImporterByTemplate(mainRegisterId);
		}
	}
}
