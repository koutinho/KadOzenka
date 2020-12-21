﻿//using System;
//using System.Collections.Generic;
//using System.Collections.Specialized;
//using System.Data;
//using System.Linq;
//using System.Text;
//using Core.Register.QuerySubsystem;
//using Core.Shared.Extensions;
//using KadOzenka.Dal.LongProcess.Reports.PricingFactorsComposition;
//using KadOzenka.Dal.ManagementDecisionSupport.StatisticalData.PricingFactorsComposition;
//using Microsoft.Practices.ObjectBuilder2;
//using ObjectModel.Directory;
//using ObjectModel.Gbu;
//using ObjectModel.KO;
//using Serilog;

//namespace KadOzenka.Dal.FastReports.StatisticalData.PricingFactorsComposition
//{
//    public class UniformReport : DataCompositionByCharacteristicsBaseReport
//	{
//		private const int PackageSize = 100000;

//		private readonly ILogger _logger;
//		protected override ILogger Logger => _logger;

//		public UniformReport()
//		{
//			_logger = Log.ForContext<UniformReport>();
//		}

//		protected override string TemplateName(NameValueCollection query)
//        {
//            return "PricingFactorsCompositionUniformReport";
//        }

//        protected override DataSet GetDataCompositionByCharacteristicsReportData(NameValueCollection query, HashSet<long> objectList = null)
//        {
//            var taskIds = GetTaskIdList(query).ToList();
           
//            var unitsCount = OMUnit.Where(x => taskIds.Contains((long) x.TaskId) && x.PropertyType_Code != PropertyTypes.CadastralQuartal).ExecuteCount();
//            Logger.Debug($"Всего в БД {unitsCount} ЕО.");

//            var operations = new List<ReportItemNew>();
//            var packageIndex = 0;
//			while (true)
//			{
//				if (operations.Count >= unitsCount)
//					break;

//				var objectIdsSubQuerySql = $@"select object_id from ko_unit 
//								where task_id in ({string.Join(',', taskIds)}) and PROPERTY_TYPE_CODE <> 2190 
//								order by object_id 
//								limit {PackageSize} offset {packageIndex * PackageSize} ";

//				var sql = $@"select cadastral_number as CadastralNumber, attributes 
//								from {DataCompositionByCharacteristicsReportsLongProcessViaTables.TableName} 
//								where object_id in ({objectIdsSubQuerySql})";
				
//				Logger.Debug(new Exception(sql), $"Начата работа с пакетом №{packageIndex}. До этого было выгружено {operations.Count} записей");
//				operations.AddRange(QSQuery.ExecuteSql<ReportItemNew>(sql));
//				Logger.Debug($"Закончена работа с пакетом №{packageIndex}");

//				packageIndex++;
//            }

//			Logger.Debug("Начато формирование таблиц");
//			var dataSet = new DataSet();
//			var itemTable = GetItemDataTable(operations);
//			dataSet.Tables.Add(itemTable);
//			Logger.Debug("Закончено формирование таблиц");

//			return dataSet;
//        }

//   //     protected DataSet GetDataCompositionByCharacteristicsReportDataOld(NameValueCollection query, HashSet<long> objectList = null)
//   //     {
//	  //      var taskIds = GetTaskIdList(query).ToList();

//			//var operations = GetOperations<ReportItem>(taskIds, Logger);
//			//Logger.Debug("Найдено {Count} объектов", operations?.Count);

//			//Logger.Debug("Начато формирование таблиц");
//	  //      var dataSet = new DataSet();
//	  //      var itemTable = GetItemDataTable(operations);
//	  //      dataSet.Tables.Add(itemTable);
//	  //      Logger.Debug("Закончено формирование таблиц");

//	  //      return dataSet;
//   //     }



//		#region Support Methods

//		private DataTable GetItemDataTable(List<ReportItemNew> operations)
//        {
//            var titleForCharacteristic = "Характеристика объекта";
//            var titleForSource = "Итоговый источник информации";

//            var dataTable = new DataTable("Data");

//            dataTable.Columns.Add("Number");
//            dataTable.Columns.Add("CadastralNumber");
//            dataTable.Columns.Add("CharacteristicNameTitle");
//            dataTable.Columns.Add("CharacteristicName");

//            //для формирования матрицы нужен дубляж значения всех строк кроме характеристик
//            for (var i = 0; i < operations.Count; i++)
//            {
//	            var index = i + 1;

//				if (operations[i].FullAttributes.Count == 0)
//                {
//                    dataTable.Rows.Add(index,
//                        operations[i].CadastralNumber,
//                        $"{titleForCharacteristic} 1",
//                        string.Empty);

//                    dataTable.Rows.Add(index,
//                        operations[i].CadastralNumber,
//                        $"{titleForSource} 1",
//                        string.Empty);
//                }
//                else
//                {
//                    for (var j = 0; j < operations[i].FullAttributes.Count; j++)
//                    {
//                        for (var counter = 0; counter < 2; counter++)
//                        {
//                            string title, value;
//                            if (counter % 2 == 0)
//                            {
//                                title = $"{titleForCharacteristic} {j + 1}";
//                                value = operations[i].FullAttributes.ElementAtOrDefault(j)?.Name;
//                            }
//                            else
//                            {
//                                title = $"{titleForSource} {j + 1}";
//                                value = operations[i].FullAttributes.ElementAtOrDefault(j)?.RegisterName;
//                            }

//                            dataTable.Rows.Add(index,
//                                operations[i].CadastralNumber,
//                                title,
//                                value);
//                        }
//                    }
//                }
//            }

//            return dataTable;
//        }

//		#endregion


//		#region Entities

//		private class ReportItemNew
//		{
//			private List<Attribute> _fullAttributes;

//			public string CadastralNumber { get; set; }
//			public long[] Attributes { get; set; }
//			public List<Attribute> FullAttributes => _fullAttributes ?? (_fullAttributes = GetUniqueAttributes());


//			private List<Attribute> GetUniqueAttributes()
//			{
//				if(Attributes == null || Attributes.Length == 0)
//					return new List<Attribute>();

//				var objectAttributes = new List<Attribute>();
//				Attributes.ForEach(attributeId =>
//				{
//					var attribute = DataCompositionByCharacteristicsService.CachedAttributes.FirstOrDefault(x => x.Id == attributeId);
//					var register = DataCompositionByCharacteristicsService.CachedRegisters.FirstOrDefault(x => x.Id == attribute?.RegisterId);
//					if (attribute == null || register == null)
//						return;

//					objectAttributes.Add(new Attribute
//					{
//						Name = attribute.Name,
//						RegisterId = register.Id,
//						RegisterName = register.Description
//					});
//				});

//				if (objectAttributes.Count == 0)
//					return new List<Attribute>();

//				var gbuAttributesExceptRosreestr = objectAttributes
//					.Where(x => x.RegisterId != DataCompositionByCharacteristicsService.RosreestrRegisterId).ToList();
//				var rosreestrAttributes = objectAttributes
//					.Where(x => x.RegisterId == DataCompositionByCharacteristicsService.RosreestrRegisterId).ToList();

//				//симметрическая разность множеств
//				var uniqueAttributes = new List<Attribute>();
//				//отбираем уникальные аттрибуты из РР
//				rosreestrAttributes.ForEach(rr =>
//				{
//					var isSameAttributesExist = gbuAttributesExceptRosreestr.Any(gbu =>
//						gbu.Name.StartsWith(rr.Name, StringComparison.InvariantCultureIgnoreCase));

//					if (!isSameAttributesExist)
//						uniqueAttributes.Add(rr);
//				});
//				//отбираем уникальные аттрибуты из всех источников кроме РР
//				gbuAttributesExceptRosreestr.ForEach(gbu =>
//				{
//					var isSameAttributesExist = rosreestrAttributes.Any(rr =>
//						gbu.Name.StartsWith(rr.Name, StringComparison.InvariantCultureIgnoreCase));

//					if (!isSameAttributesExist)
//						uniqueAttributes.Add(gbu);
//				});

//				return uniqueAttributes;
//			}
//		}


//		private class ReportItem
//		{
//			private List<Attribute> _fullAttributes;

//			public string CadastralNumber { get; set; }
//			public string[] Attributes { get; set; }
//			public List<Attribute> FullAttributes => _fullAttributes ?? (_fullAttributes = GetUniqueAttributes());


//			private List<Attribute> GetUniqueAttributes()
//			{
//				var objectAttributes = new List<Attribute>();
//				Attributes.Where(attribute => !string.IsNullOrWhiteSpace(attribute)).ForEach(attributeIdStr =>
//				{
//					foreach (var processedAttributeId in attributeIdStr.Split(','))
//					{
//						var attribute = DataCompositionByCharacteristicsService.CachedAttributes.FirstOrDefault(x => x.Id == processedAttributeId.ParseToLong());
//						var register = DataCompositionByCharacteristicsService.CachedRegisters.FirstOrDefault(x => x.Id == attribute?.RegisterId);
//						if (attribute == null || register == null)
//							continue;

//						objectAttributes.Add(new Attribute
//						{
//							Name = attribute.Name,
//							RegisterId = register.Id,
//							RegisterName = register.Description
//						});
//					}
//				});

//				if (objectAttributes.Count == 0)
//					return new List<Attribute>();

//				var gbuAttributesExceptRosreestr = objectAttributes
//					.Where(x => x.RegisterId != DataCompositionByCharacteristicsService.RosreestrRegisterId).ToList();
//				var rosreestrAttributes = objectAttributes
//					.Where(x => x.RegisterId == DataCompositionByCharacteristicsService.RosreestrRegisterId).ToList();

//				//симметрическая разность множеств
//				var uniqueAttributes = new List<Attribute>();
//				//отбираем уникальные аттрибуты из РР
//				rosreestrAttributes.ForEach(rr =>
//				{
//					var isSameAttributesExist = gbuAttributesExceptRosreestr.Any(gbu =>
//						gbu.Name.StartsWith(rr.Name, StringComparison.InvariantCultureIgnoreCase));

//					if (!isSameAttributesExist)
//						uniqueAttributes.Add(rr);
//				});
//				//отбираем уникальные аттрибуты из всех источников кроме РР
//				gbuAttributesExceptRosreestr.ForEach(gbu =>
//				{
//					var isSameAttributesExist = rosreestrAttributes.Any(rr =>
//						gbu.Name.StartsWith(rr.Name, StringComparison.InvariantCultureIgnoreCase));

//					if (!isSameAttributesExist)
//						uniqueAttributes.Add(gbu);
//				});

//				return uniqueAttributes;
//			}
//		}

//		#endregion
//	}
//}
