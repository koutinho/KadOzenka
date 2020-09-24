using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using Core.Register;
using Core.Register.QuerySubsystem;
using Core.Register.RegisterEntities;
using Core.Shared.Extensions;
using KadOzenka.Dal.FastReports.StatisticalData.Common;
using KadOzenka.Dal.GbuObject;
using Microsoft.Practices.ObjectBuilder2;
using ObjectModel.KO;

namespace KadOzenka.Dal.FastReports.StatisticalData.PricingFactorsComposition
{
    public class UniformReport : StatisticalDataReport
    {
	    private static List<RegisterData> _cachedRegisters;
	    private static List<RegisterAttribute> _cachedAttributes;
	    private static long _rosreestrRegisterId;

	    public UniformReport()
	    {
		    var mainRegister = RegisterCache.GetRegisterData(ObjectModel.Gbu.OMMainObject.GetRegisterId());

			//для тестирования
			//var test = new List<long> {2, 4, 5, 14, 42430534 };
			//--and unit.object_id in (10743778)--(549616)
			_cachedRegisters = RegisterCache.Registers.Values.Where(x => x.QuantTable == mainRegister.QuantTable &&
		                                                                 x.Id != mainRegister.Id)
			    //.Where(x => test.Contains(x.Id))
			    .ToList();

		    var registerIds = _cachedRegisters.Select(x => x.Id).ToList();
		    _cachedAttributes = RegisterCache.RegisterAttributes.Values.Where(x => registerIds.Contains(x.RegisterId)).ToList();

		    _rosreestrRegisterId = RosreestrRegisterService.RosreestrRegisterId;
	    }

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

		//public List<ReportItem> GetOperations(List<long> taskIds)
		//{
		//	var items = new List<ReportItem>();
		//	var units = GetUnits(taskIds).ToList();

		//	var objectsAttributes = GbuObjectService.GetAllAttributes(
		//		units.Select(x => x.ObjectId.GetValueOrDefault()).ToList(),
		//		dateOt: DateTime.Now.GetEndOfTheDay(),
		//		isLight: true);

		//	units.ForEach(unit =>
		//	{
		//		var objAttributes = objectsAttributes.Where(x => x.ObjectId == unit.ObjectId).ToList();

		//		items.Add(new ReportItem
		//		{
		//			CadastralNumber = unit.CadastralNumber,
		//			FullAttributes = GetUniqueAttributes(objAttributes).Select(x => new Attribute
		//			{
		//				Name = x.GetAttributeName(),
		//				RegisterName = x.RegisterData.Description
		//			}).ToList()
		//		});
		//	});

		//	return items;
		//}
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
            if (objectAttributes == null || objectAttributes.Count == 0)
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

		public List<ReportItem> GetOperations(List<long> taskIds)
		{
			var counter = 0;
			var columns = string.Empty;
			var joins = string.Empty;
			var conditions = string.Empty;
			foreach (var register in _cachedRegisters)
			{
				if (register.AllpriPartitioning == Platform.Register.AllpriPartitioningType.DataType)
				{
					var postfixes = new List<string> { "TXT", "NUM", "DT" };
					foreach (var postfix in postfixes)
					{
						counter++;
						var tableName = $"{register.AllpriTable}_{postfix}";
						var tableAlias = $"source_{counter}";

						columns = $@"{(string.IsNullOrWhiteSpace(columns) ? "" : $" {columns}, ")} 
                                    STRING_AGG(distinct cast({tableAlias}.attribute_id as text), ',')";

						joins = $@" {joins} 
                        left join {tableName} {tableAlias} on unit.object_id = {tableAlias}.object_id";
					}
				}
				else if (register.AllpriPartitioning == Platform.Register.AllpriPartitioningType.AttributeId)
				{
					var attributesData = RegisterCache.RegisterAttributes.Values.ToList()
						.Where(x => x.RegisterId == register.Id).ToList();

					foreach (var attributeData in attributesData)
					{
						if (attributeData.IsPrimaryKey)
						{
							continue;
						}

						counter++;

						var tableName = $"{register.AllpriTable}_{attributeData.Id}";
						var tableAlias = $"gbu_source_{counter}";

						columns = $@"{(string.IsNullOrWhiteSpace(columns) ? "" : $" {columns}, ")} 
                                    STRING_AGG(distinct (case when {tableAlias}.id is null then '' else '{attributeData.Id}' end), ',') ";

						joins = $@" {joins} 
                        left join {tableName} {tableAlias} on unit.object_id = {tableAlias}.object_id";
					}
				}
			}

			var resultColumns = string.IsNullOrWhiteSpace(columns) ? "" : $"{columns.TrimEnd(',')}";

			var sql = $@"with data as(select unit.cadastral_number as cadastralnumber, 
                ARRAY[{resultColumns}] as attributes
                from ko_unit unit 
                {joins} 
                --AND ( {conditions} ) 
                where unit.task_id in({string.Join(',', taskIds)})--and unit.object_id in (10743778)--(549616)
                group by unit.cadastral_number
				order by unit.cadastral_number)

				select cadastralNumber, array_remove(attributes, NULL) as attributes from data";

			return QSQuery.ExecuteSql<ReportItem>(sql);
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
	            if (operations[i].FullAttributes.Count == 0)
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
                    for (var j = 0; j < operations[i].FullAttributes.Count; j++)
                    {
                        for (var counter = 0; counter < 2; counter++)
                        {
                            string title, value;
                            if (counter % 2 == 0)
                            {
                                title = $"{titleForCharacteristic} {j + 1}";
                                value = operations[i].FullAttributes.ElementAtOrDefault(j)?.Name;
                            }
                            else
                            {
                                title = $"{titleForSource} {j + 1}";
                                value = operations[i].FullAttributes.ElementAtOrDefault(j)?.RegisterName;
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
            public string Name { get; set; }
            public string RegisterName { get; set; }
            public long? RegisterId { get; set; }
        }

		public class ReportItem
		{
			private List<Attribute> _fullAttributes;

			public string CadastralNumber { get; set; }
			public string[] Attributes { get; set; }
			public List<Attribute> FullAttributes => _fullAttributes ?? (_fullAttributes = ProcessAttributes());

			private List<Attribute> ProcessAttributes()
			{
				var objectAttributes = new List<Attribute>();
				Attributes.Where(attribute => !string.IsNullOrWhiteSpace(attribute)).ForEach(attributeIdStr =>
				{
					foreach (var processedAttributeId in attributeIdStr.Split(','))
					{
						var attribute = _cachedAttributes.FirstOrDefault(x => x.Id == processedAttributeId.ParseToLong());
						var register = _cachedRegisters.FirstOrDefault(x => x.Id == attribute?.RegisterId);
						if (attribute == null || register == null)
							continue;

						objectAttributes.Add(new Attribute
						{
							Name = attribute.Name,
							RegisterId = register.Id,
							RegisterName = register.Description
						});
					}
				});

				if (objectAttributes.Count == 0)
					return new List<Attribute>();

				var gbuAttributesExceptRosreestr = objectAttributes.Where(x => x.RegisterId != _rosreestrRegisterId).ToList();
				var rosreestrAttributes = objectAttributes.Where(x => x.RegisterId == _rosreestrRegisterId).ToList();

				//симметрическая разность множеств
				var uniqueAttributes = new List<Attribute>();
				//отбираем уникальные аттрибуты из РР
				rosreestrAttributes.ForEach(rr =>
				{
					var isSameAttributesExist = gbuAttributesExceptRosreestr.Any(gbu =>
						gbu.Name.StartsWith(rr.Name, StringComparison.InvariantCultureIgnoreCase));

					if (!isSameAttributesExist)
						uniqueAttributes.Add(rr);
				});
				//отбираем уникальные аттрибуты из всех источников кроме РР
				gbuAttributesExceptRosreestr.ForEach(gbu =>
				{
					var isSameAttributesExist = rosreestrAttributes.Any(rr =>
						gbu.Name.StartsWith(rr.Name, StringComparison.InvariantCultureIgnoreCase));

					if (!isSameAttributesExist)
						uniqueAttributes.Add(gbu);
				});

				return uniqueAttributes;
			}
		}

		//public class ReportItem
		//{
		//	public string CadastralNumber { get; set; }
		//	public List<Attribute> FullAttributes { get; set; }
		//}

		#endregion
	}
}
