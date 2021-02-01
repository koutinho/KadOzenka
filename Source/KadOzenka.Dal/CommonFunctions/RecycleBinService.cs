using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using Core.Register;
using Core.Register.QuerySubsystem;
using Core.Register.RegisterEntities;
using Core.Shared.Extensions;
using KadOzenka.Dal.CommonFunctions.Dto;
using Microsoft.Practices.EnterpriseLibrary.Data;
using ObjectModel.Common;
using ObjectModel.Core.Register;
using Serilog;

namespace KadOzenka.Dal.CommonFunctions
{
	public class RecycleBinService
	{
		private static readonly ILogger _log = Log.ForContext<RecycleBinService>();

		private string GetDeletedTableName(string mainTableName) => $"{mainTableName}_DELETED";
		private string GetTableName(string deletedTableName) => deletedTableName.Substring(0, deletedTableName.Length - "_DELETED".Length);

		public RecycleBinDto GetRecycleBinRecord(long eventId)
		{
			var recycleBinRecord = OMRecycleBin.Where(x => x.EventId == eventId).SelectAll().ExecuteFirstOrDefault();
			if(recycleBinRecord == null)
				throw new Exception($"Не найдена запись в козине с ИД {eventId}");

			return new RecycleBinDto
			{
				Id = recycleBinRecord.EventId,
				DeletedTime = recycleBinRecord.DeletedTime,
				ObjectType = RegisterCache.GetRegisterData((int) recycleBinRecord.ObjectRegisterId).Description,
				ObjectName = recycleBinRecord.Description
			};
		}

		public void MoveObjectToRecycleBin(long objectId, int registerId, long eventId)
		{
			MoveObjectsToRecycleBin(new List<long>{objectId}, registerId, eventId);
		}

		public void MoveObjectsToRecycleBin(List<long> objectIds, int registerId, long eventId)
		{
			_log.ForContext("ObjectIds", objectIds)
				.ForContext("RegisterId", registerId)
				.ForContext("EventId", eventId)
				.Debug("Перемещение объектов в корзину");

			if (objectIds.IsEmpty())
				return;

			var deleteTableExists = OMRegistersWithSoftDeletion
				.Where(x => x.RegisterId == registerId).ExecuteExists();
			if (deleteTableExists)
			{
				RegisterData registerData = RegisterCache.GetRegisterData(registerId);
				var mainTableColumns = GetColumnNames(new List<string> {registerData.QuantTable});
				var sql =
					$@"INSERT INTO {GetDeletedTableName(registerData.QuantTable)} ({string.Join(", ", mainTableColumns.Select(x => x.ColumnName).ToList())}, EVENT_ID)
					SELECT {string.Join(", ", mainTableColumns.Select(x => $"t.{x.ColumnName}").ToList())}, {eventId} 
					FROM {registerData.QuantTable} t 
					WHERE t.{registerData.PKField} in ({string.Join(",", objectIds)});
					";

				if(registerData.Id == OMRegister.GetRegisterId() || registerData.Id == OMAttribute.GetRegisterId())
				{
					sql += $"UPDATE {registerData.QuantTable} SET is_deleted=1 WHERE {registerData.PKField} in ({string.Join(",", objectIds)});";
				}
				else
				{
					sql += $"DELETE FROM {registerData.QuantTable} t WHERE t.{registerData.PKField} in ({string.Join(",", objectIds)});";
				}

				_log.ForContext("ObjectIds", objectIds)
					.ForContext("RegisterId", registerId)
					.ForContext("EventId", eventId)
					.ForContext("SQL", sql)
					.Debug("Выполнение скрипта по перемещению объектов в корзину");

				var command = DBMngr.Main.GetSqlStringCommand(sql);
				DBMngr.Main.ExecuteNonQuery(command);
				command.Dispose();
			}
		}

		public void RestoreObject(long eventId)
		{
			_log.Debug("Восстановление записи {EventId} из корзины", eventId);
			var recycleBinRecord = OMRecycleBin.Where(x => x.EventId == eventId).Select(x => x.EventId).ExecuteFirstOrDefault();
			if (recycleBinRecord == null)
			{
				throw new Exception($"Не найдена запись в корзине с ИД {eventId}");
			}

			var restoreDataSql = string.Empty;
			var deletedTableNamesWithEventId = GetDeletedTableNamesWithEventId(eventId);
			if (deletedTableNamesWithEventId.IsNotEmpty())
			{
				var allColumnsOfMainTables = GetColumnNames(deletedTableNamesWithEventId.Select(GetTableName).ToList());
				foreach (var deletedTableName in deletedTableNamesWithEventId)
				{
					var tableName = GetTableName(deletedTableName);
					if (tableName.ToUpper() == RegisterCache.GetRegisterData(OMRegister.GetRegisterId()).QuantTable.ToUpper() 
					    || tableName.ToUpper() == RegisterCache.GetRegisterData(OMAttribute.GetRegisterId()).QuantTable.ToUpper())
					{
						var pkField = tableName.ToUpper() == RegisterCache.GetRegisterData(OMRegister.GetRegisterId()).QuantTable.ToUpper()
							? RegisterCache.GetRegisterData(OMRegister.GetRegisterId()).PKField
							: RegisterCache.GetRegisterData(OMAttribute.GetRegisterId()).PKField;
						restoreDataSql += $@"
UPDATE {tableName} SET is_deleted=0 
WHERE {pkField} in (SELECT dt.{pkField} FROM {deletedTableName} dt WHERE dt.EVENT_ID={eventId});
";
					}
					else
					{
						var mainTableCols = allColumnsOfMainTables.Where(x => x.TableName.ToUpper() == tableName.ToUpper())
							.Select(x => x.ColumnName).ToList();
						restoreDataSql += $@"
INSERT INTO {tableName} ({string.Join(", ", mainTableCols)})
	SELECT {string.Join(", ", mainTableCols)}
	FROM {deletedTableName} dt
	WHERE dt.EVENT_ID={eventId};
";
					}

					restoreDataSql += $@"DELETE FROM {deletedTableName} dt WHERE dt.EVENT_ID={eventId};";
				}
			}

			using (var ts = new TransactionScope())
			{
				if (!string.IsNullOrEmpty(restoreDataSql))
				{
					_log.ForContext("EventId", eventId)
						.ForContext("SQL", restoreDataSql)
						.Debug("Выполнение скрипта по восстановлению данных");
					var command = DBMngr.Main.GetSqlStringCommand(restoreDataSql);
					DBMngr.Main.ExecuteNonQuery(command);
					command.Dispose();
				}
				recycleBinRecord.Destroy();

				ts.Complete();
			}
		}

		public void FlushOldData(int keepDataForPastNDays)
		{
			_log.Debug("Очистка данных корзины старше {KeepDataForPastNDays} дней", keepDataForPastNDays);
			var expirationDate = DateTime.Now.Date.AddDays(-keepDataForPastNDays);
			var recycleBins = OMRecycleBin.Where(x => x.DeletedTime < expirationDate).Select(x => x.EventId).Execute();
			_log.ForContext("KeepDataForPastNDays", keepDataForPastNDays)
				.Debug("Найдено {RecycleBinRecordCount} подходящих записей в корзине");

			DeleteData(recycleBins);
		}

		#region Helpers

		private List<Column> GetColumnNames(List<string> tableNames)
		{
			var sql = $@"
SELECT column_name as {nameof(Column.ColumnName)}, table_name as {nameof(Column.TableName)} 
FROM information_schema.columns WHERE table_name in ({string.Join(",", tableNames.Select(x => $"'{x.ToLower()}'"))});";
			_log.ForContext("SQL", sql)
				.Debug("Получение имен столбцов указанных таблиц");

			return QSQuery.ExecuteSql<Column>(sql);
		}

		private List<string> GetDeletedTableNamesWithEventId(long eventId)
		{
			return GetDeletedTableNamesWithEventIds(new List<long> {eventId});
		}

		private List<string> GetDeletedTableNamesWithEventIds(List<long> eventIds)
		{
			if (eventIds.IsEmpty())
				return new List<string>();

			var registersWithLogicalDeleted = OMRegistersWithSoftDeletion
				.Where(x => true).SelectAll().Execute();
			if (registersWithLogicalDeleted.IsEmpty())
				return new List<string>();

			var deletedTableName = GetDeletedTableName(registersWithLogicalDeleted[0].MainTableName);
			string sql = $@"
(SELECT '{deletedTableName}' as {nameof(Table.TableName)} FROM {deletedTableName} WHERE EVENT_ID in ({string.Join(",", eventIds)}) LIMIT 1)
";
			for (var i = 1; i < registersWithLogicalDeleted.Count; i++)
			{
				deletedTableName = GetDeletedTableName(registersWithLogicalDeleted[i].MainTableName);
				sql += $@"UNION ALL
(SELECT '{deletedTableName}' as {nameof(Table.TableName)} FROM {deletedTableName} WHERE EVENT_ID in ({string.Join(",", eventIds)}) LIMIT 1)
";
			}

			_log.ForContext("SQL", sql)
				.Debug("Получение списка deleted таблиц, содержащих данные по указанным event_id");
			var deletedTableNamesWithEventId = QSQuery.ExecuteSql<Table>(sql)
				.Select(x => x.TableName).ToList();

			return deletedTableNamesWithEventId;
		}

		private void DeleteData(List<OMRecycleBin> recycleBins)
		{
			if(recycleBins.IsEmpty())
				return;

			var registersWithLogicalDeleted = OMRegistersWithSoftDeletion
				.Where(x => true).SelectAll().Execute();

			string sql = string.Empty;
			if (registersWithLogicalDeleted.IsNotEmpty())
			{
				var deletedTableNames = GetDeletedTableNamesWithEventIds(recycleBins.Select(x => x.EventId).ToList());
				foreach (var deletedTableName in deletedTableNames)
				{
					sql += $@"
DELETE FROM {deletedTableName} WHERE EVENT_ID IN ({string.Join(",", recycleBins.Select(x => x.EventId))});";
				}
			}

			using (var ts = new TransactionScope())
			{
				if (!string.IsNullOrEmpty(sql))
				{
					_log.ForContext("SQL", sql)
						.Debug("Выполнение скрипта по очистке данных");
					var command = DBMngr.Main.GetSqlStringCommand(sql);
					DBMngr.Main.ExecuteNonQuery(command);
					command.Dispose();
				}

				_log.Debug("Удаление записей корзины");
				foreach (var recycleBin in recycleBins)
				{
					recycleBin.Destroy();
				}

				ts.Complete();
			}
		}

		#endregion Helpers

		#region Entities

		private class Column
		{
			public string ColumnName { get; set; }
			public string TableName { get; set; }
		}

		private class Table
		{
			public string TableName { get; set; }
		}

		#endregion Entities
	}
}
