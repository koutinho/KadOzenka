using KadOzenka.Dal.ScoreCommon;
using ObjectModel.Modeling;
using KadOzenka.Dal.LongProcess.InputParameters;

namespace KadOzenka.Dal.Modeling
{
    public abstract class AModelingStrategy
    {
        public abstract string Url { get; }
        protected ModelingInputParameters InputParameters { get; }
        protected OMModelingModel Model { get; }
        protected ModelingService ModelingService { get; set; }


        protected AModelingStrategy(ModelingInputParameters inputParameters, OMModelingModel model)
        {
            InputParameters = inputParameters;
            Model = model;
            ModelingService = new ModelingService(new ScoreCommonService());
        }


        public abstract void PrepareData();

        public abstract object GetRequestForService();

        public abstract void SaveResult(string responseFromService);
    }
}
