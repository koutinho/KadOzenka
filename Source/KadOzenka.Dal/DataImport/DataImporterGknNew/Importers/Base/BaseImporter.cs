using System.Collections.Generic;
using KadOzenka.Dal.DataComparing.StorageManagers;
using KadOzenka.Dal.DataExport;
using ObjectModel.KO;
using Serilog;

namespace KadOzenka.Dal.DataImport.DataImporterGknNew.Importers.Base
{
	public abstract class BaseImporter
	{
		protected void ExportTaskChanges(OMTask task)
		{
			Log.Information("Формирование протокола изменений по результатам загрузки для единицы оценки {TaskId}", task.Id);
			var path = TaskChangesDataComparingStorageManager.GetComparingDataRsmFileFullName(task);
			var unloadSettings = new KOUnloadSettings { TaskFilter = new List<long> { task.Id }, IsDataComparingUnload = true, FileName = path };
			DEKOChange.ExportUnitChangeToExcel(null, unloadSettings, null);
		}
	}
}
