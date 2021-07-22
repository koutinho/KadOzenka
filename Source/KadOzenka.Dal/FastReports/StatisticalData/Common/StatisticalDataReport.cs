using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using Core.Register.RegisterEntities;
using Core.UI.Registers.Reports.Model;
using KadOzenka.Dal.GbuObject;
using KadOzenka.Dal.Groups;
using KadOzenka.Dal.ManagementDecisionSupport.StatisticalData;
using KadOzenka.Dal.ManagementDecisionSupport.StatisticalData.Entities;
using KadOzenka.Dal.Registers.GbuRegistersServices;
using Newtonsoft.Json;
using ObjectModel.Directory;
using ObjectModel.KO;
using Platform.Reports;
using System.Data;
using CommonSdks;
using ModelingBusiness.Factors;
using ModelingBusiness.Factors.Repositories;
using ModelingBusiness.Model;
using ModelingBusiness.Model.Repositories;
using Serilog;

namespace KadOzenka.Dal.FastReports.StatisticalData.Common
{
	public abstract class StatisticalDataReport : FastReportBase
	{
        public static readonly string DateFormat = "dd.MM.yyyy";
        protected string DecimalFormat => "#,##0.00";
        public static readonly int PrecisionForDecimalValues = 2;
        protected QueryManager QueryManager;

        protected readonly GbuObjectService GbuObjectService;
		protected readonly StatisticalDataService StatisticalDataService;
        protected readonly RosreestrRegisterService RosreestrRegisterService;
        protected readonly GbuCodRegisterService GbuCodRegisterService;
        protected readonly ModelService ModelService;
        protected readonly ModelRepository ModelRepository;
        protected readonly ModelFactorsService ModelFactorsService;
		protected readonly GroupService GroupService;
		protected readonly FactorsService FactorsService;
		protected virtual ILogger Logger { get; set; }

        protected StatisticalDataReport()
		{
			QueryManager = new QueryManager();
            GbuObjectService = new GbuObjectService(QueryManager);
            StatisticalDataService = new StatisticalDataService();
            RosreestrRegisterService = new RosreestrRegisterService();
            GbuCodRegisterService = new GbuCodRegisterService();
            ModelService = new ModelService();
            ModelRepository = new ModelRepository();
            ModelFactorsService = new ModelFactorsService();
            GroupService = new GroupService();
            FactorsService = new FactorsService();
           
        }

		public override string GetTitle(long? objectId)
		{
			return ReportType.Title;
		}

		protected override DataSet GetData(NameValueCollection query, HashSet<long> objectList = null)
		{
			QueryManager.SetBaseToken(CancellationToken);

            Logger.Debug("Начат сбор данных");

			var loggedHeaders = query.AllKeys.ToDictionary(h => h, h => GetQueryParam<string>(h, query));
			Logger.ForContext("InputParameters", loggedHeaders, destructureObjects: true).Debug("Входные параметры");

            var reportData = GetReportData(query, objectList);
            Logger.Debug("Закончен сбор данных");

            return reportData;
		}

		protected abstract DataSet GetReportData(NameValueCollection query, HashSet<long> objectList = null);


		#region Filter

		protected long[] GetTaskIdList(NameValueCollection query)
        {
            var taskIdList = GetQueryParam<string>("TaskIdList", query);
            if (string.IsNullOrEmpty(taskIdList))
            {
                throw new Exception("Истекло время ожидания сессии. Обновите страницу.");
            }
            var taskIdListValue = JsonConvert.DeserializeObject<long[]>(taskIdList);

            return taskIdListValue;
        }

        protected long GetTourId(NameValueCollection query)
        {
            var tourId = GetQueryParam<long?>("TourId", query);
            if (!tourId.HasValue)
            {
                throw new Exception("Истекло время ожидания сессии. Обновите страницу.");
            }

            return tourId.Value;
        }

        protected long GetGroupIdFromFilter(NameValueCollection query)
        {
            var groupId = GetQueryParam<long>("Groups", query);
            if (groupId == 0)
                throw new Exception("Не выбрана группа");

            return groupId;
        }

        protected void InitialiseGbuAttributesFilterValue(params FilterValue[] filterValues)
        {
            if (filterValues == null)
                return;

            var attributes = GbuObjectService.GetGbuAttributes();

            foreach (var filterValue in filterValues)
            {
                if (filterValue == null)
                    continue;

                filterValue.ReportParameters = new List<ReportParameter>
                {
                    new ReportParameter {Value = string.Empty, Key = string.Empty}
                };

                filterValue.ReportParameters.AddRange(attributes.Select(x => new ReportParameter
                    { Value = $"{x.Name} ({x.ParentRegister?.RegisterDescription})", Key = $"key:{x.Id}" })
                );
            }
        }

        protected long GetFilterParameterValue(NameValueCollection query, string filterName, string nameFromInterface)
        {
            var attributeId = GetQueryParam<long?>(filterName, query);
            if (!attributeId.HasValue)
                throw new Exception($"Не указан атрибут '{nameFromInterface}'");

            return attributeId.Value;
        }

        #endregion

        #region Reflection

        protected Dictionary<string, RegisterAttribute> GetAttributesFromTourSettingsForReport(long tourId)
        {
            var attributesDictionary = new Dictionary<string, RegisterAttribute>();

            attributesDictionary.Add(nameof(InfoFromTourSettings.CadastralQuartal),
                StatisticalDataService.GetCadastralQuartalAttributeFromTourSettings(tourId));

            attributesDictionary.Add(nameof(InfoFromTourSettings.SubGroupNumber),
                StatisticalDataService.GetGroupAttributeFromTourSettings(tourId));

            return attributesDictionary;
        }

        protected static void SetAttributes(long? objectId, List<GbuObjectAttribute> gbuAttributes,
            Dictionary<string, RegisterAttribute> attributesDictionary, object item)
        {
            var objectAttributes = gbuAttributes.Where(x => x.ObjectId == objectId).ToList();
            foreach (var objectAttribute in objectAttributes)
            {
                var attributeKeys = attributesDictionary.Where(x => x.Value.Id == objectAttribute.AttributeId).Select(x => x.Key);
                foreach (var key in attributeKeys)
                {
                    item.GetType().GetProperty(key)?.SetValue(item, objectAttribute.GetValueInString());
                }
            }
        }

        #endregion

        #region Support

        protected string GetCadastralDistrict(string cadastralQuartal)
        {
            return string.IsNullOrWhiteSpace(cadastralQuartal) 
                ? null
                : cadastralQuartal.Substring(0, 5);
        }

        protected List<OMUnit> GetUnits(List<long> taskIds, PropertyTypes type)
        {
	        var query = OMUnit.Where(x => taskIds.Contains((long) x.TaskId) &&
	                                      x.PropertyType_Code == type &&
	                                      x.ObjectId != null)
		        .Select(x => new
		        {
			        x.ObjectId,
			        x.CadastralNumber,
			        x.Square,
			        x.Upks,
			        x.CadastralCost
		        });
	        return  QueryManager.ExecuteQuery(query);
        }

        protected string ProcessDate(string dateStr)
        {
            if (!string.IsNullOrWhiteSpace(dateStr) && DateTime.TryParse(dateStr, out var date))
            {
                dateStr = date.ToString(DateFormat);
            }

            return dateStr;
        }

        #endregion
    }
}
