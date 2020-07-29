﻿using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.IO;
using System.Linq;
using KadOzenka.Dal.FastReports.StatisticalData.Common;
using Core.Shared.Extensions;
using ObjectModel.Directory;
using ObjectModel.KO;

namespace KadOzenka.Dal.FastReports.StatisticalData.CadastralCostDeterminationResults
{
    public class CadastralCostDeterminationResultsMainReport : StatisticalDataReport
    {
	    public static string IndividuallyResultsGroupNamePhrase => "индивидуального расчета";
        private Dictionary<Type, ICadastralCostDeterminationResultsReport> _reportsDictionary;

        public CadastralCostDeterminationResultsMainReport()
        {
            _reportsDictionary = new Dictionary<Type, ICadastralCostDeterminationResultsReport>();
        }

        protected override string TemplateName(NameValueCollection query)
        {
            var report = GetReport(query);
            return report.GetTemplateName(query);
        }

        protected override DataSet GetData(NameValueCollection query, HashSet<long> objectList = null)
        {
            var taskIdList = GetTaskIdList(query).ToList();

            var report = GetReport(query);
            var units = report.GetUnitsForCadastralCostDetermination(taskIdList);
            var operations = GetOperations(units);

            var dataSet = new DataSet();
            var itemTable = GetItemDataTable(operations);
            dataSet.Tables.Add(itemTable);

            return dataSet;
        }

        #region Support Methods

        private ICadastralCostDeterminationResultsReport GetReport(NameValueCollection query)
        {
            Type type;
            var reportType = GetQueryParam<string>("ReportType", query);
            switch (reportType)
            {
                case "Результаты определения кадастровой стоимости":
                    type = typeof(StateResultsReport);
                    break;
                case "Сведения о результатах определения КС ОН, КС которых определен индивидуально":
                    type = typeof(IndividuallyResultsReport);
                    break;
                default:
                    throw new InvalidDataException($"Неизвестный тип формирования данных: {reportType}");
            }

            if (!_reportsDictionary.TryGetValue(type, out var concreteReport))
            {
                concreteReport = (ICadastralCostDeterminationResultsReport)Activator.CreateInstance(type);
                _reportsDictionary[type] = concreteReport;
            }

            return concreteReport;
        }

        private List<ReportItem> GetOperations(List<OMUnit> units)
        {
            if (units.Count == 0)
                return new List<ReportItem>();

            return units.Select(unit => new ReportItem
            {
                CadastralDistrict = GetCadastralDistrict(unit.CadastralBlock),
                CadastralNumber = unit.CadastralNumber,
                Type = unit.PropertyType_Code,
                Square = unit.Square,
                Upks = unit.Upks,
                Cost = unit.CadastralCost
            }).ToList();
        }

        private DataTable GetItemDataTable(List<ReportItem> operations)
        {
            var dataTable = new DataTable("ITEM");

            dataTable.Columns.Add("Number");
            dataTable.Columns.Add("CadastralDistrict");
            dataTable.Columns.Add("CadastralNumber");
            dataTable.Columns.Add("Type");
            dataTable.Columns.Add("Square");
            dataTable.Columns.Add("Upks");
            dataTable.Columns.Add("Cost");

            for (var i = 0; i < operations.Count; i++)
            {
                dataTable.Rows.Add(i + 1,
                    operations[i].CadastralDistrict,
                    operations[i].CadastralNumber,
                    operations[i].Type.GetEnumDescription(),
                    operations[i].Square,
                    operations[i].Upks,
                    operations[i].Cost);
            }

            return dataTable;
        }

        #endregion

        #region Entities

        private class ReportItem
        {
            public string CadastralDistrict { get; set; }
            public string CadastralNumber { get; set; }
            public PropertyTypes Type { get; set; }
            public decimal? Square { get; set; }
            public decimal? Upks { get; set; }
            public decimal? Cost { get; set; }
        }

        #endregion
    }
}