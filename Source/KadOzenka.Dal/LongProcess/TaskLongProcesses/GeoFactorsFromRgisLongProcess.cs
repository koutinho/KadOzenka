using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CommonSdks.ConfigurationManagers;
using Core.Register;
using Core.Register.LongProcessManagment;
using Core.Shared.Extensions;
using Core.SRD;
using GemBox.Spreadsheet;
using KadOzenka.Dal.GbuObject;
using KadOzenka.Dal.LongProcess.InputParameters;
using KadOzenka.Dal.Oks;
using KadOzenka.WebClients.RgisClient.Api;
using KadOzenka.WebClients.RgisClient.Client;
using KadOzenka.WebClients.RgisClient.Model;
using Microsoft.Practices.ObjectBuilder2;
using ObjectModel.Core.LongProcess;
using ObjectModel.Core.TD;
using ObjectModel.Directory;
using ObjectModel.Gbu;
using ObjectModel.KO;
using Serilog;
using OperationCanceledException = System.OperationCanceledException;

namespace KadOzenka.Dal.LongProcess.TaskLongProcesses
{
	public struct ReportHeaderData
	{
		public string Header { get; set; }
		public long AttributeId { get; set; }
		public int ColumnNumber { get; set; }
		public string LayerName { get; set; }
	}

	public struct ReportData
	{
		public double Distance { get; set; }
		public string ErrorMessage { get; set; }
		public int ColumnNumber { get; set; }
	}

	public struct ErrorData
	{
		public string Message { get; set; }
		public string CadNumber { get; set; }
		public ObjectType ObjType { get; set; }
	}

	public class GeoFactorsFromRgisLongProcess: LongProcess
	{
		private readonly ILogger _log = Log.ForContext<GeoFactorsFromRgisLongProcess>();
		private readonly object _lockMy = new ();
		private List<ReportHeaderData> _zuReportHeader =  new ();
		private List<ReportHeaderData> _oksReportHeader =  new ();
		private readonly int knHeaderColumnNumber = 0;
		private static string LongProcessName = "GeoFactorsFromRgisLongProcess";

		int _unitCount;
		int _unitProcessed;

		private readonly RgisDataApi _rgisDataApi;
		public GeoFactorsFromRgisLongProcess()
		{
			_rgisDataApi = new RgisDataApi();
		}

		public static void AddProcessToQueue(GeoFactorsFromRgis inputParameters)
		{
			LongProcessManager.AddTaskToQueue(LongProcessName, parameters: inputParameters.SerializeToXml());
		}

		public override void StartProcess(OMProcessType processType, OMQueue processQueue,
			CancellationToken cancellationToken)
		{
			var messageSubject = "Получение географических факторов из РГИС";

			GeoFactorsFromRgis inputParameters = new GeoFactorsFromRgis();

			if (!string.IsNullOrWhiteSpace(processQueue.Parameters))
			{
				inputParameters = processQueue.Parameters.DeserializeFromXml<GeoFactorsFromRgis>();
			}

			if (inputParameters == null || inputParameters.TaskId == 0)
			{
				WorkerCommon.SetMessage(processQueue, Common.Consts.MessageForProcessInterruptedBecauseOfNoObjectId);
				WorkerCommon.SetProgress(processQueue, Common.Consts.ProgressForProcessInterruptedBecauseOfNoObjectId);
				NotificationSender.SendNotification(processQueue, messageSubject,
					"Операция завершена с ошибкой, т.к. нет входных данных. Подробнее в списке процессов");
				return;
			}

			WorkerCommon.SetProgress(processQueue, 0);
			var task = GetTask(inputParameters.TaskId);
			var document = GetDocument(task.DocumentId);
			var units = GetUnits(task.Id);
			_unitCount = units.Count;

			_log.Debug("Найдено юнитов = {units}", units.Count);

			using var reportService = new GeoFactorsReport(messageSubject);



			var zuUnits = units.Where(x => x.PropertyType_Code == PropertyTypes.Stead).DistinctBy(x => x.CadastralNumber).ToList();
			var zuLayers = GetLayersZuOnlySelected(inputParameters.IdFactors);
			var oksUnits = units.Where(x => x.PropertyType_Code != PropertyTypes.Stead).DistinctBy(x => x.CadastralNumber).ToList();
			var oksLayers = GetLayersOksOnlySelected(inputParameters.IdFactors);
			
			SetReportHeaders(zuLayers, reportService, ObjectType.ZU);
			SetReportHeaders(oksLayers, reportService, ObjectType.Oks);

			var options = new ParallelOptions
			{
				MaxDegreeOfParallelism = 2,
				CancellationToken = cancellationToken
			};


			var zuReportData = new Dictionary<string, List<ReportData>>();
			var oksReportData = new Dictionary<string, List<ReportData>>();
			var errors = new List<ErrorData>();
			
			List<Task> tasks = new();
			if (zuLayers.Count > 0 && zuUnits.Count > 0)
			{
				int countParamsToRequest = 100;
				var groupingUnits = GetGroupingUnits(zuUnits, zuLayers, countParamsToRequest);

				 var zuTask = Task.Run(() =>
				{
					Parallel.ForEach(groupingUnits, options, omUnits =>
					{
						try
						{
							options.CancellationToken.ThrowIfCancellationRequested();
							int countParams = omUnits.Count * zuLayers.Count;
							if (countParams > 1)
							{
								ApiResponse<ResponseData> response = _rgisDataApi.GetDistanceZuFactorsValue(
									new RequestData
									{
										KadNumbers = omUnits.Select(x => x.CadastralNumber).ToList(),
										Layers = zuLayers.Select(x => x.LayerName).ToList()
									});

								PrepareResponseData(response.Data, zuUnits, zuLayers, document);
								PrepareReportData(response.Data, _zuReportHeader, zuReportData, errors, ObjectType.ZU);
								
							}
							else
							{
								ApiResponse<ResponseDataSingle> response = _rgisDataApi.GetDistanceZuFactorsValueSingle(
									new RequestData
									{
										KadNumbers = omUnits.Select(x => x.CadastralNumber).ToList(),
										Layers = zuLayers.Select(x => x.LayerName).ToList()
									});

								PrepareResponseData(response.Data, zuUnits, zuLayers, document);
								PrepareReportData(response.Data, _zuReportHeader, zuReportData, errors, ObjectType.ZU);
							}
							UpdateProgress(processQueue, omUnits.Count);
						}
						catch (OperationCanceledException ex)
						{
							// only catch
						}
						catch (Exception e)
						{
							_log.Error(e, e.Message);
							lock (_lockMy)
							{
								omUnits.ForEach(x =>
								{
									errors.Add(new ErrorData
									{
										CadNumber = x.CadastralNumber,
										Message = e.Message,
										ObjType = ObjectType.ZU
									});
								});
							}
							UpdateProgress(processQueue, omUnits.Count);
						}
						

					});

				}, cancellationToken);

				 tasks.Add(zuTask);
			}
			else
			{
				_log
					.ForContext("UnitsCount", zuUnits.Count)
					.ForContext("LayersCount", zuLayers.Count)
					.Warning("Импорт данных из Ргис для Зу не запущен, т.к нет Юнитов или слоев");
			}

			if (oksLayers.Count > 0 && oksUnits.Count > 0)
			{
				int countParamsToRequest = 100;
				var groupingUnits = GetGroupingUnits(oksUnits, oksLayers, countParamsToRequest);

				var oksTask = Task.Run(() =>
				{
					Parallel.ForEach(groupingUnits, options, omUnits =>
					{
						try
						{
							options.CancellationToken.ThrowIfCancellationRequested();
							int countParams = omUnits.Count * oksLayers.Count;
							if (countParams > 1)
							{
								ApiResponse<ResponseData> response = _rgisDataApi.GetDistanceOksFactorsValue(new RequestData
								{
									KadNumbers = omUnits.Select(x => x.CadastralNumber).ToList(),
									Layers = oksLayers.Select(x => x.LayerName).ToList()
								});

								PrepareResponseData(response.Data, oksUnits, oksLayers, document);
								PrepareReportData(response.Data, _oksReportHeader, oksReportData, errors, ObjectType.Oks);
							}
							else
							{
								ApiResponse<ResponseDataSingle> response = _rgisDataApi.GetDistanceOksFactorsValueSingle(new RequestData
								{
									KadNumbers = omUnits.Select(x => x.CadastralNumber).ToList(),
									Layers = oksLayers.Select(x => x.LayerName).ToList()
								});

								PrepareResponseData(response.Data, oksUnits, oksLayers, document);
								PrepareReportData(response.Data, _oksReportHeader, oksReportData, errors, ObjectType.Oks);
							}
							UpdateProgress(processQueue, omUnits.Count);
						}
						catch (OperationCanceledException ex)
						{
							// only catch
						}
						catch (Exception e)
						{
							_log.Error(e, e.Message);
							lock (_lockMy)
							{
								omUnits.ForEach(x => {
									errors.Add(new ErrorData
									{
										CadNumber = x.CadastralNumber,
										Message = e.Message,
										ObjType = ObjectType.Oks
									});
								});
							}
							UpdateProgress(processQueue, omUnits.Count);
						}

					});
				}, cancellationToken);
				tasks.Add(oksTask);
			}
			else
			{
				_log
					.ForContext("UnitsCount", oksUnits.Count)
					.ForContext("LayersCount", oksLayers.Count)
					.Warning("Импорт данных из Ргис для Oks не запущен, т.к нет Юнитов или слоев");
			}

			Task.WaitAll(tasks.ToArray());

			var roleId = ConfigurationManager.WebClientsConfig.RoleIdForNotification?.ParseToLongNullable();
			if (cancellationToken.IsCancellationRequested)
			{
				WorkerCommon.SetProgress(processQueue, 100);
				var cancelMessage = "Импорт данных из РГИС отменен";
				NotificationSender.SendNotification(processQueue, messageSubject, cancelMessage, roleId);
				return;
			}

			FillReport(oksReportData, reportService, ObjectType.Oks);
			FillReport(zuReportData, reportService, ObjectType.ZU);
			FillReportErrorData(errors, reportService);

			var reportId = reportService.SaveReport();

			var message = $"Импорт данных из РГИС выполнен " + $@"<a href=""{reportService.GetUrlToDownloadFile(reportId)}"">Скачать результат</a>";
			NotificationSender.SendNotification(processQueue, messageSubject, message, roleId);
			WorkerCommon.SetProgress(processQueue, 100);

		}


		#region support methods

		private OMTask GetTask(long taskId)
		{
			var task = OMTask.Where(x => x.Id == taskId)
				.Select(x => x.EstimationDate)
				.Select(x => x.DocumentId)
				.ExecuteFirstOrDefault();

			if (task == null)
				throw new Exception($"Не найдено задание на оценку с Id ='{taskId}'");

			return task;
		}

		private OMInstance GetDocument(long? documentId)
		{
			return OMInstance.Where(x => x.Id == documentId)
				.Select(x => x.Id)
				.Select(x => x.CreateDate)
				.ExecuteFirstOrDefault();
		}

		private List<OMUnit> GetUnits(long taskId)
		{
			return OMUnit.Where(x =>
					x.TaskId == taskId)
				.Select(x => x.CadastralNumber)
				.Select(x => x.ObjectId)
				.Select(x => x.PropertyType_Code)
				.Execute();
		}


		private List<OMRgisLayers> GetLayers(List<long> idsFactors)
		{
			return OMRgisLayers.Where(x => idsFactors.Contains(x.Id)).SelectAll().Execute();
		}

		private List<OMRgisLayers> GetLayersZuOnlySelected(List<long> idsFactors)
		{
			var idsZuFactors = RegisterCache.RegisterAttributes.Values
				.Where(x => x.RegisterId == OMSource26.GetRegisterId()).Select(x => x.Id);
			var selectedIdsZuFactors = idsFactors.Where(x => idsZuFactors.Contains(x)).ToList();

			return selectedIdsZuFactors.Any() 
				? GetLayers(selectedIdsZuFactors).ToList() 
				: new ();
		}

		private List<OMRgisLayers> GetLayersOksOnlySelected(List<long> idsFactors)
		{
			var idsOksFactors = RegisterCache.RegisterAttributes.Values
				.Where(x => x.RegisterId == OMSource25.GetRegisterId()).Select(x => x.Id);
			var selectedIdsOksFactors = idsOksFactors.Where(idsFactors.Contains).ToList();

			return selectedIdsOksFactors.Any() 
				? GetLayers(selectedIdsOksFactors).ToList() 
				: new ();
		}

		private List<List<OMUnit>> GetGroupingUnits(List<OMUnit> units, List<OMRgisLayers> layers, int count)
		{
			if (units.Count == 0 || layers.Count == 0) return new List<List<OMUnit>>();

			int availableCount = count / layers.Count;

			if (availableCount > 1)
			{
				var res = new List<List<OMUnit>>();
				int unitsGroup = units.Count % availableCount == 0
					? units.Count / availableCount
					: (units.Count / availableCount) + 1;


				int unitsInOneGroup = units.Count % unitsGroup == 0 
					? units.Count / unitsGroup 
					: (units.Count / unitsGroup) + 1;

				int counter = 0;
				for (int i = 0; i < unitsGroup; i++)
				{
					var tempList = new List<OMUnit>();
					for (int j = 0; j < unitsInOneGroup; j++)
					{
						if (counter == units.Count) break;
						tempList.Add(units[counter]);
						counter++;
					}
					res.Add(tempList);
				}

				return res;
			}

			return units.Select(x => new List<OMUnit> {x}).ToList();
		}


		private void SaveFactor(long objectId, long attributeId, RegisterAttributeType attributeType,
			double distance, OMInstance taskDocument)
		{
			_log.ForContext("distance", distance)
				.ForContext("attributeId", attributeId)
				.Debug("Сохранение Ргис фактора");

			var gbuObjectAttribute = new GbuObjectAttribute
			{
				ObjectId = objectId,
				AttributeId = attributeId,
				NumValue = (decimal)distance,
				S = taskDocument.CreateDate,
				Ot = taskDocument.CreateDate,
				ChangeDocId = taskDocument.Id,
				ChangeUserId = SRDSession.Current.UserID,
				ChangeDate = DateTime.Now
			};

			gbuObjectAttribute.Save();
		}


		private void PrepareResponseData(ResponseData responseData, List<OMUnit> units, List<OMRgisLayers> layers, OMInstance doc)
		{
			foreach (var res in responseData.Result)
			{
				var objId = units.FirstOrDefault(x => x.CadastralNumber == res[0].Params.KadNumber)?.ObjectId;
				var attrId = layers.FirstOrDefault(x => x.LayerName == res[0].Params.LayerName)?.Id;
				var attrType = RegisterCache.RegisterAttributes.Values.FirstOrDefault(x => x.Id == attrId)?.Type ?? RegisterAttributeType.DECIMAL;
				if (objId != null && res[0].Distance != null && attrId != null)
				{
					SaveFactor(objId.Value, attrId.Value, attrType, res[0].Distance.Value, doc);
				}
				else
				{
					_log.ForContext("attrId", attrId)
						.ForContext("objId", objId)
						.ForContext("Distance", res[0].Distance).Warning("Добавление фактора не выполненео т.к attrId, objId или Distance равны null");
				}
			}
		}

		private void PrepareResponseData(ResponseDataSingle responseData, List<OMUnit> units, List<OMRgisLayers> layers, OMInstance doc)
		{
			foreach (var res in responseData.Result)
			{
				var objId = units.FirstOrDefault(x => x.CadastralNumber == res.Params.KadNumber)?.ObjectId;
				var attrId = layers.FirstOrDefault(x => x.LayerName == res.Params.LayerName)?.Id;
				var attrType = RegisterCache.RegisterAttributes.Values.FirstOrDefault(x => x.Id == attrId)?.Type ?? RegisterAttributeType.DECIMAL;
				if (objId != null && res.Distance != null && attrId != null)
				{
					SaveFactor(objId.Value, attrId.Value, attrType, res.Distance.Value, doc);
				}
				else
				{
					_log.ForContext("attrId", attrId)
						.ForContext("objId", objId)
						.ForContext("Distance", res.Distance).Warning("Добавление фактора не выполненео т.к attrId, objId или Distance равны null");
				}
			}
		}

		private void SetReportHeaders(List<OMRgisLayers> layers, GeoFactorsReport reportService, ObjectType oType)
		{
			if (layers.Count > 0)
			{
				var headers = new List<ReportHeaderData>
				{
					new ()
					{
						ColumnNumber = 0,
						Header = "Кадастровый номер"
					}
				};

				RegisterCache.RegisterAttributes.Values
					.Where(x => layers.Select(y => y.Id).Contains(x.Id)).ForEach(x =>
					{
						headers.Add(new ReportHeaderData
						{
							AttributeId = x.Id,
							ColumnNumber = headers.Count,
							Header = x.Name,
							LayerName = layers.FirstOrDefault(y => y.Id == x.Id)?.LayerName
						});
					});


				headers.Add(new ReportHeaderData
				{
					ColumnNumber = headers.Count,
					Header = "Ошибка"
				});

				if (oType == ObjectType.ZU)
				{
					_zuReportHeader = headers;
				}
				else
				{
					_oksReportHeader = headers;
				}

				reportService.AddHeaders(headers.Select(x => x.Header).ToList(), oType);

				headers.ForEach(x =>
				{
					reportService.SetIndividualWidth(x.ColumnNumber, 10, oType);
				});
				
			}

		}

		private void PrepareReportData(ResponseData responseData, List<ReportHeaderData> headers, Dictionary<string, List<ReportData>> reportDataDictionary, List<ErrorData> errors, ObjectType objectType)
		{

			if (responseData != null && responseData.Result.Count > 0)
			{
				responseData.Result.GroupBy(x => x[0].Params.KadNumber).ForEach((x) =>
				{
					var reportData = new List<ReportData>();
					x.SelectMany(y => y).ForEach(y =>
					{
						
						lock (_lockMy)
						{
							reportData.Add(new ReportData
							{
								Distance = y.Distance ?? 0,
								ErrorMessage = y.Distance == null ? "Объект не найден" : "",
								ColumnNumber = headers.FirstOrDefault(z => z.LayerName == y.Params.LayerName)
									.ColumnNumber
							});
						}
					});
					if (reportData.Count > 0)
					{
						lock (_lockMy)
						{
							reportDataDictionary.Add(x.Key, reportData);
						}
					}
				});
			}

		}



		private void PrepareReportData(ResponseDataSingle responseData, List<ReportHeaderData> headers, Dictionary<string, List<ReportData>> reportDataDictionary, List<ErrorData> errors, ObjectType objectType)
		{

			if (responseData != null && responseData.Result.Count > 0)
			{
				responseData.Result.GroupBy(x => x.Params.KadNumber).ForEach((x) =>
				{
					var reportData = new List<ReportData>();
					x.ForEach(y =>
					{
						lock (_lockMy)
						{
							reportData.Add(new ReportData
							{
								Distance = y.Distance ?? 0,
								ErrorMessage = y.Distance == null ? "Объект не найден" : "",
								ColumnNumber = headers.FirstOrDefault(z => z.LayerName == y.Params.LayerName)
									.ColumnNumber
							});
						}
					});
					if (reportData.Count > 0)
					{
						lock (_lockMy)
						{
							reportDataDictionary.Add(x.Key, reportData);
						}
					}
				});
			}

		}

		private void FillReportErrorData(List<ErrorData> errors, GeoFactorsReport reportService)
		{
			foreach (var error in errors)
			{
				int errorColumn = error.ObjType == ObjectType.ZU ? _zuReportHeader.Count : _oksReportHeader.Count;
				var row = error.ObjType == ObjectType.ZU ? reportService.GetCurrentRowZu() : reportService.GetCurrentRowOks();
					reportService.AddValue(error.CadNumber, knHeaderColumnNumber, row, error.ObjType);
					reportService.AddErrorValue(error.Message, errorColumn - 1, row, error.ObjType);
			}
		
		}

		private void FillReport(Dictionary<string, List<ReportData>> reportData, GeoFactorsReport reportService, ObjectType type)
		{
			foreach (var data in reportData)
			{
				var row = type == ObjectType.Oks ? reportService.GetCurrentRowOks() : reportService.GetCurrentRowZu();
				reportService.AddValue(data.Key, knHeaderColumnNumber, row, type);
				data.Value.ForEach(x =>
				{
					if (x.ErrorMessage.IsNullOrEmpty())
					{
						reportService.AddValue(x.Distance.ToString(CultureInfo.InvariantCulture), x.ColumnNumber,
							row, type);
					}
					else
					{
						reportService.AddValue(x.ErrorMessage, x.ColumnNumber,
							row, type);
					}
				});
			}
		}

		private void UpdateProgress(OMQueue processQueue, int count)
		{
			lock (_lockMy)
			{
				_unitProcessed += count;
				var progress = _unitProcessed * 100 / _unitCount ;
				WorkerCommon.SetProgress(processQueue, progress);
			}
			
		}
		#endregion

	}

	public class GeoFactorsReport: GbuReportService
	{
		private Row CurrentRowZu { get; set; }
		private Row CurrentRowOks { get; set; }
		public GeoFactorsReport(string fileName): base(fileName, false)
		{

		}

		protected override void CreateFile()
		{
			_curretExcelFile = new ExcelFile();
			var sheet = _curretExcelFile.Worksheets.Add("Oks");
			sheet.Cells.Style.Font.Name = "Times New Roman";
			sheet = _curretExcelFile.Worksheets.Add("Zu");
			sheet.Cells.Style.Font.Name = "Times New Roman";

			CurrentRowOks = new Row
			{
				File = _curretExcelFile
			};
			CurrentRowZu = new Row
			{
				File = _curretExcelFile
			};
		}

		public void AddErrorValue(string value, int column, Row row, ObjectType worksheet)
		{
			AddValue(value, column, row, worksheet);
			row.File.Worksheets[(int) worksheet].Rows[row.Index].Cells[column].Style.FillPattern.SetSolid(SpreadsheetColor.FromName(ColorName.Red));
		}

		public void AddValue(string value, int column, Row row, ObjectType worksheet)
		{
			try
			{
				var cell = row.File.Worksheets[(int)worksheet].Rows[row.Index].Cells[column];

				cell.SetValue(value);
				cell.Style = new CellStyle
					{
						HorizontalAlignment = HorizontalAlignmentStyle.Center,
						VerticalAlignment = VerticalAlignmentStyle.Center
					}; ;

				IsReportEmpty = false;

				if (new Random().Next(0, 10000) > 9950)
					Serilog.Log.ForContext<ExcelFile>().Verbose("Запись значения в Excel. Строка {Row}, столбец {Column}, значение {Value}", row.Index, column, value);
			}
			catch (Exception ex)
			{
				if (new Random().Next(0, 100) > 80)
					Serilog.Log.ForContext<ExcelFile>().Warning(ex, "Ошибка записи значения в Excel. Строка {Row}, столбец {Column}, значение {Value}", row.Index, column, value);
			}
		}
		public Row GetCurrentRowOks()
		{
			var tmpRow = CurrentRowOks.Copy();

			CurrentRowOks.Index++;

			return tmpRow;
		}
		public Row GetCurrentRowZu()
		{
			var tmpRow = CurrentRowZu.Copy();

			CurrentRowZu.Index++;

			return tmpRow;
		}



		public void AddHeaders(List<string> values, ObjectType objectType)
		{
			var rowIndex = 0;
			int columnIndex = 0;
			ExcelWorksheet sheet;
			if (objectType == ObjectType.ZU)
			{
				sheet = CurrentRowZu.File.Worksheets[(int)objectType];
				CurrentRowZu.Index++;
			}
			else
			{
				sheet = CurrentRowOks.File.Worksheets[(int)objectType];
				CurrentRowOks.Index++;
			}
			foreach (string value in values)
			{
				sheet.Rows[rowIndex].Cells[columnIndex].SetValue(value);
				sheet.Rows[rowIndex].Cells[columnIndex].Style.HorizontalAlignment = HorizontalAlignmentStyle.Center;
				sheet.Rows[rowIndex].Cells[columnIndex].Style.VerticalAlignment = VerticalAlignmentStyle.Center;
				sheet.Rows[rowIndex].Cells[columnIndex].Style.Borders.SetBorders(MultipleBorders.All, SpreadsheetColor.FromName(ColorName.Black), LineStyle.Thin);
				sheet.Rows[rowIndex].Cells[columnIndex].Style.WrapText = true;
				columnIndex++;
			}

		}

		public void SetIndividualWidth(int column, int width, ObjectType objType)
		{

			_curretExcelFile.Worksheets[(int)objType].Columns[column].SetWidth(width, LengthUnit.Centimeter);
		}
	}
}