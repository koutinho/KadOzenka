namespace KadOzenka.Dal.ConfigurationManagers.KadOzenkaConfigManager.Models.DataImporterGknConfig
{
	public class DataImporterGknConfig
	{
		public GknDataAttributes GknDataAttributes { get; set; }
	}

	public class GknDataAttributes
	{
		public General General { get; set; }
		public Building Building { get; set; }
		public Construction Construction { get; set; }
		public Uncompleted Uncompleted { get; set; }
		public Flat Flat { get; set; }
		public CarPlace CarPlace { get; set; }
		public Parcel Parcel { get; set; }
	}
}
