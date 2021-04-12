namespace KadOzenka.Dal.XmlParser.GknParserXmlElements
{
	public class xmlHiredHouse
	{
		public string UseHiredHouse { get; set; }

		public bool? ActBuilding { get; set; }
		public bool? ActDevelopment { get; set; }
		public string ContractBuilding { get; set; }
		public string ContractDevelopment { get; set; }

		public bool? OwnerDecision { get; set; }
		public string ContractSupport { get; set; }

		public xmlDocument DocHiredHouse { get; set; }
	}
}
