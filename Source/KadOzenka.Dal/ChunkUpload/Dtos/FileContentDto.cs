using System.IO;

namespace KadOzenka.Dal.ChunkUpload.Dtos
{
	public class FileContentDto
	{
		public Stream FileStream { get; set; }
		public string FileName { get; set; }
		
	}
}