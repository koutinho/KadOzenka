using Core.Register.LongProcessManagment;
using ObjectModel.Core.LongProcess;
using ObjectModel.Directory;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Core.ErrorManagment;
using Core.Register.QuerySubsystem;
using Core.Shared.Extensions;
using KadOzenka.Dal.GbuObject;
using KadOzenka.Dal.ManagementDecisionSupport.StatisticalData.PricingFactorsComposition;
using ObjectModel.KO;
using Microsoft.Practices.ObjectBuilder2;
using SerilogTimings.Extensions;

namespace KadOzenka.Dal.LongProcess.Reports.PricingFactorsComposition
{
	public class UniformReportLongProcess : LongProcess
	{
		private const int PackageSize = 500000;
		private string ReportName => "Итоговый состав данных по характеристикам объектов недвижимости";
		private string MessageSubject => $"Отчет '{ReportName}'";
		private static readonly ILogger Logger = Log.ForContext<UniformReportLongProcess>();
		private static DataCompositionByCharacteristicsService DataCompositionByCharacteristicsService { get; set; }

		public UniformReportLongProcess()
		{
			DataCompositionByCharacteristicsService = new DataCompositionByCharacteristicsService();
		}



		public static long AddProcessToQueue(List<long> taskIds)
		{
			if (taskIds == null)
				throw new Exception("Не переданы ИД задач");

			return LongProcessManager.AddTaskToQueue(nameof(UniformReportLongProcess), parameters: taskIds.SerializeToXml());
		}

		public override void StartProcess(OMProcessType processType, OMQueue processQueue, CancellationToken cancellationToken)
		{
			WorkerCommon.SetProgress(processQueue, 0);

			List<long> taskIds = null;
			if (!string.IsNullOrWhiteSpace(processQueue.Parameters))
			{
				taskIds = processQueue.Parameters.DeserializeFromXml<List<long>>();
			}
			if (taskIds == null || taskIds.Count == 0)
			{
				SendMessage(processQueue, "Не переданы ИД задач для построения отчета", MessageSubject);
				return;
			}

			var urls = new List<string>();
			try
			{
				//var reportItems = GetReportItems(taskIds);

				//var urlToDownloadReport = GenerateReport(reportItems, new GbuReportService());

				//var message = "Операция успешно завершена." + $@"<a href=""{urlToDownloadReport}"">Скачать результат</a>";
				//SendMessage(processQueue, message, MessageSubject);

				////TODO для тестирования
				//var a = $"https://localhost:50252{urlToDownloadReport}";

				Logger.Debug("Начат фоновый процесс.");

				var unitsCount = OMUnit.Where(x => taskIds.Contains((long)x.TaskId) && x.PropertyType_Code != PropertyTypes.CadastralQuartal).ExecuteCount();
				Logger.Debug($"Всего в БД {unitsCount} ЕО.");

				var executedItemsCount = 0;
				var packageIndex = 0;
				while (true)
				{
					if (executedItemsCount >= unitsCount)
						break;

					Logger.Debug($"Начата работа с пакетом №{packageIndex}");

					using (Logger.TimeOperation($"Полная обработка пакета №{packageIndex} (сбор данных + генерация файла)"))
					{
						var objectIdsSubQuerySql = $@"select object_id from ko_unit 
								where task_id in ({string.Join(',', taskIds)}) and PROPERTY_TYPE_CODE <> 2190 
								order by object_id 
								limit {PackageSize} offset {packageIndex * PackageSize} ";

						var sql = $@"select cadastral_number as CadastralNumber, attributes 
								from {DataCompositionByCharacteristicsReportsLongProcessViaTables.TableName} 
								where object_id in ({objectIdsSubQuerySql})";


						List<ReportItem> currentPage;
						using (Logger.TimeOperation($"Сбор данных для пакета №{packageIndex}"))
						{
							Logger.Debug(new Exception(sql),
								$"Начат сбор данных для пакета №{packageIndex}. До этого было обработано {executedItemsCount} записей");

							currentPage = QSQuery.ExecuteSql<ReportItem>(sql);
							executedItemsCount += currentPage.Count;

							Logger.Debug(new Exception(sql), $"Закончен сбор данных для пакета №{packageIndex}.");
						}

						using (Logger.TimeOperation($"Формирование файла для пакета №{packageIndex}"))
						{
							Logger.Debug(new Exception(sql), $"Начата запись в файл для пакета №{packageIndex}.");

							var reportService = new GbuReportService();
							var urlToDownloadReport = GenerateReport(currentPage, reportService);
							urls.Add(urlToDownloadReport);

							Logger.Debug(new Exception(sql), $"Закончена запись в файл для пакета №{packageIndex}.");
						}

						Logger.Debug($"Закончена работа с пакетом №{packageIndex}");
					}

					packageIndex++;
				}

				SendMessageInternal(processQueue, "Операция успешно завершена.", urls);

				Logger.Debug("Закончен фоновый процесс.");

				////TODO для тестирования
				//var a = $"https://localhost:50252{urlToDownloadReport}";
			}
			catch (Exception exception)
			{
				var errorId = ErrorManager.LogError(exception);
				Logger.Fatal(exception, "Ошибка построения отчета");
				var message = $"Операция завершена с ошибкой: {exception.Message} (Подробнее в журнале: {errorId})";
				SendMessageInternal(processQueue, message, urls);
			}
			
			WorkerCommon.SetProgress(processQueue, 100);
		}


		#region Support Methods

		private void SendMessageInternal(OMQueue processQueue, string mainMessage, List<string> urls)
		{
			var linksToUrls = new StringBuilder();
			linksToUrls.AppendLine("Созданные файлы:").AppendLine();
			urls.ForEach(url => { linksToUrls.AppendLine($@"<a href=""{url}"">Скачать результат</a>"); });

			SendMessage(processQueue, mainMessage + "<br>" + linksToUrls, MessageSubject);
		}

		private List<ReportItem> GetReportItems(List<long> taskIds)
		{
			Logger.Debug("Начат сбор данных для отчета.");

			var unitsCount = OMUnit.Where(x => taskIds.Contains((long)x.TaskId) && x.PropertyType_Code != PropertyTypes.CadastralQuartal).ExecuteCount();
			Logger.Debug($"Всего в БД {unitsCount} ЕО.");

			var operations = new List<ReportItem>();
			var packageIndex = 0;
			while (true)
			{
				if (operations.Count >= unitsCount)
					break;

				var objectIdsSubQuerySql = $@"select object_id from ko_unit 
								where task_id in ({string.Join(',', taskIds)}) and PROPERTY_TYPE_CODE <> 2190 
								order by object_id 
								limit {PackageSize} offset {packageIndex * PackageSize} ";

				var sql = $@"select cadastral_number as CadastralNumber, attributes 
								from {DataCompositionByCharacteristicsReportsLongProcessViaTables.TableName} 
								where object_id in ({objectIdsSubQuerySql})";

				Logger.Debug(new Exception(sql), $"Начата работа с пакетом №{packageIndex}. До этого было выгружено {operations.Count} записей");
				operations.AddRange(QSQuery.ExecuteSql<ReportItem>(sql));
				Logger.Debug($"Закончена работа с пакетом №{packageIndex}");

				packageIndex++;
			}

			Logger.Debug($"Закончен сбор данных для отчета. Собрано {operations.Count} объектов");

			return operations;
		}

		private string GenerateReport(List<ReportItem> reportItems, GbuReportService reportService)
		{
			GenerateReportHeaders(reportItems, reportService);

			for (var i = 0; i < reportItems.Count; i++)
			{
				var currentInfo = reportItems[i];

				var attributesInfo = new List<string>();
				currentInfo.FullAttributes.ForEach(x =>
				{
					attributesInfo.Add(x.Name);
					attributesInfo.Add(x.RegisterName);
				});
				var values = new List<string> {(i + 1).ToString(), currentInfo.CadastralNumber};
				values.AddRange(attributesInfo);

				var row = reportService.GetCurrentRow();
				reportService.AddRow(row, values);

				if (i % 100000 == 0)
					Logger.Debug($"Обрабатывается строка №{i} из {reportItems.Count}.");
			}

			reportService.SetStyle();
			reportService.SaveReport(ReportName);

			return reportService.UrlToDownload;
		}

		private void GenerateReportHeaders(List<ReportItem> info, GbuReportService reportService)
		{
			var sequentialNumberColumn = new GbuReportService.Column
			{
				Index = 0,
				Header = "№п/п",
				Width = 2
			};

			var cadastralNumberColumn = new GbuReportService.Column
			{
				Index = 1,
				Header = "Кадастровый номер",
				Width = 4
			};

			var maxNumberOfAttributes = info.Max(x => x.Attributes?.Length) ?? 0;
			var columns = new List<GbuReportService.Column>(maxNumberOfAttributes + 2) { sequentialNumberColumn, cadastralNumberColumn };

			//2 - чтобы учесть колонки с номером по порядку и КН
			var columnWidth = 8;
			var columnIndex = 2;
			for (var i = 0; i < maxNumberOfAttributes; i++)
			{
				var characteristicColumn = new GbuReportService.Column
				{
					Header = $"Характеристика объекта {i + 1}",
					Index = columnIndex,
					Width = columnWidth
				};
				var sourceColumn = new GbuReportService.Column
				{
					Header = $"Итоговый источник информации {i + 1}",
					Index = characteristicColumn.Index + 1,
					Width = columnWidth
				};

				columns.Add(characteristicColumn);
				columns.Add(sourceColumn);

				columnIndex += 2;
			}

			reportService.AddTitle("Итоговый состав данных по характеристикам объектов недвижимости", 4);
			reportService.AddHeaders(columns);
			reportService.SetIndividualWidth(columns);
		}

		#endregion


		#region Entities

		protected class Attribute
		{
			public string Name { get; set; }
			public string RegisterName { get; set; }
			public long RegisterId { get; set; }
		}

		private class ReportItem
		{
			private List<Attribute> _fullAttributes;

			public string CadastralNumber { get; set; }
			public long[] Attributes { get; set; }
			public List<Attribute> FullAttributes => _fullAttributes ?? (_fullAttributes = GetUniqueAttributes());


			private List<Attribute> GetUniqueAttributes()
			{
				var objectAttributes = new List<Attribute>();
				Attributes.ForEach(attributeId =>
				{
					var attribute = DataCompositionByCharacteristicsService.CachedAttributes.FirstOrDefault(x => x.Id == attributeId);
					var register = DataCompositionByCharacteristicsService.CachedRegisters.FirstOrDefault(x => x.Id == attribute?.RegisterId);
					if (attribute == null || register == null)
						return;

					objectAttributes.Add(new Attribute
					{
						Name = attribute.Name,
						RegisterId = register.Id,
						RegisterName = register.Description
					});
				});

				if (objectAttributes.Count == 0)
					return new List<Attribute>();

				var gbuAttributesExceptRosreestr = objectAttributes
					.Where(x => x.RegisterId != DataCompositionByCharacteristicsService.RosreestrRegisterId).ToList();
				var rosreestrAttributes = objectAttributes
					.Where(x => x.RegisterId == DataCompositionByCharacteristicsService.RosreestrRegisterId).ToList();

				//симметрическая разность множеств
				var uniqueAttributes = new List<Attribute>();
				//отбираем уникальные аттрибуты из РР
				rosreestrAttributes.ForEach(rr =>
				{
					var isSameAttributesExist = gbuAttributesExceptRosreestr.Any(gbu =>
						gbu.Name.StartsWith(rr.Name, StringComparison.InvariantCultureIgnoreCase));

					if (!isSameAttributesExist)
						uniqueAttributes.Add(rr);
				});
				//отбираем уникальные аттрибуты из всех источников кроме РР
				gbuAttributesExceptRosreestr.ForEach(gbu =>
				{
					var isSameAttributesExist = rosreestrAttributes.Any(rr =>
						gbu.Name.StartsWith(rr.Name, StringComparison.InvariantCultureIgnoreCase));

					if (!isSameAttributesExist)
						uniqueAttributes.Add(gbu);
				});

				return uniqueAttributes;
			}
		}

		#endregion
	}
}
