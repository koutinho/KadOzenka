namespace KadOzenka.Dal.DataComparing
{
	public class DataComparingConfig
	{
		public const string DateFormat = "ddMMyyyy";

		public static DataComparingConfig Current => Core.ConfigParam.Configuration.GetParam<DataComparingConfig>("DataComparingConfig");

		public string DataComparingFolder { get; set; }
		public string FilePrefix { get; set; }
		public string NameSeparator { get; set; }
		public string RsmSuffix { get; set; }
		public string PkkoSuffix { get; set; }
	}
}
