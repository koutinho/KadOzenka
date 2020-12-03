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
using ObjectModel.Core.Register;
using ObjectModel.Directory;
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

            var model = ModelService.GetActiveModelEntityByGroupId(groupId);
            Logger.Debug("ИД модели '{ModelId}' для группы '{GroupId}'", model.Id, groupId);

            var factors = GetFactors(model);
            Logger.Debug("Найдено {FactorsCount} Факторов для модели", factors?.Count);

            var generalFactors = factors.FirstOrDefault()?.Factors ?? new List<ModelFactor>();
            
            var quantitativeFactors = GetQuantitativeFactors(generalFactors, groupId);
            Logger.Debug("Найдено {MarksForQuantitativeFactorsCount} меток для количественных факторов", quantitativeFactors?.Count);

            var qualityFactors = GetQualityFactors(generalFactors, groupId);
            Logger.Debug("Найдено {MarksForQualityFactorsCount} меток для качественных факторов", qualityFactors?.Count);

            Logger.Debug("Начато формирование таблиц");
            var dataSet = new DataSet();

            var modelTable = GetModelDataTable(model);
            var linearCoefficientsTable = GetCoefficientsDataTable(KoAlgoritmType.Line, "Linear", factors);
            var exponentialCoefficientsTable = GetCoefficientsDataTable(KoAlgoritmType.Exp, "Exponential", factors);
            var multiplicativeCoefficientsTable = GetCoefficientsDataTable(KoAlgoritmType.Multi, "Multiplicative", factors);
            var quantitativeFactorsTable = GetQuantitativeFactorsDataTable(quantitativeFactors);
            var qualityFactorsTable = GetQualityFactorsDataTable(qualityFactors);

            dataSet.Tables.Add(modelTable);
            dataSet.Tables.Add(linearCoefficientsTable);
            dataSet.Tables.Add(exponentialCoefficientsTable);
            dataSet.Tables.Add(multiplicativeCoefficientsTable);
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
            dataTable.Columns.Add("AlgorithmType");

            dataTable.Rows.Add(model.Formula, 
                model.Type_Code.GetEnumDescription(),
                model.AlgoritmType_Code.GetEnumDescription());

            return dataTable;
        }

        private DataTable GetCoefficientsDataTable(KoAlgoritmType type, string prefix, List<GroupedFactors> groupedFactors)
        {
	        var dataTable = new DataTable(prefix + "Coefficients");

	        dataTable.Columns.Add("Number");
	        dataTable.Columns.Add("Name");
	        dataTable.Columns.Add("Coefficient");

	        var factors = groupedFactors.FirstOrDefault(x => x.AlgorithmType == type)?.Factors;
	        for (var i = 0; i < factors?.Count; i++)
	        {
		        dataTable.Rows.Add(i + 1,
			        factors[i].FactorName,
			        factors[i].Weight);
	        }

	        return dataTable;
        }

        private List<GroupedFactors> GetFactors(OMModel model)
		{
			var query = ModelFactorsService.GetModelFactorsQuery(model.Id);

			query.AddColumn(OMAttribute.GetColumn(x => x.Type, nameof(ModelFactor.Type)));
            query.AddColumn(OMModelFactor.GetColumn(x => x.FactorId, nameof(ModelFactor.FactorId)));
			query.AddColumn(OMAttribute.GetColumn(x => x.Name, nameof(ModelFactor.FactorName)));
			query.AddColumn(OMModelFactor.GetColumn(x => x.Weight, nameof(ModelFactor.Weight)));
			query.AddColumn(OMModelFactor.GetColumn(x => x.AlgorithmType_Code, nameof(ModelFactor.AlgorithmType)));

			var factors = new List<ModelFactor>();
			var table = query.ExecuteQuery();
            //сделано через ExecuteQuery, потому что в query.ExecuteQuery<ModelFactor> есть ошибка
            //из-за которой данные выкачиваются неправильно (некоторые поля меняются местами)
            for (var i = 0; i < table.Rows.Count; i++)
			{
				var row = table.Rows[i];

				var type = row[nameof(ModelFactor.Type)].ParseToLong();
				var factorId = row[nameof(ModelFactor.FactorId)].ParseToLong();
				var factorName = row[nameof(ModelFactor.FactorName)].ParseToStringNullable();
				var weight = row[nameof(ModelFactor.Weight)].ParseToDecimalNullable();
				var algorithmType = row[nameof(ModelFactor.AlgorithmType)].ParseToLongNullable();

				factors.Add(new ModelFactor
                {
	                Type = type,
	                FactorId = factorId,
	                FactorName = factorName,
	                Weight = weight,
	                AlgorithmType = algorithmType == null ? KoAlgoritmType.None : (KoAlgoritmType)algorithmType
                });
			}

            //для совместимости с уже ранее созданнами моделями (не через блок "Справочники моделирования")
            if (factors.All(x => x.AlgorithmType == KoAlgoritmType.None))
            {
                factors.ForEach(x => x.AlgorithmType = model.AlgoritmType_Code);
            }

            return factors.GroupBy(x => x.AlgorithmType).Select(g => new GroupedFactors
			{
				AlgorithmType = g.Key, 
				Factors = g.ToList()
			}).ToList();
		}

        private List<QuantitativeFactor> GetQuantitativeFactors(List<ModelFactor> factors, long groupId)
        {
            var quantitativeFactors = factors.Where(x => QuantitativeTypes.Contains((RegisterAttributeType)x.Type)).ToList();
            Logger.Debug("Найдено {QuantitativeFactorsCount} количественных факторов", quantitativeFactors.Count);
            
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
            Logger.Debug("Найдено {QuantitativeFactorsCount} качественных факторов", qualityFactors.Count);

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

        public class GroupedFactors
        {
	        public KoAlgoritmType AlgorithmType { get; set; }
	        public List<ModelFactor> Factors { get; set; }
        }

        public class ModelFactor
        {
	        //public long Id { get; set; }
	        public long FactorId { get; set; }
	        public string FactorName { get; set; }
	        public long Type { get; set; }
	        public decimal? B0 { get; set; }
	        public KoAlgoritmType AlgorithmType { get; set; }
	        public decimal? Weight { get; set; }
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
