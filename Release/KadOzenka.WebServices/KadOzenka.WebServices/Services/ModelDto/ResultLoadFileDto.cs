using System.IO;

namespace KadOzenka.WebServices.Services.ModelDto
{
	public class ResultLoadFileDto
	{
		public Stream Stream { get; set; }

		public string ContentType { get; set; }

		public string FileName { get; set; }
	}
}