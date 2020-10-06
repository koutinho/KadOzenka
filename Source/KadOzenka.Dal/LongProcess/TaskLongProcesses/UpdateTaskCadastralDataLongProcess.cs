﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Core.ErrorManagment;
using Core.Register.LongProcessManagment;
using KadOzenka.Dal.GbuObject;
using KadOzenka.Dal.Tasks;
using ObjectModel.Core.LongProcess;
using ObjectModel.KO;

namespace KadOzenka.Dal.LongProcess.TaskLongProcesses
{
	public class UpdateTaskCadastralDataLongProcess : LongProcess
	{
		public const string LongProcessName = "UpdateTaskCadastralData";

		public static long AddProcessToQueue(long taskId)
		{
			return LongProcessManager.AddTaskToQueue(LongProcessName, OMTask.GetRegisterId(), taskId);
		}

		private UpdateCadastralDataService UpdateCadastralDataService { get; }
		private GbuObjectService GbuObjectService { get; }
		private TaskService TaskService { get; }

		public UpdateTaskCadastralDataLongProcess()
		{
			UpdateCadastralDataService = new UpdateCadastralDataService();
			GbuObjectService = new GbuObjectService();
			TaskService = new TaskService();
		}

		private long? _cadastralQuarterAttrId;
		private long? _buildingCadastralNumberAttrId;
		private string _taskName;
		private object _locked;

		public override void StartProcess(OMProcessType processType, OMQueue processQueue, CancellationToken cancellationToken)
		{
			WorkerCommon.SetProgress(processQueue, 0);

			if (!processQueue.ObjectId.HasValue)
			{
				WorkerCommon.SetMessage(processQueue, Consts.Consts.MessageForProcessInterruptedBecauseOfNoObjectId);
				WorkerCommon.SetProgress(processQueue, Consts.Consts.ProgressForProcessInterruptedBecauseOfNoObjectId);
				NotificationSender.SendNotification(processQueue, "Актуализация кадастровых данных",
					"Операция завершена с ошибкой, т.к. нет входных данных. Подробнее в списке процессов");
				return;
			}

			try
			{
				var reportId = UpdateTaskCadastralData(processQueue.ObjectId.Value);
				NotificationSender.SendNotification(processQueue, "Результат Операции Актуализации кадастровых данных", GetEmailMessage(reportId));
				WorkerCommon.SetProgress(processQueue, 100);
			}
			catch(Exception ex)
			{
				var errorId = ErrorManager.LogError(ex);
				var message = $"Операция Актуализации кадастровых данных была прервана: {ex.Message} (Подробнее в журнале: {errorId})";
				NotificationSender.SendNotification(processQueue, "Актуализация кадастровых данных", message);
				throw;
			}
		}

		private long UpdateTaskCadastralData(long taskId)
		{
			SetupInitialSettings(taskId);
			var units = GetUnits(taskId);

			var reportService = new GbuReportService();
			reportService.AddHeaders(0,
				new List<string>
				{
					"Кадастровый номер", "Кадастровый квартал старый", "Кадастровый квартал новый",
					"Кадастровый номер здания старый", "Кадастровый номер здания новый"
				});

			_locked = new object();
			CancellationTokenSource cancelTokenSource = new CancellationTokenSource();
			ParallelOptions options = new ParallelOptions
			{
				CancellationToken = cancelTokenSource.Token,
				MaxDegreeOfParallelism = 20
			};
			var currentDate = DateTime.Now;
			var gbuAttrIdList = new List<long>();
			if(_cadastralQuarterAttrId.HasValue)
				gbuAttrIdList.Add(_cadastralQuarterAttrId.Value);
			if(_buildingCadastralNumberAttrId.HasValue)
				gbuAttrIdList.Add(_buildingCadastralNumberAttrId.Value);

			Parallel.ForEach(units, options, unit =>
			{
				var gbuAttrValues = GbuObjectService.GetAllAttributes(unit.ObjectId.Value, null, gbuAttrIdList, currentDate);
				var currentCadastralQuarter = _cadastralQuarterAttrId.HasValue
					? gbuAttrValues.FirstOrDefault(x => x.AttributeId == _cadastralQuarterAttrId)?.GetValueInString()
					: unit.CadastralBlock;
				var currentBuildingCadastralNumber = _buildingCadastralNumberAttrId.HasValue
					? gbuAttrValues.FirstOrDefault(x => x.AttributeId == _buildingCadastralNumberAttrId)?.GetValueInString()
					: unit.BuildingCadastralNumber;

				if (currentCadastralQuarter != unit.CadastralBlock || currentBuildingCadastralNumber != unit.BuildingCadastralNumber)
				{
					lock (_locked)
					{
						AddRowToReport(reportService.GetCurrentRow(),
							unit.CadastralNumber,
							unit.CadastralBlock,
							currentCadastralQuarter,
							unit.BuildingCadastralNumber,
							currentBuildingCadastralNumber,
							reportService);
					}

					unit.CadastralBlock = currentCadastralQuarter;
					unit.BuildingCadastralNumber = currentBuildingCadastralNumber;
					unit.Save();
					unit.InheritedKOFactors();
				}
			});

			reportService.SetStyle();
			reportService.SetIndividualWidth(0, 4);
			reportService.SetIndividualWidth(1, 4);
			reportService.SetIndividualWidth(2, 4);
			reportService.SetIndividualWidth(3, 4);
			reportService.SetIndividualWidth(4, 4);
			var reportId = reportService.SaveReport("Отчет актуализация кадастровых данных", OMTask.GetRegisterId(), "KoTasks");

			return reportId;
		}

		private void SetupInitialSettings(long taskId)
		{
			var cadastralQuarterAttrId = UpdateCadastralDataService.GetCadastralDataCadastralQuarterAttributeId();
			_cadastralQuarterAttrId = cadastralQuarterAttrId;
			var buildingCadastralNumberAttrId = UpdateCadastralDataService.GetCadastralDataBuildingCadastralNumberAttributeId();
			_buildingCadastralNumberAttrId = buildingCadastralNumberAttrId;
			_taskName = TaskService.GetTemplateForTaskName(taskId);
		}

		private List<OMUnit> GetUnits(long taskId)
		{
			return OMUnit.Where(x => x.TaskId == taskId)
				.Select(x => new
				{
					x.Id,
					x.ObjectId,
					x.CadastralNumber,
					x.CadastralBlock,
					x.BuildingCadastralNumber,
					x.TourId
				})
				.Execute();
		}

		private void AddRowToReport(int rowNumber, string kn, string cadastralQuarterOld, string cadastralQuarterNew, string buildingKnOld, string buildingKnNew, GbuReportService reportService)
		{
			reportService.AddValue(kn, 0, rowNumber);
			reportService.AddValue(cadastralQuarterOld, 1, rowNumber);
			reportService.AddValue(cadastralQuarterNew, 2, rowNumber);
			reportService.AddValue(buildingKnOld, 3, rowNumber);
			reportService.AddValue(buildingKnNew, 4, rowNumber);
		}

		private string GetEmailMessage(long reportId)
		{
			return $"Операция Актуализации кадастровых данных для задания на оценку '{_taskName}' успешно завершена. " +
			       $@"<a href=""/DataExport/DownloadExportResult?exportId={reportId}"">Скачать результат</a>";
		}
	}
}
