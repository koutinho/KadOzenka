//using Core.Register.LongProcessManagment;
//using ObjectModel.Core.LongProcess;
//using ObjectModel.Directory;
//using Serilog;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading;
//using System.Threading.Tasks;
//using Core.ErrorManagment;
//using Core.Register.QuerySubsystem;
//using Core.Shared.Extensions;
//using KadOzenka.Dal.GbuObject;
//using KadOzenka.Dal.ManagementDecisionSupport.StatisticalData.PricingFactorsComposition;
//using ObjectModel.KO;
//using Microsoft.Practices.ObjectBuilder2;
//using SerilogTimings.Extensions;

//namespace KadOzenka.Dal.LongProcess.Reports.PricingFactorsComposition
//{
//	public class UniformReportLongProcess : LongProcess
//	{
//		private int _packageSize = 125000;
//		private string ReportName => "Итоговый состав данных по характеристикам объектов недвижимости";
//		private string MessageSubject => $"Отчет '{ReportName}'";
//		private static readonly ILogger Logger = Log.ForContext<UniformReportLongProcess>();
//		private static DataCompositionByCharacteristicsService DataCompositionByCharacteristicsService { get; set; }

//		public UniformReportLongProcess()
//		{
//			DataCompositionByCharacteristicsService = new DataCompositionByCharacteristicsService();
//		}



//		public static long AddProcessToQueue(UniformReportLongProcessInputParameters parameters)
//		{
//			if (parameters?.TaskIds == null || parameters.TaskIds.Count == 0)
//				throw new Exception("Не переданы ИД задач");

//			return LongProcessManager.AddTaskToQueue(nameof(UniformReportLongProcess), parameters: parameters.SerializeToXml());
//		}

//		public override void StartProcess(OMProcessType processType, OMQueue processQueue, CancellationToken cancellationToken)
//		{
//			WorkerCommon.SetProgress(processQueue, 0);

//			UniformReportLongProcessInputParameters parameters = null;
//			if (!string.IsNullOrWhiteSpace(processQueue.Parameters))
//			{
//				parameters = processQueue.Parameters.DeserializeFromXml<UniformReportLongProcessInputParameters>();
//				if (parameters.PackageSize.GetValueOrDefault() != 0)
//				{
//					_packageSize = parameters.PackageSize.GetValueOrDefault();
//				}
//			}
//			if (parameters?.TaskIds == null || parameters.TaskIds.Count == 0)
//			{
//				SendMessage(processQueue, "Не переданы ИД задач для построения отчета", MessageSubject);
//				return;
//			}

//			var urls = new List<string>();
//			try
//			{
//				//var reportItems = GetReportItems(taskIds);
//				//var urlToDownloadReport = GenerateReport(reportItems, new GbuReportService());
//				//var message = "Операция успешно завершена." + $@"<a href=""{urlToDownloadReport}"">Скачать результат</a>";
//				//SendMessage(processQueue, message, MessageSubject);
//				////TODO для тестирования
//				//var a = $"https://localhost:50252{urlToDownloadReport}";

//				Logger.Debug("Начат фоновый процесс.");

//				var unitsCount = OMUnit.Where(x => parameters.TaskIds.Contains((long)x.TaskId) && x.PropertyType_Code != PropertyTypes.CadastralQuartal).ExecuteCount();
//				Logger.Debug($"Всего в БД {unitsCount} ЕО.");

//				var processedItemsCount = 0;

//				var cancelTokenSource = new CancellationTokenSource();
//				var options = new ParallelOptions
//				{
//					CancellationToken = cancelTokenSource.Token,
//					MaxDegreeOfParallelism = 20
//				};

//				//var maxNumberOfPackages = unitsCount / _packageSize + 1;

//				using (Logger.TimeOperation($"Полная обработка трех пакетов"))
//				{
//					try
//					{
//						Parallel.For(0, 3, options, (i, s) =>
//						{
//							//if (processedItemsCount >= unitsCount)
//							//if (i >= 4)
//							//{
//							//	cancelTokenSource.Cancel();
//							//	options.CancellationToken.ThrowIfCancellationRequested();
//							//}

//							CheckCancellationToken(cancellationToken);
//							Logger.Debug($"Начата работа с пакетом №{i}");

//							using (Logger.TimeOperation($"Полная обработка пакета №{i} (сбор данных + генерация файла)"))
//							{
//								var objectIdsSubQuerySql = $@"select object_id from ko_unit 
//								where task_id in ({string.Join(',', parameters.TaskIds)}) and PROPERTY_TYPE_CODE <> 2190 
//								order by object_id 
//								limit {_packageSize} offset {i * _packageSize} ";

//								var sql = $@"select cadastral_number as CadastralNumber, attributes 
//								from {DataCompositionByCharacteristicsReportsLongProcessViaTables.TableName} 
//								where object_id in ({objectIdsSubQuerySql})";


//								CheckCancellationToken(cancellationToken);
//								List<ReportItem> currentPage;
//								using (Logger.TimeOperation($"Сбор данных для пакета №{i}"))
//								{
//									Logger.Debug(new Exception(sql), $"Начат сбор данных для пакета №{i}. До этого было обработано {processedItemsCount} записей");

//									currentPage = QSQuery.ExecuteSql<ReportItem>(sql);
//									processedItemsCount += currentPage.Count;

//									Logger.Debug(new Exception(sql), $"Закончен сбор данных для пакета №{i}.");
//								}

//								CheckCancellationToken(cancellationToken);
//								using (Logger.TimeOperation($"Формирование файла для пакета №{i}"))
//								{
//									Logger.Debug(new Exception(sql), $"Начата запись в файл для пакета №{i}.");

//									var reportService = new GbuReportService();
//									var urlToDownloadReport = GenerateReport(currentPage, reportService);
//									urls.Add(urlToDownloadReport);

//									Logger.ForContext("UrlToDownloadFile", urlToDownloadReport)
//										.Debug(new Exception(sql), $"Закончена запись в файл для пакета №{i}.");
//								}

//								Logger.Debug($"Закончена работа с пакетом №{i}");
//							}
//						});
//					}
//					catch (OperationCanceledException e)
//					{

//					}

//				}

//				SendMessageInternal(processQueue, "Операция успешно завершена.", urls);

//				Logger.Debug("Закончен фоновый процесс.");

//				////TODO для тестирования
//				//var a = $"https://localhost:50252{urlToDownloadReport}";
//			}
//			catch (Exception exception)
//			{
//				var errorId = ErrorManager.LogError(exception);
//				Logger.Fatal(exception, "Ошибка построения отчета");
//				var message = $"Операция завершена с ошибкой: {exception.Message} (Подробнее в журнале: {errorId})";
//				SendMessageInternal(processQueue, message, urls);
//			}

//			WorkerCommon.SetProgress(processQueue, 100);
//		}


//		#region Support Methods

//		private void SendMessageInternal(OMQueue processQueue, string mainMessage, List<string> urls)
//		{
//			var linksToUrls = new StringBuilder();
//			linksToUrls.AppendLine("Созданные файлы:");
//			urls.ForEach(url => { linksToUrls.AppendLine($@"<a href=""{url}"">Скачать результат</a>"); });

//			SendMessage(processQueue, mainMessage + "<br>" + linksToUrls, MessageSubject);
//		}

//		private void CheckCancellationToken(CancellationToken cancellationToken)
//		{
//			if (!cancellationToken.IsCancellationRequested)
//				return;

//			var message = "Формирование отчета было отменено пользователем";
//			Log.Error(message);
//			throw new Exception(message);
//		}

//		private List<ReportItem> GetReportItems(List<long> taskIds)
//		{
//			Logger.Debug("Начат сбор данных для отчета.");

//			var unitsCount = OMUnit.Where(x => taskIds.Contains((long)x.TaskId) && x.PropertyType_Code != PropertyTypes.CadastralQuartal).ExecuteCount();
//			Logger.Debug($"Всего в БД {unitsCount} ЕО.");

//			var operations = new List<ReportItem>();
//			var packageIndex = 0;
//			while (true)
//			{
//				if (operations.Count >= unitsCount)
//					break;

//				var objectIdsSubQuerySql = $@"select object_id from ko_unit 
//								where task_id in ({string.Join(',', taskIds)}) and PROPERTY_TYPE_CODE <> 2190 
//								order by object_id 
//								limit {_packageSize} offset {packageIndex * _packageSize} ";

//				var sql = $@"select cadastral_number as CadastralNumber, attributes 
//								from {DataCompositionByCharacteristicsReportsLongProcessViaTables.TableName} 
//								where object_id in ({objectIdsSubQuerySql})";

//				Logger.Debug(new Exception(sql), $"Начата работа с пакетом №{packageIndex}. До этого было выгружено {operations.Count} записей");
//				operations.AddRange(QSQuery.ExecuteSql<ReportItem>(sql));
//				Logger.Debug($"Закончена работа с пакетом №{packageIndex}");

//				packageIndex++;
//			}

//			Logger.Debug($"Закончен сбор данных для отчета. Собрано {operations.Count} объектов");

//			return operations;
//		}

//		private string GenerateReport(List<ReportItem> reportItems, GbuReportService reportService)
//		{
//			var locked = new object();

//			GenerateReportHeaders(reportItems, reportService, locked);

//			for (var i = 0; i < reportItems.Count; i++)
//			{
//				var currentInfo = reportItems[i];

//				var attributesInfo = new List<string>();
//				currentInfo.FullAttributes.ForEach(x =>
//				{
//					attributesInfo.Add(x.Name);
//					attributesInfo.Add(x.RegisterName);
//				});
//				var values = new List<string> { (i + 1).ToString(), currentInfo.CadastralNumber };
//				values.AddRange(attributesInfo);

//				lock (locked)
//				{
//					var row = reportService.GetCurrentRow();
//					reportService.AddRow(row, values);
//				}

//				if (i % 100000 == 0)
//					Logger.Debug($"Обрабатывается строка №{i} из {reportItems.Count}.");
//			}

//			reportService.SetStyle();
//			reportService.SaveReportZip(ReportName);

//			return reportService.UrlToDownload;
//		}

//		private void GenerateReportHeaders(List<ReportItem> info, GbuReportService reportService, object locked)
//		{
//			var sequentialNumberColumn = new GbuReportService.Column
//			{
//				Index = 0,
//				Header = "№п/п",
//				Width = 2
//			};

//			var cadastralNumberColumn = new GbuReportService.Column
//			{
//				Index = 1,
//				Header = "Кадастровый номер",
//				Width = 4
//			};

//			var maxNumberOfAttributes = info.Max(x => x.FullAttributes?.Count) ?? 0;
//			var columns = new List<GbuReportService.Column>(maxNumberOfAttributes + 2) { sequentialNumberColumn, cadastralNumberColumn };

//			//2 - чтобы учесть колонки с номером по порядку и КН
//			var columnWidth = 8;
//			var columnIndex = 2;
//			for (var i = 0; i < maxNumberOfAttributes; i++)
//			{
//				var characteristicColumn = new GbuReportService.Column
//				{
//					Header = $"Характеристика объекта {i + 1}",
//					Index = columnIndex,
//					Width = columnWidth
//				};
//				var sourceColumn = new GbuReportService.Column
//				{
//					Header = $"Итоговый источник информации {i + 1}",
//					Index = characteristicColumn.Index + 1,
//					Width = columnWidth
//				};

//				columns.Add(characteristicColumn);
//				columns.Add(sourceColumn);

//				columnIndex += 2;
//			}
//			lock (locked)
//			{
//				reportService.AddTitle("Итоговый состав данных по характеристикам объектов недвижимости", 4);
//				reportService.AddHeaders(columns);
//				reportService.SetIndividualWidth(columns);
//			}
//		}

//		#endregion


//		#region Entities

//		protected class Attribute
//		{
//			public string Name { get; set; }
//			public string RegisterName { get; set; }
//			public long RegisterId { get; set; }
//		}

//		private class ReportItem
//		{
//			private List<Attribute> _fullAttributes;

//			public string CadastralNumber { get; set; }
//			public long[] Attributes { get; set; }
//			public List<Attribute> FullAttributes => _fullAttributes ?? (_fullAttributes = GetUniqueAttributes());


//			private List<Attribute> GetUniqueAttributes()
//			{
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

//		#endregion
//	}
//}
