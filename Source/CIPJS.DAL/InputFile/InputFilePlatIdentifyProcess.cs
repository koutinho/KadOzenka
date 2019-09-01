using CIPJS.DAL.InputPlat;
using Core.Register.LongProcessManagment;
using Core.SRD;
using ObjectModel.Core.LongProcess;
using ObjectModel.Directory;
using ObjectModel.Insur;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace CIPJS.DAL.InputFile
{
    public class InputFilePlatIdentifyProcess : ILongProcess
    {
        public void StartProcess(OMProcessType processType, OMQueue processQueue, CancellationToken cancellationToken)
        {
			string logTotal = $"[{DateTime.Now:dd.MM.yyyy HH:mm:ss}]: Начало\n";
			processQueue.Message = logTotal;
			processQueue.Save();

			if (!processQueue.ObjectId.HasValue)
			{
				processQueue.Message = logTotal + $"[{DateTime.Now:dd.MM.yyyy HH:mm:ss}]: пустой ИД объекта\n";
				processQueue.Save();
				return;
			}

            InputPlatService inputPlatService = new InputPlatService();
            InputFileService inputFileService = new InputFileService();

            OMFilePlatIdentifyLog log = OMFilePlatIdentifyLog
                .Where(x => x.EmpId == processQueue.ObjectId.Value)
                .SelectAll()
                .Execute()
                .FirstOrDefault();

            if (!log.InputFileId.HasValue)
            {
				processQueue.Message = logTotal + $"[{DateTime.Now:dd.MM.yyyy HH:mm:ss}]: пустой ИД файла\n";
				processQueue.Save();

				log.Status_Code = IdentifyPlatStatus.Finished;
                log.Save();

                return;
            }

            long platCount = OMInputPlat.Where(x => x.LinkIdFile == log.InputFileId.Value).ExecuteCount();

            if (platCount == 0)
            {
                log.NotIdentiedCount = 0;
                log.IdentifiedCount = 0;
                log.Status_Code = IdentifyPlatStatus.Finished;
                log.Save();

				processQueue.Message = logTotal + $"[{DateTime.Now:dd.MM.yyyy HH:mm:ss}]: количество платежей = 0\n";
				processQueue.Save();

				return;
            }

            log.StartDate = DateTime.Now;
            log.Status_Code = IdentifyPlatStatus.Identify;
            log.PlatCount = platCount;
            log.IdentifiedCount = 0;
            log.NotIdentiedCount = 0;
            log.Save();

            int packageIndex = 0;

			logTotal += $"[{DateTime.Now:dd.MM.yyyy HH:mm:ss}]: начало обработки зачислений, у которых заполнена площадь\n";
			processQueue.Message = logTotal;
			processQueue.Save();

			//CIPJS-305 сначала обрабатываем зачисления, у которых заполнена площадь
			while (true)
            {
                List<OMInputPlat> packageInputPlatList = OMInputPlat
                    .Where(x => x.LinkIdFile == log.InputFileId.Value
                        && x.Opl != null)
                    .OrderBy(x => x.EmpId)
                    .Select(x => x.Kodpl)
                    .Select(x => x.StatusIdentif)
                    .Select(x => x.StatusIdentif_Code)
                    .Select(x => x.PmtDate)
                    .Select(x => x.PeriodRegDate)
                    .Select(x => x.SumOpl)
                    .Select(x => x.LinkBankId)
                    .Select(x => x.Opl)
                    .SetPackageIndex(packageIndex)
                    .SetPackageSize(1000)
                    .Execute();

                if (packageInputPlatList.Count == 0)
                {
                    break;
                }

                foreach(OMInputPlat inputPlat in packageInputPlatList)
                {
                    OMInputPlat identifiedInputPlat = inputPlatService.IdentifyPlat(inputPlat);

                    if (identifiedInputPlat.StatusIdentif_Code == StatusIdentifikacii.Identified ||
                        identifiedInputPlat.StatusIdentif_Code == StatusIdentifikacii.PartiallyIdentified)
                    {
                        log.IdentifiedCount++;
                    }
                    else
                    {
                        log.NotIdentiedCount++;
                    }

                    log.Save();
                }
				
				processQueue.Message = logTotal + $"[{DateTime.Now:dd.MM.yyyy HH:mm:ss}]: завершена обработка пакета {packageIndex}\n";
				processQueue.Save();
				
				packageIndex++;
            }

			logTotal += $"[{DateTime.Now:dd.MM.yyyy HH:mm:ss}]: обработано пакетов {packageIndex}. начало обработки зачислений без площади\n";
			processQueue.Message = logTotal;
			processQueue.Save();

			packageIndex = 0;
			
			//CIPJS-305 затем начисления без площади
			while (true)
            {
                List<OMInputPlat> packageInputPlatList = OMInputPlat
                    .Where(x => x.LinkIdFile == log.InputFileId.Value
                        && x.Opl == null)
                    .OrderBy(x => x.EmpId)
                    .Select(x => x.Kodpl)
                    .Select(x => x.StatusIdentif)
                    .Select(x => x.StatusIdentif_Code)
                    .Select(x => x.PmtDate)
                    .Select(x => x.PeriodRegDate)
                    .Select(x => x.SumOpl)
                    .Select(x => x.LinkBankId)
                    .Select(x => x.Opl)
                    .SetPackageIndex(packageIndex)
                    .SetPackageSize(1000)
                    .Execute();

                if (packageInputPlatList.Count == 0)
                {
                    break;
                }

                foreach (OMInputPlat inputPlat in packageInputPlatList)
                {
                    OMInputPlat identifiedInputPlat = inputPlatService.IdentifyPlat(inputPlat);

                    if (identifiedInputPlat.StatusIdentif_Code == StatusIdentifikacii.Identified ||
                        identifiedInputPlat.StatusIdentif_Code == StatusIdentifikacii.PartiallyIdentified)
                    {
                        log.IdentifiedCount++;
                    }
                    else
                    {
                        log.NotIdentiedCount++;
                    }

                    log.Save();
                }
				
				processQueue.Message = logTotal + $"[{DateTime.Now:dd.MM.yyyy HH:mm:ss}]: завершена обработка пакета {packageIndex}\n";
				processQueue.Save();

				packageIndex++;
            }

            log.EndDate = DateTime.Now;
            log.Status_Code = IdentifyPlatStatus.Finished;
            log.Save();

			logTotal += $"[{DateTime.Now:dd.MM.yyyy HH:mm:ss}]: обработано пакетов {packageIndex}.\n";
			processQueue.Message = logTotal;
			processQueue.Save();

            OMInputFile inputFile = OMInputFile.Where(x => x.EmpId == log.InputFileId.Value)
                .Select(x => x.Status)
                .Select(x => x.Status_Code)
                .ExecuteFirstOrDefault();

            if (inputFile != null)
            {
                if (inputFileService.IsStrahFileIdentifiedcCompletely(inputFile.EmpId))
                {
                    inputFile.Status_Code = UFKFileProcessingStatus.LinkedBankCompletely;
                }
                else
                {
                    inputFile.Status_Code = UFKFileProcessingStatus.LinkedBankPartially;
                }
                inputFile.Save();
            }

			//CIPJS-304 возможно по факту идентификации платежа ( после того как он связан с конкретным ФСО, его сразу и зачислять на ФСП
			if (log.NeedProcess.HasValue && log.NeedProcess.Value)
            {
                OMFileProcessLog processLog = new OMFileProcessLog();
                processLog.InputFileId = log.InputFileId.Value;
                processLog.Status_Code = FileProcessStatus.Prepare;
                processLog.UserId = SRDSession.GetCurrentUserId();
                processLog.Save();

                LongProcessManager.AddTaskToQueue("InputFileProcess", OMFileProcessLog.GetRegisterId(), processLog.EmpId);

                if (inputFile != null)
                {
                    inputFile.Status_Code = UFKFileProcessingStatus.InProcess;
                    inputFile.Save();
                }
            }

			logTotal += $"[{DateTime.Now:dd.MM.yyyy HH:mm:ss}]: Конец\n";
			processQueue.Message = logTotal;
			processQueue.Save();
		}

        public void LogError(long? objectId, Exception ex, long? errorId = null)
        {
        }

        public bool Test()
        {
            return true;
        }
    }
}
