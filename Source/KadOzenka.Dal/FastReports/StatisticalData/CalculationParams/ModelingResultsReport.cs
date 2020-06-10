using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using Core.Register.QuerySubsystem;
using Core.Shared.Extensions;
using KadOzenka.Dal.GbuObject;
using KadOzenka.Dal.Groups.Dto;
using KadOzenka.Dal.ManagementDecisionSupport.Enums;
using KadOzenka.Dal.Model.Dto;
using ObjectModel.Directory;
using ObjectModel.KO;

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

            var operations = GetOperations(taskIdList, factors);

            var dataSet = new DataSet();
            var itemTable = GetItemDataTable(operations);
            var commonTable = GetCommonDataTable(group);
            dataSet.Tables.Add(itemTable);
            dataSet.Tables.Add(commonTable);

            return HadleData(dataSet);
        }


        #region Support Methods

        private List<ReportItem> GetOperations(List<long> taskIds, List<ModelFactorDto> factors)
        {
            if(factors.Count == 0)
                return new List<ReportItem>();

            var units = GetUnits(taskIds);
            //для тестирования
            //id объекта, у которого настроены показатели вручную - 11404578
            //объект, у которого есть адрес в гбу
            //var objectIdForTesting = 11188991;
            //units[1].ObjectId = objectIdForTesting;

            var addresses = GetAddresses(units.Select(x => x.ObjectId.GetValueOrDefault()).Distinct().ToList());

            var groupedFactors = factors.GroupBy(x => x.RegisterId).Select(x => new UnitFactors
            {
                RegisterId = (int)x.Key,
                Attributes = x.Select(y => new Attribute
                {
                    Id = y.FactorId,
                    Name = y.Factor
                }).ToList()
            }).ToList();

            var items = new List<ReportItem>();
            units.ForEach(unit =>
            {
                var objectFactors = GetTourFactorsForUnit(unit.Id, groupedFactors);

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

        private List<Attribute> GetTourFactorsForUnit(long unitId, List<UnitFactors> unitFactors)
        {
            var attributes = new List<Attribute>();
            unitFactors.ForEach(factor =>
            {
                var query = new QSQuery
                {
                    MainRegisterID = factor.RegisterId,
                    Columns = factor.Attributes.Select(x => (QSColumn)new QSColumnSimple(x.Id, x.Id.ToString())).ToList(),
                    Condition = new QSConditionSimple(OMUnit.GetColumn(x => x.Id), QSConditionType.Equal, unitId)
                };

                var table = query.ExecuteQuery();
                foreach (DataRow row in table.Rows)
                {
                    factor.Attributes.ForEach(attribute =>
                    {
                        var value = row[attribute.Id.ToString()].ParseToStringNullable();
                        attributes.Add(new Attribute
                        {
                            Id = attribute.Id,
                            Name = attribute.Name,
                            Value = value
                        });
                    });
                }
            });

            return attributes;
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

        private class UnitFactors
        {
            public int RegisterId { get; set; }
            public List<Attribute> Attributes { get; set; }
        }

        private class Attribute
        {
            public long Id { get; set; }
            public string Name { get; set; }
            public string Value { get; set; }
        }

        private class ReportItem
        {
            public PropertyTypes ObjectType { get; set; }
            public string CadastralDistrict { get; set; }
            public string CadastralNumber { get; set; }
            public List<Attribute> Factors { get; set; }
            public string Address { get; set; }
            public decimal? Square { get; set; }
            public decimal? Upks { get; set; }
            public decimal? CadastralCost { get; set; }
        }

        #endregion
    }
}
