using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections.Specialized;
using System.Data;
using Core.Shared.Extensions;
using KadOzenka.Dal.ManagementDecisionSupport.StatisticalData.PricingFactorsComposition;
using Microsoft.Practices.ObjectBuilder2;
using Serilog;

namespace KadOzenka.Dal.FastReports.StatisticalData.PricingFactorsComposition
{
    public class NonuniformReport : DataCompositionByCharacteristicsBaseReport
    {
	    private readonly ILogger _logger;
	    protected override ILogger Logger => _logger;

	    public NonuniformReport()
	    {
		    _logger = Log.ForContext<NonuniformReport>();
	    }


		protected override string TemplateName(NameValueCollection query)
        {
            return "PricingFactorsCompositionNonuniformReport";
        }

        protected override DataSet GetDataCompositionByCharacteristicsReportData(NameValueCollection query, HashSet<long> objectList = null)
        {
	        var taskIds = GetTaskIdList(query).ToList();

            var operations = GetOperations<ReportItem>(taskIds);
            Logger.Debug("Найдено {Count} объектов", operations?.Count);

			Logger.Debug("Начато формирование таблиц");
			var dataSet = new DataSet();
            var itemTable = GetItemDataTable(operations);
            dataSet.Tables.Add(itemTable);
            Logger.Debug("Закончено формирование таблиц");

			return dataSet;
        }


        #region Support Methods

        private DataTable GetItemDataTable(List<ReportItem> operations)
        {
	        var titleForCharacteristic = "Характеристика объекта";
	        var titleForSource = "Источник информации";

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

					            dataTable.Rows.Add(index,
						            operations[i].CadastralNumber,
						            title,
						            value);
				            }
				            else
				            {
					            var registerNames = operations[i].FullAttributes.ElementAtOrDefault(j)?.RegisterNames;
					            for (var registerCounter = 0; registerCounter < registerNames?.Count; registerCounter++)
					            {
						            title = $"{titleForSource} {registerCounter + 1}";
						            value = registerNames[registerCounter];

						            dataTable.Rows.Add(index,
							            operations[i].CadastralNumber,
							            title,
							            value);
					            }
				            }
			            }
		            }
                }
            }

            return dataTable;
        }

        #endregion


        #region Entities

        private class SameAttributes
        {
	        public string Name { get; set; }
	        public List<string> RegisterNames { get; set; }
        }

        private class ReportItem
        {
	        private List<SameAttributes> _fullAttributes;

	        public string CadastralNumber { get; set; }
	        public string[] Attributes { get; set; }
	        public List<SameAttributes> FullAttributes => _fullAttributes ?? (_fullAttributes = GetNotUniqueAttributes());

	        private List<SameAttributes> GetNotUniqueAttributes()
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
			        return new List<SameAttributes>();

		        var gbuAttributesExceptRosreestr = objectAttributes
			        .Where(x => x.RegisterId != DataCompositionByCharacteristicsService.RosreestrRegisterId).ToList();
		        var rosreestrAttributes = objectAttributes
			        .Where(x => x.RegisterId == DataCompositionByCharacteristicsService.RosreestrRegisterId).ToList();

		        var notUniqueAttribute = new List<SameAttributes>();
		        rosreestrAttributes.ForEach(rr =>
		        {
			        var sameAttributes = gbuAttributesExceptRosreestr.Where(gbu =>
				        gbu.Name.StartsWith(rr.Name, StringComparison.InvariantCultureIgnoreCase)).ToList();
			        if (sameAttributes.Count == 0)
				        return;

			        var registerNames = new List<string> {rr.RegisterName};
			        registerNames.AddRange(sameAttributes.Select(x => x.RegisterName));
			        var attribute = new SameAttributes
                    {
				        Name = rr.Name,
				        RegisterNames = registerNames
			        };

			        notUniqueAttribute.Add(attribute);
		        });

		        return notUniqueAttribute;
	        }
        }

        #endregion
    }
}
