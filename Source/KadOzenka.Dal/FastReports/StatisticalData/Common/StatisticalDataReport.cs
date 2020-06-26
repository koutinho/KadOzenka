using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using Core.Register.RegisterEntities;
using Core.UI.Registers.Reports.Model;
using FastReport;
using FastReport.Matrix;
using KadOzenka.Dal.GbuObject;
using KadOzenka.Dal.Groups;
using KadOzenka.Dal.ManagementDecisionSupport.StatisticalData;
using KadOzenka.Dal.Model;
using Newtonsoft.Json;
using ObjectModel.Directory;
using ObjectModel.KO;
using Platform.Reports;

namespace KadOzenka.Dal.FastReports.StatisticalData.Common
{
	public abstract class StatisticalDataReport : FastReportBase
	{
        public static readonly string DateFormat = "dd.MM.yyyy";
        protected string DecimalFormat => "#,##0.00";
        public static readonly int PrecisionForDecimalValues = 2;

        protected readonly GbuObjectService GbuObjectService;
		protected readonly StatisticalDataService StatisticalDataService;
		protected readonly ModelService ModelService;
		protected readonly GroupService GroupService;
		protected readonly FactorsService FactorsService;

		protected StatisticalDataReport()
		{
			GbuObjectService = new GbuObjectService();
            StatisticalDataService = new StatisticalDataService();
            ModelService = new ModelService();
            GroupService = new GroupService();
            FactorsService = new FactorsService();
        }

		public override string GetTitle(long? objectId)
		{
			return ReportType.Title;
		}


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

        #region Data Handling

        protected DataSet HadleData(DataSet dataSet)
        {
            //TODO:SHOULD BE MOVED TO PLATFORM
            foreach (DataTable table in dataSet.Tables)
            {
                string tableName = table.TableName;
                Dictionary.Report.RegisterData(dataSet, tableName);
                GetDataSource(tableName).Enabled = true;

                // в файле *.frx банд должен называться Band<название_таблицы_в_DataSet>, например BandItem
                DataBand dataBand = FindObject("Band" + tableName) as DataBand;
                if (dataBand != null) dataBand.DataSource = GetDataSource(tableName);

                MatrixObject matrix = FindObject("Matrix" + tableName) as MatrixObject;
                if (matrix != null) matrix.DataSource = GetDataSource(tableName);
            }

            return new DataSet();
        }

        #endregion

        #region Reflection

        protected Dictionary<string, RegisterAttribute> GetAttributesFromTourSettingsForReport(long tourId)
        {
            var attributesDictionary = new Dictionary<string, RegisterAttribute>();

            attributesDictionary.Add(nameof(InfoFromTourSettings.ObjectType),
                StatisticalDataService.GetObjectTypeAttributeFromTourSettings(tourId));

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

        protected List<OMUnit> GetUnits(List<long> taskIds)
        {
            return OMUnit.Where(x => taskIds.Contains((long)x.TaskId) && x.ObjectId != null)
                .SelectAll()
                .Execute();
        }

        protected List<OMUnit> GetUnits(List<long> taskIds, PropertyTypes type)
        {
            return OMUnit.Where(x => taskIds.Contains((long)x.TaskId) &&
                                     x.PropertyType_Code == type &&
                                     x.ObjectId != null)
                .SelectAll()
                .Execute();
        }

        #endregion
    }
}
