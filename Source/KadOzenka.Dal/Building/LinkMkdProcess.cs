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
using System.Text;
using System.Threading;

namespace CIPJS.DAL.Building
{
    /// <summary>
    /// Длительный процесс по связке зданий.
    /// </summary>
    public class LinkMkdProcess : ILongProcess
    {
        const int ThreadsCount = 10;


        private readonly BuildingService _buildingService;

        public LinkMkdProcess()
        {
            _buildingService = new BuildingService();
        }

        public void LogError(long? objectId, Exception ex, long? errorId = null)
        {

        }

        public bool Test()
        {
            return true;
        }

        public void StartProcess(OMProcessType processType, OMQueue processQueue, CancellationToken cancellationToken)
        {
            try
            {
                if (processQueue == null)
                {
                    throw new Exception("Не найдена OMQueue на обработку.");
                }
                processQueue.Message = $"[{DateTime.Now:dd.MM.yyyy HH:mm:ss}]: Начало работы\n";
                processQueue.Save();

                string listFilter = "";

                if (processQueue.ObjectRegisterId != null && processQueue.ObjectRegisterId == OMList.GetRegisterId() && processQueue.ObjectId != null)
                {
                    listFilter = $" and exists (select 1 from core_list_object l where l.list_id = {processQueue.ObjectId} and l.object_id = t.emp_id)";
                }

                DbCommand command = DBMngr.Realty.GetSqlStringCommand($"select t.emp_id from insur_building_q t where t.actual = 1 {listFilter}");
                DataTable dataTable = DBMngr.Realty.ExecuteDataSet(command).Tables[0];

                processQueue.Message += $"[{DateTime.Now:dd.MM.yyyy HH:mm:ss}]: Получено объектов для связывания: {dataTable.Rows.Count}\n";
                processQueue.Save();

                List<long> mkdLinks = dataTable?.AsEnumerable()?.Select(x => x["emp_id"].ParseToLong())?.ToList();

                // Проверяем на наличие корректного количества мкд.
                if (mkdLinks == null || mkdLinks.Count != 2)
                {
                    throw new Exception($"Не найдено достаточное количство МКД для связи, кол-во МКД: {mkdLinks?.Count}");
                }

                // Начинаем обработку.
                _buildingService.LinkTwoBuilding(mkdLinks.First(), mkdLinks.Last());

                // Заканчиваем обработку.
                processQueue.Progress = 100;
                processQueue.EndDate = DateTime.Now;
                processQueue.Message += $"[{DateTime.Now:dd.MM.yyyy HH:mm:ss}]: Обработка завершена";
                processQueue.Save();
            }
            catch (Exception ex)
            {
                ex.AddExtraData($"[{DateTime.Now:dd.MM.yyyy HH:mm:ss}]: Ошибка при связке МКД");
                var errorId = ErrorManager.LogError(ex);
                processQueue.Message = $"[{DateTime.Now:dd.MM.yyyy HH:mm:ss}]: Обработка завершена c ошибкой! Текст ошибки: {ex.Message}";
                processQueue.ErrorId = errorId;
                processQueue.EndDate = DateTime.Now;
                processQueue.Save();
            }
        }


    }
}
