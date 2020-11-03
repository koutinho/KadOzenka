using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using Core.Register;
using Core.Shared.Extensions;
using KadOzenka.Dal.FastReports.StatisticalData.Common;
using KadOzenka.Dal.ManagementDecisionSupport.Enums;
using ObjectModel.KO;
using Core.UI.Registers.Reports.Model;
using KadOzenka.Dal.Modeling.Dto;
using ObjectModel.Core.Register;
using Serilog;

namespace KadOzenka.Dal.FastReports.StatisticalData.CalculationParams
{
    public class CalculationParamsReport : StatisticalDataReport
    {
        private static readonly List<RegisterAttributeType> QuantitativeTypes = new List<RegisterAttributeType>
        {
            RegisterAttributeType.INTEGER,
            RegisterAttributeType.DECIMAL
        };
        private readonly ILogger _logger;
        protected override ILogger Logger => _logger;

        public CalculationParamsReport()
        {
	        _logger = Log.ForContext<CalculationParamsReport>();
        }

        protected override string TemplateName(NameValueCollection query)
        {
            return "CalculationParamsReport";
        }

        public override void InitializeFilterValues(long objId, string senderName, bool initialization, List<FilterValue> filterValues)
        {
            GroupFilter.InitializeFilterValues(StatisticalDataType.CalculationParams, initialization, filterValues);
        }

        protected override DataSet GetReportData(NameValueCollection query, HashSet<long> objectList = null)
        {
            var groupId = GetGroupIdFromFilter(query);

            var model = ModelService.GetModelEntityByGroupId(groupId);

            var factors = GetFactors(model.Id);

            var quantitativeFactors = GetQuantitativeFactors(factors, groupId);
            var qualityFactors = GetQualityFactors(factors, groupId);

            Logger.Debug("Начато формирование таблиц");
            var dataSet = new DataSet();

            var modelTable = GetModelDataTable(model);
            var coefficientsTable = GetCoefficientsDataTable(factors);
            var quantitativeFactorsTable = GetQuantitativeFactorsDataTable(quantitativeFactors);
            var qualityFactorsTable = GetQualityFactorsDataTable(qualityFactors);

            dataSet.Tables.Add(modelTable);
            dataSet.Tables.Add(coefficientsTable);
            dataSet.Tables.Add(quantitativeFactorsTable);
            dataSet.Tables.Add(qualityFactorsTable);
            Logger.Debug("Закончено формирование таблиц");

            return dataSet;
        }


        #region Support Methods

        private DataTable GetModelDataTable(OMModel model)
        {
            var dataTable = new DataTable("Model");

            dataTable.Columns.Add("Formula");
            dataTable.Columns.Add("Type");

            dataTable.Rows.Add(model.Formula, 
                model.AlgoritmType_Code.GetEnumDescription());

            return dataTable;
        }

        private DataTable GetCoefficientsDataTable(List<ModelFactor> factors)
        {
            var dataTable = new DataTable("Coefficients");

            dataTable.Columns.Add("Number");
            dataTable.Columns.Add("Name");
            dataTable.Columns.Add("Coefficient");

            for (var i = 0; i < factors.Count; i++)
            {
                dataTable.Rows.Add(i + 1,
                    factors[i].FactorName,
                    factors[i].B0);
            }

            return dataTable;
        }

        private List<ModelFactor> GetFactors(long modelId)
		{
			var query = ModelService.GetModelFactorsQuery(modelId);

			query.AddColumn(OMAttribute.GetColumn(x => x.Type, nameof(ModelFactor.Type)));
            query.AddColumn(OMModelFactor.GetColumn(x => x.FactorId, nameof(ModelFactor.FactorId)));
			query.AddColumn(OMAttribute.GetColumn(x => x.Name, nameof(ModelFactor.FactorName)));
			query.AddColumn(OMModelFactor.GetColumn(x => x.B0, nameof(ModelFactor.B0)));

			return query.ExecuteQuery<ModelFactor>();
        }

        private List<QuantitativeFactor> GetQuantitativeFactors(List<ModelFactor> factors, long groupId)
        {
            var quantitativeFactors = factors.Where(x => QuantitativeTypes.Contains((RegisterAttributeType)x.Type)).ToList();
            var factorIds = quantitativeFactors.Select(x => x.FactorId).ToList();
            if (factorIds.Count <= 0)
                return new List<QuantitativeFactor>();

            var allMarks = OMMarkCatalog.Where(x => x.GroupId == groupId && factorIds.Contains(x.FactorId ?? 0))
                .SelectAll().Execute();

            var groupedMarks = allMarks
                .GroupBy(x => x.FactorId)
                .Select(x => new
                {
                    FactorId = x.Key,
                    MaxValue = x.Max(y => y.ValueFactor.ParseToDecimalNullable() ?? 0),
                    MinValue = x.Min(y => y.ValueFactor.ParseToDecimalNullable() ?? 0)
                }).ToList();

            var result = new List<QuantitativeFactor>();
            quantitativeFactors.ForEach(factor =>
            {
                var factorMarks = groupedMarks.FirstOrDefault(x => x.FactorId == factor.FactorId);
                if (factorMarks != null)
                {
                    result.Add(new QuantitativeFactor
                    {
                        Name = factor.FactorName,
                        MaxValue = factorMarks.MaxValue,
                        MinValue = factorMarks.MinValue
                    });
                }
            });

            return result;
        }

        private DataTable GetQuantitativeFactorsDataTable(List<QuantitativeFactor> quantitativeFactors)
        {
            var dataTable = new DataTable("QuantitativeFactors");

            dataTable.Columns.Add("Number");
            dataTable.Columns.Add("Name");
            dataTable.Columns.Add("MaxValue");
            dataTable.Columns.Add("MinValue");

            for (var i = 0; i < quantitativeFactors.Count; i++)
            {
                dataTable.Rows.Add(i + 1,
                    quantitativeFactors[i].Name,
                    quantitativeFactors[i].MaxValue.ToString(DecimalFormat),
                    quantitativeFactors[i].MinValue.ToString(DecimalFormat));
            }

            return dataTable;
        }

        private List<QualityFactor> GetQualityFactors(List<ModelFactor> factors, long groupId)
        {
            var qualityFactors = factors.Where(x => !QuantitativeTypes.Contains((RegisterAttributeType)x.Type)).ToList();
            var factorIds = qualityFactors.Select(x => x.FactorId).ToList();
            if (factorIds.Count <= 0)
                return new List<QualityFactor>();

            var allMarks = OMMarkCatalog.Where(x => x.GroupId == groupId && factorIds.Contains(x.FactorId ?? 0))
                .SelectAll().Execute();

            var result = new List<QualityFactor>();
            qualityFactors.ForEach(factor =>
            {
                var factorMarks = allMarks.Where(x => x.FactorId == factor.FactorId).ToList();
                factorMarks.ForEach(x =>
                {
                    result.Add(new QualityFactor
                    {
                        Name = factor.FactorName,
                        Value = x.ValueFactor,
                        Metka = x.MetkaFactor
                    });
                });
            });

            return result;
        }

        private DataTable GetQualityFactorsDataTable(List<QualityFactor> qualityFactors)
        {
            var dataTable = new DataTable("QualityFactors");

            dataTable.Columns.Add("Number");
            dataTable.Columns.Add("Name");
            dataTable.Columns.Add("Value");
            dataTable.Columns.Add("Metka");

            for (var i = 0; i < qualityFactors.Count; i++)
            {
                dataTable.Rows.Add(i + 1,
                    qualityFactors[i].Name,
                    qualityFactors[i].Value,
                    qualityFactors[i].Metka?.ToString(DecimalFormat));
            }

            return dataTable;
        }

        #endregion


        #region Entities

        public class ModelFactor
        {
	        public long Id { get; set; }
	        public long FactorId { get; set; }
	        public string FactorName { get; set; }
	        public long Type { get; set; }
	        public decimal B0 { get; set; }
        }

        private class QuantitativeFactor
        {
            public string Name { get; set; }
            public decimal MaxValue { get; set; }
            public decimal MinValue { get; set; }
        }

        private class QualityFactor
        {
            public string Name { get; set; }
            public string Value { get; set; }
            public decimal? Metka { get; set; }
        }

        #endregion
    }
}
