namespace KadOzenka.Web.Models.KoBase
{
	public class FileSize
	{
		public float Kb { get; set; }
		public float Mb => Kb / 1024;
	}
}
