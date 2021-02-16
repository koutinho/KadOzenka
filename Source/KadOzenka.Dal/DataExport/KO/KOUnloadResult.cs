using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;
using ObjectModel.Core.LongProcess;
using ObjectModel.Directory;
using ObjectModel.KO;
using Serilog;

namespace KadOzenka.Dal.DataExport
{
    /// <summary>
    /// Класс итоговой выгрузки результатов
    /// </summary>
    public class KOUnloadResult
    {
        private static readonly ILogger _log = Log.ForContext<KOUnloadResult>();
        public static List<KoUnloadResultType> GetKoUnloadResultTypes(KOUnloadSettings setting)
        {
            List<KoUnloadResultType> koUnloadResults = new List<KoUnloadResultType>();

            var usedUnloadTypes = typeof(KOUnloadSettings).GetFields()
                .Where(fieldInfo => fieldInfo.GetCustomAttributes(typeof(KoUnloadResultTypeAttribute), false).Length > 0
                                    && (bool) fieldInfo.GetValue(setting))
                .Select(fieldInfo =>
                    ((KoUnloadResultTypeAttribute) fieldInfo.GetCustomAttributes(typeof(KoUnloadResultTypeAttribute), false).First())
                    .UnloadType);
            koUnloadResults.AddRange(usedUnloadTypes);

            return koUnloadResults;
        }

        /// <summary>
        /// Выгрузка результатов
        /// </summary>
        public static List<ResultKoUnloadSettings> Unload(OMQueue queue, OMUnloadResultQueue unloadResultQueue, KOUnloadSettings setting)
        {
            List<ResultKoUnloadSettings> result = new List<ResultKoUnloadSettings>();
            setting.DirectoryName = "";
            var counter = new UnloadCounter(setting, queue, 90);
            SetProgress setProgress = counter.ReportProgress;

            var koUnloadResultTypes = JsonConvert.DeserializeObject<List<KoUnloadResultType>>(unloadResultQueue.UnloadTypesMapping);
            var unloadResultMethodInfoDictionary = GetUnloadResultMethodInfoDictionary();

            _log.ForContext("UnloadTypesMapping", unloadResultQueue.UnloadTypesMapping)
                .Information("Текущее количество выгрузок {UnloadCurrentCount} из {UnloadTotalCount} ", unloadResultQueue.UnloadCurrentCount, unloadResultQueue.UnloadTotalCount);

            var unloadCurrentCount = 1;
            foreach (var koUnloadResultType in koUnloadResultTypes)
            {
              
                unloadResultQueue.UnloadCurrentCount = unloadCurrentCount;
                unloadResultQueue.CurrentUnloadType_Code = koUnloadResultType;
                unloadResultQueue.Save();
                try
                {
                    //TODO: сейчас не реализована Выгрузка истории по объектам
                    if (koUnloadResultType != KoUnloadResultType.UnloadHistory)
                    {
                        var unloadResult = (List<ResultKoUnloadSettings>)unloadResultMethodInfoDictionary[koUnloadResultType]
                            .Invoke(null, new object[] { unloadResultQueue, setting, setProgress });

                        result.AddRange(unloadResult);
                    }
                }
                catch (Exception ex) {
                    _log.ForContext("UnloadCurrentCount", unloadCurrentCount)
                        .Warning(ex, "Ошибка в процессе выгрузки {CurrentUnloadTypeCode}", unloadResultQueue.CurrentUnloadType_Code);
                }
                
                unloadCurrentCount++;
            }
            _log.Debug("Выгрузка результатов {ResultCount} из {UnloadCurrentCount}", result.Count, unloadCurrentCount);
            return result;
        }

        private static Dictionary<KoUnloadResultType, MethodInfo> GetUnloadResultMethodInfoDictionary()
        {
            var assemblyTypes = Assembly.GetExecutingAssembly().GetTypes()
                .Where(x => typeof(IKoUnloadResult).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract).ToList();

            var methods = assemblyTypes
                .SelectMany(t => t.GetMethods())
                .Where(m => m.GetCustomAttributes(typeof(KoUnloadResultActionAttribute), false).Length > 0)
                .ToDictionary(
                    x => ((KoUnloadResultActionAttribute) x
                        .GetCustomAttributes(typeof(KoUnloadResultActionAttribute), false)
                        .First()).UnloadType, x => x);

            return methods;
        }

        public static void SetCurrentProgress(OMUnloadResultQueue unloadResultQueue, long progress)
        {
            unloadResultQueue.CurrentUnloadProgress = progress;
            unloadResultQueue.Save();
        }
    }
}