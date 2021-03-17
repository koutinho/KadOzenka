using GemBox.Spreadsheet;
using System;
using System.IO;
using Core.ErrorManagment;
using Core.Main.FileStorages;
using Core.Register.LongProcessManagment;
using Core.SRD;
using ObjectModel.Common;
using ObjectModel.Core.LongProcess;
using ObjectModel.KO;
using ObjectModel.Directory;
using ObjectModel.Directory.Common;

namespace KadOzenka.Dal.DataExport
{
    public struct ActOpredel
    {
        public string kn;
        public string osnovanie;
        public string code;
        public string subgroup;
        public string act_model;
        public decimal? kc;
        public string act_dop;
        public string act_other;
    }

    public class GeneralizedValuesUPKSZ
    {
        public int NumberGroup = 0; //Количество групп
        public int CountObj = 0; //Количество объектов в районе
        public string CadastralArea = ""; //Номер кадастрового района
        public string CadastralBlok = ""; //Номер кадастрового квартала
        public double[,] MinAvgMax; //Массив УПКСЗ по группам

        public GeneralizedValuesUPKSZ(int _num)
        {
            NumberGroup = _num;
            MinAvgMax = new double[4, NumberGroup];
            for (int i = 0; i < NumberGroup; i++)
            {
                MinAvgMax[0, i] = -1; //Минимальное УПКСЗ
                MinAvgMax[1, i] = 0; //Среднее УПКСЗ. В начале записывается сумма УПКСЗ. Потом пересчитывается.
                MinAvgMax[2, i] = 0; //Максимальное УПКСЗ
                MinAvgMax[3, i] = 0; //Количество объектов данной группы.
            }
        }
    }

    public class KoUnloadResultActionAttribute : Attribute
    {
        public KoUnloadResultType UnloadType { get; }

        public KoUnloadResultActionAttribute(KoUnloadResultType unloadType)
        {
            UnloadType = unloadType;
        }
    }

    public delegate void SetProgress(int taskProgress, bool reportFinish = false, string progressMessage = "");

    class UnloadCounter
    {
        private int _currentTaskProgress;
        private int _completedTasks;
        private int _totalTasks;
        private int _maxValue;
        private int _totalProgress;
        private OMQueue _queue;

        public UnloadCounter(KOUnloadSettings settings, OMQueue queue, int maxValue)
        {
            _totalProgress = 0;
            _maxValue = maxValue;
            _queue = queue;
            var counter = 0;
            if (settings.UnloadChange) counter++;
            if (settings.UnloadTable04) counter++;
            if (settings.UnloadTable05) counter++;
            if (settings.UnloadTable07) counter++;
            if (settings.UnloadTable08) counter++;
            if (settings.UnloadTable09) counter++;
            if (settings.UnloadTable10) counter++;
            if (settings.UnloadTable11) counter++;
            if (settings.UnloadXML1) counter++;
            if (settings.UnloadXML2) counter++;
            if (settings.UnloadDEKOResponseDocExportToXml) counter++;
            if (settings.UnloadDEKOVuonExportToXml) counter++;
            _totalTasks = counter;
        }

        public void ReportProgress(int prog, bool endOfProcess, string progressMessage)
        {
            if (endOfProcess)
            {
                var logMessage = $"[{DateTime.Now.ToLongTimeString()}] Завершено - {progressMessage}";
                _currentTaskProgress = 0;
                _completedTasks++;
                WorkerCommon.LogState(_queue, logMessage);
            }
            else
            {
                if (_currentTaskProgress < prog)
                {
                    var logMessage = $"[{DateTime.Now.ToLongTimeString()}] {progressMessage} - {_currentTaskProgress}%";
                    _currentTaskProgress = prog;
                    WorkerCommon.LogState(_queue, logMessage);
                }
            }

            var progress = (_completedTasks * 100 + _currentTaskProgress) * _maxValue / (_totalTasks * 100);
            if (progress > _totalProgress)
            {
                var logMessage = $"[{DateTime.Now.ToLongTimeString()}] Общий прогресс выгрузки: {_totalProgress}%";
                _totalProgress = progress;
                WorkerCommon.SetProgress(_queue, progress);
                WorkerCommon.LogState(_queue, logMessage);
            }
        }
    }

    public class SaveUnloadResult
    {
        public static long SaveResult(string nameReport, ExcelFile excel, long unloadId,
            KoUnloadResultType unloadResultType = KoUnloadResultType.None)
        {
            MemoryStream stream = new MemoryStream();
            excel.Save(stream, SaveOptions.XlsxDefault);
            stream.Seek(0, SeekOrigin.Begin);

            return SaveResult(nameReport, stream, unloadId, "xlsx", unloadResultType);
        }

        public static long SaveResult(string nameReport, Stream stream, long unloadId, string reportExtension = "xml",
            KoUnloadResultType unloadResultType = KoUnloadResultType.None)
        {
            try
            {
                var reportId = UnloadResultStorageManager.SaveUnloadFile(stream, nameReport, reportExtension, unloadId, unloadResultType);
                return reportId;
            }
            catch (Exception e)
            {
                ErrorManager.LogError(e);
                return 0;
            }
        }

        public static void SetCurrentProgress(OMUnloadResultQueue unloadResultQueue, long progress)
        {
            unloadResultQueue.CurrentUnloadProgress = progress;
            unloadResultQueue.Save();
        }
    }
}