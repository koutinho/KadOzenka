namespace KadOzenka.Dal.LongProcess.Common
{
    public static class Consts
    {
        public static long ProgressForProcessInterruptedBecauseOfNoObjectId = 100;
        public static long ProgressForProcessInterruptedBecauseOfNoDataLog = 100;
        public static long ProgressForProcessInterruptedBecauseOfNoUnloadResultQueue = 100;

        public static string MessageForProcessInterruptedBecauseOfNoObjectId => "Процесс не выполнен из-за отсутствия входных параметров (Id объекта).";

        public static string GetMessageForProcessInterruptedBecauseOfNoDataLog(long dataLogId)
        {
            return $"Процесс не выполнен, т.к. не был найден Журнал загрузки данных с Id = '{dataLogId}'.";
        }

        public static string GetMessageForProcessInterruptedBecauseOfNoUnloadResultQueue(long unloadResultQueueId) =>
	        $"Процесс не выполнен, т.к. не был найден Журнал выгрузки результатов оценки с Id = '{unloadResultQueueId}'.";

    }
}
