using System.Collections.Generic;
using KadOzenka.Dal.RecycleBin.Dto;
using ObjectModel.Common;

namespace KadOzenka.Dal.RecycleBin
{
	public interface IRecycleBinService
	{
		bool ShouldUseLongProcessForRestoringObject(long registerId);
		RecycleBinDto GetRecycleBinRecord(long eventId);
		void CreateDeletedTable(long registerId, string mainTableName);
		void MoveObjectToRecycleBin(long objectId, int registerId, long eventId);
		void MoveObjectsToRecycleBin(List<long> objectIds, int registerId, long eventId);

		void MoveObjectsToRecycleBin(List<RecycleBinService.RegisterObjects> registerObjectsList, int mainObjectRegisterId,
			string description, int userId);

		void RestoreObject(long eventId);
		void FlushOldData(int keepDataForPastNDays);
        int Save(OMRecycleBin recycleBin);
    }
}
