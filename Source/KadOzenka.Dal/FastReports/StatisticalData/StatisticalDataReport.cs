using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using Core.UI.Registers.Reports.Model;
using FastReport;
using FastReport.Matrix;
using KadOzenka.Dal.GbuObject;
using KadOzenka.Dal.ManagementDecisionSupport.StatisticalData;
using Newtonsoft.Json;
using ObjectModel.Directory;
using ObjectModel.KO;
using Platform.Reports;

namespace KadOzenka.Dal.FastReports.StatisticalData
{
	public abstract class StatisticalDataReport : FastReportBase
	{
		protected readonly GbuObjectService GbuObjectService;
		protected readonly StatisticalDataService StatisticalDataService;

		protected StatisticalDataReport()
		{
			GbuObjectService = new GbuObjectService();
            StatisticalDataService = new StatisticalDataService();

        }

		public override string GetTitle(long? objectId)
		{
			return ReportType.Title;
		}

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

        public string GetCadastralDistrict(string cadastralQuartal)
        {
            return cadastralQuartal.Substring(0, 5);
        }

        protected long GetFilterParameterValue(NameValueCollection query, string filterName, string nameFromInterface)
        {
            var attributeId = GetQueryParam<long?>(filterName, query);
            if (!attributeId.HasValue)
                throw new Exception($"Не указан атрибут '{nameFromInterface}'");

            return attributeId.Value;
        }

        protected List<OMUnit> GetUnits(List<long> taskIds, PropertyTypes type)
        {
            return OMUnit.Where(x => taskIds.Contains((long)x.TaskId) &&
                                     x.PropertyType_Code == type &&
                                     x.ObjectId != null)
                .Select(x => x.ParentGroup.GroupName)
                .SelectAll()
                .Execute();
        }
    }
}
