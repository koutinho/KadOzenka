using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Linq;
using Core.Numerator;
using Core.Register;
using Core.Register.QuerySubsystem;
using Core.Register.RegisterEntities;
using Core.Shared.Extensions;
using Core.Shared.Misc;
using KadOzenka.Dal.ConfigurationManagers.KadOzenkaConfigManager;
using KadOzenka.Dal.RecycleBin.Dto;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Newtonsoft.Json;
using Npgsql;
using ObjectModel.Common;
using ObjectModel.Core.Register;
using ObjectModel.Core.Shared;
using ObjectModel.KO;
using Platform.Main.ConfigurationManagers.CoreConfigurationManager;
using Serilog;
using SerilogTimings;

namespace KadOzenka.Dal.RecycleBin
{
	public class RecycleBinService : IRecycleBinService
	{
		private IRecycleBinRepository RecycleBinRepository { get; set; }

		private static readonly ILogger _log = Log.ForContext<RecycleBinService>();

		private string GetDeletedTableName(string mainTableName) => $"{mainTableName}_DELETED";
		private string GetTableName(string deletedTableName) => deletedTableName.Substring(0, deletedTableName.Length - "_DELETED".Length);


        public RecycleBinService(IRecycleBinRepository recycleBinRepository = null)
        {
            RecycleBinRepository = recycleBinRepository ?? new RecycleBinRepository();

        }


		public bool ShouldUseLongProcessForRestoringObject(long registerId)
		{
			return registerId == OMTask.GetRegisterId();
		}

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
				ObjectName = recycleBinRecord.Description,
				ObjectRegisterId = recycleBinRecord.ObjectRegisterId
			};
		}

		public void CreateDeletedTable(long registerId, string mainTableName)
		{
			new OMRegistersWithSoftDeletion
			{
				RegisterId = registerId,
				MainTableName = mainTableName
			}.Save();
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

			string sql = FormSqlForMovingObjectsToRecycleBin(new RegisterObjects(registerId, objectIds), eventId);

			if (!string.IsNullOrEmpty(sql))
			{
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

		public void MoveObjectsToRecycleBin(List<RegisterObjects> registerObjectsList, int mainObjectRegisterId, string description, int userId)
		{
			_log
				.ForContext("RegisterObjectsList", JsonConvert.SerializeObject(registerObjectsList))
				.Debug("Перемещение объектов в корзину");

			var recycleBinRegisterData = RegisterCache.GetRegisterData(OMRecycleBin.GetRegisterId());
			var eventId = Sequence.GetNextValue(recycleBinRegisterData.ObjectSequence);

			var recycleBinAttributeData = RegisterCache.RegisterAttributes.Values.ToList()
				.Where(x => x.RegisterId == OMRecycleBin.GetRegisterId()).ToList();
			var deletedTimeCol = recycleBinAttributeData.First(x => x.InternalName == nameof(OMRecycleBin.DeletedTime)).ValueField;
			var userIdCol = recycleBinAttributeData.First(x => x.InternalName == nameof(OMRecycleBin.UserId)).ValueField;
			var objectRegisterIdCol = recycleBinAttributeData.First(x => x.InternalName == nameof(OMRecycleBin.ObjectRegisterId)).ValueField;
			var descriptionCol = recycleBinAttributeData.First(x => x.InternalName == nameof(OMRecycleBin.Description)).ValueField;
			var createRecycleSql = $@"
INSERT INTO {recycleBinRegisterData.QuantTable}({recycleBinRegisterData.PKField}, {deletedTimeCol}, {userIdCol}, {objectRegisterIdCol}, {descriptionCol})
VALUES ({eventId}, {CrossDBSQL.ToDate(DateTime.Now)}, {userId}, {mainObjectRegisterId}, '{description?.Replace("'", "''")}');";

			var sqls = new List<string>();
			sqls.Add(createRecycleSql);
			foreach (var registerObjects in registerObjectsList)
			{
				string sql = FormSqlForMovingObjectsToRecycleBin(registerObjects, eventId);
				if(!string.IsNullOrEmpty(sql))
					sqls.Add(sql);
			}

			ExecuteCommands(sqls);
		}

		public void RestoreObject(long eventId)
		{
			_log.Debug("Восстановление записи {EventId} из корзины", eventId);
			var recycleBinRecord = OMRecycleBin.Where(x => x.EventId == eventId).Select(x => x.EventId).ExecuteFirstOrDefault();
			var recycleBinRegisterData = RegisterCache.GetRegisterData(OMRecycleBin.GetRegisterId());
			if (recycleBinRecord == null)
			{
				throw new Exception($"Не найдена запись в корзине с ИД {eventId}");
			}

			var restoreDataSqls = new List<string>();
			var deletedTableNamesWithEventId = GetDeletedTableNamesWithEventId(eventId);
			if (deletedTableNamesWithEventId.IsNotEmpty())
			{
				var allColumnsOfMainTables = GetColumnNames(deletedTableNamesWithEventId.Select(GetTableName).ToList());
				foreach (var deletedTableName in deletedTableNamesWithEventId)
				{
					var restoreDataSql = string.Empty;
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
					} else if (tableName.ToUpper() == RegisterCache.GetRegisterData(OMAttachment.GetRegisterId()).QuantTable.ToUpper()
					           || tableName.ToUpper() == RegisterCache.GetRegisterData(OMAttachmentObject.GetRegisterId()).QuantTable.ToUpper())
					{
						var pkField = tableName.ToUpper() == RegisterCache.GetRegisterData(OMAttachment.GetRegisterId()).QuantTable.ToUpper()
							? RegisterCache.GetRegisterData(OMAttachment.GetRegisterId()).PKField
							: RegisterCache.GetRegisterData(OMAttachmentObject.GetRegisterId()).PKField;
						restoreDataSql += $@"
UPDATE {tableName} SET is_deleted=0, deleted_date=null
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
	WHERE dt.EVENT_ID={eventId};";
					}

					restoreDataSql += $@"
DELETE FROM {deletedTableName} dt WHERE dt.EVENT_ID={eventId};";

					restoreDataSqls.Add(restoreDataSql);
				}
			}
			restoreDataSqls.Add($"DELETE FROM {recycleBinRegisterData.QuantTable} WHERE {recycleBinRegisterData.PKField}={eventId}");

			ExecuteCommands(restoreDataSqls);
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

        public int Save(OMRecycleBin recycleBin)
        {
            return RecycleBinRepository.Save(recycleBin);
        }

		#region Helpers

		private void ExecuteCommands(List<string> commands)
		{
			if (commands.Count == 0)
				return;

			var connectionString = CoreConfigManager.GetConnectionStringSetting()?.ConnectionString;
			var connection = new NpgsqlConnection(connectionString);
			try
			{
				connection.Open();
				ExecuteCommands(commands, connection);
			}
			finally
			{
				connection.Close();
			}
		}

		private void ExecuteCommands(List<string> commands, NpgsqlConnection connection)
		{
			var tran = connection.BeginTransaction(IsolationLevel.Serializable);
			try
			{
				foreach (var command in commands)
				{
					try
					{
						_log.ForContext("SQL", command)
							.Debug("Выполнение SQL в рамках логического удаления");
						DbCommand cmd = connection.CreateCommand();
						cmd.CommandText = command;
						cmd.CommandType = CommandType.Text;
						cmd.ExecuteNonQuery();
					}
					catch (Exception ex)
					{
						_log.ForContext("SQL", command)
							.Error(ex, "Ошибка во время выполнения SQL в рамках логического удаления");
						throw;
					}
				}

				tran.Commit();
			}
			catch (Exception ex)
			{
				_log.Error(ex, "Ошибка логического удаления");
				tran.Rollback();
				throw;
			}
		}

		private string FormSqlForMovingObjectsToRecycleBin(RegisterObjects registerObjects, long eventId)
		{
			string sql = string.Empty;
			var deleteTableExists = OMRegistersWithSoftDeletion
				.Where(x => x.RegisterId == registerObjects.RegisterId).ExecuteExists();
			if (deleteTableExists)
			{
				RegisterData registerData = RegisterCache.GetRegisterData(registerObjects.RegisterId);
				if (registerData.StorageType == StorageType.Type5)
				{
					var tableNames = new List<string>();
					if (registerData.AllpriPartitioning == Platform.Register.AllpriPartitioningType.DataType)
					{
						var postfixes = new List<string> { "TXT", "NUM", "DT" };
						foreach (var postfix in postfixes)
						{
							tableNames.Add($"{registerData.AllpriTable}_{postfix}");
						}
					}
					else if (registerData.AllpriPartitioning == Platform.Register.AllpriPartitioningType.AttributeId)
					{
						var attributesData = RegisterCache.RegisterAttributes.Values.ToList().Where(x => x.RegisterId == registerObjects.RegisterId).ToList();
						foreach (var attributeData in attributesData)
						{
							if (attributeData.IsPrimaryKey || attributeData.IsDeleted)
								continue;

							tableNames.Add($"{registerData.AllpriTable}_{attributeData.Id}");
						}
					}
					var mainTableColumns = GetColumnNames(tableNames);

					foreach (var tableName in tableNames)
					{
						sql += FormSqlForMovingObjectsToRecycleBinForOneTable(registerObjects, eventId, registerData, tableName, mainTableColumns.Where(x => x.TableName.ToUpper() == tableName.ToUpper()).ToList());
					}
				}
				else if (registerData.StorageType == StorageType.Type4)
				{
					var mainTableColumns = GetColumnNames(new List<string> { registerData.QuantTable });
					sql = FormSqlForMovingObjectsToRecycleBinForOneTable(registerObjects, eventId, registerData, registerData.QuantTable, mainTableColumns);
				}
			}

			return sql;
		}

		private string FormSqlForMovingObjectsToRecycleBinForOneTable(RegisterObjects registerObjects, long eventId, RegisterData registerData, string tableName, List<Column> mainTableColumns)
		{
			var sql = $@"
INSERT INTO {GetDeletedTableName(tableName)} ({string.Join(", ", mainTableColumns.Select(x => x.ColumnName).ToList())}, EVENT_ID)
					SELECT {string.Join(", ", mainTableColumns.Select(x => $"{x.ColumnName}").ToList())}, {eventId} 
					FROM {tableName} 
					{registerObjects.GetSqlSearchPredicate()};";
			if (registerData.Id == OMRegister.GetRegisterId() || registerData.Id == OMAttribute.GetRegisterId())
			{
				sql += $@"
UPDATE {tableName} SET is_deleted=1 {registerObjects.GetSqlSearchPredicate()};";
			}
			else if (registerData.Id == OMAttachment.GetRegisterId() ||
					 registerData.Id == OMAttachmentObject.GetRegisterId())
			{
				sql += $@"
UPDATE {tableName} SET is_deleted=1, deleted_date=CURRENT_DATE {registerObjects.GetSqlSearchPredicate()};";
			}
			else
			{
				sql += $@"
DELETE FROM {tableName} {registerObjects.GetSqlSearchPredicate()};";
			}

			return sql;
		}

		private List<Column> GetColumnNames(List<string> tableNames)
		{
			if (tableNames.IsEmpty())
				return new List<Column>();

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

			var deletedTableNames = GetDeletedTableNamesForRegister(registersWithLogicalDeleted[0]);
			string sql = $@"
(SELECT '{deletedTableNames[0]}' as {nameof(Table.TableName)} FROM {deletedTableNames[0]} WHERE EVENT_ID in ({string.Join(",", eventIds)}) LIMIT 1)
";
			foreach (var deletedTableName in deletedTableNames.Skip(1).ToList())
			{
				sql += $@"UNION ALL
(SELECT '{deletedTableName}' as {nameof(Table.TableName)} FROM {deletedTableName} WHERE EVENT_ID in ({string.Join(",", eventIds)}) LIMIT 1)
";
			}

			for (var i = 1; i < registersWithLogicalDeleted.Count; i++)
			{
				deletedTableNames = GetDeletedTableNamesForRegister(registersWithLogicalDeleted[i]);
				foreach (var deletedTableName in deletedTableNames)
				{
					sql += $@"UNION ALL
(SELECT '{deletedTableName}' as {nameof(Table.TableName)} FROM {deletedTableName} WHERE EVENT_ID in ({string.Join(",", eventIds)}) LIMIT 1)
";
				}
			}

			_log.ForContext("SQL", sql)
				.Debug("Получение списка deleted таблиц, содержащих данные по указанным event_id");
			var deletedTableNamesWithEventId = QSQuery.ExecuteSql<Table>(sql)
				.Select(x => x.TableName).ToList();

			return deletedTableNamesWithEventId;
		}

		private List<string> GetDeletedTableNamesForRegister(OMRegistersWithSoftDeletion entity)
		{
			var deletedTableNames = new List<string>();
			RegisterData registerData = RegisterCache.GetRegisterData((int)entity.RegisterId);
			if (registerData.StorageType == StorageType.Type5)
			{
				if (registerData.AllpriPartitioning == Platform.Register.AllpriPartitioningType.DataType)
				{
					var postfixes = new List<string> { "TXT", "NUM", "DT" };
					foreach (var postfix in postfixes)
					{
						deletedTableNames.Add(GetDeletedTableName($"{registerData.AllpriTable}_{postfix}"));
					}
				}
				else if (registerData.AllpriPartitioning == Platform.Register.AllpriPartitioningType.AttributeId)
				{
					var attributesData = RegisterCache.RegisterAttributes.Values.ToList().Where(x => x.RegisterId == entity.RegisterId).ToList();
					foreach (var attributeData in attributesData)
					{
						if (attributeData.IsPrimaryKey || attributeData.IsDeleted)
							continue;

						deletedTableNames.Add(GetDeletedTableName($"{registerData.AllpriTable}_{attributeData.Id}"));
					}
				}
			}
			else if (registerData.StorageType == StorageType.Type4)
			{
				deletedTableNames.Add(GetDeletedTableName(entity.MainTableName));
			}

			return deletedTableNames;
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

			var recycleBinRegisterData = RegisterCache.GetRegisterData(OMRecycleBin.GetRegisterId());
			sql += $@"
DELETE FROM {recycleBinRegisterData.QuantTable} WHERE {recycleBinRegisterData.PKField} IN ({string.Join(",", recycleBins.Select(x => x.EventId))});";

			using (Operation.Time("Выполнение скрипта по очистке данных"))
			{
				ExecuteCommands(new List<string> { sql });
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

		public class RegisterObjects
		{
			public int RegisterId { get;}
			public List<long> ObjectIds { get; }
			public string ObjectIdsSqlSearchQuery { get;}

			public RegisterObjects(int registerId)
			{
				RegisterId = registerId;
			}

			public RegisterObjects(int registerId, List<long> objectIds)
			{
				RegisterId = registerId;
				ObjectIds = objectIds;
			}

			public RegisterObjects(int registerId, string objectIdsSqlSearchQuery)
			{
				RegisterId = registerId;
				ObjectIdsSqlSearchQuery = objectIdsSqlSearchQuery;
			}

			public virtual string GetSqlSearchPredicate()
			{
				RegisterData registerData = RegisterCache.GetRegisterData(RegisterId);

				if (!string.IsNullOrEmpty(ObjectIdsSqlSearchQuery))
				{
					return $"WHERE {registerData.PKField} in ({ObjectIdsSqlSearchQuery})";
				}

				if (ObjectIds.IsNotEmpty())
				{
					return $"WHERE {registerData.PKField} in ({string.Join(",", ObjectIds)})";
				}

				return string.Empty;
			}
		}

		public class GbuRegisterObjects : RegisterObjects
		{
			public long ChangeDocId { get; }
			public string ObjectIdsSql { get; }

			public GbuRegisterObjects(int registerId, long changeDocId, string objectIdsSql) : base(registerId)
			{
				ChangeDocId = changeDocId;
				ObjectIdsSql = objectIdsSql;
			}

			public override string GetSqlSearchPredicate()
			{
				return $"WHERE change_doc_id={ChangeDocId} and object_id in ({ObjectIdsSql})";
			}
		}
		#endregion Entities
	}
}
