using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using Core.Shared.Extensions;
using KadOzenka.Dal.GbuObject;
using KadOzenka.Dal.Groups.Dto;
using KadOzenka.Dal.ManagementDecisionSupport.Enums;
using KadOzenka.Dal.Model.Dto;
using ObjectModel.Directory;

namespace KadOzenka.Dal.FastReports.StatisticalData.CalculationParams
{
    public class ModelingResultsReport : BaseCalculationParamsReport
    {
        protected override string TemplateName(NameValueCollection query)
        {
            return "CalculationParamsModelingResultsReport";
        }

        protected override StatisticalDataType GetReportType()
        {
            return StatisticalDataType.ModelingResults;
        }

        protected override DataSet GetData(NameValueCollection query, HashSet<long> objectList = null)
        {
            var taskIdList = GetTaskIdList(query).ToList();
            var groupId = GetGroupIdFromFilter(query);
            var group = GroupService.GetGroupById(groupId);

            var model = ModelService.GetModelByGroupId(group.Id);
            var factors = ModelService.GetModelFactors(model.Id);

            var operations = GetOperations(group.Id.Value, taskIdList, factors);

            //TODO remove after testing
            //var dictionary1 = new Dictionary<string, string>
            //{
            //    {"Материал стен", "wallMat 1" },
            //    {"Расстояние до метро", "distanceToMetro 1" },
            //    {"Test", null }
            //};
            //var dictionary2 = new Dictionary<string, string>
            //{
            //    {"Материал стен", "wallMat 2" },
            //    {"Расстояние до метро", "distanceToMetro 2" }
            //};
            //var dictionary3 = new Dictionary<string, string>
            //{
            //    {"Материал стен", "wallMat 1" },
            //    {"Расстояние до метро", "distanceToMetro 1" },
            //    {"Test", "test" }
            //};
            //operations = new List<ReportItem>
            //{
            //    new ReportItem
            //    {
            //        ObjectType = PropertyTypes.Building,
            //        CadastralNumber = "KN_1",
            //        CadastralDistrict = "KD_1",
            //        Factors = dictionary1,
            //        Address = "Address_1",
            //        Square = 1,
            //        Upks = 1,
            //        CadastralCost = 1
            //    },
            //    new ReportItem
            //    {
            //        ObjectType = PropertyTypes.Building,
            //        CadastralNumber = "KN_2",
            //        CadastralDistrict = "KD_2",
            //        Factors = dictionary2,
            //        Address = "Address_2",
            //        Square = 2,
            //        Upks = 2,
            //        CadastralCost = 2
            //    },
            //    new ReportItem
            //    {
            //        ObjectType = PropertyTypes.Building,
            //        CadastralNumber = "KN_3",
            //        CadastralDistrict = "KD_3",
            //        Factors = dictionary3,
            //        Address = "Address_3",
            //        Square = 3,
            //        Upks = 3,
            //        CadastralCost = 3
            //    }
            //};

            var dataSet = new DataSet();
            var itemTable = GetItemDataTable(operations);
            var commonTable = GetCommonDataTable(group);
            dataSet.Tables.Add(itemTable);
            dataSet.Tables.Add(commonTable);

            return HadleData(dataSet);
        }


        #region Support Methods

        private List<ReportItem> GetOperations(long groupId, List<long> taskIds, List<ModelFactorDto> factors)
        {
            if(factors.Count == 0)
                return new List<ReportItem>();
            
            var attributes = factors
                .Select(x => new {Id = x.FactorId, Name = x.Factor, RegisterId = x.RegisterId})
                .DistinctBy(x => x.Id).ToList();

            var factorsDictionary = new Dictionary<string, string>();
            attributes.Select(x => x.Name).Distinct().ToList().ForEach(x => factorsDictionary.Add(x, null));

            var units = GetUnits(taskIds);
            //для тестирования
            //var objectIdForTesting = 11188991;
            //units[1].ObjectId = objectIdForTesting;
            var addresses = GetAddresses(units.Select(x => x.ObjectId.GetValueOrDefault()).Distinct().ToList());

            var items = new List<ReportItem>();
            units.ForEach(unit =>
            {
                //todo tour factors
                var objectFactors = factorsDictionary;

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
            var addressAttribute = StatisticalDataService.GetRosreestrAddressAttribute();

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
                        keyValuePair.Key,
                        keyValuePair.Value,
                        operations[i].Address,
                        operations[i].Square,
                        operations[i].Upks,
                        operations[i].CadastralCost);
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
            public Dictionary<string, string> Factors { get; set; }
            public string Address { get; set; }
            public decimal? Square { get; set; }
            public decimal? Upks { get; set; }
            public decimal? CadastralCost { get; set; }
        }

        #endregion
    }
}
