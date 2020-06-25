using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using Core.Register;
using Core.Shared.Extensions;
using KadOzenka.Dal.FastReports.StatisticalData.Common;
using KadOzenka.Dal.ManagementDecisionSupport.Enums;
using KadOzenka.Dal.Model.Dto;
using ObjectModel.KO;

namespace KadOzenka.Dal.FastReports.StatisticalData.CalculationParams
{
    public class CalculationParamsReport : ReportWithGroup
    {
        private static readonly List<RegisterAttributeType> QuantitativeTypes = new List<RegisterAttributeType>
        {
            RegisterAttributeType.INTEGER,
            RegisterAttributeType.DECIMAL
        };

        protected override string TemplateName(NameValueCollection query)
        {
            return "CalculationParamsReport";
        }

        protected override StatisticalDataType GetReportType()
        {
            return StatisticalDataType.CalculationParams;
        }

        protected override DataSet GetData(NameValueCollection query, HashSet<long> objectList = null)
        {
            var groupId = GetGroupIdFromFilter(query);

            var model = ModelService.GetModelByGroupId(groupId);
            var factors = ModelService.GetModelFactors(model.Id);
            var quantitativeFactors = GetQuantitativeFactors(factors, groupId);
            var qualityFactors = GetQualityFactors(factors, groupId);

            var dataSet = new DataSet();

            var modelTable = GetModelDataTable(model);
            var coefficientsTable = GetCoefficientsDataTable(factors);
            var quantitativeFactorsTable = GetQuantitativeFactorsDataTable(quantitativeFactors);
            var qualityFactorsTable = GetQualityFactorsDataTable(qualityFactors);

            dataSet.Tables.Add(modelTable);
            dataSet.Tables.Add(coefficientsTable);
            dataSet.Tables.Add(quantitativeFactorsTable);
            dataSet.Tables.Add(qualityFactorsTable);

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

        private DataTable GetCoefficientsDataTable(List<ModelFactorDto> factors)
        {
            var dataTable = new DataTable("Coefficients");

            dataTable.Columns.Add("Number");
            dataTable.Columns.Add("Name");
            dataTable.Columns.Add("Coefficient");

            for (var i = 0; i < factors.Count; i++)
            {
                dataTable.Rows.Add(i + 1,
                    factors[i].Factor,
                    factors[i].B0);
            }

            return dataTable;
        }

        private List<QuantitativeFactor> GetQuantitativeFactors(List<ModelFactorDto> factors, long groupId)
        {
            var quantitativeFactors = factors.Where(x => QuantitativeTypes.Contains(x.Type)).ToList();
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
                        Name = factor.Factor,
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

        private List<QualityFactor> GetQualityFactors(List<ModelFactorDto> factors, long groupId)
        {
            var qualityFactors = factors.Where(x => !QuantitativeTypes.Contains(x.Type)).ToList();
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
                        Name = factor.Factor,
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
