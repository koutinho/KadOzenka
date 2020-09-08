using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using Core.Shared.Extensions;
using KadOzenka.Dal.FastReports.StatisticalData.Common;
using KadOzenka.Dal.GbuObject;
using KadOzenka.Dal.Groups.Dto;
using KadOzenka.Dal.ManagementDecisionSupport.Enums;
using KadOzenka.Dal.ManagementDecisionSupport.StatisticalData;
using ObjectModel.Directory;
using Core.UI.Registers.Reports.Model;
using ObjectModel.KO;

namespace KadOzenka.Dal.FastReports.StatisticalData.CalculationParams
{
    public class ModelingResultsReport : StatisticalDataReport
    {
        protected override string TemplateName(NameValueCollection query)
        {
            return "CalculationParamsModelingResultsReport";
        }

        public override void InitializeFilterValues(long objId, string senderName, bool initialization, List<FilterValue> filterValues)
        {
            GroupFilter.InitializeFilterValues(StatisticalDataType.ModelingResults, initialization, filterValues);
        }

        protected override DataSet GetData(NameValueCollection query, HashSet<long> objectList = null)
        {
            var taskIdList = GetTaskIdList(query).ToList();
            var groupId = GetGroupIdFromFilter(query);

            var group = GroupService.GetGroupById(groupId);
            var model = OMModel.Where(x => x.GroupId == groupId).SelectAll().ExecuteFirstOrDefault();

            var operations = GetOperations(taskIdList, model?.Id);

            var dataSet = new DataSet();
            var itemTable = GetItemDataTable(operations);
            var commonTable = GetCommonDataTable(group);
            dataSet.Tables.Add(itemTable);
            dataSet.Tables.Add(commonTable);

            return dataSet;
        }


        #region Support Methods

        private List<ReportItem> GetOperations(List<long> taskIds, long? modelId)
        {
            var units = GetUnits(taskIds);
            //для тестирования
            //id объекта, у которого настроены показатели вручную - 11404578
            //объект, у которого есть адрес в гбу
            //var objectIdForTesting = 11188991;
            //units[1].ObjectId = objectIdForTesting;
            if (units == null || units.Count == 0)
                return new List<ReportItem>();

            var addresses = GetAddresses(units.Select(x => x.ObjectId.GetValueOrDefault()).Distinct().ToList());

            var groupedFactors = modelId == null
                ? new List<FactorsService.PricingFactors>()
                : FactorsService.GetGroupedModelFactors(modelId.Value);
            var attributes = groupedFactors.SelectMany(x => x.Attributes).ToList();
            var pricingFactors = FactorsService.GetPricingFactorsForUnits(units.Select(x => x.Id).Distinct().ToList(), groupedFactors);

            var items = new List<ReportItem>();
            units.ForEach(unit =>
            {
                var objectFactors = pricingFactors.TryGetValue(unit.Id, out var value) ? value : attributes;

                var objectAddress = addresses.FirstOrDefault(x => x.ObjectId == unit.ObjectId)?.GetValueInString();

                items.Add(new ReportItem
                {
                    ObjectType = unit.PropertyType_Code,
                    CadastralDistrict = GetCadastralDistrict(unit.CadastralBlock),
                    CadastralNumber = unit.CadastralNumber,
                    Factors = objectFactors,
                    Address = objectAddress,
                    Square = unit.Square,
                    Upks = unit.Upks,
                    CadastralCost = unit.CadastralCost
                });
            });

            return items.OrderBy(x => x.CadastralDistrict).ToList();
        }

        private List<GbuObjectAttribute> GetAddresses(List<long> objectIds)
        {
            var addressAttribute = RosreestrRegisterService.GetRosreestrAddressAttribute();

            return GbuObjectService.GetAllAttributes(
                objectIds,
                new List<long> {addressAttribute.RegisterId},
                new List<long> {addressAttribute.Id},
                DateTime.Now.GetEndOfTheDay());
        }

        private DataTable GetItemDataTable(List<ReportItem> operations)
        {
            var dataTable = new DataTable("Data");

            dataTable.Columns.Add("Number");
            dataTable.Columns.Add("ObjectType");
            dataTable.Columns.Add("CadastralDistrict");
            dataTable.Columns.Add("CadastralNumber");
            dataTable.Columns.Add("FactorName");
            dataTable.Columns.Add("FactorValue");
            dataTable.Columns.Add("Address");
            dataTable.Columns.Add("Square");
            dataTable.Columns.Add("Upks");
            dataTable.Columns.Add("CadastralCost");

            for (var i = 0; i < operations.Count; i++)
            {
                var counter = i + 1;
                foreach (var keyValuePair in operations[i].Factors)
                {
                    dataTable.Rows.Add(counter,
                        operations[i].ObjectType == PropertyTypes.None ? null : operations[i].ObjectType.GetEnumDescription(),
                        operations[i].CadastralDistrict,
                        operations[i].CadastralNumber,
                        keyValuePair.Name,
                        keyValuePair.Value,
                        operations[i].Address,
                        operations[i].Square?.ToString(DecimalFormat),
                        operations[i].Upks?.ToString(DecimalFormat),
                        operations[i].CadastralCost?.ToString(DecimalFormat));
                }
            }

            return dataTable;
        }

        private DataTable GetCommonDataTable(GroupDto group)
        {
            var dataTable = new DataTable("Common");

            dataTable.Columns.Add("GroupName");

            dataTable.Rows.Add(group.Name);

            return dataTable;
        }

        #endregion

        #region Entities

        private class ReportItem
        {
            public PropertyTypes ObjectType { get; set; }
            public string CadastralDistrict { get; set; }
            public string CadastralNumber { get; set; }
            public List<FactorsService.PricingFactor> Factors { get; set; }
            public string Address { get; set; }
            public decimal? Square { get; set; }
            public decimal? Upks { get; set; }
            public decimal? CadastralCost { get; set; }
        }

        #endregion
    }
}
