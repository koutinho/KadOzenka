using System;
using System.Text.RegularExpressions;
using KadOzenka.Dal.Modeling.Entities;
using KadOzenka.Dal.ScoreCommon;
using ObjectModel.Core.LongProcess;
using ObjectModel.Modeling;

namespace KadOzenka.Dal.Modeling
{
    public abstract class AModelingStrategy
    {
        protected ModelingService ModelingService { get; set; }
        //для логирования работы
        protected OMQueue ProcessQueue { get; set; }


        protected AModelingStrategy(OMQueue processQueue)
        {
            ModelingService = new ModelingService(new ScoreCommonService());
            ProcessQueue = processQueue;
        }


        public abstract string GetUrl();

        public abstract void PrepareData();

        public abstract object GetRequestForService();

        public abstract void ProcessServiceResponse(GeneralResponse responseFromService);

        public abstract void RollBackResult();

        public abstract void SendSuccessNotification(OMQueue processQueue);

        public abstract void SendFailNotification(OMQueue processQueue, Exception exception);


        protected OMModelingModel GetModel(long modelId)
        {
            var model = OMModelingModel.Where(x => x.Id == modelId).SelectAll().ExecuteFirstOrDefault();
            if (model == null)
                throw new Exception($"Не найдена модель с Id='{modelId}'");

            return model;
        }

        protected string PreProcessAttributeName(string name)
        {
            var pattern = new Regex("[() ]");
            return pattern.Replace(name, string.Empty);
        }

        protected void AddLog(string message)
        {
            var previousLog = string.IsNullOrWhiteSpace(ProcessQueue.Log) ? string.Empty : ProcessQueue.Log;
            var newLog = previousLog + message;
            ProcessQueue.Log = newLog;
            ProcessQueue.Save();
        }
    }
}
