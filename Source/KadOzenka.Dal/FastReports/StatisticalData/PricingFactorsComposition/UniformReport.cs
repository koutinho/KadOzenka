using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using Core.Register;
using Core.Register.QuerySubsystem;
using Core.Register.RegisterEntities;
using Core.Shared.Extensions;
using Core.Shared.Misc;
using KadOzenka.Dal.FastReports.StatisticalData.Common;
using KadOzenka.Dal.GbuObject;
using ObjectModel.KO;

namespace KadOzenka.Dal.FastReports.StatisticalData.PricingFactorsComposition
{
    public class UniformReport : StatisticalDataReport
    {
        protected override string TemplateName(NameValueCollection query)
        {
            return "PricingFactorsCompositionUniformReport";
        }

        protected override DataSet GetData(NameValueCollection query, HashSet<long> objectList = null)
        {
            var taskIds = GetTaskIdList(query).ToList();

            var operations = GetOperations(taskIds);

            var dataSet = new DataSet();
            var itemTable = GetItemDataTable(operations);
            dataSet.Tables.Add(itemTable);

            return dataSet;
        }


        #region Support Methods

        public List<ReportItem> GetOperations(List<long> taskIds)
        {
            var items = new List<ReportItem>();
            var units = GetUnits(taskIds).ToList();

            var objectsAttributes = GbuObjectService.GetAllAttributes(
                units.Select(x => x.ObjectId.GetValueOrDefault()).ToList(),
                dateOt: DateTime.Now.GetEndOfTheDay(),
                isLight: true);

            units.ForEach(unit =>
            {
                var objAttributes = objectsAttributes.Where(x => x.ObjectId == unit.ObjectId).ToList();

                items.Add(new ReportItem
                {
                    CadastralNumber = unit.CadastralNumber,
                    Attributes = GetUniqueAttributes(objAttributes).Select(x => new Attribute
                    {
                        AttributeName = x.GetAttributeName(),
                        RegisterName = x.RegisterData.Description
                    }).ToList()
                });
            });

            return items;
        }

        public List<ReportItem> GetOperations2(List<long> taskIds)
        {
            var mainRegister = RegisterCache.GetRegisterData(ObjectModel.Gbu.OMMainObject.GetRegisterId());

            var aaa = new List<long> {2, 4 , 5, 14, 42430534 };
            var sources = RegisterCache.Registers.Values.Where(x => x.QuantTable == mainRegister.QuantTable &&
                                                                x.Id != mainRegister.Id)
                //.Where(x => aaa.Contains(x.Id))
                .Select(x => x.Id).ToList();

            var dateOt = DateTime.Now.GetEndOfTheDay();

            var registers = sources.Distinct().ToList();
            
            var counter = 0;
            var columns = string.Empty;
            var joins = string.Empty;
            var conditions = string.Empty;
            foreach (var registerId in registers)
            {
	            var registerData = RegisterCache.GetRegisterData(registerId);

	            if (registerData.AllpriPartitioning == Platform.Register.AllpriPartitioningType.DataType)
	            {
		            var postfixes = new List<string> {"TXT", "NUM", "DT"};
		            foreach (var postfix in postfixes)
		            {
			            counter++;
                        var tableName = $"{registerData.AllpriTable}_{postfix}";
			            var tableAlias = $"source_{counter}";
			            var tableAliasForFirstCondition = $"{tableAlias}_2";
			            var tableAliasForSecondCondition = $"{tableAlias}_3";

			            columns = $@"{(string.IsNullOrWhiteSpace(columns) ? "" : $" {columns} || ',' || ")} 
                                    STRING_AGG(distinct coalesce(cast({tableAlias}.attribute_id as text), ''), ',') ";

			            joins = $@" {joins} 
                        left join {tableName} {tableAlias} on unit.object_id = {tableAlias}.object_id";

			            var currentConditions = $@"
                        ({tableAlias}.ID = (SELECT MAX({tableAliasForFirstCondition}.id) FROM {tableName} {tableAliasForFirstCondition} 
                        WHERE {tableAliasForFirstCondition}.object_id = {tableAlias}.object_id 
                        AND {tableAliasForFirstCondition}.attribute_id = {tableAlias}.attribute_id 
                        AND {tableAliasForFirstCondition}.ot = (SELECT MAX({tableAliasForSecondCondition}.ot) FROM {tableName} {tableAliasForSecondCondition} 
                        WHERE {tableAliasForSecondCondition}.object_id = {tableAlias}.object_id 
                        AND {tableAliasForSecondCondition}.attribute_id = {tableAlias}.attribute_id 
                        AND {tableAliasForSecondCondition}.Ot <= {CrossDBSQL.ToDate(dateOt)})))";

			            conditions = string.IsNullOrWhiteSpace(conditions)
				            ? $@"{currentConditions}"
				            : $@"{conditions} AND ({currentConditions})";
		            }
	            }
	            else if (registerData.AllpriPartitioning == Platform.Register.AllpriPartitioningType.AttributeId)
	            {
		            var attributesData = RegisterCache.RegisterAttributes.Values.ToList()
			            .Where(x => x.RegisterId == registerId)
			            .Where(x => x.Id == 1 || x.Id == 44).ToList();

		            foreach (var attributeData in attributesData)
		            {
			            if (attributeData.IsPrimaryKey)
			            {
				            continue;
			            }

                        counter++;

                        var tableName = $"{registerData.AllpriTable}_{attributeData.Id}";
			            var tableAlias = $"gbu_source_{counter}";
			            var tableAliasForFirstCondition = $"{tableAlias}_2";

			            columns = $@"{(string.IsNullOrWhiteSpace(columns) ? "" : $" {columns} || ',' || ")} 
                                    STRING_AGG(distinct (case when {tableAlias}.id is null then '' else '{attributeData.Id}' end), ',') ";

                        joins = $@" {joins} 
                        left join {tableName} {tableAlias} on unit.object_id = {tableAlias}.object_id";

						var currentConditions = $@"{tableAlias}.OT = (SELECT MAX({tableAliasForFirstCondition}.OT) 
							FROM {tableName} {tableAliasForFirstCondition} 
							WHERE {tableAliasForFirstCondition}.object_id = {tableAlias}.object_id AND {tableAliasForFirstCondition}.Ot <= {CrossDBSQL.ToDate(dateOt)})
							or {tableAlias}.OT is null";

                        conditions = string.IsNullOrWhiteSpace(conditions)
	                        ? $@"{currentConditions}"
	                        : $@"{conditions} AND ({currentConditions})";
                    }
	            }
            }

            var resultColumns = string.IsNullOrWhiteSpace(columns) ? "" : $"{columns.TrimEnd(',')}";

            var sql = $@"select unit.cadastral_number, 
                ({resultColumns}) as attributes
                from ko_unit unit 
                {joins} 
                AND ( {conditions} ) 
                where unit.task_id in({string.Join(',', taskIds)})--and unit.object_id in (10743778)--(549616)
                group by unit.cadastral_number";

            var results = QSQuery.ExecuteSql<DbResult>(sql);
            return results.Select(result => new ReportItem
            {
                CadastralNumber = result.CadastralNumber,
                Attributes = result.Attributes.Split(',').Where(attribute => !string.IsNullOrWhiteSpace(attribute)).Select(attributeIdStr =>
                {
                    var attribute = RegisterCache.RegisterAttributes.Values.ToList().FirstOrDefault(x => x.Id == attributeIdStr.ParseToLong());
                    var register = RegisterCache.Registers.Values.FirstOrDefault(x => x.Id == attribute?.RegisterId);
                    return new Attribute
                    {
                        AttributeName = attribute?.Name,
                        RegisterName = register?.Description
                    };
                }).ToList()
            }).ToList();
        }

        public class DbResult
        {
            public string CadastralNumber { get; set; }
            public string Attributes { get; set; }
        }

        protected List<OMUnit> GetUnits(List<long> taskIds)
        {
            return OMUnit.Where(x => taskIds.Contains((long)x.TaskId) && x.ObjectId != null)
                .Select(x => new
                {
                    x.ObjectId,
                    x.CadastralNumber
                })
                .OrderBy(x => x.CadastralNumber)
                .Execute();
        }

        private List<GbuObjectAttribute> GetUniqueAttributes(List<GbuObjectAttribute> objectAttributes)
        {
            if(objectAttributes == null || objectAttributes.Count == 0)
                return new List<GbuObjectAttribute>();

            var gbuAttributesExceptRosreestr = objectAttributes
                .Where(x => x.RegisterData.Id != RosreestrRegisterService.RosreestrRegisterId).ToList();

            var rosreestrAttributes = objectAttributes
                .Where(x => x.RegisterData.Id == RosreestrRegisterService.RosreestrRegisterId).ToList();

            //симметрическая разность множеств
            var uniqueAttributes = new List<GbuObjectAttribute>();
            //отбираем уникальные аттрибуты из РР
            rosreestrAttributes.ForEach(rr =>
            {
                var rrAttributeName = rr.GetAttributeName();

                var sameAttributes = gbuAttributesExceptRosreestr.Where(gbu =>
                        gbu.GetAttributeName().StartsWith(rrAttributeName, StringComparison.InvariantCultureIgnoreCase)).ToList();
                if (sameAttributes.Count == 0)
                    uniqueAttributes.Add(rr);
            });
            //отбираем уникальные аттрибуты из всех источников кроме РР
            gbuAttributesExceptRosreestr.ForEach(gbu =>
            {
                var gbuAttributeName = gbu.GetAttributeName();

                var sameAttributes = rosreestrAttributes.Where(rr =>
                        gbuAttributeName.StartsWith(rr.GetAttributeName(), StringComparison.InvariantCultureIgnoreCase)).ToList();
                if (sameAttributes.Count == 0)
                    uniqueAttributes.Add(gbu);
            });

            return uniqueAttributes;
        }

        private DataTable GetItemDataTable(List<ReportItem> operations)
        {
            var titleForCharacteristic = "Характеристика объекта";
            var titleForSource = "Итоговый источник информации";

            var dataTable = new DataTable("Data");

            dataTable.Columns.Add("Number");
            dataTable.Columns.Add("CadastralNumber");
            dataTable.Columns.Add("CharacteristicNameTitle");
            dataTable.Columns.Add("CharacteristicName");

            //для формирования матрицы нужен дубляж значения всех строк кроме характеристик
            for (var i = 0; i < operations.Count; i++)
            {
                if (operations[i].Attributes.Count == 0)
                {
                    dataTable.Rows.Add(i + 1,
                        operations[i].CadastralNumber,
                        $"{titleForCharacteristic} 1",
                        string.Empty);

                    dataTable.Rows.Add(i + 1,
                        operations[i].CadastralNumber,
                        $"{titleForSource} 1",
                        string.Empty);
                }
                else
                {
                    for (var j = 0; j < operations[i].Attributes.Count; j++)
                    {
                        for (var counter = 0; counter < 2; counter++)
                        {
                            string title, value;
                            if (counter % 2 == 0)
                            {
                                title = $"{titleForCharacteristic} {j + 1}";
                                value = operations[i].Attributes.ElementAtOrDefault(j)?.AttributeName;
                            }
                            else
                            {
                                title = $"{titleForSource} {j + 1}";
                                value = operations[i].Attributes.ElementAtOrDefault(j)?.RegisterName;
                            }

                            dataTable.Rows.Add(i + 1,
                                operations[i].CadastralNumber,
                                title,
                                value);
                        }
                    }
                }
            }

            return dataTable;
        }

        #endregion


        #region Entities

        public class Attribute
        {
            public string AttributeName { get; set; }
            public string RegisterName { get; set; }
        }

        public class ReportItem
        {
            public string CadastralNumber { get; set; }
            public List<Attribute> Attributes { get; set; }
        }

        #endregion
    }
}
