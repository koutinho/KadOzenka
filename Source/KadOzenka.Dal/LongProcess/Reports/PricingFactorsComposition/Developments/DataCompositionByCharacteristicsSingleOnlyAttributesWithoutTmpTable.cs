//using System;
//using System.Collections.Generic;
//using System.Text;
//using System.Threading;
//using Core.Register.LongProcessManagment;
//using Core.Register.QuerySubsystem;
//using Microsoft.Practices.EnterpriseLibrary.Data;
//using ObjectModel.Core.LongProcess;
//using Serilog;
//using SerilogTimings.Extensions;

//namespace KadOzenka.Dal.LongProcess.Reports.PricingFactorsComposition.Developments
//{
//	//5 sec
//	public class DataCompositionByCharacteristicsSingleOnlyAttributesWithoutTmpTable : LongProcess
//	{
//		private static readonly ILogger Logger = Log.ForContext<DataCompositionByCharacteristicsSingleOnlyAttributesWithoutTmpTable>();


//		public static long AddProcessToQueue()
//		{
//			return LongProcessManager.AddTaskToQueue(nameof(DataCompositionByCharacteristicsSingleOnlyAttributesWithTmpTable));
//		}

//		public override void StartProcess(OMProcessType processType, OMQueue processQueue, CancellationToken cancellationToken)
//		{
//			Logger.Debug("Старт фонового процесса: {Description}.", processType.Description);

//			var objectsToUpdate = GetObjectsToUpdate();

//			using (Logger.TimeOperation("Полное время работы процесса"))
//			{
//				var sb = new StringBuilder();
//				objectsToUpdate.ForEach(x => { sb.Append($"({x.ObjectId}, ARRAY[{string.Join(',', x.Attributes)}]::bigint[]),"); });
//				sb.Length--;

//				var sql = $@"update data_composition_by_characteristics_by_tables as cache_table set
//						    attributes = ARRAY(SELECT DISTINCT UNNEST(cache_table.attributes || tmp.attributes))
//							from (values {sb} ) as tmp(object_id, attributes) 
//							where tmp.object_id = cache_table.object_id";

//				Logger.Debug(new Exception(sql), "Sql-запрос");

//				var command = DBMngr.Main.GetSqlStringCommand(sql);
//				DBMngr.Main.ExecuteNonQuery(command);
//			}

//			Logger.Debug("Финиш фонового процесса: {Description}.", processType.Description);
//		}


//		#region Support Methods

//		private List<ObjectNewAttribute> GetObjectsToUpdate()
//		{
//			var sql = "select object_id as objectId, attributes from data_composition_by_characteristics_tmp";

//			return QSQuery.ExecuteSql<ObjectNewAttribute>(sql);
//		}

//		public class ObjectNewAttribute
//		{
//			public long ObjectId { get; set; }
//			public long[] Attributes { get; set; }
//		}

//		#endregion
//	}
//}
