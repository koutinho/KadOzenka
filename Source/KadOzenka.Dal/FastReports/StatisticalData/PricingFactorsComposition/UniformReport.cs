using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using Core.Shared.Extensions;
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

        private List<ReportItem> GetOperations(List<long> taskIds)
        {
            var items = new List<ReportItem>();
            var units = GetUnits(taskIds);

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

        private class Attribute
        {
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
