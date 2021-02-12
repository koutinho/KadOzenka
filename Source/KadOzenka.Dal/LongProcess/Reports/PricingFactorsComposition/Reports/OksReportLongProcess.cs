using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using KadOzenka.Dal.CancellationQueryManager;
using KadOzenka.Dal.ManagementDecisionSupport.StatisticalData.Entities;
using ObjectModel.Core.LongProcess;
using Serilog;
using System.Threading.Tasks;
using Core.ErrorManagment;
using Core.Register.LongProcessManagment;
using Core.Shared.Extensions;
using KadOzenka.Dal.GbuObject;
using KadOzenka.Dal.LongProcess.Reports.PricingFactorsComposition.Entities;
using KadOzenka.Dal.ManagementDecisionSupport.StatisticalData;
using KadOzenka.Dal.Registers.GbuRegistersServices;
using ObjectModel.Directory;
using ObjectModel.KO;
using SerilogTimings.Extensions;

namespace KadOzenka.Dal.LongProcess.Reports.PricingFactorsComposition.Reports
{
	public class OksReportLongProcess : LongProcessForReportsBase
	{
		public static readonly string DateFormat = "dd.MM.yyyy";
		protected readonly string BaseFolderWithSql = "PricingFactorsComposition";
		private int _packageSize = 150000;
		private object _locker;
		public string ReportName => "Состав данных по перечню объектов недвижимости, подлежащих государственной кадастровой оценке (объекты капитального строительства)";
		private string MessageSubject => $"Отчет '{ReportName}'";
		protected ILogger Logger { get; set; }
		protected StatisticalDataService StatisticalDataService { get; set; }
		protected RosreestrRegisterService RosreestrRegisterService { get; set; }
		private readonly QueryManager _queryManager;


		public OksReportLongProcess()
		{
			_locker = new object();
			_queryManager = new QueryManager();
			StatisticalDataService = new StatisticalDataService();
			RosreestrRegisterService = new RosreestrRegisterService();
			Logger = Log.ForContext<OksReportLongProcess>();
		}

		public override void AddToQueue(object input)
		{
			var parameters = input as ReportLongProcessInputParameters;
			if (!AreInputParametersValid(parameters))
				throw new Exception("Не переданы ИД задач");

			LongProcessManager.AddTaskToQueue(nameof(OksReportLongProcess), parameters: parameters.SerializeToXml());
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

			var message = string.Empty;
			try
			{
				using (Logger.TimeOperation("Общее время обработки всех пакетов"))
				{
					var unitsCount = OMUnit.Where(x => parameters.TaskIds.Contains((long)x.TaskId) &&
													   x.PropertyType_Code != PropertyTypes.Stead &&
													   x.PropertyType_Code != PropertyTypes.CadastralQuartal).ExecuteCount();
					Logger.Debug("Всего в БД {UnitsCount} ЕО.", unitsCount);
					if (unitsCount == 0)
					{
						message = "У заданий на оценку нет единиц оценки";
						return;
					}


					var tourId = OMTask.Where(x => x.Id == parameters.TaskIds[0]).Select(x => x.TourId).ExecuteFirstOrDefault().TourId.GetValueOrDefault();
					var taskIdStr = string.Join(',', parameters.TaskIds);
					var baseSql = GetBaseSql(tourId);
					var localCancelTokenSource = new CancellationTokenSource();
					var options = new ParallelOptions
					{
						CancellationToken = localCancelTokenSource.Token,
						MaxDegreeOfParallelism = 4
					};
					var numberOfPackages = unitsCount / _packageSize + 1;
					var processedPackageCount = 0;
					var processedItemsCount = 0;
					Parallel.For(0, numberOfPackages, options, (i, s) =>
					{
						var unitsCondition = $@"where unit.TASK_ID IN ({taskIdStr}) AND 
										unit.PROPERTY_TYPE_CODE <> 4 AND unit.PROPERTY_TYPE_CODE<>2190 AND 
										unit.OBJECT_ID IS NOT NULL
										order by unit.id 
										limit {_packageSize} offset {i * _packageSize}";

						var sql = string.Format(baseSql, unitsCondition);
						Logger.Debug(new Exception(sql), "Начата работа с пакетом №{PackageNumber} из {MaxPackagesCount}", i, numberOfPackages);

						CheckCancellationToken(cancellationToken, localCancelTokenSource, options);
						List<ReportItem> currentOperations;
						using (Logger.TimeOperation("Сбор данных для пакета №{i}", i))
						{
							//TODO
							currentOperations = _queryManager.ExecuteSql<ReportItem>(sql).OrderBy(x => x.CadastralNumber).ToList();
							processedItemsCount += currentOperations.Count;
							Logger.Debug("Выкачено {ProcessedItemsCount} записей", processedItemsCount);
						}

						CheckCancellationToken(cancellationToken, localCancelTokenSource, options);
						using (Logger.TimeOperation("Формирование файла для пакета №{i}", i))
						{
							GenerateReport(currentOperations);
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
					urlToDownload = CustomReportsService.SaveReport(ReportName);
				}

				SendMessageInternal(processQueue, message, urlToDownload);
			}

			WorkerCommon.SetProgress(processQueue, 100);
			Logger.Debug("Закончен фоновый процесс.");
		}


		#region Support Methods

		private bool AreInputParametersValid(ReportLongProcessInputParameters parameters)
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

		private string GetBaseSql(long tourId)
		{
			var sql = StatisticalDataService.GetSqlFileContent(BaseFolderWithSql, "OksForLongProcess");

			var commissioningYear = RosreestrRegisterService.GetCommissioningYearAttribute();
			var buildYear = RosreestrRegisterService.GetBuildYearAttribute();
			var formationDate = RosreestrRegisterService.GetFormationDateAttribute();
			var undergroundFloorsNumber = RosreestrRegisterService.GetUndergroundFloorsNumberAttribute();
			var floorsNumber = RosreestrRegisterService.GetFloorsNumberAttribute();
			var wallMaterial = RosreestrRegisterService.GetWallMaterialAttribute();
			var location = RosreestrRegisterService.GetLocationAttribute();
			var address = RosreestrRegisterService.GetAddressAttribute();
			var buildingPurpose = RosreestrRegisterService.GetBuildingPurposeAttribute();
			var placementPurpose = RosreestrRegisterService.GetPlacementPurposeAttribute();
			var constructionPurpose = RosreestrRegisterService.GetConstructionPurposeAttribute();
			var objectName = RosreestrRegisterService.GetObjectNameAttribute();

			var objectType = StatisticalDataService.GetObjectTypeAttributeFromTourSettings(tourId);
			var cadastralQuartal = StatisticalDataService.GetCadastralQuartalAttributeFromTourSettings(tourId);
			var subGroupNumber = StatisticalDataService.GetGroupAttributeFromTourSettings(tourId);

			var sqlWithParameters = string.Format(sql, "{0}", commissioningYear.Id,
				buildYear.Id, formationDate.Id, undergroundFloorsNumber.Id, floorsNumber.Id, wallMaterial.Id, location.Id,
				address.Id, buildingPurpose.Id, placementPurpose.Id, constructionPurpose.Id, objectName.Id,
				objectType.Id, cadastralQuartal.Id, subGroupNumber.Id);

			return sqlWithParameters;
		}

		private void GenerateReport(List<ReportItem> reportItems)
		{
			var excelFileGenerator = new GemBoxExcelFileGenerator();

			var headerColumns = GenerateReportHeaders();
			excelFileGenerator.AddHeaders(headerColumns);

			for (var i = 0; i < reportItems.Count; i++)
			{
				var values = GenerateReportReportRow(i, reportItems[i]);
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
					CustomReportsService.AddExcelFileToGeneralZipArchive(excelFileGenerator.GetStream(), ReportName);
				}
			}
		}

		private List<object> GenerateReportReportRow(int index, ReportItem item)
		{
			return new List<object>
			{
				(index + 1).ToString(),
				item.CadastralNumber,
				item.CommissioningYear,
				item.BuildYear,
				ProcessDate(item.FormationDate),
				item.UndergroundFloorsNumber,
				item.FloorsNumber,
				item.WallMaterial,
				item.Location,
				item.Address,
				item.Purpose,
				item.ObjectName,
				item.Square,
				item.ObjectType,
				item.CadastralQuartal,
				item.CostValue,
				item.DateValuation,
				item.DateEntering,
				item.DateApproval,
				item.DocNumber,
				item.DocDate,
				item.DocName,
				item.ApplicationDate,
				item.RevisalStatementDate
			};
		}

		private List<GbuReportService.Column> GenerateReportHeaders()
		{
			var columns = new List<GbuReportService.Column>
			{
				new GbuReportService.Column
				{
					Header = "№ п/п",
					Width = 4
				},
				new GbuReportService.Column
				{
					Header = "КН",
					Width = 6
				},
				new GbuReportService.Column
				{
					Header = "Год ввода в эксплуатацию",
					Width = 5
				},
				new GbuReportService.Column
				{
					Header = "Год постройки",
					Width = 4
				},
				new GbuReportService.Column
				{
					Header = "Дата образования",
					Width = 4
				},
				new GbuReportService.Column
				{
					Header = "Количество подземных этажей",
					Width = 4
				},
				new GbuReportService.Column
				{
					Header = "Количество этажей",
					Width = 4
				},
				new GbuReportService.Column
				{
					Header = "Материал стен",
					Width = 4
				},
				new GbuReportService.Column
				{
					Header = "Местоположение",
					Width = 4
				},
				new GbuReportService.Column
				{
					Header = "Адрес",
					Width = 4
				},
				new GbuReportService.Column
				{
					Header = "Назначение",
					Width = 4
				},
				new GbuReportService.Column
				{
					Header = "Наименование объекта",
					Width = 4
				},
				new GbuReportService.Column
				{
					Header = "Площадь",
					Width = 4
				},
				new GbuReportService.Column
				{
					Header = "Тип объекта",
					Width = 4
				},
				new GbuReportService.Column
				{
					Header = "Кадастровый квартал",
					Width = 4
				},
				new GbuReportService.Column
				{
					Header = "Значение кадастровой стоимости",
					Width = 4
				},
				new GbuReportService.Column
				{
					Header = "Дата определения кадастровой стоимости",
					Width = 4
				},
				new GbuReportService.Column
				{
					Header = "Дата внесения сведений о кадастровой стоимости в ЕГРН",
					Width = 4
				},
				new GbuReportService.Column
				{
					Header = "Дата утверждения кадастровой стоимости",
					Width = 4
				},
				new GbuReportService.Column
				{
					Header = "Номер акта об утверждении кадастровой стоимости",
					Width = 4
				},
				new GbuReportService.Column
				{
					Header = "Дата акта об утверждении кадастровой стоимости",
					Width = 4
				},
				new GbuReportService.Column
				{
					Header = "Наименование документа об утверждении кадастровой стоимости",
					Width = 4
				},
				new GbuReportService.Column
				{
					Header = "Дата начала применения кадастровой стоимости",
					Width = 4
				}
				,
				new GbuReportService.Column
				{
					Header = "Дата подачи заявления о пересмотре кадастровой стоимости",
					Width = 4
				}
			};

			var counter = 0;
			columns.ForEach(x => x.Index = counter++);

			return columns;
		}

		protected string ProcessDate(string dateStr)
		{
			if (!string.IsNullOrWhiteSpace(dateStr) && DateTime.TryParse(dateStr, out var date))
			{
				dateStr = date.ToString(DateFormat);
			}

			return dateStr;
		}

		#endregion

		#region Entities

		private class ReportItem : InfoFromTourSettings
		{
			//From Unit
			public string CadastralNumber { get; set; }
			public decimal? Square { get; set; }

			//From Rosreestr
			public string CommissioningYear { get; set; }
			public string BuildYear { get; set; }
			public string FormationDate { get; set; }
			public string UndergroundFloorsNumber { get; set; }
			public string FloorsNumber { get; set; }
			public string WallMaterial { get; set; }
			public string Location { get; set; }
			public string Address { get; set; }
			public string Purpose { get; set; }
			public string ObjectName { get; set; }

			//From Tour Settings


			//From KO.CostRosreestr (KO_COST_ROSREESTR)
			/// <summary>
			/// Значение кадастровой стоимости
			/// </summary>
			public decimal? CostValue { get; set; }
			/// <summary>
			/// Дата определения кадастровой стоимости
			/// </summary>
			public DateTime? DateValuation { get; set; }
			/// <summary>
			/// Дата внесения сведений о кадастровой стоимости в ЕГРН
			/// </summary>
			public DateTime? DateEntering { get; set; }
			/// <summary>
			/// Дата утверждения кадастровой стоимости
			/// </summary>
			public DateTime? DateApproval { get; set; }
			/// <summary>
			/// Номер акта об утверждении кадастровой стоимости
			/// </summary>
			public string DocNumber { get; set; }
			/// <summary>
			/// Дата акта об утверждении кадастровой стоимости
			/// </summary>
			public DateTime? DocDate { get; set; }
			/// <summary>
			/// Наименование документа об утверждении кадастровой стоимости
			/// </summary>
			public string DocName { get; set; }
			/// <summary>
			/// Дата начала применения кадастровой стоимости
			/// </summary>
			public DateTime? ApplicationDate { get; set; }
			/// <summary>
			/// Дата подачи заявления о пересмотре кадастровой стоимости
			/// </summary>
			public DateTime? RevisalStatementDate { get; set; }
		}

		#endregion
	}
}
