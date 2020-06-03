using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using Core.Register;
using Core.Register.QuerySubsystem;
using Core.Shared.Extensions;
using Core.Shared.Misc;
using Core.UI.Registers.Reports.Model;
using KadOzenka.Dal.ManagementDecisionSupport;
using KadOzenka.Dal.ManagementDecisionSupport.Enums;
using KadOzenka.Dal.Model;
using KadOzenka.Dal.Model.Dto;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using ObjectModel.KO;

namespace KadOzenka.Dal.FastReports.StatisticalData.CalculationParams
{
    public class CalculationParamsReport : StatisticalDataReport
    {
        private string DecimalFormat => "#,##0.00";
        private static readonly List<RegisterAttributeType> QuantitativeTypes = new List<RegisterAttributeType>
        {
            RegisterAttributeType.INTEGER,
            RegisterAttributeType.DECIMAL
        };

        protected override string TemplateName(NameValueCollection query)
        {
            return "CalculationParamsReport";
        }

        public override void InitializeFilterValues(long objId, string senderName, bool initialisation, List<FilterValue> filterValues)
        {
            if (!initialisation)
                return;

            var groupsFilterValue = filterValues.FirstOrDefault(f => f.ParamName == "Groups");
            if (groupsFilterValue != null)
            {
                var taskIds = GetTaskIdsFromSession();
                var groups = GetGroups(taskIds);

                groupsFilterValue.ReportParameters = new List<ReportParameter>();
                groupsFilterValue.ReportParameters.AddRange(groups.Select(x => new ReportParameter { Value = $"{x.Name}", Key = $"key:{x.GroupId}" }));
            }
        }


        protected override DataSet GetData(NameValueCollection query, HashSet<long> objectList = null)
        {
            var modelService = new ModelService();
            var groupId = GetQueryParam<long>("Groups", query);
            if (groupId == 0)
                throw new Exception("Не выбрана группа");

            var model = modelService.GetModelByGroupId(groupId);
            var factors = modelService.GetModelFactors(model.Id);
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

        private List<long> GetTaskIdsFromSession()
        {
            var reportCode = StatisticalDataType.CalculationParams.GetAttributeValue<StatisticalDataFmReportCodeAttribute>(
                nameof(StatisticalDataFmReportCodeAttribute.Code));

            var taskIdsStr = HttpContextHelper.HttpContext.Session.GetString($"Report{reportCode}TaskFilter");

            return JsonConvert.DeserializeObject<List<long>>(taskIdsStr);
        }

        private List<Group> GetGroups(List<long> taskIds)
        {
            var query = new QSQuery
            {
                MainRegisterID = OMUnit.GetRegisterId(),
                Condition = new QSConditionGroup
                {
                    Type = QSConditionGroupType.And,
                    Conditions = new List<QSCondition>
                    {
                        new QSConditionSimple(OMTask.GetColumn(x => x.Id), QSConditionType.In, taskIds.Select(x => (double)x).ToList())
                    }
                },
                Joins = new List<QSJoin>
                {
                    new QSJoin
                    {
                        RegisterId = OMGroup.GetRegisterId(),
                        JoinCondition = new QSConditionSimple
                        {
                            ConditionType = QSConditionType.Equal,
                            LeftOperand = OMUnit.GetColumn(x => x.GroupId),
                            RightOperand = OMGroup.GetColumn(x => x.Id)
                        },
                        JoinType = QSJoinType.Inner
                    }
                }
            };

            query.AddColumn(OMGroup.GetColumn(x => x.Id, nameof(Group.GroupId)));
            query.AddColumn(OMGroup.GetColumn(x => x.GroupName, nameof(Group.Name)));

            var result = query.ExecuteQuery<Group>();

            return result.DistinctBy(x => x.GroupId).ToList();
        }

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

            var allMarks = OMMarkCatalog.Where(x => x.GroupId == groupId && factorIds.Contains(x.FactorId))
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

            var allMarks = OMMarkCatalog.Where(x => x.GroupId == groupId && factorIds.Contains(x.FactorId))
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

        private class Group
        {
            public long Id { get; set; }
            public long GroupId { get; set; }
            public string Name { get; set; }
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
