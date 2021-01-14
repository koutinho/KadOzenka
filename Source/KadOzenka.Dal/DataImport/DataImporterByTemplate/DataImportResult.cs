using System.IO;

namespace KadOzenka.Dal.DataImport.DataImporterByTemplate
{
	public class DataImportResult
	{
		public Stream ResultFile { get; }
		public DataImportStatus Status { get; }

		public DataImportResult(Stream resultFile, DataImportStatus status)
		{
			ResultFile = resultFile;
			Status = status;
		}
	}
}
