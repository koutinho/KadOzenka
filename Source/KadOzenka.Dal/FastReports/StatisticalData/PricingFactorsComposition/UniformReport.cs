using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using Core.Shared.Extensions;

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

            return HadleData(dataSet);
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

            var uniqueAttributes = GetUniqueAttributeIds();

            var objectsAttributes = GbuObjectService.GetAllAttributes(
                units.Where(x => x.ObjectId != null).Select(x => x.ObjectId.Value).ToList(),
                uniqueAttributes.Select(x => x.RegisterId).Distinct().ToList(),
                uniqueAttributes.Select(x => x.AttributeId).Distinct().ToList(),
                dateOt: DateTime.Now.GetEndOfTheDay());

            units.ForEach(unit =>
            {
                var item = new ReportItem
                {
                    CadastralNumber = unit.CadastralNumber
                };

                var objAttributes = objectsAttributes.Where(x => x.ObjectId == unit.ObjectId).ToList();
                if (objAttributes.Count <= 0)
                    return;

                item.Attributes = objAttributes.Select(x => new Attribute
                {
                    RegisterName = x.RegisterData?.Description,
                    AttributeName = x.GetAttributeName()
                }).ToList();

                items.Add(item);
            });

            return items;
        }

        private List<Attribute> GetUniqueAttributeIds()
        {
            var allGbuAttributes = GbuObjectService.GetGbuAttributes()
                .Select(x => new Attribute
                {
                    AttributeId = x.Id,
                    RegisterId = x.RegisterId,
                    AttributeName = x.Name
                }).ToList();

            var gbuAttributesExceptRosreestr = allGbuAttributes
                .Where(x => x.RegisterId != StatisticalDataService.RosreestrRegisterId).ToList();

            var rosreestrAttributes = allGbuAttributes
                .Where(x => x.RegisterId == StatisticalDataService.RosreestrRegisterId).ToList();

            ////для тестирования
            //gbuAttributesExceptRosreestr = new List<Test>
            //{
            //    new Test{Id = 1, Name = "АЛ_1", RegisterId = 1},
            //    new Test{Id = 2, Name = "материал стен РСМ", RegisterId = 2},
            //    new Test{Id = 3, Name = "АЛ_2", RegisterId = 3},
            //};

            //rosreestrAttributes = new List<Test>
            //{
            //    new Test{Id = 1, Name = "РР1", RegisterId = 1},
            //    new Test{Id = 2, Name = "Материал стен", RegisterId = 2},
            //    new Test{Id = 3, Name = "РР2", RegisterId = 3},
            //};

            //симметрическая разность множеств
            var uniqueAttributes = new List<Attribute>();
            //отбираем уникальные аттрибуты из РР
            rosreestrAttributes.ForEach(rr =>
            {
                var sameAttributes = gbuAttributesExceptRosreestr.Where(gbu =>
                    gbu.AttributeName.StartsWith(rr.AttributeName, StringComparison.InvariantCultureIgnoreCase)).ToList();
                if (sameAttributes.Count == 0)
                    uniqueAttributes.Add(rr);
            });
            //отбираем уникальные аттрибуты из всех источников кроме РР
            gbuAttributesExceptRosreestr.ForEach(gbu =>
            {
                var sameAttributes = rosreestrAttributes
                    .Where(rr => gbu.AttributeName.StartsWith(rr.AttributeName, StringComparison.InvariantCultureIgnoreCase)).ToList();
                if (sameAttributes.Count == 0)
                    uniqueAttributes.Add(gbu);
            });

            return uniqueAttributes;
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
                        }
                        else
                        {
                            title = $"Итоговый источник информации {j + 1}";
                            value = operations[i].Attributes.ElementAtOrDefault(j)?.RegisterName;
                        }

                        dataTable.Rows.Add(i + 1,
                            operations[i].CadastralNumber, 
                            title,
                            value);
                    }
                   
                }
            }

            return dataTable;
        }

        #endregion


        #region Entities

        private class Attribute
        {
            public long AttributeId { get; set; }
            public long RegisterId { get; set; }
            public string AttributeName { get; set; }
            public string RegisterName { get; set; }
        }

        private class ReportItem
        {
            public string CadastralNumber { get; set; }
            public List<Attribute> Attributes { get; set; }
        }

        #endregion
    }
}
