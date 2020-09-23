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

            //var test = new List<long> {2, 4, 5, 14, 42430534 };
            var test = new List<long> { 2, 4 };
            var sources = RegisterCache.Registers.Values.Where(x => x.QuantTable == mainRegister.QuantTable &&
                                                                x.Id != mainRegister.Id)
                //.Where(x => test.Contains(x.Id))
                .Select(x => x.Id).ToList();

            var dateOt = DateTime.Now.GetEndOfTheDay();

            var registers = sources.Distinct().ToList();
            
            var counter = 0;
            var columns = string.Empty;
            foreach (var registerId in registers)
            {
	            var registerData = RegisterCache.GetRegisterData(registerId);

	            if (registerData.AllpriPartitioning == Platform.Register.AllpriPartitioningType.DataType)
	            {
		            var postfixes = new List<string> {"TXT", "NUM", "DT"};
		            foreach (var postfix in postfixes)
		            {
			            var tableName = $"{registerData.AllpriTable}_{postfix}";
			            var tableAlias = $"source_{++counter}";
			            var tableAliasForFirstCondition = $"{tableAlias}_2";
			            var tableAliasForSecondCondition = $"{tableAlias}_3";

						var currentColumn = $@" 
						COALESCE((select string_agg(cast ({tableAlias}.attribute_id as text), ',') from {tableName} {tableAlias} where unit.object_id = {tableAlias}.object_id and
                        ({tableAlias}.ID = (SELECT MAX({tableAliasForFirstCondition}.id) FROM {tableName} {tableAliasForFirstCondition} 
                        WHERE {tableAliasForFirstCondition}.object_id = {tableAlias}.object_id 
                        AND {tableAliasForFirstCondition}.attribute_id = {tableAlias}.attribute_id 
                        AND {tableAliasForFirstCondition}.ot = (SELECT MAX({tableAliasForSecondCondition}.ot) FROM {tableName} {tableAliasForSecondCondition} 
                        WHERE {tableAliasForSecondCondition}.object_id = {tableAlias}.object_id 
                        AND {tableAliasForSecondCondition}.attribute_id = {tableAlias}.attribute_id 
                        AND {tableAliasForSecondCondition}.Ot <= {CrossDBSQL.ToDate(dateOt)})))
						), '') ";

			            columns = string.IsNullOrWhiteSpace(columns)
				            ? $@"{currentColumn}"
				            : $@"{columns}  || ',' || {currentColumn}";
		            }
	            }
	            else if (registerData.AllpriPartitioning == Platform.Register.AllpriPartitioningType.AttributeId)
	            {
		            var attributes = RegisterCache.RegisterAttributes.Values.ToList()
			            .Where(x => x.RegisterId == registerId)
			            .ToList();

		            foreach (var attribute in attributes)
		            {
			            if (attribute.IsPrimaryKey)
				            continue;

			            var tableName = $"{registerData.AllpriTable}_{attribute.Id}";
			            var tableAlias = $"gbu_source_{++counter}";
			            var tableAliasForCondition = $"{tableAlias}_2";

			            var currentColumn = $@"
							(select COALESCE((STRING_AGG(distinct (case when {tableAlias}.id is null then '' else '{attribute.Id}' end), ',')), '')
							from {tableName} {tableAlias} 
							where unit.object_id = {tableAlias}.object_id AND
							({tableAlias}.OT = (SELECT MAX({tableAliasForCondition}.OT) 
							FROM {tableName} {tableAliasForCondition} 
							WHERE {tableAliasForCondition}.object_id = {tableAlias}.object_id AND {tableAliasForCondition}.Ot <= {CrossDBSQL.ToDate(dateOt)})
							or {tableAlias}.OT is null))";

			            columns = string.IsNullOrWhiteSpace(columns)
				            ? $@"{currentColumn}"
				            : $@"{columns} || ',' || ({currentColumn})";
		            }
	            }
            }

            var sql = $@"select unit.cadastral_number as CadastralNumber, 
			    ({columns}) as attributes
				from ko_unit unit
			    where unit.task_id in({string.Join(',', taskIds)})--and unit.object_id in (10743778)--(549616)
			    group by unit.cadastral_number, unit.object_id
				order by unit.cadastral_number";

            var results = QSQuery.ExecuteSql<DbResult>(sql);

            return results.Select(result => new ReportItem
            {
                CadastralNumber = result.CadastralNumber,
                Attributes = GetUniqueAttributes2(result.Attributes)
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

        private List<Attribute> GetUniqueAttributes2(string attributesStr)
        {
	        var objectAttributes = attributesStr.Split(',').Where(attribute => !string.IsNullOrWhiteSpace(attribute)).Select(attributeIdStr =>
	        {
		        var attribute = RegisterCache.RegisterAttributes.Values.ToList()
			        .FirstOrDefault(x => x.Id == attributeIdStr.ParseToLong());
		        var register = RegisterCache.Registers.Values.FirstOrDefault(x => x.Id == attribute?.RegisterId);
		        return new Attribute
		        {
			        AttributeName = attribute?.Name,
                    RegisterId = register?.Id,
			        RegisterName = register?.Description
		        };
	        }).ToList();


            if (objectAttributes.Count == 0)
		        return new List<Attribute>();

	        var gbuAttributesExceptRosreestr = objectAttributes
		        .Where(x => x.RegisterId != RosreestrRegisterService.RosreestrRegisterId).ToList();

	        var rosreestrAttributes = objectAttributes
		        .Where(x => x.RegisterId == RosreestrRegisterService.RosreestrRegisterId).ToList();

	        //симметрическая разность множеств
	        var uniqueAttributes = new List<Attribute>();
	        //отбираем уникальные аттрибуты из РР
	        rosreestrAttributes.ForEach(rr =>
	        {
		        var sameAttributes = gbuAttributesExceptRosreestr.Where(gbu => !string.IsNullOrWhiteSpace(gbu.AttributeName) &&
                    gbu.AttributeName.StartsWith(rr.AttributeName, StringComparison.InvariantCultureIgnoreCase)).ToList();

		        if (sameAttributes.Count == 0)
			        uniqueAttributes.Add(rr);
	        });
	        //отбираем уникальные аттрибуты из всех источников кроме РР
	        gbuAttributesExceptRosreestr.ForEach(gbu =>
	        {
		        var sameAttributes = rosreestrAttributes.Where(rr => !string.IsNullOrWhiteSpace(rr.AttributeName) &&
                    gbu.AttributeName.StartsWith(rr.AttributeName, StringComparison.InvariantCultureIgnoreCase)).ToList();

		        if (sameAttributes.Count == 0)
			        uniqueAttributes.Add(gbu);
	        });

	        return uniqueAttributes;
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
            public long? RegisterId { get; set; }
        }

        public class ReportItem
        {
            public string CadastralNumber { get; set; }
            public List<Attribute> Attributes { get; set; }
        }

        #endregion
    }
}
