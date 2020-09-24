using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Text;
using Core.Register;
using Core.Register.QuerySubsystem;
using Core.Register.RegisterEntities;
using Core.Shared.Extensions;
using Core.Shared.Misc;
using KadOzenka.Dal.FastReports.StatisticalData.Common;
using KadOzenka.Dal.GbuObject;
using Microsoft.Practices.ObjectBuilder2;
using ObjectModel.Directory;
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
            //var test = new List<long> { 14 };
            var sources = RegisterCache.Registers.Values.Where(x => x.QuantTable == mainRegister.QuantTable &&
                                                                x.Id != mainRegister.Id)
                //.Where(x => test.Contains(x.Id))
                .Select(x => x.Id).ToList();

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

			            var currentColumn = $@" 
						(select string_agg(distinct cast ({tableAlias}.attribute_id as text), ',') 
						from {tableName} {tableAlias} where unit.object_id = {tableAlias}.object_id)";

			            columns = string.IsNullOrWhiteSpace(columns)
				            ? $@"{currentColumn}"
				            : $@"{columns}, {currentColumn}";
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

			            var currentColumn = $@"
							(select (case when {tableAlias}.id is null then '' else '44' end)
							from {tableName} {tableAlias} 
							where unit.object_id = {tableAlias}.object_id limit 1)";

			            columns = string.IsNullOrWhiteSpace(columns)
				            ? $@"{currentColumn}"
				            : $@"{columns}, ({currentColumn})";
		            }
	            }
            }

            var sql = $@"with data as(
				select unit.cadastral_number as CadastralNumber,
				ARRAY[
			    {columns}
				] as attributes
				from ko_unit unit
			    where unit.task_id in({string.Join(',', taskIds)}) 
				--and PROPERTY_TYPE_CODE = 4 
				--and unit.object_id in (10743778)--(549616)
			    group by unit.cadastral_number, unit.object_id
				order by unit.cadastral_number)

				select cadastralNumber, array_remove(attributes, NULL) as attributes from data";

            var results = QSQuery.ExecuteSql<DbResult>(sql);

            var cachedAttributes = RegisterCache.RegisterAttributes.Values.ToList();
            var cachedRegisters = RegisterCache.Registers.Values.ToList();

            return results.Select(result => new ReportItem
            {
                CadastralNumber = result.CadastralNumber,
                Attributes = GetUniqueAttributes2(result.Attributes, cachedAttributes, cachedRegisters)
            }).ToList();
        }

        private List<Attribute> GetUniqueAttributes2(string[] attributesStr,
	        List<RegisterAttribute> cachedAttributes, List<RegisterData> cachedRegisters)
        {
	        var objectAttributes = new List<Attribute>();
	        attributesStr.Where(attribute => !string.IsNullOrWhiteSpace(attribute)).ForEach(attributeIdStr =>
	        {
		        foreach (var processedAttributeId in attributeIdStr.Split(','))
		        {
			        var attribute = cachedAttributes.FirstOrDefault(x => x.Id == processedAttributeId.ParseToLong());

			        var register = cachedRegisters.FirstOrDefault(x => x.Id == attribute?.RegisterId);

			        if (attribute == null || register == null)
				        continue;

			        objectAttributes.Add(new Attribute
			        {
				        AttributeName = attribute.Name,
				        RegisterId = register.Id,
				        RegisterName = register.Description
			        });
		        }
	        });

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
		        var sameAttributes = gbuAttributesExceptRosreestr.Where(gbu =>
				        gbu.AttributeName.StartsWith(rr.AttributeName, StringComparison.InvariantCultureIgnoreCase))
			        .ToList();

		        if (sameAttributes.Count == 0)
			        uniqueAttributes.Add(rr);
	        });
	        //отбираем уникальные аттрибуты из всех источников кроме РР
	        gbuAttributesExceptRosreestr.ForEach(gbu =>
	        {
		        var sameAttributes = rosreestrAttributes.Where(rr =>
				        gbu.AttributeName.StartsWith(rr.AttributeName, StringComparison.InvariantCultureIgnoreCase))
			        .ToList();

		        if (sameAttributes.Count == 0)
			        uniqueAttributes.Add(gbu);
	        });

	        return uniqueAttributes;
        }


        public class DbResult
        {
            public string CadastralNumber { get; set; }
            public string[] Attributes { get; set; }
        }

        protected List<OMUnit> GetUnits(List<long> taskIds)
        {
            return OMUnit.Where(x => taskIds.Contains((long)x.TaskId) && x.ObjectId != null 
                                                                      //&& x.PropertyType_Code == PropertyTypes.Stead
                                                                      )
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
