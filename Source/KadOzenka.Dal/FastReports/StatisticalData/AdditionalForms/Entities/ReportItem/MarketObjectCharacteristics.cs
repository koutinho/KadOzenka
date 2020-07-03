using KadOzenka.Dal.FastReports.StatisticalData.AdditionalForms.Entities.DataInfo;
using ObjectModel.Directory;

namespace KadOzenka.Dal.FastReports.StatisticalData.AdditionalForms.Entities.ReportItem
{
	public class MarketObjectCharacteristics
	{
		public string CadastralNumber { get; set; }
		public PropertyTypes Type { get; set; }
		public string RosreestrSquareValue { get; set; }
		public string ObjectNameTypeOfUse { get; set; }
		public string Purpose { get; set; }
		public string Address { get; set; }
		public string Location { get; set; }

		public MarketObjectCharacteristics(ObjectMainInfo objectMainInfo)
		{
			CadastralNumber = objectMainInfo.CadastralNumber;
			Type = objectMainInfo.PropertyType;
			RosreestrSquareValue = objectMainInfo.RosreestrSquareValue;
			ObjectNameTypeOfUse = objectMainInfo.PropertyType == PropertyTypes.Stead
				? objectMainInfo.TypeOfUse
				: objectMainInfo.ObjectName;

			switch (objectMainInfo.PropertyType)
			{
				case PropertyTypes.Building:
					Purpose = objectMainInfo.BuildingPurpose;
					break;
				case PropertyTypes.Pllacement:
					Purpose = objectMainInfo.PlacementPurpose;
					break;
				case PropertyTypes.Construction:
					Purpose = objectMainInfo.ConstructionPurpose;
					break;
			}

			Address = objectMainInfo.Address;
			Location = objectMainInfo.Location;
		}
	}
}
