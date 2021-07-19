using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Core.Messages;
using Core.Register.LongProcessManagment;
using Core.Shared.Extensions;
using GemBox.Spreadsheet;
using KadOzenka.Dal.DataExport;
using KadOzenka.Dal.Enum;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Newtonsoft.Json;
using ObjectModel.Core.LongProcess;
using ObjectModel.Sud;
using Serilog;

namespace KadOzenka.Dal.LongProcess
{
	public class AdditionalAnalysisChecker: ILongProcess
	{
		private readonly ILogger _log = Log.ForContext<AdditionalAnalysisChecker>();

		public void StartProcess(OMProcessType processType, OMQueue processQueue, CancellationToken cancellationToken)
		{
            WorkerCommon.SetProgress(processQueue, 0);

            DbCommand command = DBMngr.Main.GetStoredProcCommand("additional_analysis_checker", processQueue.Id);
			DataTable dt = DBMngr.Main.ExecuteDataSet(command).Tables[0];

			var res = dt.Rows[0].ItemArray[0];

			var messageAddresses = JsonConvert.DeserializeObject<MessageAddressersDto>(processType.Parameters);

            WorkerCommon.SetProgress(processQueue, 75);

            if (res.ParseToInt() == 0)
			{
				SendResultNotificationWithoutChange(messageAddresses);
			}
			else
			{
				SendResultNotification(processQueue.Id, res.ParseToInt(), messageAddresses);
			}

            WorkerCommon.SetProgress(processQueue, 100);
        }

		public void LogError(long? objectId, Exception ex, long? errorId = null)
		{
			_log.ForContext("ErrorId", errorId).Error(ex, "Ошибка фонового процесса. ID объекта {objectId}", objectId);
		}

		public bool Test()
		{
			return true;
		}

		internal static void SendResultNotificationWithoutChange(MessageAddressersDto addresses)
		{
			new MessageService().SendMessages(new MessageDto
			{
				Addressers = addresses,
				Subject = $"Результат проверки объектов от: {DateTime.Now.Date}",
				Message = @"Процесс дополнительного анализа завершен успешно, объектов подходящих под критерии дополнительного анализа не обнаружено",
				IsUrgent = true,
				IsEmail = true,
                ExpireDate = DateTime.Now.AddHours(2)
			});
		}

		internal static void SendResultNotification(long queueId, long additionalCheckCount, MessageAddressersDto addresses)
		{
			new MessageService().SendMessages(new MessageDto
			{
				Addressers = addresses,
				Subject = $"Результат проверки объектов от: {DateTime.Now.Date})",
				Message = $@"Процесс дополнительного анализа завершен успешно. Объекты удовлетворяющие условию: <a href=""/Sud/GetReportAdditionalCheck?idProcess={queueId}"">{additionalCheckCount}</a>",
				IsUrgent = true,
				IsEmail = true,
                ExpireDate = DateTime.Now.AddHours(2)
			});
		}


		/// <summary>
		/// Выгрузка данных по результатам проверки доп анализа объектов
		/// </summary>
		public static Stream GetReportAdditionalCheck(int idProcess)
		{
			ExcelFile excelTemplate = new ExcelFile();

			var mainWorkSheet = excelTemplate.Worksheets.Add("Объекты для доп. анализа");

			CommonSdks.DataExportCommon.AddRow(mainWorkSheet, 0, new object[] { "Кадастровый номер объекта", "Тип объекта", "Адрес", "Дата определения", "Кадастровая стоимость", "№ дела", @"Причина установки признака ""Требуется дополнительный анализ""" });

			mainWorkSheet.Columns[0].SetWidth(200, LengthUnit.Pixel);
			mainWorkSheet.Columns[1].SetWidth(150, LengthUnit.Pixel);
			mainWorkSheet.Columns[2].SetWidth(300, LengthUnit.Pixel);
			mainWorkSheet.Columns[3].SetWidth(200, LengthUnit.Pixel);
			mainWorkSheet.Columns[4].SetWidth(200, LengthUnit.Pixel);
			mainWorkSheet.Columns[5].SetWidth(200, LengthUnit.Pixel);
			mainWorkSheet.Columns[6].SetWidth(500, LengthUnit.Pixel);
			List<OMDopAnalisLog> objs = OMDopAnalisLog.Where(x => x.IdProcess == idProcess).SelectAll().Execute();
			int curIndex = 0;
			if (objs.Count > 0)
			{
				CancellationTokenSource cancelTokenSource = new CancellationTokenSource();
				ParallelOptions options = new ParallelOptions
				{
					CancellationToken = cancelTokenSource.Token,
					MaxDegreeOfParallelism = 20
				};

				object locked = new object();
				List<List<object>> values = new List<List<object>>();

				Parallel.ForEach(objs, options, obj =>
				{
					curIndex++;
					if (curIndex % 40 == 0) Console.WriteLine(curIndex);


					string Kn = obj.Kn?.Replace("\n", "").Replace("\r", "").Replace(" ", "");
					List<object> value = new List<object>();
					value.Add(Kn);
					value.Add(obj.TypeObj);
					value.Add(obj.Address);
					value.Add(obj.DateDefinition.GetValueOrDefault().ToShortDateString());
					value.Add(obj.Kc);
					value.Add(obj.SudNumber);
					value.Add(((CaseAdditionalCheckerEnum)obj.ParameterCase).GetEnumDescription());


					lock (locked)
					{
								values.Add(value);
					}
				});

				int row = 1;
				foreach (List<object> value in values)
				{
					CommonSdks.DataExportCommon.AddRow(mainWorkSheet, row, value.ToArray());
					row++;
				}
				Console.WriteLine(values.Count);
			}

			MemoryStream stream = new MemoryStream();
			excelTemplate.Save(stream, SaveOptions.XlsxDefault);
			stream.Seek(0, SeekOrigin.Begin);
			return stream;
		}
	}
}