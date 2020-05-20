using System;
using KadOzenka.Dal.ScoreCommon;
using ObjectModel.Core.LongProcess;
using ObjectModel.Modeling;

namespace KadOzenka.Dal.Modeling
{
    public abstract class AModelingStrategy
    {
        public abstract string Url { get; }
        protected ModelingService ModelingService { get; set; }


        protected AModelingStrategy()
        {
            ModelingService = new ModelingService(new ScoreCommonService());
        }


        public abstract void PrepareData();

        public abstract object GetRequestForService();

        public abstract void SaveResult(string responseFromService);

        public abstract void RollBackResult();

        public abstract void SendSuccessNotification(OMQueue processQueue);

        public abstract void SendFailNotification(OMQueue processQueue);


        protected OMModelingModel GetModel(long modelId)
        {
            var model = OMModelingModel.Where(x => x.Id == modelId).SelectAll().ExecuteFirstOrDefault();
            if (model == null)
                throw new Exception($"Не найдена модель с Id='{modelId}'");

            return model;
        }
    }
}
