using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections.Specialized;
using System.Data;
using Core.Shared.Extensions;
using KadOzenka.Dal.FastReports.StatisticalData.Common;
using KadOzenka.Dal.GbuObject;
using ObjectModel.KO;

namespace KadOzenka.Dal.FastReports.StatisticalData.PricingFactorsComposition
{
    public class NonuniformReport : StatisticalDataReport
    {
        protected override string TemplateName(NameValueCollection query)
        {
            return "PricingFactorsCompositionNonuniformReport";
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

        private List<ReportItem> GetOperations(List<long> taskIds)
        {
            var items = new List<ReportItem>();
            var units = GetUnits(taskIds);
            ////для тестирования
            //units = new List<OMUnit>
            //{
            //    new OMUnit{CadastralNumber = "test 1", Id = 14974931, ObjectId = 11251387 },
            //    new OMUnit{CadastralNumber = "test 2", Id = 13900719, ObjectId = 10433956 }
            //};

            var objectsAttributes = GbuObjectService.GetAllAttributes(
                units.Where(x => x.ObjectId != null).Select(x => x.ObjectId.Value).ToList(),
                dateOt: DateTime.Now.GetEndOfTheDay());

            units.ForEach(unit =>
            {
                var objAttributes = objectsAttributes.Where(x => x.ObjectId == unit.ObjectId).ToList();

                items.Add(new ReportItem
                {
                    CadastralNumber = unit.CadastralNumber,
                    Attributes = GetNotUniqueAttributes(objAttributes)
                });
            });

            return items;
        }

        private List<Attribute> GetNotUniqueAttributes(List<GbuObjectAttribute> objectAttributes)
        {
            if (objectAttributes == null || objectAttributes.Count == 0)
                return new List<Attribute>();

            var gbuAttributesExceptRosreestr = objectAttributes
                .Where(x => x.RegisterData.Id != StatisticalDataService.RosreestrRegisterId).ToList();

            var rosreestrAttributes = objectAttributes
                .Where(x => x.RegisterData.Id == StatisticalDataService.RosreestrRegisterId).ToList();

            var notUniqueAttribute = new List<Attribute>();
            rosreestrAttributes.ForEach(rr =>
            {
                var rrAttributeName = rr.GetAttributeName();

                var sameAttributes = gbuAttributesExceptRosreestr.Where(gbu =>
                        gbu.GetAttributeName().StartsWith(rrAttributeName, StringComparison.InvariantCultureIgnoreCase)).ToList();
                if (sameAttributes.Count == 0)
                    return;

                var registerNames = new List<string> {rr.RegisterData.Description};
                registerNames.AddRange(sameAttributes.Select(x => x.RegisterData.Description));
                var attribute = new Attribute
                {
                    AttributeName = rrAttributeName,
                    RegisterNames = registerNames
                };

                notUniqueAttribute.Add(attribute);
            });

            return notUniqueAttribute;
        }

        private DataTable GetItemDataTable(List<ReportItem> operations)
        {
            var dataTable = new DataTable("Data");

            dataTable.Columns.Add("Number");
            dataTable.Columns.Add("CadastralNumber");
            dataTable.Columns.Add("CharacteristicNameTitle");
            dataTable.Columns.Add("CharacteristicName");

            //для формирования матрицы нужен дубляж значения всех строк кроме характеристик
            for (var i = 0; i < operations.Count; i++)
            {
                for (var j = 0; j < operations[i].Attributes.Count; j++)
                {
                    for (var counter = 0; counter < 2; counter++)
                    {
                        string title, value;
                        if (counter % 2 == 0)
                        {
                            title = $"Характеристика объекта {j + 1}";
                            value = operations[i].Attributes.ElementAtOrDefault(j)?.AttributeName;

                            dataTable.Rows.Add(i + 1,
                                operations[i].CadastralNumber,
                                title,
                                value);
                        }
                        else
                        {
                            var registerNames = operations[i].Attributes.ElementAtOrDefault(j)?.RegisterNames;
                            for (var registerCounter = 0; registerCounter < registerNames?.Count; registerCounter++)
                            {
                                title = $"Источник информации {registerCounter + 1}";
                                value = registerNames[registerCounter];

                                dataTable.Rows.Add(i + 1,
                                    operations[i].CadastralNumber,
                                    title,
                                    value);
                            }
                        }
                    }
                }
            }

            return dataTable;
        }

        #endregion


        #region Entities

        private class Attribute
        {
            public string AttributeName { get; set; }
            public List<string> RegisterNames { get; set; }
        }

        private class ReportItem
        {
            public string CadastralNumber { get; set; }
            public List<Attribute> Attributes { get; set; }
        }

        #endregion
    }
}
