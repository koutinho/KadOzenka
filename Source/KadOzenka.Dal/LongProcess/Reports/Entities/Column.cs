namespace KadOzenka.Dal.LongProcess.Reports.Entities
{
	public class Column
	{
		public int Index { get; set; }
		public string Header { get; set; }
		public int Width { get; set; }

		public Column()
		{
			Width = 3;
		}
	}
}
