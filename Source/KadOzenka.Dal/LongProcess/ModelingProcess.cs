using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;
using Core.Register.LongProcessManagment;
using ObjectModel.Core.LongProcess;
using System.Threading;
using System.Threading.Tasks;
using Core.Register.QuerySubsystem;
using Core.Shared.Extensions;
using KadOzenka.Dal.Correction;
using KadOzenka.Dal.LongProcess.InputParameters;
using KadOzenka.Dal.Modeling;
using KadOzenka.Dal.Modeling.Dto;
using KadOzenka.Dal.ScoreCommon;
using Newtonsoft.Json;
using ObjectModel.Directory;
using ObjectModel.Directory.MarketObjects;
using ObjectModel.Market;
using ObjectModel.Modeling;

namespace KadOzenka.Dal.LongProcess
{
	public class ModelingProcess : LongProcess
	{
        public const string LongProcessName = nameof(ModelingProcess);

        private static HttpClient _httpClient = new HttpClient();
        //TODO код для отладки, будет удален позже
        //private string _urlToTrainModel = ConfigurationManager.AppSettings["trainModelLink"];
        //private string _urlToCalculation = ConfigurationManager.AppSettings["calculateModelLink"];
        private string _urlToTrainModel = "http://82.148.28.237:5000/api/teach/TestModel";
        private string _urlToCalculation = "http://82.148.28.237:5000/api/predict/TestModel";
        private ModelingService ModelingService { get; set; }

        public static void AddProcessToQueue(long modelId)
		{
			LongProcessManager.AddTaskToQueue(LongProcessName, objectId: modelId, registerId: OMModelingModel.GetRegisterId());
		}

        //TODO
		public override void StartProcess(OMProcessType processType, OMQueue processQueue, CancellationToken cancellationToken)
		{
            //WorkerCommon.SetProgress(processQueue, 0);
            //if (!processQueue.ObjectId.HasValue)
            //{
            //	WorkerCommon.SetMessage(processQueue, Consts.Consts.MessageForProcessInterruptedBecauseOfNoObjectId);
            //	WorkerCommon.SetProgress(processQueue, Consts.Consts.ProgressForProcessInterruptedBecauseOfNoObjectId);
            //	return;
            //}

            var isTrainingMode = false;
            if (!string.IsNullOrWhiteSpace(processQueue.Parameters))
            {
                var request = processQueue.Parameters.DeserializeFromXml<ModelingRequest>();
                isTrainingMode = request.IsTrainingMode;
            }

            ModelingService = new ModelingService(new ScoreCommonService());
            var modelId = processQueue.ObjectId.Value;
            var model = GetModel(modelId, isTrainingMode);

            if (isTrainingMode)
            {
                //ModelingService.CreateObjectsForModel(modelId);
            }

            var coefficientsForModel = GetCoefficientsForModel(modelId, isTrainingMode);

            //SendDataToService(coefficientsForModel, model.Name, isTrainingMode).GetAwaiter().GetResult();

            if (isTrainingMode)
            {
                model.WasTrained = true;
                model.Save();
            }

            //WorkerCommon.SetProgress(processQueue, 100);
        }


        #region Support Methods

        private OMModelingModel GetModel(long modelId, bool isTrainingMode)
        {
            var model = OMModelingModel.Where(x => x.Id == modelId).SelectAll().ExecuteFirstOrDefault();
            if (model == null)
                throw new Exception($"Не найдена модель с Id='{modelId}'");

            if (!isTrainingMode && !model.WasTrained.GetValueOrDefault())
                throw new Exception($"Модель '{model.Name}' с Id='{modelId}' не была обучена. Расчет невозможен.");

            return model;
        }

        private Data GetCoefficientsForModel(long modelId, bool isForTraining)
        {
            var data = new Data(isForTraining);

            var modelAttributes = ModelingService.GetModelAttributes(modelId);
            data.AttributeNames.AddRange(modelAttributes.Select(x => x.AttributeName).ToList());

            var modelObjects = GetIncludedModelObjects(modelId, isForTraining);
            modelObjects.ForEach(modelObject =>
            {
                var modelObjectAttributes = modelObject.Coefficients.DeserializeFromXml<List<ModelAttributeDto>>();
                if (modelObjectAttributes == null || modelObjectAttributes.Count == 0)
                    return;

                var coefficients = new List<decimal?>();
                modelAttributes.ForEach(modelAttribute =>
                {
                    coefficients.Add(modelObjectAttributes.FirstOrDefault(x => x.AttributeId == modelAttribute.AttributeId)?.Coefficient);
                });

                data.Coefficients.Add(coefficients);
                data.Prices.Add(modelObject.Price);
                data.CadastralNumbers.Add(modelObject.CadastralNumber);
                data.OmModelToMarketObjectsIds.Add(modelObject.Id);
            });

            return data;
        }

        private List<OMModelToMarketObjects> GetIncludedModelObjects(long modelId, bool isForTraining)
        {
            return OMModelToMarketObjects
                .Where(x => x.ModelId == modelId && x.IsExcluded.Coalesce(false) == false &&
                            x.IsForTraining.Coalesce(false) == isForTraining).SelectAll().Execute();
        }


        private async Task SendDataToService(Data data, string modelName, bool isTrainingMode)
        {
            var stringPayload = await Task.Run(() => JsonConvert.SerializeObject(data));
            var httpContent = new StringContent(stringPayload, Encoding.UTF8, "application/json");

            var path = isTrainingMode ? _urlToTrainModel : _urlToCalculation;
            if (string.IsNullOrWhiteSpace(path))
                throw new Exception("Не найден URL для сервиса моделирования");

            var response = await _httpClient.PostAsync(path, httpContent);
            response.EnsureSuccessStatusCode();

            //if (!isTrainingMode)
            {
                await SavePriceFromModel(data, response);
            }
        }

        private static async Task SavePriceFromModel(Data data, HttpResponseMessage responseMessage)
        {
            var responseContentStr = await responseMessage.Content.ReadAsStringAsync();
            responseContentStr = Regex.Replace(responseContentStr, @"\\u([0-9A-Fa-f]{4})", m => ((char)Convert.ToInt32(m.Groups[1].Value, 16)).ToString());
            if (string.IsNullOrWhiteSpace(responseContentStr))
                throw new Exception("Сервис для моделирования вернул пустой ответ");
            
            var preprocessedPricesStr = responseContentStr.Substring(1, responseContentStr.Length - 2);
            var prices = preprocessedPricesStr.Split(',').Select(x => Convert.ToDecimal(x, CultureInfo.InvariantCulture))
                .ToArray();
            //if(prices.Length != data.CadastralNumbers.Count)
            //    throw new Exception("Сервис для моделирования вернул цены не для всех объектов");
            for (var i = 0; i < data.CadastralNumbers.Count; i++)
            {
                var modelObject = OMModelToMarketObjects.Where(x => x.Id == data.OmModelToMarketObjectsIds[i])
                    .ExecuteFirstOrDefault();
                if (modelObject == null)
                    continue;

                modelObject.PriceFromModel = prices[i];
                modelObject.Save();
            }
        }

        #endregion


        public class Data
        {
            [JsonIgnore]
            public bool IsForTraining { get; set; }
            [JsonIgnore]
            public List<string> CadastralNumbers { get; set; }
            [JsonIgnore]
            public List<long> OmModelToMarketObjectsIds { get; set; }

            [JsonProperty("y_train")]
            public List<decimal> Prices { get; set; }
            [JsonProperty("columns")]
            public List<string> AttributeNames { get; set; }
            [JsonProperty("x_predict")]
            //[JsonProperty("x_train")]
            public List<List<decimal?>> Coefficients { get; set; }

            public Data(bool isForTraining)
            {
                IsForTraining = isForTraining;
                CadastralNumbers = new List<string>();
                OmModelToMarketObjectsIds = new List<long>();

                Prices = new List<decimal>();
                AttributeNames = new List<string>();
                Coefficients = new List<List<decimal?>>();
            }

            public bool ShouldSerializePrices()
            {
                return IsForTraining;
            }

            public bool ShouldSerializeAttributeNames()
            {
                return IsForTraining;
            }
        }
    }
}
