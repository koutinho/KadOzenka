using System.IO;
using System.Threading;
using ObjectModel.Common;
using ObjectModel.KO;

namespace KadOzenka.Dal.DataImport.DataImporterGknNew.Importers.Base
{
	public interface IDataImporterGkn
	{
		void Import(FileStream fileStream, OMTask task, OMImportDataLog dataLog, CancellationToken processCancellationToken);
	}
}
