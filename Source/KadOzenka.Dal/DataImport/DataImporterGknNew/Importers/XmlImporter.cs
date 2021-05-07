using System.IO;
using System.Threading;
using KadOzenka.Dal.DataImport.DataImporterGknNew.Importers.Base;
using KadOzenka.Dal.Logger;
using ObjectModel.KO;
using Serilog;

namespace KadOzenka.Dal.DataImport.DataImporterGknNew.Importers
{
	/// <summary>
	/// Класс для импорта .xml документа ЗнО любого типа кроме "Обращений"
	/// </summary>
	public class XmlImporter : BaseImporter
	{
		public XmlImporter(DataImporterGknLongProcessProgressLogger dataImporterGknLongProcessProgressLogger)
			: base(dataImporterGknLongProcessProgressLogger, Log.ForContext<XmlImporter>())
		{
		}


		protected override void ImportGkn(DataImporterGkn dataImporterGkn, FileStream fileStream, string pathSchema, OMTask task,
			CancellationToken cancellationToken, object additionalParameters = null)
		{
			dataImporterGkn.ImportDataGknFromXml(fileStream, pathSchema, task, cancellationToken);
		}
	}
}
