using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Core.ErrorManagment;
using Core.Register.LongProcessManagment;
using Core.Shared.Extensions;
using KadOzenka.Dal.CancellationQueryManager;
using KadOzenka.Dal.LongProcess.Reports.CadastralCostDeterminationResults.Entities;
using KadOzenka.Dal.Registers.GbuRegistersServices;
using ObjectModel.Core.LongProcess;
using ObjectModel.Directory;
using ObjectModel.KO;
using Serilog;
using SerilogTimings.Extensions;

namespace KadOzenka.Dal.LongProcess.Reports.CadastralCostDeterminationResults
{
	public class BaseReportLongProcess : LongProcessForReportsBase
	{
		private readonly QueryManager _queryManager;
		private const int PackageSize = 200000;
		public static string IndividuallyResultsGroupNamePhrase => "индивидуального расчета";
		private string ReportName { get; set; }
		private string MessageSubject => $"Отчет '{ReportName}'";
		private object _locker;
		protected ILogger Logger { get; }


		public BaseReportLongProcess()
		{
			_queryManager = new QueryManager();
			_locker = new object();
			Logger = Log.ForContext<BaseReportLongProcess>();
		}

		public static void AddProcessToQueue(ReportLongProcessInputParameters input)
		{
			if (!AreInputParametersValid(input))
				throw new Exception("Не переданы параметры для построения отчета");

			LongProcessManager.AddTaskToQueue(nameof(BaseReportLongProcess), parameters: input.SerializeToXml());
		}

		public override void StartProcess(OMProcessType processType, OMQueue processQueue, CancellationToken cancellationToken)
		{
			_queryManager.SetBaseToken(cancellationToken);
			Logger.Debug("Начат фоновый процесс.");
			Logger.ForContext("InputParameters", processQueue.Parameters).Debug("Входные параметры");
			WorkerCommon.SetProgress(processQueue, 0);

			var parameters = processQueue.Parameters?.DeserializeFromXml<ReportLongProcessInputParameters>();
			if (!AreInputParametersValid(parameters))
			{
				NotificationSender.SendNotification(processQueue, MessageSubject, "Не переданы параметры для построения отчета");
				return;
			}

			var reportType = parameters.Type == ReportType.State ? typeof(StateResultsReport) : typeof(IndividuallyResultsReport);
			var taskIds = parameters.TaskIds;
			var taskIdStr = string.Join(',', taskIds);
			var report = (ICadastralCostDeterminationResultsReport)Activator.CreateInstance(reportType);
			ReportName = report.ReportName;
			Logger.Debug("Тип отчета {ReportType}", report.GetType().ToString());

			var message = string.Empty;
			try
			{
				using (Logger.TimeOperation("Общее время обработки всех пакетов"))
				{
					var groupIds = report.GetAvailableGroupIds();
					var groupIdsStr = string.Join(',', groupIds);
					Logger.Debug("Найдено {GroupsCount} Групп", groupIds.Count);

					var unitsCount = OMUnit.Where(x => taskIds.Contains((long)x.TaskId) && groupIds.Contains((long)x.GroupId) &&
					                                   x.PropertyType_Code != PropertyTypes.CadastralQuartal).ExecuteCount();
					Logger.Debug("Всего в БД {UnitsCount} ЕО.", unitsCount);
					if (unitsCount == 0)
					{
						message = "У заданий на оценку нет единиц оценки, принадлежащих к группе ";
						if (reportType == typeof(StateResultsReport))
						{
							message += $"не {IndividuallyResultsGroupNamePhrase}";
						}
						else
						{
							message += IndividuallyResultsGroupNamePhrase;
						}
						return;
					}


					var localCancelTokenSource = new CancellationTokenSource();
					var options = new ParallelOptions
					{
						CancellationToken = localCancelTokenSource.Token,
						MaxDegreeOfParallelism = 4
					};
					var cadastralQuarterAttributeId = new GbuCodRegisterService().GetCadastralQuarterFinalAttribute().Id;
					var numberOfPackages = unitsCount / PackageSize + 1;
					var processedPackageCount = 0;
					var processedItemsCount = 0;
					Parallel.For(0, numberOfPackages, options, (i, s) =>
					{
						var unitsCondition = $@"where unit.task_id IN ({taskIdStr}) AND 
									unit.GROUP_ID IN ({groupIdsStr}) AND
									(unit.PROPERTY_TYPE_CODE <> 2190 or unit.PROPERTY_TYPE_CODE is null)
										order by unit.id 
										limit {PackageSize} offset {i * PackageSize}";

						var sql = $@"/*{i}*/ with object_ids as (
									select object_id from ko_unit unit {unitsCondition}
								),
								cadastralDistrictAttrValues as (
									select * from  gbu_get_allpri_attribute_values( ARRAY(select object_id from object_ids), {cadastralQuarterAttributeId})
								)
								SELECT
									SUBSTRING(COALESCE(cadastralDistrictAttr.attributeValue, unit.CADASTRAL_BLOCK), 0, 6) as CadastralDistrict,
									unit.CADASTRAL_NUMBER AS CadastralNumber,
									unit.PROPERTY_TYPE AS Type,
									unit.SQUARE AS SQUARE,
									unit.UPKS AS UPKS,
									unit.CADASTRAL_COST AS Cost
										FROM KO_UNIT unit
										LEFT JOIN cadastralDistrictAttrValues cadastralDistrictAttr ON unit.object_id=cadastralDistrictAttr.objectId
										{unitsCondition}";
						Logger.Debug(new Exception(sql), "Начата работа с пакетом №{PackageNumber} из {MaxPackagesCount}", i, numberOfPackages);


						CheckCancellationToken(cancellationToken, localCancelTokenSource, options);
						List<ReportItem> currentOperations;
						using (Logger.TimeOperation("Сбор данных для пакета №{i}", i))
						{
							currentOperations = _queryManager.ExecuteSql<ReportItem>(sql).OrderBy(x => x.CadastralDistrict).ToList();
							processedItemsCount += currentOperations.Count;
							Logger.Debug("Выкачено {ProcessedItemsCount} записей", processedItemsCount);
						}

						CheckCancellationToken(cancellationToken, localCancelTokenSource, options);
						using (Logger.TimeOperation("Формирование файла для пакета №{i}", i))
						{
							GenerateReport(currentOperations, report);
						}

						lock (_locker)
						{
							processedPackageCount++;
							LongProcessProgressLogger.LogProgress(numberOfPackages, processedPackageCount, processQueue);
						}

						Logger.Debug("Закончена работа с пакетом №{PackageNumber}", i);
					});
				}
			}
			catch (OperationCanceledException)
			{
				message = "Формирование отчета было отменено пользователем";
				Logger.Error(message);
			}
			catch (Exception exception)
			{
				var errorId = ErrorManager.LogError(exception);
				Logger.Fatal(exception, "Ошибка построения отчета");

				message = $"Операция завершена с ошибкой: {exception.Message} (Подробнее в журнале: {errorId})";
			}
			finally
			{
				string urlToDownload;
				using (Logger.TimeOperation("Сохранение zip-файла"))
				{
					urlToDownload = CustomReportsService.SaveReport(report.ReportName);
				}

				SendMessageInternal(processQueue, message, urlToDownload);
			}

			WorkerCommon.SetProgress(processQueue, 100);
			Logger.Debug("Закончен фоновый процесс.");
		}


		#region Support Methods

		private static bool AreInputParametersValid(ReportLongProcessInputParameters parameters)
		{
			return parameters?.TaskIds != null && parameters.TaskIds.Count != 0;
		}

		private void SendMessageInternal(OMQueue processQueue, string mainMessage, string urlToDownload)
		{
			var fullMessage = string.IsNullOrWhiteSpace(urlToDownload)
				? mainMessage
				: mainMessage + "<br>" + $@"<a href=""{urlToDownload}"">Скачать результат</a>";

			NotificationSender.SendNotification(processQueue, MessageSubject, fullMessage);
		}

		private void GenerateReport(List<ReportItem> reportItems, ICadastralCostDeterminationResultsReport report)
		{
			var excelFileGenerator = new GemBoxExcelFileGenerator();

			var headerColumns = report.GenerateReportHeaders(reportItems);
			excelFileGenerator.AddHeaders(headerColumns);

			for (var i = 0; i < reportItems.Count; i++)
			{
				var values = report.GenerateReportReportRow(i, reportItems[i]);
				excelFileGenerator.AddRow(values);

				if (i % 100000 == 0)
					Logger.Debug("Обрабатывается строка №{i} из {reportItemsCount}.", i, reportItems.Count);
			}

			excelFileGenerator.SetIndividualWidth(headerColumns);
			excelFileGenerator.SetStyle();

			//попытка принудительно освободить память
			reportItems = null;
			GC.Collect();

			lock (_locker)
			{
				using (Logger.TimeOperation("Добавление zip-файла"))
				{
					CustomReportsService.AddExcelFileToGeneralZipArchive(excelFileGenerator.GetStream(), report.ReportName);
				}
			}
		}

		#endregion
	}
}
