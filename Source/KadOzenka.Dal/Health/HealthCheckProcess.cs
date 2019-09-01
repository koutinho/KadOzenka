using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Core.ErrorManagment;
using Core.Register.LongProcessManagment;
using Core.Shared.Exceptions;
using Core.Shared.Extensions;
using ObjectModel.Core.LongProcess;
using Platform.Main.ConfigParam.HealthCheck;

namespace CIPJS.DAL.Health
{
    public class HealthCheckProcess : ILongProcess
    {
        private HealthCheck currentCheck = null;

        public void StartProcess(OMProcessType processType, OMQueue processQueue, CancellationToken cancellationToken)
        {
            var parameters = processType.Parameters.DeserializeFromXml<HealthCheckProcessParameters>();
            var config = HealthCheckConfiguration.Config;
            HealthCheck[] checks;
            switch (parameters.Interval)
            {
                case HealthCheckInterval.Hourly:
                    checks = config.HourlyChecks;
                    break;
                case HealthCheckInterval.Daily:
                    checks = config.DailyChecks;
                    break;
                case HealthCheckInterval.Weekly:
                    checks = config.WeeklyChecks;
                    break;
                default:
                    throw new NotImplementedException();
            }

			int faultCount = 0;
			processQueue.Log = "Получено проверок: " + checks.Length + "\n";
			processQueue.Save();

			foreach (var healthCheck in checks)
            {
                this.currentCheck = healthCheck;
                if (cancellationToken.IsCancellationRequested)
                    return;


				bool success = new HealthCheckService(healthCheck).Start();

				if (!success)
					faultCount++;

				processQueue.Log += $"Проверка \"{healthCheck.Name}\": " + (success ? "Выполнена" : "Зафиксирована проблема") + "\n";
				processQueue.Message = $"Зафиксировано проблем: {faultCount}";
				processQueue.Save();
			}
        }

        public void LogError(long? objectId, Exception ex, long? errorId = null)
        {
            if (ex is TaskCanceledException)
                return;
            if (currentCheck?.Name == null)
                return;
            ex.Data.Add(ExceptionInitializer.ExtraDataKey, "Текущая проверка: " + this.currentCheck.Name);
            ErrorManager.LogError(ex);
        }

        public bool Test() => true;
    }
}
