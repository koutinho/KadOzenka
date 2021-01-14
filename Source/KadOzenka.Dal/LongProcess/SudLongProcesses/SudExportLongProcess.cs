using System;
using System.Linq;
using ObjectModel.Common;

namespace KadOzenka.Dal.LongProcess.SudLongProcesses
{
	public abstract class SudExportLongProcess : LongProcess
	{
		public override void LogError(long? objectId, Exception ex, long? errorId = null)
		{
			base.LogError(objectId, ex, errorId);
			OMExportByTemplates export = OMExportByTemplates
				.Where(x => x.Id == objectId)
				.SelectAll()
				.Execute()
				.FirstOrDefault();

			if (export == null)
			{
				return;
			}

			export.Status = (long)ObjectModel.Directory.Common.ImportStatus.Faulted;
			export.DateFinished = DateTime.Now;
			export.ResultMessage = $"{ex.Message}{(errorId != null ? $" (журнал № {errorId})" : String.Empty)}";
			export.Save();
		}
	}
}
