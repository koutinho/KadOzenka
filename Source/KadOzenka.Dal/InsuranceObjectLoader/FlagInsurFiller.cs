using CIPJS.DAL.Building;
using Core.ErrorManagment;
using Core.Register.LongProcessManagment;
using Core.Shared.Exceptions;
using Core.Shared.Extensions;
using Microsoft.Practices.EnterpriseLibrary.Data;
using ObjectModel.Core.LongProcess;
using ObjectModel.Core.Register;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CIPJS.DAL.InsuranceObjectLoader
{
    public class FlagInsurFiller : ILongProcess
	{
		const int ThreadsCount = 10;

		class ImportObject
		{
			public long InsurBuildingId { get; set; }
			public long BtiBuildingId { get; set; }
			public long? LinkEgrnBuild { get; set; }
			public bool? FlagInsurCalculated { get; set; }
		}

		public void StartProcess(OMProcessType processType, OMQueue processQueue, CancellationToken cancellationToken)
		{
			string commonLog = $"[{DateTime.Now:dd.MM.yyyy HH:mm:ss}]: Начало работы\n";
			processQueue.Message = commonLog;
			processQueue.Save();

			string listFilter = "";

			if (processQueue.ObjectRegisterId != null && processQueue.ObjectRegisterId == OMList.GetRegisterId() && processQueue.ObjectId != null)
			{
				listFilter = $" and exists (select 1 from core_list_object l where l.list_id = {processQueue.ObjectId} and l.object_id = t.emp_id)";
			}

            //DbCommand command = DBMngr.Realty.GetSqlStringCommand($"select t.emp_id, t.link_bti_fsks, t.link_egrn_bild, t.flag_insur_calculated from insur_building_q t where t.actual = 1 and t.link_bti_fsks is not null {listFilter}");
            DbCommand command = DBMngr.Realty.GetSqlStringCommand($"select t.emp_id, t.link_bti_fsks, t.link_egrn_bild, t.flag_insur_calculated from insur_building_q t where t.actual = 1 {listFilter}");
            DataTable dataTable = DBMngr.Realty.ExecuteDataSet(command).Tables[0];

			commonLog += $"[{DateTime.Now:dd.MM.yyyy HH:mm:ss}]: Получено объектов для обновления: {dataTable.Rows.Count}\n";
			processQueue.Message = commonLog;
			processQueue.Save();

			List<ImportObject> objects = dataTable.AsEnumerable().Select(x => new ImportObject{
				InsurBuildingId = x["emp_id"].ParseToLong(),
				BtiBuildingId = x["link_bti_fsks"].ParseToLong(),
				LinkEgrnBuild = x["link_egrn_bild"].ParseToLongNullable(),
				FlagInsurCalculated = x["flag_insur_calculated"].ParseToBooleanNullable(),
			}).ToList();

			int packageNumber = 0;
			int changedCount = 0;
			int notChanged = 0;
			int errorCount = 0;
			int processedTotal = 0;

			int PackageSize = 1000;
			
			while (true)
			{
				List<ImportObject> package = objects.Skip(PackageSize * packageNumber).Take(PackageSize).ToList();

				if(package.Count == 0)
				{
					break;
				}

				List<long> btiBuildingIds = package.Select(x => x.BtiBuildingId).ToList();

				List<ObjectModel.Bti.OMBtiBuilding> btiBuildings = ObjectModel.Bti.OMBtiBuilding.Where(x => btiBuildingIds.Contains(x.EmpId)).SelectAll().Execute();

				var btiBuildingIdsDictionary = btiBuildings.ToDictionary(x => x.EmpId, x => x);

				ParallelOptions options = new ParallelOptions()
				{
					CancellationToken = cancellationToken,
					MaxDegreeOfParallelism = ThreadsCount
				};

				Parallel.ForEach(package, options, x =>
				{
					try
					{
						ObjectModel.Bti.OMBtiBuilding btiBuilding = null;

						bool? flagInsurCalculated = null;

						if(btiBuildingIdsDictionary.TryGetValue(x.BtiBuildingId, out btiBuilding))
						{
							flagInsurCalculated = BuildingService.CalculateFlagInsur(x.LinkEgrnBuild, btiBuilding);
						}

						if(x.FlagInsurCalculated != flagInsurCalculated)
						{
							new ObjectModel.Insur.OMBuilding
							{
								EmpId = x.InsurBuildingId,
								FlagInsurCalculated = flagInsurCalculated
							}.Save();

                            //CIPJS-950: Доработка функционала копирования признака "Подлежит страхованию" в карточке МКД
                            BuildingService.CopyFlatFlagInsurFromBuilding(x.InsurBuildingId, flagInsurCalculated);

                            changedCount++;
                        }
						else
						{
							notChanged++;
						}
					}
					catch (Exception ex)
					{
						errorCount++;

						ex.AddExtraData($"Ошибка обновления проставления признака Подлежит страхованию: InsurBuildingId={x.InsurBuildingId}; BtiBuildingId={x.BtiBuildingId};");
						ErrorManager.LogError(ex);
					}
				});

                /* Сохранить прогресс выполнения */
                processQueue.Message = commonLog + 
					$"[{DateTime.Now:dd.MM.yyyy HH:mm:ss}]: Пакет = {packageNumber}; Обновлено = {changedCount}; Обновление не требуется: {notChanged}; Ошибка обновления = {errorCount}\n";
                packageNumber++;
				processedTotal += objects.Count < PackageSize ? objects.Count : objects.Count;


				processQueue.Progress = processedTotal * 100 / objects.Count;
                processQueue.Save();
			}

			processQueue.Progress = 100;
			processQueue.Message += $"[{DateTime.Now:dd.MM.yyyy HH:mm:ss}]: Обработка завершена";
			processQueue.Save();
		}

		public void LogError(long? objectId, Exception ex, long? errorId = null) { }

		public bool Test() { return true; }
	}
}
