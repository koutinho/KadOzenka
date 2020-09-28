using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using Core.Register.QuerySubsystem;
using Core.Shared.Extensions;
using KadOzenka.Dal.FastReports.StatisticalData.Common;
using KadOzenka.Dal.ManagementDecisionSupport.StatisticalData.PricingFactorsComposition;
using Microsoft.Practices.ObjectBuilder2;

namespace KadOzenka.Dal.FastReports.StatisticalData.PricingFactorsComposition
{
    public class UniformReport : StatisticalDataReport
    {
	    public static DataCompositionByCharacteristicsService DataCompositionByCharacteristicsService { get; set; }

	    public UniformReport()
	    {
			DataCompositionByCharacteristicsService = new DataCompositionByCharacteristicsService();
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

		public List<ReportItem> GetOperations(List<long> taskIds)
		{
			var longPerformanceTaskIds = DataCompositionByCharacteristicsService.GetCachedTaskIds();
			var tasIdsForRuntime = taskIds.Except(longPerformanceTaskIds).ToList();
			var tasIdsForCache = longPerformanceTaskIds.Intersect(taskIds).ToList();

			var runtimeResults = new List<ReportItem>();
			if (tasIdsForRuntime.Count != 0)
			{
				var runtimeSql = DataCompositionByCharacteristicsService.GetSqlForRuntime(tasIdsForRuntime);
				runtimeResults = QSQuery.ExecuteSql<ReportItem>(runtimeSql);
			}

			if (tasIdsForCache.Count != 0)
			{
				var cacheSql = DataCompositionByCharacteristicsService.GetSqlForCache(tasIdsForCache);
				var cacheResults = QSQuery.ExecuteSql<ReportItem>(cacheSql);
				runtimeResults.AddRange(cacheResults);
			}

			return runtimeResults;
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
	            var index = i + 1;

				if (operations[i].FullAttributes.Count == 0)
                {
                    dataTable.Rows.Add(index,
                        operations[i].CadastralNumber,
                        $"{titleForCharacteristic} 1",
                        string.Empty);

                    dataTable.Rows.Add(index,
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

                            dataTable.Rows.Add(index,
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
			public List<Attribute> FullAttributes => _fullAttributes ?? (_fullAttributes = GetUniqueAttributes());


			private List<Attribute> GetUniqueAttributes()
			{
				var objectAttributes = new List<Attribute>();
				Attributes.Where(attribute => !string.IsNullOrWhiteSpace(attribute)).ForEach(attributeIdStr =>
				{
					foreach (var processedAttributeId in attributeIdStr.Split(','))
					{
						var attribute = DataCompositionByCharacteristicsService.CachedAttributes.FirstOrDefault(x => x.Id == processedAttributeId.ParseToLong());
						var register = DataCompositionByCharacteristicsService.CachedRegisters.FirstOrDefault(x => x.Id == attribute?.RegisterId);
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

				var gbuAttributesExceptRosreestr = objectAttributes
					.Where(x => x.RegisterId != DataCompositionByCharacteristicsService.RosreestrRegisterId).ToList();
				var rosreestrAttributes = objectAttributes
					.Where(x => x.RegisterId == DataCompositionByCharacteristicsService.RosreestrRegisterId).ToList();

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

		#endregion
	}
}
