using Core.Register.LongProcessManagment;
using ObjectModel.Core.LongProcess;
using ObjectModel.Directory;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Core.ErrorManagment;
using Core.Main.FileStorages;
using Core.Register.QuerySubsystem;
using Core.Shared.Extensions;
using Core.SRD;
using GemBox.Spreadsheet;
using Ionic.Zip;
using KadOzenka.Dal.DataExport;
using KadOzenka.Dal.GbuObject;
using KadOzenka.Dal.ManagementDecisionSupport.StatisticalData.PricingFactorsComposition;
using Microsoft.Practices.EnterpriseLibrary.Data;
using ObjectModel.KO;
using Microsoft.Practices.ObjectBuilder2;
using ObjectModel.Common;
using ObjectModel.Directory.Common;
using ObjectModel.Gbu;
using SerilogTimings.Extensions;

namespace KadOzenka.Dal.LongProcess.Reports.PricingFactorsComposition
{
	//TODO связать с отчетом через DataCompositionByCharacteristicsService (убрать методы в сервис)
	public class UniformReportLongProcess : LongProcess
	{
		private int _packageSize = 125000;
		private string ReportName => "Итоговый состав данных по характеристикам объектов недвижимости";
		private string MessageSubject => $"Отчет '{ReportName}'";
		private static readonly ILogger Logger = Log.ForContext<UniformReportLongProcess>();
		private static DataCompositionByCharacteristicsService DataCompositionByCharacteristicsService { get; set; }

		public UniformReportLongProcess()
		{
			DataCompositionByCharacteristicsService = new DataCompositionByCharacteristicsService();
		}



		public static long AddProcessToQueue(UniformReportLongProcessInputParameters parameters)
		{
			if (parameters?.TaskIds == null || parameters.TaskIds.Count == 0)
				throw new Exception("Не переданы ИД задач");

			return LongProcessManager.AddTaskToQueue(nameof(UniformReportLongProcess), parameters: parameters.SerializeToXml());
		}

		public override void StartProcess(OMProcessType processType, OMQueue processQueue, CancellationToken cancellationToken)
		{
			Logger.Debug("Начат фоновый процесс.");
			WorkerCommon.SetProgress(processQueue, 0);

			UniformReportLongProcessInputParameters parameters = null;
			if (!string.IsNullOrWhiteSpace(processQueue.Parameters))
			{
				parameters = processQueue.Parameters.DeserializeFromXml<UniformReportLongProcessInputParameters>();
				if (parameters.PackageSize.GetValueOrDefault() != 0)
				{
					_packageSize = parameters.PackageSize.GetValueOrDefault();
				}
			}
			if (parameters?.TaskIds == null || parameters.TaskIds.Count == 0)
			{
				SendMessage(processQueue, "Не переданы ИД задач для построения отчета", MessageSubject);
				return;
			}

			if (!IsCacheTableExists())
				throw new Exception("Не найдена таблица с данными для отчета");

			var urls = new List<string>();
			try
			{
				var unitsCount = OMUnit.Where(x => parameters.TaskIds.Contains((long)x.TaskId) && x.PropertyType_Code != PropertyTypes.CadastralQuartal).ExecuteCount();
				Logger.Debug($"Всего в БД {unitsCount} ЕО.");

				var cancelTokenSource = new CancellationTokenSource();
				var options = new ParallelOptions
				{
					CancellationToken = cancelTokenSource.Token,
					MaxDegreeOfParallelism = 20
				};

				var numberOfPackages = unitsCount / _packageSize + 1;
				var processedItemsCount = 0;
				using (Logger.TimeOperation("Полная обработка всех пакетов"))
				{
					try
					{
						Parallel.For(0, numberOfPackages, options, (i, s) =>
						{
							if (processedItemsCount >= unitsCount)
							{
								cancelTokenSource.Cancel();
								options.CancellationToken.ThrowIfCancellationRequested();
							}

							CheckCancellationToken(cancellationToken);
							Logger.Debug($"Начата работа с пакетом №{i}");

							using (Logger.TimeOperation($"Полная обработка пакета №{i} (сбор данных + генерация файла)"))
							{
								var objectIdsSubQuerySql = $@"select object_id from ko_unit 
								where task_id in ({string.Join(',', parameters.TaskIds)}) and PROPERTY_TYPE_CODE <> 2190 
								order by object_id 
								limit {_packageSize} offset {i * _packageSize} ";

								var sql = $@"select cadastral_number as CadastralNumber, attributes 
								from {DataCompositionByCharacteristicsReportsLongProcessViaTables.TableName} 
								where object_id in ({objectIdsSubQuerySql})";

								CheckCancellationToken(cancellationToken);
								List<ReportItem> currentPage;
								using (Logger.TimeOperation($"Сбор данных для пакета №{i}"))
								{
									Logger.Debug(new Exception(sql), $"Начат сбор данных для пакета №{i}. До этого было обработано {processedItemsCount} записей");

									currentPage = QSQuery.ExecuteSql<ReportItem>(sql);
									processedItemsCount += currentPage.Count;
								}

								CheckCancellationToken(cancellationToken);
								using (Logger.TimeOperation($"Формирование файла для пакета №{i}"))
								{
									Logger.Debug(new Exception(sql), $"Начата запись в файл для пакета №{i}.");

									var urlToDownloadReport = GenerateReport(currentPage);
									urls.Add(urlToDownloadReport);

									Logger.ForContext("UrlToDownloadFile", urlToDownloadReport)
										.Debug(new Exception(sql), $"Закончена запись в файл для пакета №{i}.");
								}

								Logger.Debug($"Закончена работа с пакетом №{i}");
							}
						});
					}
					catch (OperationCanceledException)
					{
						Logger.Error("Закончена обработка, вызвана отмена токена");
					}
				}

				SendMessageInternal(processQueue, "Операция успешно завершена.", urls);

				Logger.Debug("Закончен фоновый процесс.");
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

		public bool IsCacheTableExists()
		{
			var isExists = false;

			var sql = $@"SELECT EXISTS (SELECT FROM information_schema.tables WHERE table_name = '{DataCompositionByCharacteristicsReportsLongProcessViaTables.TableName}') as {nameof(isExists)}";
			var command = DBMngr.Main.GetSqlStringCommand(sql);
			var dataTable = DBMngr.Main.ExecuteDataSet(command).Tables[0];

			if (dataTable.Rows.Count > 0)
			{
				isExists = dataTable.Rows[0][nameof(isExists)].ParseToBooleanNullable().GetValueOrDefault();
			}

			return isExists;
		}

		private void SendMessageInternal(OMQueue processQueue, string mainMessage, List<string> urls)
		{
			var linksToUrls = new StringBuilder();
			linksToUrls.AppendLine("Созданные файлы:");
			urls.ForEach(url => { linksToUrls.AppendLine($@"<a href=""{url}"">Скачать результат</a>"); });

			SendMessage(processQueue, mainMessage + "<br>" + linksToUrls, MessageSubject);
		}

		private void CheckCancellationToken(CancellationToken cancellationToken)
		{
			if (!cancellationToken.IsCancellationRequested)
				return;

			var message = "Формирование отчета было отменено пользователем";
			Logger.Error(message);
			throw new Exception(message);
		}

		private string GenerateReport(List<ReportItem> reportItems)
		{
			var headerColumns = GenerateReportHeaders(reportItems);

			var encoding = Encoding.GetEncoding(1251);
			MemoryStream stream;
			using (stream = new MemoryStream())
			{
				WriteToStream(headerColumns.Select(x => x.Header).ToList(), encoding, stream);

				for (var i = 0; i < reportItems.Count; i++)
				{
					var currentInfo = reportItems[i];

					var attributesInfo = new List<string>();
					currentInfo.FullAttributes.ForEach(x =>
					{
						attributesInfo.Add(x.Name);
						attributesInfo.Add(x.RegisterName);
					});
					var values = new List<string> { (i + 1).ToString(), currentInfo.CadastralNumber };
					values.AddRange(attributesInfo);

					WriteToStream(values, encoding, stream);

					if (i % 100000 == 0)
						Logger.Debug($"Обрабатывается строка №{i} из {reportItems.Count}.");
				}

				return SaveReportZip(stream, ReportName);
			}
		}

		public string SaveReportZip(MemoryStream stream, string fileName, long? mainRegisterId = null, string registerViewId = null, CsvSaveOptions csvSaveOptions = null)
		{
			try
			{
				using (var zipFile = new ZipFile())
				{
					zipFile.AlternateEncoding = Encoding.UTF8;
					zipFile.AlternateEncodingUsage = ZipOption.AsNecessary;

					using (Logger.TimeOperation($"Добавление файла '{fileName}' в zip"))
					{
						stream.Seek(0, SeekOrigin.Begin);
						zipFile.AddEntry($"{fileName}.csv", stream);
					}

					var zipStream = new MemoryStream();
					zipFile.Save(zipStream);
					zipStream.Seek(0, SeekOrigin.Begin);
					var zipFileName = $"{fileName} (архив)";

					using (Logger.TimeOperation($"Начато сохранение zip-файла '{zipFileName}'"))
					{
						var export = SaveReport(zipStream, zipFileName, "zip");
						//TODO CIPJSKO-645: убрать magic string и зависимость от DataExport
						return $"/DataExport/DownloadExportResult?exportId={export.Id}";
					}
				}
			}
			catch (Exception ex)
			{
				Logger.Error(ex, "Сохранение отчета завершилось исключением");
				ErrorManager.LogError(ex);
				throw;
			}
		}

		private void WriteToStream(List<string> str, Encoding encoding, MemoryStream stream)
		{
			str.Add("\n");
			var headers = string.Join(',', str);
			byte[] firstString = encoding.GetBytes(headers);
			stream.Write(firstString, 0, firstString.Length);
		}

		private List<GbuReportService.Column> GenerateReportHeaders(List<ReportItem> info)
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

			var maxNumberOfAttributes = info.Max(x => x.FullAttributes?.Count) ?? 0;
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

			return columns;
		}

		//TODO CIPJSKO-645: убрать зависимость от DataExporterCommon, нужно сохранять отчет в папку с отчетами
		public OMExportByTemplates SaveReport(MemoryStream stream, string fileName, string fileExtension)
		{
			var currentDate = DateTime.Now;
			var export = new OMExportByTemplates
			{
				UserId = SRDSession.GetCurrentUserId().GetValueOrDefault(),
				DateCreated = currentDate,
				DateStarted = currentDate,
				Status = (int)ImportStatus.Added,
				FileResultTitle = fileName,
				FileExtension = fileExtension,
				MainRegisterId = OMMainObject.GetRegisterId(),
				RegisterViewId = "GbuObjects"
			};
			export.Save();

			export.DateFinished = DateTime.Now;
			export.ResultFileName = DataExporterCommon.GetStorageResultFileName(export.Id);
			export.Status = (long)ImportStatus.Completed;
			FileStorageManager.Save(stream, DataExporterCommon.FileStorageName, export.DateFinished.Value, export.ResultFileName);
			export.Save();

			return export;
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
