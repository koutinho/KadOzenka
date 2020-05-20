using System;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using Core.Register.LongProcessManagment;
using ObjectModel.Core.LongProcess;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;


namespace KadOzenka.Dal.LongProcess
{
	public abstract class LongProcess : ILongProcess
	{
        protected const int PercentageInterval = 10;
        protected NotificationSender NotificationSender { get; set; }

		protected LongProcess()
		{
			NotificationSender = new NotificationSender();
		}

		public abstract void StartProcess(OMProcessType processType, OMQueue processQueue,
			CancellationToken cancellationToken);

		public virtual void LogError(long? objectId, Exception ex, long? errorId = null)
		{
			throw new NotImplementedException();
		}

		public bool Test()
		{
			return true;
		}

        protected async Task<string> SendDataToService(HttpClient httpClient, string url, object data)
        {
            if (string.IsNullOrWhiteSpace(url))
                throw new Exception("Не найден URL для сервиса");

            var stringPayload = await Task.Run(() => JsonConvert.SerializeObject(data));
            var httpContent = new StringContent(stringPayload, Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync(url, httpContent);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }

        protected string PreProcessServiceResponse(string responseContentStr)
        {
            //обрабатываем кириллицу
            responseContentStr = Regex.Replace(responseContentStr, @"\\u([0-9A-Fa-f]{4})", m => ((char)Convert.ToInt32(m.Groups[1].Value, 16)).ToString());
            if (string.IsNullOrWhiteSpace(responseContentStr))
                throw new Exception("Сервис для моделирования вернул пустой ответ");

            //TODO переделаем на обработку json-объекта ошибки, после реализации в сервисе
            if (responseContentStr.ToLower().Contains("message"))
                throw new Exception("Сервис для моделирования вернул ошибку: " + responseContentStr);

            return responseContentStr;
        }
    }
}
