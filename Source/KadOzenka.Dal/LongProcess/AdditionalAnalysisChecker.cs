﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Core.Messages;
using Core.Register.LongProcessManagment;
using Core.Shared.Extensions;
using GemBox.Spreadsheet;
using KadOzenka.Dal.DataExport;
using KadOzenka.Dal.Enum;
using Microsoft.Practices.EnterpriseLibrary.Data;
using ObjectModel.Core.LongProcess;
using ObjectModel.Sud;
using Platform.LongProcessManagment.Model;

namespace KadOzenka.Dal.LongProcess
{
	public class AdditionalAnalysisChecker: ILongProcess
	{
		public void StartProcess(OMProcessType processType, OMQueue processQueue, CancellationToken cancellationToken)
		{
			DbCommand command = DBMngr.Main.GetStoredProcCommand("additional_analysis_checker", processQueue.Id);
			DataTable dt = DBMngr.Main.ExecuteDataSet(command).Tables[0];

			var res = dt.Rows[0].ItemArray[0];

			var longProcessParametersModel = new LongProcessParametersModel();
			if (!string.IsNullOrEmpty(processType.Parameters))
			{
				var ms = new MemoryStream(Encoding.UTF8.GetBytes(processType.Parameters));
				var ser = new DataContractJsonSerializer(longProcessParametersModel.GetType());
				longProcessParametersModel = ser.ReadObject(ms) as LongProcessParametersModel;
				ms.Close();
			}
			if (res.ParseToInt() == 0)
			{
				SendResultNotificationWithoutChange(longProcessParametersModel);
			}
			else
			{
				SendResultNotification(processQueue.Id, res.ParseToInt(), longProcessParametersModel);
			}
		}

		public void LogError(long? objectId, Exception ex, long? errorId = null)
		{
			throw new NotImplementedException();
		}

		public bool Test()
		{
			return true;
		}

		internal static void SendResultNotificationWithoutChange(LongProcessParametersModel processParameters)
		{
			new MessageService().SendMessages(new MessageDto
			{
				UserIds = processParameters.UserIds,
				RoleIds = processParameters.RoleIds,
				DepartmentIds = processParameters.DepartmentIds,
				QueryIds = processParameters.QueryIds,
				Subject = $"Результат проверки объектов от: {DateTime.Now.Date}",
				Message = @"Процесс дополнительного анализа завершен успешно, объектов подходящих под критерии дополнительного анализа не обнаружено",
				IsUrgent = true,
				IsEmail = true
			});
		}

		internal static void SendResultNotification(long queueId, long additionalCheckCount, LongProcessParametersModel processParameters)
		{
			new MessageService().SendMessages(new MessageDto
			{
				UserIds = processParameters.UserIds,
				RoleIds = processParameters.RoleIds,
				DepartmentIds = processParameters.DepartmentIds,
				QueryIds = processParameters.QueryIds,
				Subject = $"Результат проверки объектов от: {DateTime.Now.Date})",
				Message = $@"Процесс дополнительного анализа завершен успешно. Объекты удовлетворяющие условию: <a href=""/Sud/GetReportAdditionalCheck?idProcess={queueId}"">{additionalCheckCount}</a>",
				IsUrgent = true,
				IsEmail = true
			});
		}


		/// <summary>
		/// Выгрузка данных по результатам проверки доп анализа объектов
		/// </summary>
		public static Stream GetReportAdditionalCheck(int idProcess)
		{
			ExcelFile excelTemplate = new ExcelFile();

			var mainWorkSheet = excelTemplate.Worksheets.Add("Объекты для доп. анализа");

			DataExportCommon.AddRow(mainWorkSheet, 0, new object[] { "Кадастровый номер объекта", "Тип объекта", "Адрес", "Дата определения", "Кадастровая стоимость", "№ дела", @"Причина установки признака ""Требуется дополнительный анализ""" });

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
					DataExportCommon.AddRow(mainWorkSheet, row, value.ToArray());
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