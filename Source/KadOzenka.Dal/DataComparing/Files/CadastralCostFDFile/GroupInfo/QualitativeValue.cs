namespace KadOzenka.Dal.DataComparing.Files.CadastralCostFDFile.GroupInfo
{
	public class QualitativeValue
	{
		public string Id { get; protected set; }
		public string Value { get; protected set; }
		public string Dimension { get; protected set; }

		public QualitativeValue(string id, string val, string dimension)
		{
			Id = id;
			Value = val;
			Dimension = dimension;
		}
	}
}