using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Core.Register;
using Core.SRD;
using GemBox.Spreadsheet;
using KadOzenka.Dal.GbuObject;
using KadOzenka.Dal.Oks;
using KadOzenka.WebClients.RgisClient.Api;
using KadOzenka.WebClients.RgisClient.Client;
using KadOzenka.WebClients.RgisClient.Model;
using ObjectModel.Core.TD;
using ObjectModel.Directory;
using ObjectModel.Gbu;
using ObjectModel.KO;
using Serilog;

namespace KadOzenka.Dal.LongProcess.TaskLongProcesses
{
	public struct ReportData
	{
		public string Header { get; set; }
		public long AttributeId { get; set; }
		public int ColumnNumber { get; set; }
	} 

	public class GeoFactorsFromRgisLongProcess
	{
		private readonly ILogger _log = Log.ForContext<GeoFactorsFromRgisLongProcess>();
		private object lockMy = new ();
		private List<ReportData> zuReportData =  new ();
		private List<ReportData> oksReportData =  new ();


		private readonly RgisDataApi _rgisDataApi;
		public GeoFactorsFromRgisLongProcess()
		{
			_rgisDataApi = new RgisDataApi();
		}

		public void StartProcess(long taskId, List<long> idsFactors)
		{
			var messageSubject = "Получение географических факторов из РГИС";

			var task = GetTask(taskId);
			var document = GetDocument(task.DocumentId);
			var units = GetUnits(task.Id);


			using var reportService = new GeoFactorsReport("Получение географических факторов из РГИС");
			reportService.AddHeaders(new List<string> {"Кадастровый номер"});


			var zuUnits = units.Where(x => x.PropertyType_Code == PropertyTypes.Stead).ToList();
			var zuLayers = GetLayersZuOnlySelected(idsFactors);
			var oksUnits = units.Where(x => x.PropertyType_Code != PropertyTypes.Stead).ToList();
			var oksLayers = GetLayersOksOnlySelected(idsFactors);
			
			SetReportHeaders(zuLayers, reportService, ObjectType.ZU);

			var options = new ParallelOptions
			{
				MaxDegreeOfParallelism = 10,
				CancellationToken = CancellationToken.None //todo replase LP
			};

			
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
							int countParams = omUnits.Count * zuLayers.Count;
							if (countParams > 1)
							{
								ApiResponse<ResponseData> response = _rgisDataApi.GetDistanceZuFactorsValue(new RequestData
								{
									KadNumbers = omUnits.Select(x => x.CadastralNumber).ToList(),
									Layers = zuLayers.Select(x => x.LayerName).ToList()
								});

								PrepareResponseData(response.Data, zuUnits, zuLayers, document);

							}
							else
							{
								ApiResponse<ResponseDataSingle> response = _rgisDataApi.GetDistanceZuFactorsValueSingle(new RequestData
								{
									KadNumbers = omUnits.Select(x => x.CadastralNumber).ToList(),
									Layers = zuLayers.Select(x => x.LayerName).ToList()
								});

								PrepareResponseData(response.Data, zuUnits, zuLayers, document);
							}

						}
						catch (Exception e)
						{
							_log.Error(e, e.Message);
							//report 
						}

					});

				});

				 tasks.Add(zuTask);
			}
			else
			{
				_log.Debug("Импорт данных из Ргис для Зу не запущен");
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
							int countParams = omUnits.Count * oksLayers.Count;
							if (countParams > 1)
							{
								ApiResponse<ResponseData> response = _rgisDataApi.GetDistanceOksFactorsValue(new RequestData
								{
									KadNumbers = omUnits.Select(x => x.CadastralNumber).ToList(),
									Layers = oksLayers.Select(x => x.LayerName).ToList()
								});

								PrepareResponseData(response.Data, oksUnits, oksLayers, document);

							}
							else
							{
								ApiResponse<ResponseDataSingle> response = _rgisDataApi.GetDistanceOksFactorsValueSingle(new RequestData
								{
									KadNumbers = omUnits.Select(x => x.CadastralNumber).ToList(),
									Layers = oksLayers.Select(x => x.LayerName).ToList()
								});

								PrepareResponseData(response.Data, oksUnits, oksLayers, document);
							}

						}
						catch (Exception e)
						{
							_log.Error(e, e.Message);
							//report 
						}

					});

				});
				tasks.Add(oksTask);
			}

			Task.WaitAll(tasks.ToArray());
			//ObjectType

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
				? OMRgisLayers.Where(x => selectedIdsZuFactors.Contains(x.Id)).SelectAll().Execute().ToList() 
				: new ();
		}

		private List<OMRgisLayers> GetLayersOksOnlySelected(List<long> idsFactors)
		{
			var idsOksFactors = RegisterCache.RegisterAttributes.Values
				.Where(x => x.RegisterId == OMSource25.GetRegisterId()).Select(x => x.Id);
			var selectedIdsOksFactors = idsOksFactors.Where(idsFactors.Contains).ToList();

			return selectedIdsOksFactors.Any() 
				?  OMRgisLayers.Where(x => selectedIdsOksFactors.Contains(x.Id)).SelectAll().Execute().ToList() 
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

		private void SetReportHeaders(List<OMRgisLayers> layers, GbuReportService reportService, ObjectType oType)
		{
			if (layers.Count > 0)
			{
				var headers = RegisterCache.RegisterAttributes.Values.Where(x => layers.Select(y => y.Id).Contains(x.Id))
					.Select((x, i) => new ReportData
					{
						AttributeId = x.Id,
						ColumnNumber = i + 1,
						Header = x.Name
					}).ToList();

				if (oType == ObjectType.ZU)
				{
					zuReportData = headers;
				}
				else
				{
					oksReportData = headers;
				}

				reportService.AddHeaders(headers.Select(x => x.Header).ToList());

				headers.ForEach(x =>
				{
					reportService.SetIndividualWidth(x.ColumnNumber, 10);
				});
				
			}

		}

		#endregion


	}

	public class GeoFactorsReport: GbuReportService
	{
		private Row CurrentRowZu { get; set; }
		public GeoFactorsReport(string fileName): base(fileName)
		{

		}

		protected override void CreateFile()
		{
			_curretExcelFile = new ExcelFile();
			var sheet = _curretExcelFile.Worksheets.Add("Oks");
			sheet.Cells.Style.Font.Name = "Times New Roman";
			sheet = _curretExcelFile.Worksheets.Add("Zu");
			sheet.Cells.Style.Font.Name = "Times New Roman";

			CurrentRow = new Row
			{
				File = _curretExcelFile
			};
			CurrentRowZu = new Row
			{
				File = _curretExcelFile
			};
		}


		public void AddValue(string value, int column, Row row, CellStyle cellStyle = null, int worksheet = 0)
		{
			try
			{
				var cell = row.File.Worksheets[worksheet].Rows[row.Index].Cells[column];

				cell.SetValue(value);

				if (cellStyle != null)
					cell.Style = cellStyle;

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
		public new Row GetCurrentRow()
		{
			var tmpRow = CurrentRow.Copy();

			CurrentRow.Index++;

			return tmpRow;
		}
	}
}